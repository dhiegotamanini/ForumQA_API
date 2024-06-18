namespace ForumQA.Domain.Abstrations
{
    public interface ISqlDatabaseCommands
    {
        List<T> GetDateWithJoin<T, U, V>(string query, Func<T, U, V, T> map, object parameters, string splitOn);
        List<T> GetList<T>(string sql);
        List<T> GetList<T>(string sql, int id, bool isAnswerPost = false);
        T GetItem<T>(string sql, int id);
        void ExecuteCommand<T>(string sql, T parameter);        
    }
}
