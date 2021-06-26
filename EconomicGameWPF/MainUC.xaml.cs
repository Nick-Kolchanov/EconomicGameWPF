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
using System.Collections;

namespace EconomicGameWPF
{
    /// <summary>
    /// Логика взаимодействия для MainUC.xaml
    /// </summary>
    public partial class MainUC : UserControl
    {
        private ArrayList levelDescriptions;
        private int choosedLevel;
        private readonly int maxLevelInd;
        public event ChangeUCEvent ChangeUCClick;

        public MainUC(int levelInd)
        {
            InitializeComponent();
            Loaded += OnUCLoaded;
            maxLevelInd = levelInd;
        }

        private void OnUCLoaded(object sender, RoutedEventArgs e)
        {
            levelName.Text = "Выберите уровень";
            levelDescription.Text = "...";
            levelDescriptions = (ArrayList)Application.Current.Resources["LevelsDescriptions"];

            foreach (var item in roadGrid.Children)
            {
                if (item is Button button)
                {
                    button.Click += Button_Click;
                    int.TryParse(button.Content.ToString(), out int levelInd);
                    if (levelInd > maxLevelInd)
                    {
                        button.IsEnabled = false;
                    }
                }
            } 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse((sender as Button)?.Content.ToString(), out int levelInd))
            {
                return;
            }

            if (levelInd > 6)
                playButton.IsEnabled = false;
            else
                playButton.IsEnabled = true;
            
            ChooseLevel(levelInd);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            switch (choosedLevel)
            {
                case 1:
                    ChangeUCClick?.Invoke(UCType.First);
                    break;
                case 2:
                    ChangeUCClick?.Invoke(UCType.OpportunityCosts);
                    break;
                case 3:
                    ChangeUCClick?.Invoke(UCType.SystemTypes);
                    break;
                case 4:
                    ChangeUCClick?.Invoke(UCType.Demand);
                    break;
                case 5:
                    ChangeUCClick?.Invoke(UCType.Supply);
                    break;
                case 6:
                    ChangeUCClick?.Invoke(UCType.MarketEquillibrium);
                    break;
            }
        }

        private void ChooseLevel(int ind)
        {
            var info = levelDescriptions[ind - 1].ToString().Split(';');
            levelName.Text = info[0];
            levelDescription.Text = info[1];
            choosedLevel = ind;
        }
    }
}
