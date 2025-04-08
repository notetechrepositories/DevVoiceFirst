using DevVoiceFirst.Model;
using System.Data;

namespace DevVoiceFirst.IRepository
{
    public interface IRoleRepo
    {
        //----------------------------------program role----------------------------------------//

        Task<IEnumerable<ProgramRoleModel>> GetAllProgramRoleAsync(Dictionary<string, string> filters);

        //------------------------------------ NeW Sys Role-------------------------------------


        Task<IEnumerable<RoleModel>> GetAllSysRoleAsync(Dictionary<string, string> filters);
        Task<int> AddSysRoleAsync(object parameters, IDbTransaction transaction = null);
        Task<int> UpdateSysRoleAsync(object parameters, IDbTransaction transaction = null);
        Task<int> DeleteSysRoleAsync(string id, IDbTransaction transaction = null);
    }
}
