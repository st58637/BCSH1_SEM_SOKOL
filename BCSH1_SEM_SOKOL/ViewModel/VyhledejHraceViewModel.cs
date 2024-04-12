﻿using BCSH1_SEM_SOKOL.Model;
using BCSH1_SEM_SOKOL.ViewModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Media;

public class VyhledejHraceViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Vesnice> VyfiltrovaneVesnice { get; }

    public RelayCommand UlozCommand { get; }

    public VyhledejHraceViewModel(List<Vesnice> vesnice)
    {
        VyfiltrovaneVesnice = new ObservableCollection<Vesnice>(vesnice);

        UlozCommand = new RelayCommand(Uloz);
    }

    private void Zavrit()
    {
        Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
        window.Close();
    }
    private void Uloz()
    {
        int pocetVesnic = 0;

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        saveFileDialog.Title = "Uložit seznam vesnic";

        if (saveFileDialog.ShowDialog() == true)
        {
            using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
            {
                foreach (Vesnice vesnice in VyfiltrovaneVesnice)
                {
                    string url = $"https://ts31.x3.international.travian.com/karte.php?x={vesnice.X}&y={vesnice.Y}";
                    writer.WriteLine(url);
                    pocetVesnic++;
                }
            }
        }

        MessageBox.Show($"Do souboru bylo uloženo {pocetVesnic} odkazů na vesnice.");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}