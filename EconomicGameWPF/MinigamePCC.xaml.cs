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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EconomicGameWPF
{
    /// <summary>
    /// Логика взаимодействия для Minigame1.xaml
    /// </summary>
    public partial class MinigamePCC : UserControl
    {
        public event ChangeUCEvent ChangeUCClick;

        public MinigamePCC()
        {
            InitializeComponent();
            taskBox.Visibility = Visibility.Collapsed;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeUCClick?.Invoke(UCType.Main);
        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            taskBox.Visibility = Visibility.Visible;
            taskButton.IsEnabled = false;
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            var cnt = 0;
            if ((bool)checkBox1.IsChecked)
                cnt++;
            if (!(bool)checkBox2.IsChecked)
                cnt++;
            if ((bool)checkBox3.IsChecked)
                cnt++;
            if (!(bool)checkBox4.IsChecked)
                cnt++;
            if (!(bool)checkBox5.IsChecked)
                cnt++;
            if ((bool)checkBox6.IsChecked)
                cnt++;
            if ((bool)checkBox7.IsChecked)
                cnt++;
            if (!(bool)checkBox8.IsChecked)
                cnt++;
            if ((bool)RadioButton21.IsChecked)
                cnt++;
            if ((bool)RadioButton32.IsChecked)
                cnt++;
            if ((bool)RadioButton23.IsChecked)
                cnt++;

            if (cnt == 11)
                MessageBox.Show($"Ваш результат = {cnt} из 11. Отлично, молодец!");
            else if (cnt > 7)
                MessageBox.Show($"Ваш результат = {cnt} из 11. Неплохо");
            else
                MessageBox.Show($"Ваш результат = {cnt} из 11.");

            ChangeUCClick?.Invoke(UCType.Main, 4);
        }
    }
}
