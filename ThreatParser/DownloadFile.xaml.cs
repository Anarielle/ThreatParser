using System;
using System.Collections.Generic;
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

namespace ThreatParser
{
    /// <summary>
    /// Логика взаимодействия для DownloadFile.xaml
    /// </summary>
    public partial class DownloadFile : Window
    {
        public DownloadFile()
        {
            InitializeComponent();
        }

        public void Download(string path)
        {
            Uri link = new Uri("https://bdu.fstec.ru/files/documents/thrlist.xlsx");
            using (WebClient client = new WebClient())
            {
                DownloadFile downloadWindow = new DownloadFile();
                downloadWindow.Show();
                client.DownloadFileAsync(link, path + "\\thrlist.xlsx");
                client.DownloadProgressChanged += (s, e) =>
                {
                    //DownloadFile download = new DownloadFile();
                    //download.Show();
                    pbLoadingStatus.Value = e.ProgressPercentage;                                        
                };
                client.DownloadFileCompleted += (s, e) =>
                {
                    downloadWindow.Close();
                };
            }
        }
    }
}
