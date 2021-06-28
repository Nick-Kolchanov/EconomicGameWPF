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
        bool onlyCheck;
        int scorePerc;

        public MinigameOpportunityСosts()
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
            if (!onlyCheck)
            {
                ChangeUCClick?.Invoke(UCType.Main, 3, scorePerc);
                return;
            }

            var total = 8;
            var cnt = 0;
            var correctBrush = new SolidColorBrush(Color.FromRgb(0, 200, 0));
            var wrongBrush = new SolidColorBrush(Color.FromRgb(200, 0, 0));

            checkBox2.Foreground = correctBrush;
            checkBox3.Foreground = correctBrush;
            checkBox4.Foreground = correctBrush;
            RadioButton41.Foreground = correctBrush;
            RadioButton22.Foreground = correctBrush;
            RadioButton23.Foreground = correctBrush;

            foreach (var elem in checkBoxPanel.Children)
            {
                if (elem is CheckBox checkBox)
                {
                    if ((bool)checkBox.IsChecked)
                    {
                        if (checkBox.Foreground != correctBrush)
                            checkBox.Foreground = wrongBrush;
                        else
                            cnt++;
                    }
                    else
                    {
                        if (checkBox.Foreground != correctBrush)
                            cnt++;
                    }
                }
            }

            foreach (var elem in radioButtonsPanel.Children)
            {
                if (elem is StackPanel panel)
                {
                    foreach (var elemPanel in panel.Children)
                    {
                        if (elemPanel is RadioButton button)
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
