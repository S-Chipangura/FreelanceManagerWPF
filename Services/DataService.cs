using System.Collections.Generic;
using System.IO;
using FreelanceManager.Models;
using FreelanceManager.Utils;

namespace FreelanceManager.Services
{
    public class DataService
    {
        private readonly string _filePath;
        public List<Client> Clients { get; set; }

        public DataService()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appData, "FreelanceManagerWPF");

            Directory.CreateDirectory(folder); // ensures folder exists

            _filePath = Path.Combine(folder, "clients.json");

            Clients = JsonHelper.LoadFromJson<List<Client>>(_filePath) ?? new List<Client>();
        }

        public void Save() => JsonHelper.SaveToJson(_filePath, Clients);

        public Client GetClient(string id)
        {
            return Clients.Find(c => c.Id == id) ?? throw new InvalidOperationException($"Client with ID {id} not found.");
        }

        public void AddClient(Client client)
        {
            Clients.Add(client);
            Save();
        }
    }
}
