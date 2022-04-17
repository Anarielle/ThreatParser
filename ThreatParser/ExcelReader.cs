using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Net;
using System.Windows;
using Forms = System.Windows.Forms;
using System.Threading;

namespace ThreatParser
{
    internal class ExcelReader
    {
        public static string path = Environment.CurrentDirectory;
        public static Uri link = new Uri("https://bdu.fstec.ru/files/documents/thrlist.xlsx");
        public static void UploadFile()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo newFile;

            while (!File.Exists(path + @"\thrlist.xlsx"))
            {
                MessageBox.Show($"Файл не найден! Путь: {path}", "Ошибка чтения файла",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                if (MessageBox.Show("Хотите изменить путь к файлу?", "Директория файла",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Forms.FolderBrowserDialog folderBrowser = new Forms.FolderBrowserDialog();
                    folderBrowser.ShowDialog();
                    path = folderBrowser.SelectedPath;
                }
                else
                {
                    path = Environment.CurrentDirectory;
                    MessageBox.Show($"Файл будет загружен в корневую директорию: {path}", "Загрузка файла", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    using (WebClient client = new WebClient())
                    {
                        try
                        {
                            client.DownloadFile(link, path + @"\thrlist.xlsx");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show($"Не удалось загрузить файл. Проверьте подключение к интернету",
                                "Ошибка загрузки файла", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }

            newFile = new FileInfo(path + @"\thrlist.xlsx");
            ReadFile(newFile, MainWindow.threats);
        }
        public static void ReadFile(FileInfo newFile, List<Threat> threats)
        {
            using (ExcelPackage excelPackage = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet"];

                int id = 0;
                string name = string.Empty;
                string description = string.Empty;
                string source = string.Empty;
                string objectInfl = string.Empty;
                bool isPrivacy = false;
                bool isIntegrity = false;
                bool isAccessibility = false;

                if (worksheet == null)
                {
                    MessageBox.Show($"Не удалось загрузить файл.\nПожалуйста перезапустите приложение и " +
                        $"попробуйте снова", "Ошибка чтения файла", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    //int lastRow = worksheet.Cells.Where(cell => !cell.Value.ToString().Equals("")).Last().End.Row;
                    for (int i = 3; i <= 224; i++)
                    {
                        for (int j = 1; j <= 8; j++)
                        {
                            switch (j)
                            {
                                case 1:
                                    id = int.Parse(worksheet.Cells[i, j].Value.ToString());
                                    break;
                                case 2:
                                    name = worksheet.Cells[i, j].Value.ToString();
                                    break;
                                case 3:
                                    description = worksheet.Cells[i, j].Value.ToString();
                                    break;
                                case 4:
                                    source = worksheet.Cells[i, j].Value.ToString();
                                    break;
                                case 5:
                                    objectInfl = worksheet.Cells[i, j].Value.ToString();
                                    break;
                                case 6:
                                    if (int.Parse(worksheet.Cells[i, j].Value.ToString()) == 1)
                                    {
                                        isPrivacy = true;
                                    }
                                    else
                                    {
                                        isPrivacy = false;
                                    }
                                    break;

                                case 7:
                                    if (int.Parse(worksheet.Cells[i, j].Value.ToString()) == 1)
                                    {
                                        isIntegrity = true;
                                    }
                                    else
                                    {
                                        isIntegrity = false;
                                    }
                                    break;

                                case 8:
                                    if (int.Parse(worksheet.Cells[i, j].Value.ToString()) == 1)
                                    {
                                        isAccessibility = true;
                                    }
                                    else
                                    {
                                        isAccessibility = false;
                                    }
                                    break;

                            }
                        }
                        threats.Add(new Threat(id, name, description, source, objectInfl, isPrivacy, isIntegrity, isAccessibility));                       
                    }
                }
            }
        }

    }
}
