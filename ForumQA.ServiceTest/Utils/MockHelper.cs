using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumQA.ServiceTest.Utils
{
    public static class MockHelper
    {
        public static T CreateTestObject<T>(List<object> providedMocks = null) where T : class
        {
            // Get the constructor with the most parameters
            var ctor = typeof(T).GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();

            if (ctor == null)
            {
                throw new InvalidOperationException($"No suitable constructor found for type {typeof(T).Name}");
            }

            // Initialize the provided mocks dictionary
            var providedMocksDict = providedMocks?
                .ToDictionary(m => m.GetType().GetInterfaces().FirstOrDefault() ?? m.GetType())
                ?? new Dictionary<Type, object>();

            // Create an array of parameters for the constructor
            var parameters = ctor.GetParameters()
                .Select(p =>
                {
                    if (providedMocksDict.TryGetValue(p.ParameterType, out var providedMock))
                    {
                        return providedMock;
                    }
                    return CreateMock(p.ParameterType);
                })
                .ToArray();

            // Invoke the constructor with the parameters
            return (T)ctor.Invoke(parameters);
        }

        private static object CreateMock(Type type)
        {
            // Use Moq to create a mock of the given type
            var mockType = typeof(Mock<>).MakeGenericType(type);
            var mock = Activator.CreateInstance(mockType);
            return mockType.GetProperty("Object").GetValue(mock);
        }

        public static List<T> CreateTestObjects<T>(int count, List<object> providedMocks = null) where T : class
        {
            var list = new List<T>();
            for (int i = 0; i < count; i++)
            {
                list.Add(CreateTestObject<T>(providedMocks));
            }
            return list;
        }
    }
}
