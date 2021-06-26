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
    public partial class MinigameOpportunityСosts : UserControl
    {
        public event ChangeUCEvent ChangeUCClick;

        public MinigameOpportunityСosts()
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton button)
            {
                if (button.Name == "buyWithBankRadioButton")
                {
                    rentTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    buyWithBankTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    buyYourselfTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(200, 30, 30));

                    resultTextBlock.Text = "Это самый дорогой вариант. Получится 500 000 руб/мес., вместо 240 000 руб. Красным выделены альтернативные издержки.";
                }
                else if (button.Name == "buyYourselfRadioButton")
                {
                    rentTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(200, 30, 30));
                    buyWithBankTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    buyYourselfTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

                    resultTextBlock.Text = "Это лучший вариант. Получится, что вы потеряли 200 000 руб/мес. (которые могли бы получить, положив деньги на вклад), но не платите 240 000 руб. Красным выделены альтернативные издержки.";
                }
                else
                {
                    rentTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    buyWithBankTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    buyYourselfTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(200, 30, 30));

                    resultTextBlock.Text = "Это неплохой вариант, но вы в убытке на 40 000 руб/мес. Получится 240 000 руб/мес. И пусть вы получаете 200 000 руб/мес. из-за вклада, вы остаетесь в минусе. Красным выделены альтернативные издержки.";
                }
            }
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            var cnt = 0;
            if (!(bool)checkBox1.IsChecked)
                cnt++;
            if ((bool)checkBox2.IsChecked)
                cnt++;
            if ((bool)checkBox3.IsChecked)
                cnt++;
            if ((bool)checkBox4.IsChecked)
                cnt++;
            if (!(bool)checkBox5.IsChecked)
                cnt++;
            if ((bool)RadioButton41.IsChecked)
                cnt++;
            if ((bool)RadioButton22.IsChecked)
                cnt++;
            if ((bool)RadioButton23.IsChecked)
                cnt++;

            if (cnt == 7)
                MessageBox.Show($"Ваш результат = {cnt} из 7. Отлично, молодец!");
            else if (cnt > 4)
                MessageBox.Show($"Ваш результат = {cnt} из 7. Неплохо");
            else
                MessageBox.Show($"Ваш результат = {cnt} из 7.");

            ChangeUCClick?.Invoke(UCType.Main, 3);
        }
    }
}
