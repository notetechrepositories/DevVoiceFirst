﻿using Dapper;
using DevVoiceFirst.Context;
using DevVoiceFirst.IRepository;
using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DapperContext _dapperContext;

        public UserRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> AddAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                INSERT INTO t5_users(id_t5_users,t5_first_name,t5_last_name,t5_address_1,t5_address_2,t5_zip_code,
                t5_mobile_no,t5_email,t5_password,t5_salt_key,t5_birth_year,t5_sex,id_t2_1_local,id_t5_1_m_user_roles,inserted_by,inserted_date) 
                VALUES (@Id,@FirstName,@LastName,@Address1,@Address2,@ZipCode,@Mobile,@Email,@Password,@saltKey,@BirthDate,
                @Sex,@Local,@RoleId,@InsertedBy,@InsertedDate);";
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
            var query = "UPDATE t5_users set is_delete=0,is_active=1 WHERE id_t5_users = @id";
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

        public async Task<IEnumerable<UserModel>> GetAllAsync(Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t5_users WHERE is_delete='0' " ;

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

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<UserModel>(query);
            }
        }

        public async Task<UserDetailsModel> GetAllUserDetailsByUserId(string userId)
        {
            var query = "select * from t5_users where id_t5_users = @userId";


            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<UserDetailsModel>(query, new { userId = userId });// for passing a single parameter
            }
        }

        public async Task<UserModel> GetByIdAsync(string id, Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t5_users WHERE id_t5_users = @id";

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

            var parameters = new DynamicParameters();  //for multiple parameter pass on the function 
            parameters.Add("id", id);

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<UserModel>(query, parameters);
            }
        }

        public async Task<UserDetailsModel> GetUserDetailsByEmailOrPhone(string username)
        {
            var query = "select * from t5_users where t5_mobile_no = @name OR t5_email = @name";


            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<UserDetailsModel>(query, new { name = username });// for passing a single parameter
            }
        }

        public async Task<int> UpdateAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                UPDATE t5_users
                SET 
                    t5_first_name = @FirstName, 
                    t5_last_name = @LastName, 
                    t5_address_1 = @Address1, 
                    t5_address_2 = @Address2, 
                    t5_zip_code = @ZipCode, 
                    t5_mobile_no = @Mobile, 
                    t5_email = @Email, 
                    t5_birth_year = @BirthDate, 
                    t5_sex = @Sex, 
                    id_t2_1_local = @Local, 
                    updated_by = @UpdatedBy, 
                    updated_date = @UpdatedDate
                WHERE id_t5_users = @Id";
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

        public async Task<int> UpdatePasswordAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                UPDATE t5_users
                SET 
                    t5_password = @Password, 
                    t5_salt_key = @SaltKey, 
                    updated_by = @UpdatedBy, 
                    updated_date = @UpdatedDate
                WHERE id_t5_users = @Id";

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

        public async Task<int> UpdateStatus(string id, int status, IDbTransaction? transaction = null)
        {
            var query = "UPDATE t5_users set is_active=@status  WHERE id_t5_users = @id";

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
