using System.Data;
using Dapper;
using DevVoiceFirst.Context;
using DevVoiceFirst.IRepository;
using DevVoiceFirst.Model;

namespace DevVoiceFirst.Repository;

public class CountryRepo : ICountryRepo
    {
        private readonly DapperContext _dapperContext;

        public CountryRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> AddAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                INSERT INTO t2_1_country(id_t2_1_country,t2_1_country_name,t2_1_div1_called,t2_1_div2_called,t2_1_div3_called,inserted_by,inserted_date) 
                VALUES (@Id,@Name,@Div1,@Div2,@Div3,@InsertedBy,@InsertedDate);";
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


        public async Task<int> DeleteAsync(string id, IDbTransaction? transaction = null)
        {
            var query = "delete FROM t2_1_country  WHERE id_t2_1_country = @id";
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
        public async Task<int> UpdateStatus(string id, string status, IDbTransaction? transaction = null)
        {
            var query = "UPDATE t2_1_country set is_active=@status  WHERE id_t2_1_country = @id";
            if (transaction != null)
            {
                // 🔥 If transaction exists, reuse connection without disposing
                return await transaction.Connection.ExecuteAsync(query, new { id = id ,status=status}, transaction);
            }
            else
            {
                // 🔥 Else, create and dispose new connection
                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteAsync(query, new { id = id ,status = status });
                }
            }
           
        }

        public async Task<IEnumerable<CountryModel>> GetAllAsync(Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t2_1_country ";

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
                        whereClauses = " " + key + "='" + value + "'";
                    }
                    else
                    {
                        whereClauses += " AND " + key + "='" + value + "'";
                    }

                }
                query += " WHERE " + whereClauses+";";
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<CountryModel>(query);
            }
        }
        public async Task<IEnumerable<CountryModel>> GetAscAll(Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t2_1_country ";

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
                        whereClauses = " " + key + "='" + value + "'";
                    }
                    else
                    {
                        whereClauses += " AND " + key + "='" + value + "'";
                    }

                }
                query += " WHERE " + whereClauses + " order by t2_1_country_name ASC;";
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<CountryModel>(query);
            }
        }

        public async Task<CountryModel> GetByIdAsync(string id, Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t2_1_country  WHERE id_t2_1_country = @id ";

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
                query +=  whereClauses + ";";
            }

            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<CountryModel>(query, parameters);
            }
        }

        public async Task<int> UpdateAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                UPDATE t2_1_country
                SET 
                    t2_1_country_name = @Name, 
                    t2_1_div1_called = @Div1, 
                    t2_1_div2_called = @Div2, 
                    t2_1_div3_called = @Div3, 
                    updated_by = @UpdatedBy, 
                    updated_date = @UpdatedDate
                WHERE id_t2_1_country = @Id";

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
    }