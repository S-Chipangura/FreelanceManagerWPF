using System.Collections.Generic;


namespace FreelanceManager.Models
{
    public class Client
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}

