using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logbook.Lib;
using LogBook.LogBookCore.Messages;
using LogBook.LogBookCore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.LogBookCore.ViewModel
{
    public partial class MainViewModel(IRepository repository, IAlertService alertService) : ObservableObject
    {
       
        public string Header => "Fahrtenbuch";

        IRepository _repository = repository;
        IAlertService _alertService = alertService;

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
        void Delete(Lib.Entry entry)
        {
            Lib.Entry entryToDelete = _repository.find(entry.Id);

            if (entryToDelete != null)
            {
                var res = _repository.Delete(entryToDelete);

                if (res)
                {
                    this.SelectedEntry = null;
                    this.Entries.Remove(entry);
                    _alertService.ShowAlert("Erfolg", "Der Eintrag wurde gelöscht");
                }
                else
                {
                    // alert not possible to delete from repository
                    _alertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gelöscht werden");
                }
            }
            else
            {
                // alert entrý not found
                _alertService.ShowAlert("Fehler", "Der Eintrag konnte nicht gefunden werden");
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

                WeakReferenceMessenger.Default.Send(new AddMessage(entry));
            }

        }
    }
}
