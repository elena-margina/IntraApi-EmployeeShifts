namespace IntraApi.App.Models
{
    public class RoleListVm
    {
        public int ID { get; set; }
        public bool IsPrimary { get; set; }
        public int SeatsAvailable { get; set; }
        public bool IsAvailable { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
