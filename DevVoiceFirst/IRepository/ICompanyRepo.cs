using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.IRepository
{
    public interface ICompanyRepo
    {
        Task<IEnumerable<CompanyModel>> GetAllAsync(Dictionary<string, string> filters);
        Task<CompanyModel> GetByIdAsync(string id, Dictionary<string, string> filters);
        Task<int> AddAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> UpdateAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> DeleteAsync(string id, IDbTransaction? transaction = null);
        Task<int> UpdateStatus(string id, int status, IDbTransaction? transaction = null);
    }
}
