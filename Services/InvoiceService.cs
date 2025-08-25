using System;
using System.Linq;
using FreelanceManager.Models;

namespace FreelanceManager.Services
{
    public class InvoiceService
    {
        private readonly DataService _dataService;
        public InvoiceService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void AddInvoice(string clientId, string projectId, Invoice invoice)
        {
            var client = _dataService.GetClient(clientId);
            if (client == null) return;

            var project = client.Projects.Find(p => p.Id == projectId);
            if (project == null) return;

            project.Invoices.Add(invoice);
            _dataService.Save();
        }
    }
}

