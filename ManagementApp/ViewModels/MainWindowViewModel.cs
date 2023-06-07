using GalaSoft.MvvmLight.Command;
using ManagementApp.Models;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ManagementApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string ConnectionsString = "Server=(localdb)\\local;" +
"                               Database=BD1d2b_2022;" +
"                               Trusted_Connection=True;";

        private ObservableCollection<Dostawcy> _dostawcy;
        public ObservableCollection<Dostawcy> Dostawcy
        {
            get => _dostawcy;
            set => this.RaiseAndSetIfChanged(ref _dostawcy, value);
        }

        private ObservableCollection<Kategorie> _kategorie;
        public ObservableCollection<Kategorie> Kategorie
        {
            get => _kategorie;
            set => this.RaiseAndSetIfChanged(ref _kategorie, value);
        }

        private ObservableCollection<Klienci> _klienci;
        public ObservableCollection<Klienci> Klienci
        {
            get => _klienci;
            set => this.RaiseAndSetIfChanged(ref _klienci, value);
        }

        private ObservableCollection<PozycjeZamówienia> _pozycje;
        public ObservableCollection<PozycjeZamówienia> Pozycje
        {
            get => _pozycje;
            set => this.RaiseAndSetIfChanged(ref _pozycje, value);
        }

        private ObservableCollection<Pracownicy> _pracownicy;
        public ObservableCollection<Pracownicy> Pracownicy
        {
            get => _pracownicy;
            set => this.RaiseAndSetIfChanged(ref _pracownicy, value);
        }

        private ObservableCollection<Produkty> _produkty;
        public ObservableCollection<Produkty> Produkty
        {
            get => _produkty;
            set => this.RaiseAndSetIfChanged(ref _produkty, value);
        }

        private ObservableCollection<Spedytorzy> _spedytorzy;
        public ObservableCollection<Spedytorzy> Spedytorzy
        {
            get => _spedytorzy;
            set => this.RaiseAndSetIfChanged(ref _spedytorzy, value);
        }

        private ObservableCollection<Zamówienia> _zamówienia;
        public ObservableCollection<Zamówienia> Zamówienia
        {
            get => _zamówienia;
            set => this.RaiseAndSetIfChanged(ref _zamówienia, value);
        }


        public ObservableCollection<object> Tables { get; }
        public ObservableCollection<string> TableNames { get; }

        public ICommand ChangeFocusCommand { get; }


        private void ChangeFocus(string tableIndexString)
        {
            if (int.TryParse(tableIndexString, out int tableIndex))
            {
                //CurrentTable = (ObservableCollection)Tables[tableIndex];
            }
        }


        public MainWindowViewModel()
        {            Tables = new ObservableCollection<object>();
            TableNames = new ObservableCollection<string>();

            Tables.Clear();
            TableNames.Clear();
            Dostawcy = new ObservableCollection<Dostawcy>(ManagmentModelManipulation.ReadDataFromTable<Dostawcy>("Dostawcy", ConnectionsString));
            Tables.Add(Dostawcy);
            TableNames.Add("Dostawcy");
            Kategorie = new ObservableCollection<Kategorie>(ManagmentModelManipulation.ReadDataFromTable<Kategorie>("Kategorie", ConnectionsString));
            Tables.Add(Kategorie);
            TableNames.Add("Kategorie");
            Klienci = new ObservableCollection<Klienci>(ManagmentModelManipulation.ReadDataFromTable<Klienci>("Klienci", ConnectionsString));
            Tables.Add(Klienci);
            TableNames.Add("Klienci");
            Pozycje = new ObservableCollection<PozycjeZamówienia>(ManagmentModelManipulation.ReadDataFromTable<PozycjeZamówienia>("PozycjeZamówienia", ConnectionsString));
            Tables.Add(Pozycje);
            TableNames.Add("PozycjeZamówienia");
            Pracownicy = new ObservableCollection<Pracownicy>(ManagmentModelManipulation.ReadDataFromTable<Pracownicy>("Pracownicy", ConnectionsString));
            Tables.Add(Pracownicy);
            TableNames.Add("Pracownicy");
            Produkty = new ObservableCollection<Produkty>(ManagmentModelManipulation.ReadDataFromTable<Produkty>("Produkty", ConnectionsString));
            Tables.Add(Produkty);
            TableNames.Add("Produkty");
            Spedytorzy = new ObservableCollection<Spedytorzy>(ManagmentModelManipulation.ReadDataFromTable<Spedytorzy>("Spedytorzy", ConnectionsString));
            Tables.Add(Spedytorzy);
            TableNames.Add("Spedytorzy");
            Zamówienia = new ObservableCollection<Zamówienia>(ManagmentModelManipulation.ReadDataFromTable<Zamówienia>("Zamówienia", ConnectionsString));
            Tables.Add(Zamówienia);
            TableNames.Add("Zamówienia");
            ChangeFocusCommand = new RelayCommand<string>(ChangeFocus);
        }
    }
}