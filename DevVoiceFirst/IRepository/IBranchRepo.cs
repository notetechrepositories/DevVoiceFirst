using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.IRepository
{
    public interface IBranchRepo
    {
        Task<IEnumerable<BranchModel>> GetAllAsync(Dictionary<string, string> filters);
        Task<BranchModel> GetByIdAsync(string id, Dictionary<string, string> filters);
        Task<int> AddAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> UpdateAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> DeleteAsync(string id, IDbTransaction? transaction = null);
        Task<int> UpdateStatus(string id, int status, IDbTransaction? transaction = null);
    }
}
