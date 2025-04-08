namespace DevVoiceFirst.DtoModel
{
    public class DivisonOneDtoModel
    {
        public string t2_1_div1_name { get; }
        public string id_t2_1_country { get; }

        public DivisonOneDtoModel(string name, string id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("t2_1_div1_name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("id_t2_1_country is required.", nameof(id));

            t2_1_div1_name = name;
            id_t2_1_country = id;
        }
    }

    public class ImportDivisionOneModel
    {
        public string t2_1_div1_name { get; }
        public string t2_1_country_name { get; }

        public ImportDivisionOneModel(string name, string country)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("t2_1_div1_name is required.", nameof(name));

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("t2_1_country_name is required.", nameof(country));

            t2_1_div1_name = name;
            t2_1_country_name = country;
        }
    }
}
