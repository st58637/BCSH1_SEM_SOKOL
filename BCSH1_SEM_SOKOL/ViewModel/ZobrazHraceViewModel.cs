using BCSH1_SEM_SOKOL.Model;
using BCSH1_SEM_SOKOL.ViewModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

public class ZobrazHraceViewModel : INotifyPropertyChanged
{
    private Hrac _hrac;

    public Hrac Hrac
    {
        get { return _hrac; }
        set
        {
            _hrac = value;
            OnPropertyChanged(nameof(Hrac));
        }
    }
    private int _pocetVesnicHrace;
    public int PocetVesnicHrace
    {
        get { return _pocetVesnicHrace; }
        set
        {
            if (_pocetVesnicHrace != value)
            {
                _pocetVesnicHrace = value;
                OnPropertyChanged(nameof(PocetVesnicHrace));
            }
        }
    }

    private int _celkovaPopulaceHrace;
    public int CelkovaPopulaceHrace
    {
        get { return _celkovaPopulaceHrace; }
        set
        {
            if (_celkovaPopulaceHrace != value)
            {
                _celkovaPopulaceHrace = value;
                OnPropertyChanged(nameof(CelkovaPopulaceHrace));
            }
        }
    }

    private Vesnice _vybranaVesnice;

    public Vesnice VybranaVesnice
    {
        get { return _vybranaVesnice; }
        set
        {
            _vybranaVesnice = value;
            OnPropertyChanged(nameof(VybranaVesnice));
        }
    }
    public ObservableCollection<Vesnice> VesniceHrace { get; } = new ObservableCollection<Vesnice>();

    private MainWindowViewModel _mainWindowViewModel;   
    public RelayCommand ZavritCommand { get; }
    public RelayCommand OdebratCommand { get; }

    public ZobrazHraceViewModel(Hrac hrac, MainWindowViewModel mainWindowViewModel)
    {
        Hrac = hrac;

        _mainWindowViewModel = mainWindowViewModel;

        ZavritCommand = new RelayCommand(Zavrit);

        OdebratCommand = new RelayCommand(OdebratVesnici);

        foreach (var vesnice in _hrac.Vesnice)
        {
            VesniceHrace.Add(vesnice);
        }

        PocetVesnicHrace = _hrac.Vesnice.Count;

        CelkovaPopulaceHrace = _hrac.Vesnice.Sum(v => v.Populace);
    }

    private void Zavrit()
    {
        Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
        window.Close();
    }

    private void OdebratVesnici()
    {
        if (VybranaVesnice == null)
        {
            MessageBox.Show("Nebyla vybrána žádná vesnice k odebrání!");
            return;
        }

        _hrac.Vesnice.Remove(VybranaVesnice);

        _mainWindowViewModel._vesnice.Remove(VybranaVesnice);

        _mainWindowViewModel.AktualizujKolekce();

        AktualizujOkno();
    }

    private void AktualizujOkno()
    {
        VesniceHrace.Clear();

        foreach (var vesnice in _hrac.Vesnice)
        {
            VesniceHrace.Add(vesnice);
        }

        PocetVesnicHrace = _hrac.Vesnice.Count;

        CelkovaPopulaceHrace = _hrac.Vesnice.Sum(v => v.Populace);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}