using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logbook.Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.LogBookApp.ViewModel
{
    public partial class MainViewModel(IRepository repository) : ObservableObject
    {
        public string Header => "Fahrtenbuch";

        IRepository _repository = repository;

        // nichts anderes wie eine Liste, mit einem zusätzlichen Feature, welches die Oberfläche informiert
        [ObservableProperty]
        ObservableCollection<Lib.Entry> _entries = [];

        [ObservableProperty]
        Lib.Entry _selectedEntry = null;

        [ObservableProperty]
        DateTime _start = DateTime.Now;

        [ObservableProperty]
        DateTime _end = DateTime.Now;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCommand))]
        string _description = string.Empty;

        [ObservableProperty]
        string _numberPlate = string.Empty;

        [ObservableProperty]
        int _startKM = 0;

        [ObservableProperty]
        int _endKM = 0;

        [ObservableProperty]
        string _from = string.Empty;

        [ObservableProperty]
        string _to = string.Empty;

        [RelayCommand]
        void LoadData()
        {
            try
            {
                var entries = _repository.GetAll();

                if (entries != null)
                {
                    foreach (var entry in entries)
                    {
                        Entries.Add(entry);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool CanAdd => this.Description.Length > 0;

        [RelayCommand(CanExecute = nameof(CanAdd))]
        void Add()
        {
            /*
            Lib.Entry entrySaalfelden = new(DateTime.Now.AddDays(3),
                                  DateTime.Now.AddDays(3).AddMinutes(20),
                                  25500,
                                  25514,
                                  "ZE-XY123",
                                  "Zell am See",
                                  "Saalfelden")
            {
                Description = "Fahrt nach Saalfelden"
            };
            */
            Lib.Entry entry = new(this.Start, this.End, this.StartKM, this.EndKM, this.NumberPlate, this.From, this.To);

            if (this.Description.Length > 0)
            {
                entry.Description = this.Description;
            }

            var result = _repository.Add(entry);
            if(result == true)
            {
                this.Entries.Add(entry);
                this.Description = string.Empty;
                this.From = "";
                this.To = "";
                this.StartKM = this.EndKM;
                this.EndKM = 0;
            }

        }
    }
}
