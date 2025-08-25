using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FreelanceManager.Models;
using FreelanceManager.Services;

namespace FreelanceManagerWPF
{
    public partial class MainWindow : Window
    {
        private DataService _dataService;
        private InvoiceService _invoiceService;
        private NotificationService _notificationService;

        public MainWindow()
        {
            InitializeComponent();

            _dataService = new DataService();
            _invoiceService = new InvoiceService(_dataService);
            _notificationService = new NotificationService();

            RefreshClientComboBoxes();
        }

        private void RefreshClientComboBoxes()
        {
            ClientComboBox.ItemsSource = _dataService.Clients;
            ReportClientComboBox.ItemsSource = _dataService.Clients;
        }

        private void RefreshProjectComboBox()
        {
            if (ClientComboBox.SelectedItem is Client selectedClient)
            {
                ProjectComboBox.ItemsSource = selectedClient.Projects;
            }
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            var client = new Client
            {
                Id = Guid.NewGuid().ToString(),
                Name = ClientNameTextBox.Text,
                Email = ClientEmailTextBox.Text
            };
            _dataService.AddClient(client);
            RefreshClientComboBoxes();
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            if (ClientComboBox.SelectedItem is Client client)
            {
                var project = new Project
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = ProjectNameTextBox.Text,
                    StartDate = DateTime.Now,
                    TotalAmount = double.Parse(ProjectAmountTextBox.Text)
                };
                client.Projects.Add(project);
                _dataService.Save();
                RefreshProjectComboBox();
            }
        }

        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            var client = ClientComboBox.SelectedItem as Client;
            var project = ProjectComboBox.SelectedItem as Project;

            if (client?.Id != null && project?.Id != null)
            {
                var invoice = new Invoice
                {
                    Id = Guid.NewGuid().ToString(),
                    Amount = double.Parse(InvoiceAmountTextBox.Text),
                    Date = DateTime.Now,
                    Paid = InvoicePaidCheckBox.IsChecked ?? false
                };

                _invoiceService.AddInvoice(client.Id, project.Id, invoice);
                MessageBox.Show("Invoice added successfully!");
            }
            else
            {
                MessageBox.Show("Please select a valid client and project first.");
            }
        }


        private void ReportClientComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ReportClientComboBox.SelectedItem is Client client)
            {
                ReportListBox.Items.Clear();
                foreach (var project in client.Projects)
                {
                    ReportListBox.Items.Add($"Project: {project.Name} (Total: {project.TotalAmount})");
                    foreach (var invoice in project.Invoices)
                    {
                        ReportListBox.Items.Add($"  Invoice: {invoice.Date.ToShortDateString()} Amount: {invoice.Amount} Paid: {invoice.Paid}");
                    }
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && (tb.Text == "Client Name" || tb.Text == "Project Name" || tb.Text == "Invoice Amount"))
                tb.Text = "";
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                // Restore default placeholder text based on the TextBox name
                switch (tb.Name)
                {
                    case "ClientNameTextBox":
                        tb.Text = "Client Name";
                        break;
                    case "ClientEmailTextBox":
                        tb.Text = "Client Email";
                        break;
                    case "ProjectNameTextBox":
                        tb.Text = "Project Name";
                        break;
                    case "ProjectAmountTextBox":
                        tb.Text = "Total Amount";
                        break;
                    case "InvoiceAmountTextBox":
                        tb.Text = "Invoice Amount";
                        break;
                }
            }
        }
        private void ClearAllData_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear all clients, projects, and invoices?",
                                         "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _dataService.Clients.Clear();
                _dataService.Save();
                MessageBox.Show("All data cleared successfully!");

                // Optional: refresh UI elements like ComboBoxes or ListViews
                ClientComboBox.ItemsSource = null;
                ProjectComboBox.ItemsSource = null;
                ClientComboBox.ItemsSource = _dataService.Clients;
            }
        }

    }
}
