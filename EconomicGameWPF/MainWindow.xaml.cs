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
using System.IO;
using Microsoft.Win32;

namespace EconomicGameWPF
{ 
    public enum UCType
    {
        Main,
        First,
        OpportunityCosts,
        PCC,
        SystemTypes,
        Supply,
        Demand,
        MarketEquillibrium,
        Elastic
    }
    public delegate void ChangeUCEvent(UCType type, int maxLevelInd = 0);

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int maxLevelInd;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadSave();
        }

        public void ChangeUserControl(UCType type, int _maxLevelInd = 0)
        {
            if (_maxLevelInd != 0 && _maxLevelInd > maxLevelInd)
                maxLevelInd = _maxLevelInd;

            switch (type)
            {
                case UCType.Main:
                    MainUC mainUC = new MainUC(maxLevelInd);
                    mainUC.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = mainUC;
                    break;
                case UCType.First:
                    Minigame1 minigame = new Minigame1();
                    minigame.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = minigame;
                    break;
                case UCType.OpportunityCosts:
                    MinigameOpportunityСosts minigameOC = new MinigameOpportunityСosts();
                    minigameOC.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = minigameOC;
                    break;
                case UCType.PCC:
                    MinigamePCC minigamePCC = new MinigamePCC();
                    minigamePCC.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = minigamePCC;
                    break;
                case UCType.SystemTypes:
                    MinigameSystemTypes minigameST = new MinigameSystemTypes();
                    minigameST.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = minigameST;
                    break;
                case UCType.Demand:
                    MinigameDemand minigameD = new MinigameDemand();
                    minigameD.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = minigameD;
                    break;
                case UCType.Supply:
                    MinigameSupply minigameS = new MinigameSupply();
                    minigameS.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = minigameS;
                    break;
                case UCType.MarketEquillibrium:
                    MinigameMarket minigameM = new MinigameMarket();
                    minigameM.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = minigameM;
                    break;
                case UCType.Elastic:
                    MinigameElastic minigameE = new MinigameElastic();
                    minigameE.ChangeUCClick += ChangeUserControl;
                    OutputView.Content = minigameE;
                    break;
            }
        }

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }

            if (!File.Exists(filePath))
            {
                return;
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                if (!int.TryParse(sr.ReadLine(), out int _levelInd))
                {
                    MessageBox.Show("Неверный файл сохранения!");
                    return;
                }
                    
                maxLevelInd = _levelInd;
                ChangeUserControl(UCType.Main);
            }
        }

        private void LoadSave()
        {
            using (StreamReader sr = new StreamReader(Application.Current.Resources["SaveDir"].ToString()))
            {
                if (!int.TryParse(sr.ReadLine(), out int _levelInd))
                    return;
                    
                maxLevelInd = _levelInd;
                ChangeUserControl(UCType.Main);
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(Application.Current.Resources["SaveDir"].ToString(), false))
            {
                sw.WriteLine(maxLevelInd);
            }
            MessageBox.Show("Сохранено!");
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ResetMenuItem_Click(object sender, RoutedEventArgs e)
        {
            maxLevelInd = 1;
            using (StreamWriter sw = new StreamWriter(Application.Current.Resources["SaveDir"].ToString(), false))
            {
                sw.WriteLine(maxLevelInd);
            }
            ChangeUserControl(UCType.Main);
        }
    }

}
