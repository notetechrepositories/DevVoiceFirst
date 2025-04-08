using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.IRepository
{
    public interface IUserRepo
    {
        Task<IEnumerable<UserModel>> GetAllAsync(Dictionary<string, string> filters);
        Task<UserDetailsModel> GetUserDetailsByEmailOrPhone(string username);
        Task<UserDetailsModel> GetAllUserDetailsByUserId(string userId);
        Task<UserModel> GetByIdAsync(string id, Dictionary<string, string> filters);
        Task<int> AddAsync(object parameters, IDbTransaction transaction = null);
        Task<int> UpdateAsync(object parameters, IDbTransaction transaction = null);
        Task<int> UpdatePasswordAsync(object parameters, IDbTransaction transaction = null);
        Task<int> DeleteAsync(string id, IDbTransaction transaction = null);
        Task<int> UpdateStatus(string id, int status, IDbTransaction transaction = null);
    }
}
