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
        public static List<Threat> threats = new List<Threat>();

        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            ExcelReader.UploadFile(); 
        }

        private void bUpdate_Click(object sender, RoutedEventArgs e)
        {
            List<Threat> threatsUpdate = new List<Threat>();
            List<string> changes = new List<string>();
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(ExcelReader.link, ExcelReader.path + @"\thrlist.xlsx");
                }
                catch(System.Net.WebException)
                {
                    MessageBox.Show($"Не удалось загрузить файл.\nПожалуйста закройте файл Excel",
                        "Ошибка загрузки файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }  
            }

            ExcelReader.ReadFile(new FileInfo(ExcelReader.path + @"\thrlist.xlsx"), threatsUpdate);

            foreach (var threatUp in threatsUpdate)
            {
                foreach (var threat in threats)
                {
                    if(threat.Id == threatUp.Id)
                    {
                        if(threat.Name != threatUp.Name)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись\tНаименование УБИ: {threat.Name}\n" +
                                $"Новая запись \tНаименование УБИ: {threatUp.Name}");
                        }
                        if (threat.Description != threatUp.Description)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tОписание: {threat.Description}\n" +
                                $"Новая запись: \tОписание: {threatUp.Description}");
                        }
                        if (threat.Source != threatUp.Source)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tИсточник угрозы: {threat.Source}\n" +
                                $"Новая запись: \tИсточник угрозы: {threatUp.Source}");
                        }
                        if (threat.Object != threatUp.Object)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tОбъект воздействия: {threat.Object}\n" +
                                $"Новая запись: \tОбъект воздействия: {threatUp.Object}");
                        }
                        if (threat.isBreachPrivacy != threatUp.isBreachPrivacy)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tНарушение конфиденциальности: {(threat.isBreachPrivacy ? "да" : "нет")}\n" +
                                $"Новая запись: \tНарушение конфиденциальности: {(threatUp.isBreachPrivacy ? "да" : "нет")}");
                        }
                        if (threat.isBreachIntegrity != threatUp.isBreachIntegrity)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tНарушение целостности: {(threat.isBreachIntegrity ? "да" : "нет")}\n" +
                                $"Новая запись: \tНарушение целостности: {(threatUp.isBreachIntegrity ? "да" : "нет")}");
                        }
                        if(threat.isBreachAccess != threatUp.isBreachAccess)
                        {
                            changes.Add($"Идентификатор записи: {threat.Id}\n" +
                                $"Старая запись:\tНарушение доступности: {(threat.isBreachAccess ? "да" : "нет")}\n" +
                                $"Новая запись: \tНарушение доступности: {(threatUp.isBreachAccess ? "да" : "нет")}");
                        }    
                    }
                }
            }
            MessageBox.Show($"Обновление прошло успешно!!!\nОбновлено {changes.ToHashSet().Count} записей!");
            foreach (var change in changes)
            {
                MessageBox.Show(change);
            }

            threats = threatsUpdate;
            ThreatsGrid.ItemsSource = threats;            
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog folderBrowser = new Forms.FolderBrowserDialog();
            folderBrowser.ShowDialog();
        }

        private void cbPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            ThreatsGrid.ItemsSource = threats;
            
        }

        private void ThreatsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show($"{ThreatsGrid.SelectedItem as Threat}");
        }
    }
}
