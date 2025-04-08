using DevVoiceFirst.DtoModel;

namespace DevVoiceFirst.IServices
{
    public interface IDivisionService
    {

        //-------------------------------- div one-----------------------------------------------//
        Task<(Dictionary<string, object>, string, int)> AddAsync(DivisonOneDtoModel DivisionOne);
        Task<(Dictionary<string, object>, string, int)> UpdateAsync(DivisonOneDtoModel DivisionOne);
        Task<(Dictionary<string, object>, string, int)> GetAllAsync(Dictionary<string, string> filters);
        Task<(Dictionary<string, object>, string, int)> GetByIdAsync(string id, Dictionary<string, string> filters);
        Task<(Dictionary<string, object>, string, int)> DeleteAsync(string id);
        Task<(Dictionary<string, object>, string, int)> UpdateStatus(UpdateStatusDtoModel updateStatusDtoModel);

        Task<(Dictionary<string, object>, string, int)> ImportStateByCountry(List<ImportDivisionOneModel> importlist);

    }
}
