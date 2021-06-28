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
    public partial class MinigameElastic : UserControl
    {
        public event ChangeUCEvent ChangeUCClick;
        bool onlyCheck;
        int scorePerc;

        public MinigameElastic()
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
                ChangeUCClick?.Invoke(UCType.Main, 10, scorePerc);
                return;
            }

            var total = 5;
            var cnt = 0;
            var correctBrush = new SolidColorBrush(Color.FromRgb(0, 200, 0));
            var wrongBrush = new SolidColorBrush(Color.FromRgb(200, 0, 0));

            radioButton12.Foreground = correctBrush;
            radioButton21.Foreground = correctBrush;
            radioButton32.Foreground = correctBrush;
            radioButton43.Foreground = correctBrush;
            radioButton51.Foreground = correctBrush;

            foreach (var item in radioButtonPanel.Children)
            {
                if (item is WrapPanel panel)
                {
                    foreach (var itemPanel in panel.Children)
                    {
                        if (itemPanel is RadioButton button)
                        {
                            if ((bool)button.IsChecked && button.Foreground != correctBrush)
                                button.Foreground = wrongBrush;
                            else if ((bool)button.IsChecked && button.Foreground == correctBrush)
                                cnt++;
                        }
                    }
                }
            }

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
