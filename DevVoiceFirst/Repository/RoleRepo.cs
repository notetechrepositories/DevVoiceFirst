
using Dapper;
using DevVoiceFirst.Context;
using DevVoiceFirst.IRepository;
using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.Repository
{
    public class RoleRepo : IRoleRepo
    {
        private readonly DapperContext _dapperContext;

        public RoleRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        //............Get Program ...........//


        public async Task<IEnumerable<ProgramRoleModel>> GetAllProgramRoleAsync(Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t7_program Where is_delete='n' ";

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
                query += " WHERE " + whereClauses + ";";
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<ProgramRoleModel>(query);
            }
        }
        //------------------------------------ NeW Sys Role-------------------------------------
        public async Task<int> AddSysRoleAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                INSERT INTO t5_1_sys_roles(id_t5_1_sys_roles,t5_1_sys_roles_name,t5_1_sys_all_location_access,t5_1_sys_all_issues,inserted_by,inserted_date) 
                VALUES (@Id,@Name,@AllLocationAccess,@AllIssueAcces,@InsertedBy,@InsertedDate);";
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

        public async Task<int> DeleteSysRoleAsync(string id, IDbTransaction? transaction = null)
        {
            var query = "UPDATE t5_1_sys_roles set is_delete='y' WHERE id_t5_1_sys_roles = @id";

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

        public async Task<IEnumerable<RoleModel>> GetAllSysRoleAsync(Dictionary<string, string> filters)
        {
            var query = "SELECT * FROM t5_1_sys_roles Where is_delete='n' ";

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

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<RoleModel>(query);
            }
        }
        public async Task<int> UpdateSysRoleAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                UPDATE t5_1_sys_roles
                SET 
                    t5_1_sys_roles_name = @Name, 
                    t5_1_sys_all_location_access=@AllLocationAccess,
                    t5_1_sys_all_issues=@AllIssueAcces,
                    updated_by = @UpdatedBy, 
                    updated_date = @UpdatedDate
                WHERE id_t5_1_sys_roles = @Id"
            ;

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
}
