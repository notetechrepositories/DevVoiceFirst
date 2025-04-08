using Dapper;
using DevVoiceFirst.Context;
using DevVoiceFirst.IRepository;
using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.Repository
{
    public class DivisionRepo : IDivisionRepo
    {
        private readonly DapperContext _dapperContext;

        public DivisionRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }
        //-------------------------------- div one-----------------------------------------------//
        public async Task<int> AddDivOneAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                INSERT INTO t2_1_div1 (id_t2_1_div1,id_t2_1_country,t2_1_div1_name,inserted_by,inserted_date) 
                VALUES (@Id,@CountryId,@Name,@InsertedBy,@InsertedDate);";
            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, parameters, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, parameters);
                }
            }
        }

        public async Task<int> DeleteDivOneAsync(string id, IDbTransaction? transaction = null)
        {
            var query = "delete FROM t2_1_div1  WHERE id_t2_1_div1 = @id";

            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, new { id = id }, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, new { id = id });
                }
            }

        }

        public async Task<IEnumerable<DivisionOneModel>> GetAllDivOneAsync(Dictionary<string, string> filters)
        {
            var query = "SELECT t2_1_div1.id_t2_1_div1,t2_1_div1.id_t2_1_country,t2_1_div1.t2_1_div1_name,t2_1_country.t2_1_country_name FROM t2_1_div1 inner join t2_1_country on t2_1_country.id_t2_1_country=t2_1_div1.id_t2_1_country ";

            if (filters != null && filters.Any())
            {
                var keys = new List<string>(filters.Keys);
                var whereClauses = "";
                for (int i = 0; i < keys.Count; i++)
                {
                    string key = keys[i];
                    string value = filters[key];
                    if (i == 0)
                    {
                        whereClauses = " t2_1_div1." + key + "='" + value + "'";
                    }
                    else
                    {
                        whereClauses += " AND t2_1_div1." + key + "='" + value + "'";
                    }

                }
                query += " WHERE " + whereClauses + ";";
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<DivisionOneModel>(query);
            }
        }

        public async Task<DivisionOneModel> GetByIdDivOneAsync(string id, Dictionary<string, string> filters)
        {
            var query = "SELECT t2_1_div1.id_t2_1_div1,t2_1_div1.id_t2_1_country,t2_1_div1.t2_1_div1_name,t2_1_country.t2_1_country_name FROM t2_1_div1 inner join t2_1_country on t2_1_country.id_t2_1_country=t2_1_div1.id_t2_1_country WHERE id_t2_1_div1 = @id";

            if (filters != null && filters.Any())
            {
                var keys = new List<string>(filters.Keys);
                var whereClauses = "";
                for (int i = 0; i < keys.Count; i++)
                {
                    string key = keys[i];
                    string value = filters[key];
                    whereClauses += " AND t2_1_div1." + key + "='" + value + "'";


                }
                query += whereClauses + ";";
            }

            var parameters = new DynamicParameters();
            parameters.Add("id", id);



            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<DivisionOneModel>(query, parameters);
            }
        }

        public async Task<int> UpdateDivOneAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                UPDATE t2_1_div1 
                SET 
                    t2_1_div1_name = @Name, 
                    id_t2_1_country = @CountryId, 
                    updated_by = @UpdatedBy, 
                    updated_date = @UpdatedDate
                WHERE id_t2_1_div1 = @Id";

            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, parameters, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, parameters);
                }
            }
        }
        public async Task<int> UpdateStatusDivOne(string id, string status, IDbTransaction? transaction = null)
        {
            var query = "UPDATE t2_1_div1 set is_active=@status  WHERE id_t2_1_div1 = @id";

            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, new { id = id, status = status }, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, new { id = id, status = status });
                }
            }


        }


        //-------------------------------- div two-----------------------------------------------//

        public async Task<int> AddDivTwoAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                INSERT INTO t2_1_div2 (id_t2_1_div2,id_t2_1_div1,t2_1_div2_name,inserted_by,inserted_date) 
                VALUES (@Id,@Div1Id,@Name,@InsertedBy,@InsertedDate);";
            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, parameters, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, parameters);
                }
            }
        }

        public async Task<int> DeleteDivTwoAsync(string id, IDbTransaction? transaction = null)
        {
            var query = "DELETE FROM t2_1_div2 WHERE id_t2_1_div2 = @id";

            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, new { id = id }, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, new { id = id });
                }
            }
            
        }

        public async Task<IEnumerable<DivisionTwoModel>> GetAllDivTwoAsync(Dictionary<string, string> filters)
        {
            var query = "SELECT t2_1_div2.id_t2_1_div2,t2_1_div2.id_t2_1_div1,t2_1_div2.t2_1_div2_name,t2_1_div1.t2_1_div1_name from t2_1_div2 " +
                  "inner join t2_1_div1 on t2_1_div1.id_t2_1_div1=t2_1_div2.id_t2_1_div1 ";

            if (filters != null && filters.Any())
            {
                var keys = new List<string>(filters.Keys);
                var whereClauses = "";
                for (int i = 0; i < keys.Count; i++)
                {
                    string key = keys[i];
                    string value = filters[key];
                    if (i == 0)
                    {
                        whereClauses = " t2_1_div2." + key + "='" + value + "'";
                    }
                    else
                    {
                        whereClauses += " AND t2_1_div2." + key + "='" + value + "'";
                    }
                }
                query += " WHERE " + whereClauses + ";";
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<DivisionTwoModel>(query);
            }
        }

        public async Task<DivisionTwoModel> GetByIdDivTwoAsync(string id, Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t2_1_div2 WHERE id_t2_1_div2 = @id";

            if (filters != null && filters.Any())
            {
                var keys = new List<string>(filters.Keys);
                var whereClauses = "";
                for (int i = 0; i < keys.Count; i++)
                {
                    string key = keys[i];
                    string value = filters[key];
                    whereClauses += " AND " + key + "='" + value + "'";
                }
                query += whereClauses + ";";
            }

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<DivisionTwoModel>(query, parameters);
            }
        }

        public async Task<int> UpdateDivTwoAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                UPDATE t2_1_div2
                SET 
                    t2_1_div2_name = @Name, 
                    id_t2_1_div1 = @Div1Id, 
                    updated_by = @UpdatedBy, 
                    updated_date = @UpdatedDate
                WHERE id_t2_1_div2 = @Id";

            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, parameters, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, parameters);
                }
            }
        }
        public async Task<int> UpdateStatusDivTwo(string id, string status, IDbTransaction? transaction = null)
        {
            var query = "UPDATE t2_1_div2 set is_active=@status  WHERE id_t2_1_div2 = @id";

            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, new { id = id, status = status }, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, new { id = id, status = status });
                }
            }


        }

        //-------------------------------- div three-----------------------------------------------//

        public async Task<int> AddDivThreeAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                INSERT INTO t2_1_div3(id_t2_1_div3,t2_1_div3_name,id_t2_1_div2,inserted_by,inserted_date) 
                VALUES (@Id,@Name,@Div2Id,@InsertedBy,@InsertedDate);";
            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, parameters, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, parameters);
                }
            }
        }

        public async Task<int> DeleteDivThreeAsync(string id, IDbTransaction? transaction = null)
        {
            var query = "delete FROM t2_1_div3  WHERE id_t2_1_div3 = @id";


            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, new { id = id }, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, new { id = id });
                }
            }
            
        }

        public async Task<IEnumerable<DivisionThreeModel>> GetAllDivThreeAsync(Dictionary<string, string> filters)
        {
            var query = "SELECT t2_1_div3.id_t2_1_div3,t2_1_div3.id_t2_1_div2,t2_1_div3.t2_1_div3_name,t2_1_div2.t2_1_div2_name from t2_1_div3 " +
                  "inner join t2_1_div2 on t2_1_div2.id_t2_1_div2 = t2_1_div3.id_t2_1_div2";

            if (filters != null && filters.Any())
            {
                var keys = new List<string>(filters.Keys);
                var whereClauses = "";
                for (int i = 0; i < keys.Count; i++)
                {
                    string key = keys[i];
                    string value = filters[key];
                    if (i == 0)
                    {
                        whereClauses = " t2_1_div3." + key + "='" + value + "'";
                    }
                    else
                    {
                        whereClauses += " AND t2_1_div3." + key + "='" + value + "'";
                    }
                }
                query += " WHERE " + whereClauses + ";";
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<DivisionThreeModel>(query);
            }
        }

        public async Task<DivisionThreeModel> GetByIdDivThreeAsync(string id, Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t2_1_div3 WHERE id_t2_1_div3 = @id";

            if (filters != null && filters.Any())
            {
                var keys = new List<string>(filters.Keys);
                var whereClauses = "";
                for (int i = 0; i < keys.Count; i++)
                {
                    string key = keys[i];
                    string value = filters[key];
                    whereClauses += " AND " + key + "='" + value + "'";
                }
                query += whereClauses + ";";
            }

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<DivisionThreeModel>(query, parameters);
            }
        }

        public async Task<int> UpdateDivThreeAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                UPDATE t2_1_div3
                SET 
                    t2_1_div3_name = @Name, 
                    id_t2_1_div2=@Div2Id,
                    updated_by = @UpdatedBy, 
                    updated_date = @UpdatedDate
                WHERE id_t2_1_div3 = @Id";

            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, parameters, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, parameters);
                }
            }
        }

        public async Task<int> UpdateStatusDivThree(string id, string status, IDbTransaction? transaction = null)
        {
            var query = "UPDATE t2_1_div3 set is_active=@status  WHERE id_t2_1_div3 = @id";

            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, new { id = id, status = status }, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, new { id = id, status = status });
                }
            }

            
        }
    }
}
