namespace DevVoiceFirst.DtoModel;

public class CountryDTOModel
{
    public string t2_1_country_name { get; set; }

    public string? t2_1_div1_called { get; set; }
    public string? t2_1_div2_called { get; set; }
    public string? t2_1_div3_called { get; set; }

    public CountryDTOModel(string countryName,string div1Name,string div2Name,string div3Name)
    {
        if (string.IsNullOrWhiteSpace(countryName))
            throw new ArgumentException("t2_1_country_name is required.", nameof(countryName));
        
      
        t2_1_country_name=countryName;
        t2_1_div1_called=div1Name;
        t2_1_div2_called=div2Name;
        t2_1_div3_called=div3Name;
        
    }
    
}

public class UpdateDTOModel: CountryDTOModel
{
    public UpdateDTOModel(string countryName,string div1Name,string div2Name,string div3Name, string idT21Country) : base(countryName, div1Name,div2Name,div3Name)
    { 
        if (string.IsNullOrWhiteSpace(idT21Country))
            throw new ArgumentException("id_t2_1_country is required.", nameof(idT21Country));
        
        
        id_t2_1_country = idT21Country;
    }

    public string id_t2_1_country { get; set; }
}

public class ImportCountryModel
{
    public string t2_1_country_name { get; set; }
    public string? t2_1_div1_called   { get; set; }
    public string? t2_1_div2_called{ get; set; }
    public string? t2_1_div3_called { get; set; }
    
    public ImportCountryModel(string countryName,string? div1Name,string? div2Name,string? div3Name)
    {
        if (string.IsNullOrWhiteSpace(countryName))
            throw new ArgumentException("t2_1_country_name is required.", nameof(countryName));
        
      
        t2_1_country_name=countryName;
        t2_1_div1_called=div1Name;
        t2_1_div2_called=div2Name;
        t2_1_div3_called=div3Name;
        
    }

    
}