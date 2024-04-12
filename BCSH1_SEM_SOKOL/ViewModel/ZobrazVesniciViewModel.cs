using BCSH1_SEM_SOKOL.Model;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;

public class ZobrazVesniciViewModel : INotifyPropertyChanged
{
    private Vesnice _vesnice;

    public Vesnice Vesnice
    {
        get { return _vesnice; }
        set
        {
            _vesnice = value;
            OnPropertyChanged(nameof(Vesnice));
        }
    }
    public RelayCommand ZavritCommand { get; }

    public ZobrazVesniciViewModel(Vesnice vesnice)
    {
        Vesnice = vesnice;

        ZavritCommand = new RelayCommand(Zavrit);
    }

    private void Zavrit()
    {
        Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
        window.Close();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}