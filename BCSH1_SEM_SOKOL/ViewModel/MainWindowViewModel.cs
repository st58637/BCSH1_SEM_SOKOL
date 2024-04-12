using BCSH1_SEM_SOKOL.Model;
using BCSH1_SEM_SOKOL.View;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BCSH1_SEM_SOKOL.ViewModel
{

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public List<Hrac> _hraci { get; set; }
        public List<Vesnice> _vesnice { get; set; }
        public List<Aliance> _aliance { get; set; }
        public List<Narod> _narody { get; set; }
        public ObservableCollection<Aliance> _alianceComboBox { get; set; }

        public ObservableCollection<Aliance> _alianceDataGrid { get; set; }

        public ObservableCollection<Narod> _narodyComboBox { get; set; }

        public ObservableCollection<Hrac> _hraciDataGrid { get; set; }

        public ObservableCollection<Vesnice> _vesniceDataGrid { get; set; }

        public RelayCommand PridatCommand { get; }
        public RelayCommand UpravitCommand { get; }
        public RelayCommand OdebratCommand { get; }
        public RelayCommand ZobrazCommand { get; }
        public RelayCommand VyhledatCommand { get; }

        public RelayCommand PridatHraceCommand { get; }
        public RelayCommand UpravitHraceCommand { get; }
        public RelayCommand OdebratHraceCommand { get; }
        public RelayCommand ZobrazHraceCommand { get; }

        public RelayCommand PridatAlianciCommand { get; }
        public RelayCommand UpravitAlianciCommand { get; }
        public RelayCommand OdebratAlianciCommand { get; }
        public RelayCommand ZobrazAlianciCommand { get; }


        private Vesnice _vybranaVesnice;

        private string _jmeno;

        public string Jmeno { get; set; }

        public Vesnice VybranaVesnice
        {
            get { return _vybranaVesnice; }
            set
            {
                _vybranaVesnice = value;
                OnPropertyChanged(nameof(VybranaVesnice));

                if (_vybranaVesnice != null)
                {
                    JmenoVesnice = _vybranaVesnice.Jmeno;
                    X = _vybranaVesnice.X;
                    Y = _vybranaVesnice.Y;
                    VybranyHracID = _vybranaVesnice.Vlastnik.ID;
                    Populace = _vybranaVesnice.Populace;
                }
            }
        }

        private Hrac _vybranyHrac;

        public Hrac VybranyHrac
        {
            get { return _vybranyHrac; }
            set
            {
                _vybranyHrac = value;
                OnPropertyChanged(nameof(VybranyHrac));

                if (_vybranyHrac != null)
                {
                    JmenoHrace = _vybranyHrac.Jmeno;
                    VybranaAlianceID = _vybranyHrac.Aliance?.ID;
                    VybranyNarod = _vybranyHrac.Narod;
                }
            }
        }

        private Aliance _vybranaAliance;

        public Aliance VybranaAliance
        {
            get { return _vybranaAliance; }
            set
            {
                _vybranaAliance = value;
                OnPropertyChanged(nameof(VybranaAliance));

                if (_vybranaAliance != null)
                {
                    JmenoAliance = VybranaAliance.Zkratka;
                }
            }
        }

        private string _jmenoVesnice;
        public string JmenoVesnice
        {
            get { return _jmenoVesnice; }
            set
            {
                _jmenoVesnice = value;
                OnPropertyChanged(nameof(JmenoVesnice));
            }
        }

        private string _jmenoHrace;
        public string JmenoHrace
        {
            get { return _jmenoHrace; }
            set
            {
                _jmenoHrace = value;
                OnPropertyChanged(nameof(JmenoHrace));
            }
        }

        private string _jmenoAliance;
        public string JmenoAliance
        {
            get { return _jmenoAliance; }
            set
            {
                _jmenoAliance = value;
                OnPropertyChanged(nameof(JmenoAliance));
            }
        }

        private int _x;
        public int X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        private int _y;
        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        private int _populace;
        public int Populace
        {
            get { return _populace; }
            set
            {
                _populace = value;
                OnPropertyChanged(nameof(Populace));
            }
        }

        private int _vybranyHracID;
        public int VybranyHracID
        {
            get { return _vybranyHracID; }
            set
            {
                _vybranyHracID = value;
                OnPropertyChanged(nameof(VybranyHracID));
            }
        }

        private int? _vybranaAlianceID;
        public int? VybranaAlianceID
        {
            get { return _vybranaAlianceID; }
            set
            {
                _vybranaAlianceID = value;
                OnPropertyChanged(nameof(VybranaAlianceID));
            }
        }

        private Narod _vybranyNarod;
        public Narod VybranyNarod
        {
            get { return _vybranyNarod; }
            set
            {
                _vybranyNarod = value;
                OnPropertyChanged(nameof(VybranyNarod));
            }
        }

        private int _minPopulaceHrace;
        public int MinPopulaceHrace
        {
            get { return _minPopulaceHrace; }
            set
            {
                _minPopulaceHrace = value;
                OnPropertyChanged(nameof(MinPopulaceHrace));
            }
        }

        private int _maxPopulaceHrace;
        public int MaxPopulaceHrace
        {
            get { return _maxPopulaceHrace; }
            set
            {
                _maxPopulaceHrace = value;
                OnPropertyChanged(nameof(MaxPopulaceHrace));
            }
        }

        private int _minPopulaceVesnice;
        public int MinPopulaceVesnice
        {
            get { return _minPopulaceVesnice; }
            set
            {
                _minPopulaceVesnice = value;
                OnPropertyChanged(nameof(MinPopulaceVesnice));
            }
        }

        private int _maxPopulaceVesnice;
        public int MaxPopulaceVesnice
        {
            get { return _maxPopulaceVesnice; }
            set
            {
                _maxPopulaceVesnice = value;
                OnPropertyChanged(nameof(MaxPopulaceVesnice));
            }
        }

        private bool _riman;
        public bool Riman
        {
            get { return _riman; }
            set
            {
                _riman = value;
                OnPropertyChanged(nameof(Riman));
            }
        }

        private bool _gal;
        public bool Gal
        {
            get { return _gal; }
            set
            {
                _gal = value;
                OnPropertyChanged(nameof(Gal));
            }
        }

        private bool _german;
        public bool German
        {
            get { return _german; }
            set
            {
                _german = value;
                OnPropertyChanged(nameof(German));
            }
        }

        private bool _spartan;
        public bool Spartan
        {
            get { return _spartan; }
            set
            {
                _spartan = value;
                OnPropertyChanged(nameof(Spartan));
            }
        }

        private bool _egyptan;
        public bool Egyptan
        {
            get { return _egyptan; }
            set
            {
                _egyptan = value;
                OnPropertyChanged(nameof(Egyptan));
            }
        }

        private bool _hun;
        public bool Hun
        {
            get { return _hun; }
            set
            {
                _hun = value;
                OnPropertyChanged(nameof(Hun));
            }
        }

        private bool _natari;
        public bool Natari
        {
            get { return _natari; }
            set
            {
                _natari = value;
                OnPropertyChanged(nameof(Natari));
            }
        }

        public MainWindowViewModel()
        {
            _hraci = new List<Hrac>();
            _aliance = new List<Aliance>();
            _aliance.Add(new Aliance(null, ""));
            _vesnice = new List<Vesnice>();
            _narody = Enum.GetValues(typeof(Narod)).Cast<Narod>().ToList();

            NactiZeSouboru();

            _alianceDataGrid = new ObservableCollection<Aliance>(_aliance.Where(a => a.ID != null));

            _alianceComboBox = new ObservableCollection<Aliance>(_aliance);

            _narodyComboBox = new ObservableCollection<Narod>(_narody.Where(n => n != Narod.Priroda && n != Narod.Natari));

            _hraciDataGrid = new ObservableCollection<Hrac>(_hraci);
            
            _vesniceDataGrid = new ObservableCollection<Vesnice>(_vesnice);

            PridatCommand = new RelayCommand(PridatVesnici);
            UpravitCommand = new RelayCommand(UpravitVesnici);
            OdebratCommand = new RelayCommand(OdebratVesnici);
            ZobrazCommand = new RelayCommand(ZobrazVesnici);
            VyhledatCommand = new RelayCommand(VyhledatHrace);

            PridatHraceCommand = new RelayCommand(PridatHrace);
            UpravitHraceCommand = new RelayCommand(UpravitHrace);
            OdebratHraceCommand = new RelayCommand(OdebratHrace);
            ZobrazHraceCommand = new RelayCommand(ZobrazHrace);

            PridatAlianciCommand = new RelayCommand(PridatAlianci);
            UpravitAlianciCommand = new RelayCommand(UpravitAlianci);
            OdebratAlianciCommand = new RelayCommand(OdebratAlianci);
            ZobrazAlianciCommand = new RelayCommand(ZobrazAlianci);
        }

        public void NactiZeSouboru()
        {
            string cesta = "map.sql";
            using (StreamReader reader = new StreamReader(cesta))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    string poleID = data[0];
                    int x = int.Parse(data[1]);
                    int y = int.Parse(data[2]);
                    Narod narod = (Narod)int.Parse(data[3]);
                    int vesniceID = int.Parse(data[4]);
                    string vesniceJmeno = "";

                    int pocetSlovVNazvuVesnice = data.Length - 15;

                    if (pocetSlovVNazvuVesnice > 1)
                    {
                        for (int i = 5; i < 5 + pocetSlovVNazvuVesnice; i++)
                        {
                            vesniceJmeno += data[i].Trim('\'');
                            if (i != 5 + pocetSlovVNazvuVesnice - 1)
                                vesniceJmeno += ",";
                        }
                    }
                    else
                    {
                        vesniceJmeno = data[5].Trim('\'');
                    }

                    int hracID = int.Parse(data[pocetSlovVNazvuVesnice + 5]);
                    string hracJmeno = data[pocetSlovVNazvuVesnice + 6].Trim('\'');
                    int? alianceID = string.IsNullOrEmpty(data[pocetSlovVNazvuVesnice + 7]) ? null : (int?)int.Parse(data[pocetSlovVNazvuVesnice + 7]);
                    string alianceZkratka = data[pocetSlovVNazvuVesnice + 8].Trim('\'');
                    int populace = int.Parse(data[pocetSlovVNazvuVesnice + 9]);

                    Hrac hrac = _hraci.Find(p => p.ID == hracID);
                    
                    if (hrac == null)
                    {
                        hrac = new Hrac(hracID, hracJmeno, narod);
                        _hraci.Add(hrac);
                    }

                    Vesnice vesnice = new Vesnice(vesniceID, x, y, vesniceJmeno, hrac, populace);
                    _vesnice.Add(vesnice);
                    hrac.Vesnice.Add(vesnice);

                    if (alianceID.HasValue && alianceID != 0)
                    {
                        Aliance aliance = _aliance.Find(a => a.ID == alianceID.Value);
                        if (aliance == null)
                        {
                            aliance = new Aliance(alianceID.Value, alianceZkratka);
                            _aliance.Add(aliance);
                        }
                       
                        if (!aliance.Hraci.Contains(hrac))
                        {
                            aliance.Hraci.Add(hrac);
                            hrac.Aliance = aliance;
                        }
                    }  
                }
            }
        }

        private int GenerujID()
        {
            Random random = new Random();
            int noveID;

            do
            {
                noveID = random.Next(1, 100000);
            } while (_vesnice.Any(v => v.ID == noveID));

            return noveID;
        }

        private int GenerujIdHrace()
        {
            Random random = new Random();
            int noveID;

            do
            {
                noveID = random.Next(1, 20000);
            } while (_hraci.Any(v => v.ID == noveID));

            return noveID;
        }

        private int GenerujIdAliance()
        {
            Random random = new Random();
            int noveID;

            do
            {
                noveID = random.Next(1, 1000);
            } while (_aliance.Any(v => v.ID == noveID));

            return noveID;
        }

        private void PridatVesnici()
        {
            if (_vesnice.Any(v => v.Vlastnik.ID == VybranyHracID && v.Jmeno == JmenoVesnice))
            {
                MessageBox.Show("Hráč již má vesnici se stejným jménem.");
                return;
            }

            if (X < -200 || X > 200 || Y < -200 || Y > 200)
            {
                MessageBox.Show("Souřadnice X a Y musí být v rozmezí -200 a 200.");
                return;
            }

            if (_vesnice.Any(v => v.X == X  && v.Y == Y))
            {
                MessageBox.Show("Na zadaných souřadnicích už je jiná vesnice.");
                return;
            }

            if (Populace < 0)
            {
                MessageBox.Show("Populace musí být větší nebo rovna nule.");
                return;
            }

            int noveID = GenerujID();

            Hrac hrac = _hraci.FirstOrDefault(h => h.ID == VybranyHracID);

            Vesnice novaVesnice = new Vesnice(noveID, X, Y, JmenoVesnice, hrac , Populace);
            _vesnice.Add(novaVesnice);
            hrac.Vesnice.Add(novaVesnice);
            MessageBox.Show("Nová vesnice byla úspěšně přidána.");

            AktualizujKolekce();
        }

        private void PridatHrace()
        {
            if (_hraci.Any(h => h.Jmeno == JmenoHrace))
            {
                MessageBox.Show("Hráč s tímto jménem již existuje!");
                return;
            }

            if (JmenoHrace == null)
            {
                MessageBox.Show("Musíte zadat jméno hráče!");
                return;
            }

            if (VybranyNarod == null)
            {
                MessageBox.Show("Musíte vybrat národ hráče!");
                return;
            }

            int idHrace = GenerujIdHrace();

            Narod narod = _narody.FirstOrDefault(n => n.ToString().Equals(VybranyNarod));

            Aliance? aliance = _aliance.FirstOrDefault(n => n.ID == VybranaAlianceID);

            Hrac hrac = new Hrac(GenerujIdHrace(), JmenoHrace, narod);

            hrac.Aliance = aliance;

            if (aliance != null && aliance.Hraci.Count != 60)
            {
                aliance.Hraci.Add(hrac);
            } else
            {
                MessageBox.Show("Vybraná aliance je momentálně plná!");
                return;
            }

            _hraci.Add(hrac);

            Random random = new Random();

            int x;

            int y;

            do
            {
                x = random.Next(-200, 200);  
                y = random.Next(-200, 200); 
            } while (_vesnice.Any(v => v.X == x && v.Y == y));

            string jmenoVesnice = JmenoHrace + "'s village";

            Vesnice vesnice = new Vesnice(GenerujID(), x, y, jmenoVesnice, hrac, 8);

            _vesnice.Add(vesnice);

            hrac.Vesnice.Add(vesnice);

            MessageBox.Show("Nový hráč byl úspěšně přidán.");

            AktualizujKolekce();
        }

        private void PridatAlianci()
        {
            if (JmenoAliance == null)
            {
                MessageBox.Show("Musíte zadat zkratku aliance!");
                return;
            }

            if (_aliance.Any(a => a.Zkratka == JmenoAliance))
            {
                MessageBox.Show("Aliance se stejnou zkratkou již existuje!");
                return;
            }

            _aliance.Add(new Aliance(GenerujIdAliance(), JmenoAliance));

            MessageBox.Show("Nová aliance byla úspěšně přidána.");

            AktualizujKolekce();
        }

        private void UpravitVesnici()
        {
            if (VybranaVesnice == null)
            {
                MessageBox.Show("Nebyla vybrána vesnice k úpravě.");
                return;
            }

            Vesnice vesnice = VybranaVesnice;

            if (_vesnice.Any(v => v.Vlastnik.ID == VybranyHracID && v.Jmeno == JmenoVesnice && v.ID != VybranaVesnice.ID))
            {
                MessageBox.Show("Hráč již má vesnici se stejným jménem.");
                return;
            }

            if (X < -200 || X > 200 || Y < -200 || Y > 200)
            {
                MessageBox.Show("Souřadnice X a Y musí být v rozmezí -200 a 200.");
                return;
            }

            if (_vesnice.Any(v => v.X == X && v.Y == Y && v.ID != VybranaVesnice.ID))
            {
                MessageBox.Show("Na zadaných souřadnicích už je jiná vesnice.");
                return;
            }

            if (Populace <= 0)
            {
                MessageBox.Show("Populace musí být větší než nula.");
                return;
            }

            Hrac novyMajitel = _hraci.FirstOrDefault(h => h.ID == VybranyHracID);
            
            if (novyMajitel == null)
            {
                MessageBox.Show("Nový majitel nebyl nalezen.");
                return;
            }

            vesnice.Vlastnik.Vesnice.Remove(vesnice);

            vesnice.Jmeno = JmenoVesnice;
            vesnice.X = X;
            vesnice.Y = Y;
            vesnice.Populace = Populace;
            vesnice.Vlastnik = novyMajitel;

            novyMajitel.Vesnice.Add(vesnice);

            MessageBox.Show("Vesnice byla úspěšně upravena.");

            AktualizujKolekce();
        }

        private void UpravitHrace()
        {
            if (VybranyHrac == null)
            {
                MessageBox.Show("Nebyl vybrán hráč k úpravě!");
                return;
            }

            Hrac hrac = VybranyHrac;

            if (!hrac.Jmeno.Equals(JmenoHrace))
            {
                if (_hraci.Any(h => h.Jmeno.Equals(JmenoHrace)))
                {
                    MessageBox.Show("Hráč se zadaným jménem již existuje!");
                    return;
                }
            }

            Aliance aliance = _aliance.FirstOrDefault(a => a.ID == VybranaAlianceID);
                
            if (aliance != null && aliance.Hraci.Count == 60)
            {
                MessageBox.Show("Vybraná aliance je momentálně plná!");
                return;
            }

            Aliance? puvodniAliance = hrac.Aliance;

            hrac.Jmeno = JmenoHrace;
            hrac.Narod = VybranyNarod;
            hrac.Aliance = aliance;

            if (aliance != null && puvodniAliance != aliance && puvodniAliance != null)
            {
                aliance.Hraci.Add(hrac);
                puvodniAliance.Hraci.Remove(hrac);
            }

            if (puvodniAliance == null && aliance != null)
            {
                aliance.Hraci.Add(hrac);
            }

            MessageBox.Show("Hráč byl úspěšně upraven.");

            AktualizujKolekce();
        }

        private void UpravitAlianci()
        {
            if (VybranaAliance == null)
            {
                MessageBox.Show("Nebyla vybrána aliance k úpravě!");
                return;
            }

            if (VybranaAliance.Zkratka.Equals(JmenoAliance))
            {
                return;
            }

            VybranaAliance.Zkratka = JmenoAliance;

            MessageBox.Show("Aliance byla úspěšně upravena.");

            AktualizujKolekce();
        }

        private void OdebratVesnici()
        {
            if (VybranaVesnice == null)
            {
                MessageBox.Show("Nebyla vybrána vesnice k odebrání.");
                return;
            }

            VybranaVesnice.Vlastnik.Vesnice.Remove(VybranaVesnice);

            _vesnice.Remove(VybranaVesnice);

            MessageBox.Show("Vesnice byla úspěšně odebrána.");

            AktualizujKolekce();
        }

        private void OdebratHrace()
        {
            if (VybranyHrac == null)
            {
                MessageBox.Show("Nebyl vybrán hráč k odebrání.");
                return;
            }

            Hrac hrac = VybranyHrac;

            if (hrac.Aliance != null)
            {
                hrac.Aliance.Hraci.Remove(hrac);
            }

            _vesnice.RemoveAll(vesnice => vesnice.Vlastnik == hrac);

            _hraci.Remove(hrac);

            MessageBox.Show("Hráč byl úspěšně odebrán.");

            AktualizujKolekce();
        }

        private void OdebratAlianci()
        {
            if (VybranaAliance == null)
            {
                MessageBox.Show("Nebyla vybrána aliance k odebrání!");
                return;
            }

            _aliance.Remove(VybranaAliance);

            foreach (var hrac in _hraci.Where(h => h.Aliance == VybranaAliance))
            {
                hrac.Aliance = null;
            }

            MessageBox.Show("Aliance byla úspěšně odebrána.");

            AktualizujKolekce();
        }

        private void ZobrazVesnici()
        {
            if (VybranaVesnice == null)
            {
                MessageBox.Show("Nebyla vybrána žádná vesnice!");
                return;
            }

            ZobrazVesniciViewModel viewModel = new ZobrazVesniciViewModel(VybranaVesnice);

            ZobrazVesniciWindow zobrazVesniciWindow = new ZobrazVesniciWindow();

            zobrazVesniciWindow.DataContext = viewModel;

            zobrazVesniciWindow.ShowDialog();
        }

        private void ZobrazHrace()
        {
            if (VybranyHrac == null)
            {
                MessageBox.Show("Nebyl vybrán žádný hráč!");
                return;
            }

            ZobrazHraceViewModel viewModel = new ZobrazHraceViewModel(VybranyHrac, this);

            ZobrazHraceWindow zobrazHraceWindow = new ZobrazHraceWindow();

            zobrazHraceWindow.DataContext = viewModel;

            zobrazHraceWindow.ShowDialog();
        }
        private void ZobrazAlianci()
        {
            if (VybranaAliance == null)
            {
                MessageBox.Show("Nebyla vybrána žádná aliance!");
                return;
            }

            ZobrazAlianciViewModel viewModel = new ZobrazAlianciViewModel(VybranaAliance, this);

            ZobrazAlianciWindow zobrazAlianciWindow = new ZobrazAlianciWindow();

            zobrazAlianciWindow.DataContext = viewModel;

            zobrazAlianciWindow.ShowDialog();
        }

        public List<Vesnice> FiltrujVesnice()
        {
            var filtrovaneVesnice = _vesnice.Where(vesnice =>
        vesnice.Populace >= MinPopulaceVesnice &&
        vesnice.Populace <= MaxPopulaceVesnice &&
        vesnice.Vlastnik != null &&
        (Riman && vesnice.Vlastnik.Narod == Narod.Riman ||
        Gal && vesnice.Vlastnik.Narod == Narod.Gal ||
        German && vesnice.Vlastnik.Narod == Narod.German ||
        Spartan && vesnice.Vlastnik.Narod == Narod.Spartan ||
        Egyptan && vesnice.Vlastnik.Narod == Narod.Egyptan ||
        Hun && vesnice.Vlastnik.Narod == Narod.Hun ||
        Natari && vesnice.Vlastnik.Narod == Narod.Natari) &&
        vesnice.Vlastnik.CelkovaPopulace >= MinPopulaceHrace &&
        vesnice.Vlastnik.CelkovaPopulace <= MaxPopulaceHrace).ToList();

            return filtrovaneVesnice;
        }

        public void VyhledatHrace()
        {
            VyhledejHraceViewModel viewModel = new VyhledejHraceViewModel(FiltrujVesnice());

            VyhledejHraceWindow vyhledejHraceWindow = new VyhledejHraceWindow();

            vyhledejHraceWindow.DataContext = viewModel;

            vyhledejHraceWindow.ShowDialog();
        }
        public void AktualizujKolekce()
        {
            _alianceDataGrid.Clear();
            foreach (var aliance in _aliance.Where(a => a.ID != null))
            {
                _alianceDataGrid.Add(aliance);
            }

            _alianceComboBox.Clear();
            foreach (var aliance in _aliance)
            {
                _alianceComboBox.Add(aliance);
            }

            _hraciDataGrid.Clear();
            foreach (var hraci in _hraci)
            {
                _hraciDataGrid.Add(hraci);
            }

            _vesniceDataGrid.Clear();
            foreach (var vesnice in _vesnice)
            {
                _vesniceDataGrid.Add(vesnice);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
