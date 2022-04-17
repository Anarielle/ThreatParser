using System;
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
        public static Uri link = new Uri("https://bdu.fstec.ru/files/documents/thrlist.xlsx");
        public static string path = Environment.CurrentDirectory;
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
                ExcelReader.ReadFile(newFile, MainWindow.threats);
                new MainWindow().Show();
                Close();
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
                    ExcelReader.ReadFile(newFile, MainWindow.threats);
                    new MainWindow().Show();
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show($"Не удалось загрузить файл. Проверьте подключение к интернету",
                        "Ошибка загрузки файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }            
        }
    }
}
