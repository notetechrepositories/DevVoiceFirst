using Dapper;
using DevVoiceFirst.Context;
using DevVoiceFirst.IRepository;
using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.Repository
{
    public class BranchRepo: IBranchRepo
    {
        private readonly DapperContext _dapperContext;

        public BranchRepo(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> AddAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                INSERT INTO t2_company_branch(id_t2_company_branch,id_t1_company,t2_company_branch_name,t2_id_branch_type,
                t2_address_1,t2_address_2,t2_zip_code,t2_mobile_no,t2_phone_no,t2_email,id_t2_1_local,inserted_by,inserted_date) 
                VALUES (@Id,@CompanyId,@Name,@BranchType,@Address1,@Address2,@ZipCode,@Mobile,@PhoneNo,@Email,@Local,@InsertedBy,@InsertedDate);";
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
            var query = "UPDATE t2_company_branch set is_delete=1,is_active=0  WHERE id_t2_company_branch = @id";
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
        public async Task<int> UpdateStatus(string id, int status, IDbTransaction? transaction = null)
        {
            var query = "UPDATE t2_company_branch set is_active=@status  WHERE id_t2_company_branch = @id";
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

        public async Task<IEnumerable<BranchModel>> GetAllAsync(Dictionary<string, string> filters)
        {
            var query = "SELECT t4_1_selection_values.t4_1_selection_values_name as company_branch_type_name,t2_company_branch.id_t2_company_branch,t2_company_branch.id_t1_company," +
                "t2_company_branch.t2_company_branch_name,t2_company_branch.t2_id_branch_type,t2_company_branch.t2_address_1,t2_company_branch.t2_address_2," +
                "t2_company_branch.t2_zip_code,t2_company_branch.t2_mobile_no,t2_company_branch.t2_phone_no,t2_company_branch.t2_email,t2_1_local.t2_1_local_name," +
                "t2_1_country.t2_1_country_name,t2_1_div1.t2_1_div1_name,t2_1_div2.t2_1_div2_name,t2_1_div3.t2_1_div3_name,t2_1_local.id_t2_1_div1,t2_1_local.id_t2_1_div2,t2_1_local.id_t2_1_div3,t2_1_local.id_t2_1_country," +
                "t2_company_branch.is_active,t2_company_branch.is_delete" +
                  " FROM t2_company_branch" +
                  " INNER JOIN t2_1_local ON t2_1_local.id_t2_1_local = t2_company_branch.id_t2_1_local LEFT JOIN t2_1_country ON t2_1_country.id_t2_1_country = t2_1_local.id_t2_1_country" +
                  " LEFT JOIN t2_1_div1 ON t2_1_div1.id_t2_1_div1 = t2_1_local.id_t2_1_div1" +
                  " LEFT JOIN t2_1_div2 ON t2_1_div2.id_t2_1_div2 = t2_1_local.id_t2_1_div2" +
                  " Left join t4_1_selection_values on t4_1_selection_values.id_t4_1_selection_values=t2_company_branch.t2_id_branch_type" +
                  " LEFT JOIN t2_1_div3 ON t2_1_div3.id_t2_1_div3 = t2_1_local.id_t2_1_div3 ";

            if (filters != null && filters.Any())
            {
                var keys = new List<string>(filters.Keys);
                var whereClauses = "t2_company_branch.is_delete=0 ";
                for (int i = 0; i < keys.Count; i++)
                {
                    string key = keys[i];
                    string value = filters[key];

                    whereClauses += " AND t2_company_branch." + key + "='" + value + "'";

                }
                query += " WHERE " + whereClauses + ";";
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<BranchModel>(query);
            }
        }

        public async Task<BranchModel> GetByIdAsync(string id, Dictionary<string, string> filters)
        {
            var query = @"SELECT t4_1_selection_values.t4_1_selection_values_name AS company_branch_type_name,t2_company_branch.id_t2_company_branch,t2_company_branch.id_t1_company,t2_company_branch.t2_company_branch_name,t2_company_branch.t2_id_branch_type,t2_company_branch.t2_address_1,t2_company_branch.t2_address_2,t2_company_branch.t2_zip_code,t2_company_branch.t2_mobile_no,t2_company_branch.t2_phone_no,t2_company_branch.t2_email,t2_1_local.t2_1_local_name,t2_1_country.t2_1_country_name,t2_1_div1.t2_1_div1_name,t2_1_div2.t2_1_div2_name,t2_1_div3.t2_1_div3_name,t2_1_local.id_t2_1_div1,t2_1_local.id_t2_1_div2,t2_1_local.id_t2_1_div3,t2_1_local.id_t2_1_country,t2_company_branch.is_active,t2_company_branch.is_delete FROM t2_company_branch LEFT JOIN t2_1_local ON t2_1_local.id_t2_1_local = t2_company_branch.id_t2_1_local LEFT JOIN t2_1_country ON t2_1_country.id_t2_1_country = t2_1_local.id_t2_1_country LEFT JOIN t2_1_div1 ON t2_1_div1.id_t2_1_div1 = t2_1_local.id_t2_1_div1 LEFT JOIN t2_1_div2 ON t2_1_div2.id_t2_1_div2 = t2_1_local.id_t2_1_div2 LEFT JOIN t2_1_div3 ON t2_1_div3.id_t2_1_div3 = t2_1_local.id_t2_1_div3 LEFT JOIN t4_1_selection_values ON t4_1_selection_values.id_t4_1_selection_values = t2_company_branch.t2_id_branch_type WHERE t2_company_branch.id_t2_company_branch = 'e93d0a4c-6b52-4280-9243-564147208b0c';";

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
                return await connection.QuerySingleOrDefaultAsync<BranchModel>(query, parameters);
            }
        }

        public async Task<int> UpdateAsync(object parameters, IDbTransaction? transaction = null)
        {
            var query = @"
                UPDATE t2_company_branch
                SET 
                    t2_company_branch_name = @Name, 
                    t2_id_branch_type = @BranchType, 
                    t2_address_1 = @Address1, 
                    t2_address_2 = @Address2, 
                    t2_zip_code = @ZipCode, 
                    t2_mobile_no = @Mobile, 
                    t2_phone_no = @PhoneNo, 
                    t2_email = @Email, 
                    id_t2_1_local = @Local, 
                    updated_by = @UpdatedBy, 
                    updated_date = @UpdatedDate
                WHERE id_t2_company_branch = @Id";

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
