using Dapper;
using ForumQA.Domain.Abstrations;
using ForumQA.Domain.Models;
using ForumQA.Domain.Utils;
using System.Data;
using System.Data.SqlClient;

namespace ForumQA.Infrastructure
{
    public class SqlDatabaseCommands : ISqlDatabaseCommands
    {
        private readonly string _connectionString;
        public SqlDatabaseCommands(AppSettings appSettings)
        {
            _connectionString = appSettings.ConnectionString;
        }
        
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public List<T> GetList<T>(string sql)
        {
            using(IDbConnection db = GetConnection())
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                return db.Query<T>(sql, commandType: CommandType.Text).ToList();
            }
        }

        public T GetItem<T>(string sql, int id)
        {
            using(IDbConnection db = GetConnection())
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }
                return db.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }

        public void ExecuteCommand<T>(string sql, T parameter)
        {
            using(IDbConnection db = GetConnection())
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                db.Execute(sql, parameter);
            }
        }

        public List<T> GetList<T>(string sql, int id, bool isAnswerPost)
        {
            using(IDbConnection db = GetConnection())
            {
                if(db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                if (!isAnswerPost)
                {
                    return db.Query<T>(sql, new { ForumType = id }).ToList();
                }

                var test = GetLisWithJoint<AnswerPost>(1);
                return db.Query<T>(sql, new { postId = id }).ToList();

            }
        }

        public List<T> GetLisWithJoint<T>(int postId)
        {
            string sql = @"
                             SELECT 
                                        A.Id, 
                                        A.Answer, 
                                        A.AnswerDate, 
                                        A.UserId, 
                                        A.PostId,
                                        P.TITLE AS PostTitle, 
                                        P.Description AS PostDescription,
                                        U.Id AS User_Id, 
                                        U.Name AS UserName, 
                                        U.Photo AS UserPhoto, 
                                        U.MemberSince AS UserMemberSince
                             FROM       AnswerPost   A
                             INNER JOIN POST         P ON A.PostId = P.Id
                             INNER JOIN [USER]       U ON U.Id     = A.UserId
                             WHERE                   A.PostId      =  " + postId;
            
            using (IDbConnection db = GetConnection())
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }


                var resultado = db.Query<AnswerPost, dynamic, AnswerPost>(
                sql,
                (answerPost, dynamicUser) =>
                {
                    answerPost.User = new User
                    {
                        UserName = dynamicUser.UserName,
                        UserPhoto = dynamicUser.UserPhoto,
                        MemberSince = dynamicUser.UserMemberSince 
                    };
                    return answerPost;
                },
                param: new { PostId = postId },
                splitOn: "PostTitle, UserName" 
                ).ToList();

                return db.Query<T>(sql, new { postId }).ToList();
            }
        }

        public List<T> GetDateWithJoin<T, U, V>(string query, Func<T, U, V, T> map, object parameters, string splitOn)
        {
            using (IDbConnection db = GetConnection())
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                var resultado = db.Query<T, U, V, T>(query,map,parameters,splitOn: splitOn).ToList();
                return resultado;
            }               
        }


        public AnswerPost ObterAnswerPostEUsuario(int postId)
        {
            string query = @"
            SELECT A.Id, A.Answer, A.AnswerDate, A.UserId, A.PostId,
                   P.TITLE AS PostTitle, P.Description AS PostDescription,
                   U.Name AS UserName, U.Photo AS UserPhoto, U.MemberSince AS UserMemberSince
            FROM AnswerPost A
            INNER JOIN POST P ON A.PostId = P.Id
            INNER JOIN [USER] U ON U.Id = A.UserId
            WHERE A.PostId = @PostId";


            using (IDbConnection db = GetConnection())
            {
                var result = db.Query<AnswerPost, dynamic, AnswerPost>(
                                query,
                                (answerPost, dynamicUser) =>
                                {
                                    answerPost.User = new User
                                    {
                                        UserId = dynamicUser.UserId,
                                        UserName = dynamicUser.UserName,
                                        UserPhoto = dynamicUser.UserPhoto,
                                        MemberSince = dynamicUser.UserMemberSince
                                    };
                                    return answerPost;
                                },
                                param: new { PostId = postId },
                                splitOn: "PostTitle, UserName" 
                            ).Distinct().FirstOrDefault();

                return result;
            }
        }

    }
}
