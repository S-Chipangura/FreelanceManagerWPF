using System;

namespace FreelanceManager.Services
{
    public class NotificationService
    {
        public void NotifyInvoiceDue(string clientName, double amount)
        {
            Console.WriteLine($"[NOTIFICATION] {clientName}, you have an invoice due: ${amount}");
        }
    }
}
