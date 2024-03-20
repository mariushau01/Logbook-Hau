using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logbook.Lib;
using LogBook.LogBookCore.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogBook.LogBookCore.ViewModels
{
    public partial class ReportViewModel : ObservableObject
    {
        IRepository _repository;
        public ReportViewModel(IRepository repository)
        {
            _repository = repository;

            WeakReferenceMessenger.Default.Register<AddMessage>(this, (r, m) =>
            {
                // m.Value: unser Entry-Objekt
                System.Diagnostics.Debug.WriteLine(m.Value);

                //add to list
                this.Entries.Add(m.Value);
            });
        }

        private bool _isloaded = false;
        [ObservableProperty]
        ObservableCollection<Logbook.Lib.Entry> _entries = [];

        [RelayCommand]
        void LoadData()
        {
            if (!_isloaded)
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
                        _isloaded = true;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // naja: Entries.Clear();

        }
    }
}
