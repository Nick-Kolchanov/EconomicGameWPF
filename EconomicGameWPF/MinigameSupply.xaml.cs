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
    public partial class MinigameSupply : UserControl
    {
        public event ChangeUCEvent ChangeUCClick;

        public MinigameSupply()
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

            if ((bool)radioButton11.IsChecked)
                cnt++;
            if ((bool)radioButton22.IsChecked)
                cnt++;
            if ((bool)radioButton34.IsChecked)
                cnt++;
            if ((bool)radioButton41.IsChecked)
                cnt++;
            if ((bool)radioButton53.IsChecked)
                cnt++;

            if (cnt == 5)
                MessageBox.Show($"Ваш результат = {cnt} из 5. Отлично, молодец!");
            else if (cnt > 2)
                MessageBox.Show($"Ваш результат = {cnt} из 5. Неплохо");
            else
                MessageBox.Show($"Ваш результат = {cnt} из 5.");

            ChangeUCClick?.Invoke(UCType.Main, 6);
        }
    }
}
