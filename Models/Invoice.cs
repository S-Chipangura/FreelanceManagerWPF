using System;

namespace FreelanceManager.Models
{
    public class Invoice
    {
        public string? Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public bool Paid { get; set; }
    }
}

