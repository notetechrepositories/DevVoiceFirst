using System.Data;
using DevVoiceFirst.Model;

namespace DevVoiceFirst.IRepository;

public interface ICountryRepo
{
    Task<IEnumerable<CountryModel>> GetAllAsync(Dictionary<string, string> filters);
    Task<IEnumerable<CountryModel>> GetAscAll(Dictionary<string, string> filters);
    Task<CountryModel> GetByIdAsync(string id, Dictionary<string, string> filters);
    Task<int> AddAsync(object parameters,IDbTransaction? transaction = null);
    Task<int> UpdateAsync(object parameters,IDbTransaction? transaction = null);
    Task<int> DeleteAsync(string id, IDbTransaction? transaction = null);
    Task<int> UpdateStatus(string id, string status,IDbTransaction? transaction = null);
}