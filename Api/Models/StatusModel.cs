namespace Api.Models
{
    public class StatusModel
    {
        public string Status { get; set; }

        public StatusModel(string status)
        {
            Status = status;
        }
    }
}