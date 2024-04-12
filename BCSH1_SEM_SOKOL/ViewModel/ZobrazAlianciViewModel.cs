using BCSH1_SEM_SOKOL.Model;
using BCSH1_SEM_SOKOL.ViewModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

public class ZobrazAlianciViewModel : INotifyPropertyChanged
{
    private Aliance _aliance;

    public Aliance Aliance
    {
        get { return _aliance; }
        set
        {
            _aliance = value;
            OnPropertyChanged(nameof(Aliance));
        }
    }
    private int _pocetHracuAliance;
    public int PocetHracuAliance
    {
        get { return _pocetHracuAliance; }
        set
        {
            if (_pocetHracuAliance != value)
            {
                _pocetHracuAliance = value;
                OnPropertyChanged(nameof(PocetHracuAliance));
            }
        }
    }

    private int _celkovaPopulaceAliance;
    public int CelkovaPopulaceAliance
    {
        get { return _celkovaPopulaceAliance; }
        set
        {
            if (_celkovaPopulaceAliance != value)
            {
                _celkovaPopulaceAliance = value;
                OnPropertyChanged(nameof(CelkovaPopulaceAliance));
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
        }
    }
    public ObservableCollection<Hrac> HraciAliance { get; } = new ObservableCollection<Hrac>();

    private MainWindowViewModel _mainWindowViewModel;
    public RelayCommand ZavritCommand { get; }
    public RelayCommand OdebratCommand { get; }

    public ZobrazAlianciViewModel(Aliance aliance, MainWindowViewModel mainWindowViewModel)
    {
        Aliance = aliance;

        _mainWindowViewModel = mainWindowViewModel;

        ZavritCommand = new RelayCommand(Zavrit);

        OdebratCommand = new RelayCommand(OdebratHrace);

        foreach (var hrac in _aliance.Hraci)
        {
            HraciAliance.Add(hrac);
        }

        PocetHracuAliance = _aliance.Hraci.Count;

        CelkovaPopulaceAliance = _aliance.Hraci.SelectMany(h => h.Vesnice).Sum(v => v.Populace);
    }

    private void Zavrit()
    {
        Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
        window.Close();
    }

    private void OdebratHrace()
    {
        if (VybranyHrac == null)
        {
            MessageBox.Show("Nebyl vybrán žádný hráč k odebrání!");
            return;
        }

        _aliance.Hraci.Remove(VybranyHrac);

        foreach (var vesnice in _mainWindowViewModel._vesnice.Where(v => v.Vlastnik == VybranyHrac).ToList())
        {
            _mainWindowViewModel._vesnice.Remove(vesnice);
        }

        _mainWindowViewModel._hraci.Remove(VybranyHrac);

        _mainWindowViewModel.AktualizujKolekce();

        AktualizujOkno();
    }

    private void AktualizujOkno()
    {
        HraciAliance.Clear();

        foreach (var hrac in _aliance.Hraci)
        {
            HraciAliance.Add(hrac);
        }

        PocetHracuAliance = _aliance.Hraci.Count;

        CelkovaPopulaceAliance = _aliance.Hraci.SelectMany(h => h.Vesnice).Sum(v => v.Populace);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}