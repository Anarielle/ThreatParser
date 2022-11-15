using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Net;
using Forms = System.Windows.Forms;

namespace ThreatParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static List<Threat> threats = new List<Threat>();
        private static int page = 1;
        DownloadFile downloadFile = new DownloadFile();

        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            downloadFile.Show();
            Hide();
            
            InitializeComponent();
            
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            List<Threat> threatsUpdate;
            List<string> changes = new List<string>();
            HashSet<int> id = new HashSet<int>();
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(DownloadFile.link, DownloadFile.path + @"\thrlist.xlsx");
                }
                catch (System.Net.WebException)
                {
                    MessageBox.Show($"Не удалось обновить файл.\nВозможно проблемы с подключением к интернету или с доступом к сайту." +
                        $"\nЕсли у вас открыт текущий файл Excel, пожалуйста закройте его и повторите попытку",
                        "Ошибка обновления файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            threatsUpdate = ExcelReader.ReadFile(new FileInfo(DownloadFile.path + @"\thrlist.xlsx"));

            foreach (var threatUp in threatsUpdate)
            {
                foreach (var threat in threats)
                {
                    if (threat.Id == threatUp.Id)
                    {
                        if (threat.Name != threatUp.Name)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись\tНаименование УБИ: {threat.Name}\n" +
                                $"Новая запись \tНаименование УБИ: {threatUp.Name}");
                            id.Add(threat.Id);
                        }
                        if (threat.Description != threatUp.Description)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tОписание: {threat.Description}\n" +
                                $"Новая запись: \tОписание: {threatUp.Description}");
                            id.Add(threat.Id);
                        }
                        if (threat.Source != threatUp.Source)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tИсточник угрозы: {threat.Source}\n" +
                                $"Новая запись: \tИсточник угрозы: {threatUp.Source}");
                            id.Add(threat.Id);
                        }
                        if (threat.Object != threatUp.Object)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tОбъект воздействия: {threat.Object}\n" +
                                $"Новая запись: \tОбъект воздействия: {threatUp.Object}");
                            id.Add(threat.Id);
                        }
                        if (threat.isBreachPrivacy != threatUp.isBreachPrivacy)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tНарушение конфиденциальности: {(threat.isBreachPrivacy ? "да" : "нет")}\n" +
                                $"Новая запись: \tНарушение конфиденциальности: {(threatUp.isBreachPrivacy ? "да" : "нет")}");
                            id.Add(threat.Id);
                        }
                        if (threat.isBreachIntegrity != threatUp.isBreachIntegrity)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tНарушение целостности: {(threat.isBreachIntegrity ? "да" : "нет")}\n" +
                                $"Новая запись: \tНарушение целостности: {(threatUp.isBreachIntegrity ? "да" : "нет")}");
                            id.Add(threat.Id);
                        }
                        if (threat.isBreachAccess != threatUp.isBreachAccess)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tНарушение доступности: {(threat.isBreachAccess ? "да" : "нет")}\n" +
                                $"Новая запись: \tНарушение доступности: {(threatUp.isBreachAccess ? "да" : "нет")}");
                            id.Add(threat.Id);
                        }
                    }
                }
            }

            if (changes.Count != 0)
            {
                MessageBox.Show($"Обновление прошло успешно!!!\nОбновлено {id.Count} записей!", "Обновление данных");
                ListOfChanges listOfChanges = new ListOfChanges();
                listOfChanges.listBoxChanges.ItemsSource = changes;
                listOfChanges.Show();

                threats = threatsUpdate;
                ThreatsGrid.ItemsSource = threats;
                ThreatsGridDetailed.ItemsSource = threats;
            }
            else
            {
                MessageBox.Show($"Обновление не требуется, у вас свежие данные.\nОбновлено {changes.ToHashSet().Count} записей!", "Обновление данных");
            }
        }

        private void cbPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (threats.Count != 0)
            {
                if (cbPages.SelectedIndex == 0)
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(0, 20);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(0, 20);
                }

                if (cbPages.SelectedIndex == 1)
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(0, 40);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(0, 40);
                }

                if (cbPages.SelectedIndex == 2)
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(0, 60);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(0, 60);
                }
                downloadFile.Hide();
            }            
            page = 1;
        }
        private void ThreatsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show($"{ThreatsGrid.SelectedItem as Threat}");
        }

        private void bDetailedInfo_Click(object sender, RoutedEventArgs e)
        {
            if (ThreatsGrid.IsVisible)
            {
                ThreatsGrid.Visibility = Visibility.Hidden;
                ThreatsGridDetailed.Visibility = Visibility.Visible;
                bDetailedInfo.Content = "Краткие сведения";
            }
            else
            {
                ThreatsGrid.Visibility = Visibility.Visible;
                ThreatsGridDetailed.Visibility = Visibility.Hidden;
                bDetailedInfo.Content = "Подробные сведения";
            }
        }

        private void bForward_Click(object sender, RoutedEventArgs e)
        {            
            if (cbPages.SelectedIndex == 0 && page <= threats.Count / 20)
            {
                if (page * 20 + 20 >= threats.Count)
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(page * 20, threats.Count - page * 20);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(page * 20, threats.Count - page * 20);
                }
                else
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(page * 20, 20);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(page * 20, 20);
                }
                page++;
            }
            if (cbPages.SelectedIndex == 1 && page <= threats.Count / 40)
            {
                if (page * 40 + 40 > threats.Count)
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(page * 40, threats.Count - page * 40);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(page * 40, threats.Count - page * 40);
                }
                else
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(page * 40, 40);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(page * 40, 40);
                }
                page++;
            }
            if (cbPages.SelectedIndex == 2 && page <= threats.Count / 60)
            {
                if (page * 60 + 60 > threats.Count)
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(page * 60, threats.Count - page * 60);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(page * 60, threats.Count - page * 60);
                }
                else
                {
                    ThreatsGrid.ItemsSource = threats.GetRange(page * 60, 60);
                    ThreatsGridDetailed.ItemsSource = threats.GetRange(page * 60, 60);
                }
                page++;
            }
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            if (page >= 2)
            {
                page--;
                switch(cbPages.SelectedIndex)
                {
                    case 0:
                        ThreatsGrid.ItemsSource = threats.GetRange((page - 1) * 20, 20);
                        ThreatsGridDetailed.ItemsSource = threats.GetRange((page - 1) * 20, 20);
                        break;
                    case 1:
                        ThreatsGrid.ItemsSource = threats.GetRange((page - 1) * 40, 40);
                        ThreatsGridDetailed.ItemsSource = threats.GetRange((page - 1) * 40, 40);
                        break;
                    case 2:
                        ThreatsGrid.ItemsSource = threats.GetRange((page - 1) * 60, 60);
                        ThreatsGridDetailed.ItemsSource = threats.GetRange((page - 1) * 60, 60);
                        break;
                    default:
                        break;
                }                
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
