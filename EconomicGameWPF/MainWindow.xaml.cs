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
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

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
    public delegate void ChangeUCEvent(UCType type, int maxLevelInd = 0, int localScore = -1);

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int maxLevelInd;
        readonly bool easyGame = false;
        static Dictionary<int, int> stats;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnWindowLoaded;
            stats = new Dictionary<int, int>();

            using (StreamReader sr = new StreamReader(Application.Current.Resources["SettingsDir"].ToString()))
            {
                var text = sr.ReadToEnd();
                if (text[2] == '1')
                    easyGame = true;
            }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadSave();
        }


        public static int GetAvgPercent()
        {
            var sum = 0;
            var cnt = 0;
            foreach (var lvl in stats)
            {
                sum += lvl.Value;
                cnt++;
            }

            if (cnt == 0)
                return 0;
            else
                return sum / cnt;
        }

        public void ChangeUserControl(UCType type, int _maxLevelInd = 0, int localScorePerc = -1)
        {
            UpdateStats(_maxLevelInd, localScorePerc);

            if (_maxLevelInd != 0 && _maxLevelInd > maxLevelInd)
                maxLevelInd = _maxLevelInd;

            if (localScorePerc != -1 && !easyGame)
            {
                if (localScorePerc < 50)
                {
                    MessageBox.Show("Чтобы продвинуться дальше, нужно набрать более 50% правильных ответов.");
                    maxLevelInd--;
                }
            }

            if (_maxLevelInd == 10 && GetAvgPercent() > 90)
            {
                MessageBox.Show("Вы получили медальку! Отлично!");
                medalMenu.Visibility = Visibility.Visible;
            }


            switch (type)
            {
                case UCType.Main:
                    MainUC mainUC = new MainUC(maxLevelInd, GetAvgPercent());
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

        private void UpdateStats(int _maxLevelInd, int localScorePerc)
        {
            if (_maxLevelInd != 0 && localScorePerc != -1)
                if (stats.TryGetValue(_maxLevelInd - 1, out int oldPerc))
                {
                    if (localScorePerc > oldPerc)
                        stats[_maxLevelInd - 1] = localScorePerc;
                }
                else
                {
                    stats[_maxLevelInd - 1] = localScorePerc;
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
                    var bv = new BinaryFormatter();
                    try
                    {
                        var fs = new FileStream(filePath, FileMode.Open);
                        stats = (Dictionary<int, int>)bv.Deserialize(fs);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Неверный файл сохранения!");
                        return;
                    }
                }
                else
                {
                    maxLevelInd = _levelInd;
                }
            }


            ChangeUserControl(UCType.Main);
        }

        private void LoadSave()
        {
            using (FileStream fs = new FileStream(Application.Current.Resources["StatsDir"].ToString(), FileMode.Open))
            {
                var bf = new BinaryFormatter();
                stats = (Dictionary<int, int>)bf.Deserialize(fs);
            }

            using (StreamReader sr = new StreamReader(Application.Current.Resources["SaveDir"].ToString()))
            {
                if (!int.TryParse(sr.ReadLine(), out int _levelInd))
                    return;
                    
                maxLevelInd = _levelInd;
                ChangeUserControl(UCType.Main);
            }
        }

        private void Save()
        {
            using (StreamWriter sw = new StreamWriter(Application.Current.Resources["SaveDir"].ToString(), false))
            {
                sw.WriteLine(maxLevelInd);
            }

            using (FileStream fs = new FileStream(Application.Current.Resources["StatsDir"].ToString(), FileMode.OpenOrCreate))
            {
                new BinaryFormatter().Serialize(fs, stats);
            }
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Save();

            MessageBox.Show("Сохранено!");
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Close();
        }

        private void StatsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var statString = "Статистика по уровням:\n";
            var lvlDesc = (ArrayList)Application.Current.Resources["LevelsDescriptions"];

            foreach (var lvl in stats)
            {
                statString += lvlDesc[lvl.Key - 1].ToString().Split(';')[0];
                statString += " = " + lvl.Value + "%\n";
            }
            statString += "\nСредний процент = " + GetAvgPercent();
            MessageBox.Show(statString);
        }

        private void ResetMenuItem_Click(object sender, RoutedEventArgs e)
        {
            maxLevelInd = 1;
            stats.Clear();
            Save();

            ChangeUserControl(UCType.Main);
        }
    }

}
