using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace GASiteLinkExtensionUpdater
{
    public class ExcelReader
    {
        public List<SiteLinkModel> Read(string siteLinkFilePath)
        {
            var filePath = siteLinkFilePath;
            List<SiteLinkModel> sitelinksDataFromExcel = new List<SiteLinkModel>();

            if (File.Exists(filePath))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets["Sheet1"];
                  
                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        SiteLinkModel siteLinkModel = new SiteLinkModel();

                        siteLinkModel.FeedId = long.Parse(worksheet.Cells[i, 1].Value.ToString().Trim().Replace("\r\n", " "));
                        siteLinkModel.FeedItemId = long.Parse(worksheet.Cells[i, 2].Value.ToString().Trim().Replace("\r\n", " "));
                        siteLinkModel.AccountId = worksheet.Cells[i, 3].Value.ToString().Trim().Replace("\r\n", " ");
                        siteLinkModel.Text = worksheet.Cells[i, 4].Value.ToString().Trim().Replace("\r\n", " ");
                        siteLinkModel.DescriptionLine1 = worksheet.Cells[i, 5].Value.ToString().Trim().Replace("\r\n", " ");
                        siteLinkModel.DescriptionLine2 = worksheet.Cells[i, 6].Value.ToString().Trim().Replace("\r\n", " ");
                        siteLinkModel.Url = worksheet.Cells[i, 7].Value.ToString().Trim().Replace("\r\n", " ");
                        sitelinksDataFromExcel.Add(siteLinkModel);
                    }                    
                }
            }
            else
            {
                Logger.Log(Logger.LogType.ERROR, "Excel File with Site Link Does not Found at Path: " + filePath);
            }
            return sitelinksDataFromExcel;
        }
    }
}
