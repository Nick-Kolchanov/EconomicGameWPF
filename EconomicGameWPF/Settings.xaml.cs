using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Threading;

namespace EconomicGameWPF
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private readonly Menu parentWindow;

        public Settings(object parent)
        {
            InitializeComponent();
            Loaded += Settings_Loaded;
            Closing += Settings_Closing;
            parentWindow = parent as Menu;
        }

        private void Settings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(Application.Current.Resources["SettingsDir"].ToString(), false))
            {
                var text = "";
                if ((bool)MusicCheckBox.IsChecked)
                    text += '1';
                else
                    text += '0';

                if ((bool)ThemeCheckBox.IsChecked)
                    text += '1';
                else
                    text += '0';

                if ((bool)EasyModeCheckBox.IsChecked)
                    text += '1';
                else
                    text += '0';

                sw.WriteLine(text);
            }
        }

        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            using (StreamReader sr = new StreamReader(Application.Current.Resources["SettingsDir"].ToString()))
            {
                var text = sr.ReadToEnd();
                if (text[0] == '1')
                    MusicCheckBox.IsChecked = true;
                else
                    MusicCheckBox.IsChecked = false;

                if (text[1] == '1')
                    ThemeCheckBox.IsChecked = true;
                else
                    ThemeCheckBox.IsChecked = false;

                if (text[2] == '1')
                    EasyModeCheckBox.IsChecked = true;
                else
                    EasyModeCheckBox.IsChecked = false;
            }
        }


        private void MusicCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            parentWindow.soundPlayer.PlayLooping();
        }

        private void MusicCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            parentWindow.soundPlayer.Stop();
        }

        private void ThemeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Извините, это еще находится в разработке");
        }

        private void EasyModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Теперь для продвижения по уровням не нужно набирать более 50% правильных ответов.");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
