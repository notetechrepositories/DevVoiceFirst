using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.IRepository
{
    public interface IDivisionRepo
    {
        //-------------------------------- div one-----------------------------------------------//
        Task<IEnumerable<DivisionOneModel>> GetAllDivOneAsync(Dictionary<string, string> filters);
        Task<DivisionOneModel> GetByIdDivOneAsync(string id, Dictionary<string, string> filters);
        Task<int> AddDivOneAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> UpdateDivOneAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> DeleteDivOneAsync(string id, IDbTransaction? transaction = null);
        Task<int> UpdateStatusDivOne(string id, string status, IDbTransaction? transaction = null);

        //-------------------------------- div two-----------------------------------------------//
        Task<IEnumerable<DivisionTwoModel>> GetAllDivTwoAsync(Dictionary<string, string> filters);
        Task<DivisionTwoModel> GetByIdDivTwoAsync(string id, Dictionary<string, string> filters);
        Task<int> AddDivTwoAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> UpdateDivTwoAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> DeleteDivTwoAsync(string id, IDbTransaction? transaction = null);
        Task<int> UpdateStatusDivTwo(string id, string status, IDbTransaction? transaction = null);

        //-------------------------------- div three-----------------------------------------------// 
        Task<IEnumerable<DivisionThreeModel>> GetAllDivThreeAsync(Dictionary<string, string> filters);
        Task<DivisionThreeModel> GetByIdDivThreeAsync(string id, Dictionary<string, string> filters);
        Task<int> AddDivThreeAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> UpdateDivThreeAsync(object parameters, IDbTransaction? transaction = null);
        Task<int> DeleteDivThreeAsync(string id, IDbTransaction? transaction = null);
        Task<int> UpdateStatusDivThree(string id, string status, IDbTransaction? transaction = null);
    }
}
