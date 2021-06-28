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
        bool onlyCheck;
        int scorePerc;

        public MinigameMarket()
        {
            InitializeComponent();
            taskBox.Visibility = Visibility.Collapsed;
            readyButton.Content = "Проверить";
            onlyCheck = true;
            scorePerc = 0;
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
            if (!onlyCheck)
            {
                ChangeUCClick?.Invoke(UCType.Main, 8, scorePerc);
                return;
            }

            var total = 7;
            var cnt = 0;
            var correctBrush = new SolidColorBrush(Color.FromRgb(0, 200, 0));
            var wrongBrush = new SolidColorBrush(Color.FromRgb(200, 0, 0));

            if ((demand.SelectedItem as TextBlock)?.Text == "D")
            {
                cnt++;
                ((TextBlock)demand.SelectedItem).Foreground = correctBrush;
            }
            else
                ((TextBlock)demand.SelectedItem).Foreground = wrongBrush;

            if ((income.SelectedItem as TextBlock)?.Text == "I")
            {
                cnt++;
                ((TextBlock)income.SelectedItem).Foreground = correctBrush;
            }
            else
                ((TextBlock)income.SelectedItem).Foreground = wrongBrush;

            if ((quantDem.SelectedItem as TextBlock)?.Text == "Q(d)")
            {
                cnt++;
                ((TextBlock)quantDem.SelectedItem).Foreground = correctBrush;
            }
            else
                ((TextBlock)quantDem.SelectedItem).Foreground = wrongBrush;

            if ((supply.SelectedItem as TextBlock)?.Text == "S")
            {
                cnt++;
                ((TextBlock)supply.SelectedItem).Foreground = correctBrush;
            }
            else
                ((TextBlock)supply.SelectedItem).Foreground = wrongBrush;

            if ((price.SelectedItem as TextBlock)?.Text == "P")
            {
                cnt++;
                ((TextBlock)price.SelectedItem).Foreground = correctBrush;
            }
            else
                ((TextBlock)price.SelectedItem).Foreground = wrongBrush;

            if ((tr.SelectedItem as TextBlock)?.Text == "TR")
            {
                cnt++;
                ((TextBlock)tr.SelectedItem).Foreground = correctBrush;
            }
            else
                ((TextBlock)tr.SelectedItem).Foreground = wrongBrush;

            if ((quantSup.SelectedItem as TextBlock)?.Text == "Q(s)")
            {
                cnt++;
                ((TextBlock)quantSup.SelectedItem).Foreground = correctBrush;
            }
            else
                ((TextBlock)quantSup.SelectedItem).Foreground = wrongBrush;


            if (cnt == total)
                MessageBox.Show($"Ваш результат = {cnt} из {total}. Отлично, молодец!");
            else if (cnt > Math.Ceiling(total / 2f))
                MessageBox.Show($"Ваш результат = {cnt} из {total}. Неплохо");
            else
                MessageBox.Show($"Ваш результат = {cnt} из {total}.");

            scorePerc = 100 * cnt / total;
            readyButton.Content = "К следующим заданиям";
            onlyCheck = false;
        }
    }
}
