namespace DevVoiceFirst.DtoModel
{
    public class UpdateStatusDtoModel
    {
        public string Status { get; }
        public string Id { get; }

        public UpdateStatusDtoModel(string status, string id)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status is required.", nameof(status));

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Id is required.", nameof(id));

            Status = status;
            Id= id;
        }
    }
}
