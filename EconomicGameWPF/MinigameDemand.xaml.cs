﻿using System;
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
    public partial class MinigameDemand : UserControl
    {
        public event ChangeUCEvent ChangeUCClick;

        public MinigameDemand()
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
            foreach (var item in SubstPanel.Children)
            {
                if (item is Image img)
                {
                    if (img.Source.ToString() == subst1.Source.ToString() ||
                        img.Source.ToString() == subst2.Source.ToString() ||
                        img.Source.ToString() == subst3.Source.ToString())
                    {
                        cnt++;
                    }
                }
            }

            foreach (var item in ComplPanel.Children)
            {
                if (item is Image img)
                {
                    if (img.Source.ToString() == compl1.Source.ToString() ||
                        img.Source.ToString() == compl2.Source.ToString() ||
                        img.Source.ToString() == compl3.Source.ToString())
                    {
                        cnt++;
                    }
                }
            }

            if (cnt == 6)
                MessageBox.Show($"Ваш результат = {cnt} из 6. Отлично, молодец!");
            else if (cnt > 3)
                MessageBox.Show($"Ваш результат = {cnt} из 6. Неплохо");
            else
                MessageBox.Show($"Ваш результат = {cnt} из 6.");

            ChangeUCClick?.Invoke(UCType.Main, 5);
        }

        private void SubstPanel_Drop(object sender, DragEventArgs e)
        {
           var imageControl = new Image() { MaxWidth = 300, MaxHeight = 300, Source = (ImageSource)e.Data.GetData(typeof(ImageSource)), Margin = new Thickness(10)};
            (((Border)sender).Child as StackPanel).Children.Add(imageControl);
        }

        private void SubstPanel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = e.Source as Image;
            DataObject data = new DataObject(typeof(ImageSource), image.Source);
            DragDrop.DoDragDrop(image, data, DragDropEffects.Move);
            (sender as Image).Visibility = Visibility.Collapsed;
        }

        
    }
}
