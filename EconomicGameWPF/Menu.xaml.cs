using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EconomicGameWPF
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public SoundPlayer soundPlayer;
        public Menu()
        {
            InitializeComponent();
            soundPlayer = new SoundPlayer();
            soundPlayer.SoundLocation = "../../Media/music.wav";
            soundPlayer.Load();
            soundPlayer.PlayLooping();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWindow = new Settings(this);
            settingsWindow.ShowDialog();
        }

        private void AuthorsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Колчанов Никита, Гончаров Сергей - студенты первого курса ПИ в ВШЭ. \n\nЗа основу взят курс ВШЭ \"Экономика для неэкономистов\".");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
