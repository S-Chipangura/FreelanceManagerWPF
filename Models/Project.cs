using System;
using System.Collections.Generic;


namespace FreelanceManager.Models
{
    public class Project
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public double TotalAmount { get; set; }
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}

