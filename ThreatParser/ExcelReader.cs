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
        public static List<Threat> ReadFile(FileInfo newFile)
        {
            List<Threat> threats = new List<Threat>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
                    MessageBox.Show($"Не удалось прочитать файл.\nПожалуйста загрузите файл заново или " +
                        $"укажите директорию к другой копии файла", "Ошибка чтения файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
                else
                {
                    for (int i = 3; i <= worksheet.Dimension.End.Row; i++)
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
                                default:
                                    throw new Exception();

                            }
                        }
                        threats.Add(new Threat(id, name, description, source, objectInfl, isPrivacy, isIntegrity, isAccessibility));                       
                    }
                }
            }
            return threats;
        }

    }
}
