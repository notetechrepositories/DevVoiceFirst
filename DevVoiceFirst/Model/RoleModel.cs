namespace DevVoiceFirst.Model
{
    public class RoleModel
    {
        public string id_t5_1_sys_roles { get; set; }
        public string t5_1_sys_roles_name { get; set; }
        public string t5_1_sys_all_location_access { get; set; }
        public string t5_1_sys_all_issues { get; set; }
        public string inserted_by { get; set; }
        public DateTime inserted_date { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_date { get; set; }
    }
    public class ProgramRoleModel
    {
        public string program_id { get; set; }
        public string program_name { get; set; }
        public string description { get; set; }
        public string add { get; set; }
        public string edit { get; set; }
        public string delete { get; set; }
        public string view { get; set; }
        public string update_from_excel { get; set; }
        public string download_excel { get; set; }
        public string download_pdf { get; set; }
        public string inserted_by { get; set; }
        public DateTime inserted_date { get; set; }
        public string updated_by { get; set; }
        public DateTime updated_date { get; set; }

    }
}
