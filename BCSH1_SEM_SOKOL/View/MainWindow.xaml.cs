using BCSH1_SEM_SOKOL.ViewModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BCSH1_SEM_SOKOL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel viewModel = new MainWindowViewModel();

            DataContext = viewModel;
        }

        private void XY_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                int number;
                if (!int.TryParse(textBox.Text, out number) || number < -200 || number > 200)
                {
                    MessageBox.Show("Zadávejte čísla v rozsahu od -200 do 200.");
                    textBox.Text = "0";
                }
            }
        }

        private void Population_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                int number;
                if (!int.TryParse(textBox.Text, out number) || number < 0)
                {
                    MessageBox.Show("Populace musí být větší nebo rovna 0.");
                    textBox.Text = "0";
                }
            }
        }
    }
}