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
    public partial class MinigameSystemTypes : UserControl
    {
        public event ChangeUCEvent ChangeUCClick;

        public MinigameSystemTypes()
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

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!popup1.IsOpen)
                popup1.IsOpen = true;

            var mousePosition = Mouse.GetPosition(popupParent);
            popup1.HorizontalOffset = mousePosition.X + 15;
            popup1.VerticalOffset = mousePosition.Y;

            if (slider1.Value < 10)
                popup1text.Text = "Сев. Корея";
            else if (slider1.Value < 20)
                popup1text.Text = "Куба";
            else if (slider1.Value < 30)
                popup1text.Text = "Россия";
            else if (slider1.Value < 40)
                popup1text.Text = "Швеция";
            else if (slider1.Value < 50)
                popup1text.Text = "Нидерланды";
            else if (slider1.Value < 60)
                popup1text.Text = "Германия";
            else if (slider1.Value < 70)
                popup1text.Text = "Англия";
            else if (slider1.Value < 80)
                popup1text.Text = "Япония";
            else if (slider1.Value < 90)
                popup1text.Text = "Сша";
            else
                popup1text.Text = "Гонконг";

        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeUCClick?.Invoke(UCType.Main, 5);
        }
    }
}
