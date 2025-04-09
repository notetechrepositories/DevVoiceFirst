using DevVoiceFirst.DtoModel;

namespace DevVoiceFirst.IServices;

public interface ICountryService
{
    Task<(Dictionary<string, object>, string,int)> AddAsync(CountryDTOModel  Country);
    Task<(Dictionary<string, object>, string, int)> UpdateAsync(UpdateDTOModel Country);
    Task<(Dictionary<string, object>, string, int)> GetAllAsync(Dictionary<string, string> filters);
    Task<(Dictionary<string, object>, string, int)> GetByIdAsync(string id, Dictionary<string, string> filters);
    Task<(Dictionary<string, object>, string, int)> DeleteAsync(string id);
    Task<(Dictionary<string, object>, string, int)> ImportCountry(List<ImportCountryModel> import);
    Task<(Dictionary<string, object>, string, int)> UpdateStatus(UpdateStatusDtoModel updateStatusDtoModel);
    //Task<(Dictionary<string, object>, string, int)> ImportTest(List<ImportCountryModel> import);
}