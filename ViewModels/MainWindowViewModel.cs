using GalaSoft.MvvmLight.Command;
using ManagementApp.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ManagementApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public string ConnectionsString = "Server=(localdb)\\local;" +
        "                               Database=BD1d2b_2022;" +
        "                               Trusted_Connection=True;";

        private ObservableCollection<Dostawcy> _dostawcy = new ObservableCollection<Dostawcy>();
        public ObservableCollection<Dostawcy> Dostawcy
        {
            get => _dostawcy;
            set => this.RaiseAndSetIfChanged(ref _dostawcy, value);
        }

        private ObservableCollection<Kategorie> _kategorie = new ObservableCollection<Kategorie>();
        public ObservableCollection<Kategorie> Kategorie
        {
            get => _kategorie;
            set => this.RaiseAndSetIfChanged(ref _kategorie, value);
        }

        private ObservableCollection<Klienci> _klienci = new ObservableCollection<Klienci>();
        public ObservableCollection<Klienci> Klienci
        {
            get => _klienci;
            set => this.RaiseAndSetIfChanged(ref _klienci, value);
        }

        private ObservableCollection<PozycjeZamówienia> _pozycje = new ObservableCollection<PozycjeZamówienia>();
        public ObservableCollection<PozycjeZamówienia> Pozycje
        {
            get => _pozycje;
            set => this.RaiseAndSetIfChanged(ref _pozycje, value);
        }

        private ObservableCollection<Pracownicy> _pracownicy = new ObservableCollection<Pracownicy>();
        public ObservableCollection<Pracownicy> Pracownicy
        {
            get => _pracownicy;
            set => this.RaiseAndSetIfChanged(ref _pracownicy, value);
        }

        private ObservableCollection<Produkty> _produkty = new ObservableCollection<Produkty>();
        public ObservableCollection<Produkty> Produkty
        {
            get => _produkty;
            set => this.RaiseAndSetIfChanged(ref _produkty, value);
        }

        private ObservableCollection<Spedytorzy> _spedytorzy = new ObservableCollection<Spedytorzy>();
        public ObservableCollection<Spedytorzy> Spedytorzy
        {
            get => _spedytorzy;
            set => this.RaiseAndSetIfChanged(ref _spedytorzy, value);
        }

        private ObservableCollection<Zamówienia> _zamówienia = new ObservableCollection<Zamówienia>();
        public ObservableCollection<Zamówienia> Zamówienia
        {
            get => _zamówienia;
            set => this.RaiseAndSetIfChanged(ref _zamówienia, value);
        }

        private ObservableCollection<object> _currentTable;
        public ObservableCollection<object> CurrentTable
        {
            get => _currentTable;
            set => this.RaiseAndSetIfChanged(ref _currentTable, value);
        }

        public ICommand ChangeFocusCommand { get; }

        private void ChangeFocus(string tableIndexString)
        {
            if (int.TryParse(tableIndexString, out int tableIndex))
            {
                if (tableIndex >= 0 && tableIndex < Tables.Count)
                {
                    Type objectType = Tables[tableIndex].GetType().GetGenericArguments()[0];
                    Type collectionType = typeof(ObservableCollection<>).MakeGenericType(objectType);
                    object currentTable = Activator.CreateInstance(collectionType);
                    CurrentTable = (ObservableCollection<object>)currentTable;
                }
            }
        }


        public ObservableCollection<object> Tables { get; }
        public ObservableCollection<string> TableNames { get; }

        public MainWindowViewModel()
        {
            Tables = new ObservableCollection<object>();
            TableNames = new ObservableCollection<string>();

            Tables.Clear();
            TableNames.Clear();
            Dostawcy = new ObservableCollection<Dostawcy>(ManagementModelManipulation.ReadDataFromTable<Dostawcy>("Dostawcy", ConnectionsString));
            Tables.Add(Dostawcy);
            TableNames.Add("Dostawcy");
            Kategorie = new ObservableCollection<Kategorie>(ManagementModelManipulation.ReadDataFromTable<Kategorie>("Kategorie", ConnectionsString));
            Tables.Add(Kategorie);
            TableNames.Add("Kategorie");
            Klienci = new ObservableCollection<Klienci>(ManagementModelManipulation.ReadDataFromTable<Klienci>("Klienci", ConnectionsString));
            Tables.Add(Klienci);
            TableNames.Add("Klienci");
            Pozycje = new ObservableCollection<PozycjeZamówienia>(ManagementModelManipulation.ReadDataFromTable<PozycjeZamówienia>("PozycjeZamówienia", ConnectionsString));
            Tables.Add(Pozycje);
            TableNames.Add("PozycjeZamówienia");
            Pracownicy = new ObservableCollection<Pracownicy>(ManagementModelManipulation.ReadDataFromTable<Pracownicy>("Pracownicy", ConnectionsString));
            Tables.Add(Pracownicy);
            TableNames.Add("Pracownicy");
            Produkty = new ObservableCollection<Produkty>(ManagementModelManipulation.ReadDataFromTable<Produkty>("Produkty", ConnectionsString));
            Tables.Add(Produkty);
            TableNames.Add("Produkty");
            Spedytorzy = new ObservableCollection<Spedytorzy>(ManagementModelManipulation.ReadDataFromTable<Spedytorzy>("Spedytorzy", ConnectionsString));
            Tables.Add(Spedytorzy);
            TableNames.Add("Spedytorzy");
            Zamówienia = new ObservableCollection<Zamówienia>(ManagementModelManipulation.ReadDataFromTable<Zamówienia>("Zamówienia", ConnectionsString));
            Tables.Add(Zamówienia);
            TableNames.Add("Zamówienia");
            ChangeFocusCommand = new RelayCommand<string>(ChangeFocus);
        }

    }
}