using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logbook.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logbook.LogBookApp.ViewModel
{
    public partial class MainViewModel(IRepository repository) : ObservableObject
    {
        public string Header => "Fahrtenbuch";

        IRepository _repository = repository;

}
    }
}
