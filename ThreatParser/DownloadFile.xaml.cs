﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Forms = System.Windows.Forms;

namespace ThreatParser
{
    /// <summary>
    /// Логика взаимодействия для DownloadFile.xaml
    /// </summary>
    public partial class DownloadFile : Window
    {
        internal readonly Uri link = new Uri("https://bdu.fstec.ru/files/documents/thrlist.xlsx");
        internal string path = Environment.CurrentDirectory;
        public DownloadFile()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            tbPath.Text = Environment.CurrentDirectory;
        }

        private void bReview_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog folderBrowser = new Forms.FolderBrowserDialog();
            folderBrowser.ShowDialog();
            path = folderBrowser.SelectedPath;
            tbPath.Text = folderBrowser.SelectedPath;
        }

        private void bFind_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(path + @"\thrlist.xlsx"))
            {
                MessageBox.Show($"Файл не найден! Измените директорию или загрузите его", "Ошибка чтения файла",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                FileInfo newFile = new FileInfo(path + @"\thrlist.xlsx");
                MainWindow.threats = ExcelReader.ReadFile(newFile);
                if (MainWindow.threats != null)
                {
                    new MainWindow().Show();
                    Hide();
                }
            }
        }

        private void bDownload_Click(object sender, RoutedEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(link, path + @"\thrlist.xlsx");
                    FileInfo newFile = new FileInfo(path + @"\thrlist.xlsx");
                    MainWindow.threats = ExcelReader.ReadFile(newFile);
                    if (MainWindow.threats != null)
                    {
                        new MainWindow().Show();
                        Hide();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show($"Не удалось загрузить файл. Возможно проблемы с подключением к интернету или с доступом к сайту",
                        "Ошибка загрузки файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
