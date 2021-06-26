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
    public partial class MinigameMarket : UserControl
    {
        public event ChangeUCEvent ChangeUCClick;

        public MinigameMarket()
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

            if ((demand.SelectedItem as TextBlock)?.Text == "D")
                cnt++;
            if ((income.SelectedItem as TextBlock)?.Text == "I")
                cnt++;
            if ((quantDem.SelectedItem as TextBlock)?.Text == "Q(d)")
                cnt++;
            if ((supply.SelectedItem as TextBlock)?.Text == "S")
                cnt++;
            if ((price.SelectedItem as TextBlock)?.Text == "P")
                cnt++;
            if ((tr.SelectedItem as TextBlock)?.Text == "TR")
                cnt++;
            if ((quantSup.SelectedItem as TextBlock)?.Text == "Q(s)")
                cnt++;

            if (cnt == 7)
                MessageBox.Show($"Ваш результат = {cnt} из 7. Отлично, молодец!");
            else if (cnt > 4)
                MessageBox.Show($"Ваш результат = {cnt} из 7. Неплохо");
            else
                MessageBox.Show($"Ваш результат = {cnt} из 7.");

            ChangeUCClick?.Invoke(UCType.Main, 8);
        }
    }
}
