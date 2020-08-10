using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GASiteLinkExtensionUpdater
{
    public class ExcelReader
    {
        /// <summary>
        /// Reads Sitelinks to be updated from Excel Spreadsheet.
        /// </summary>
        /// <param name="siteLinkFilePath">Path of SpreadSheet</param>
        /// <returns>List of SiteLinks and Values modelled into Class SiteLinkModel</returns>
        public List<SiteLinkModel> Read(string siteLinkFilePath)
        {
            List<SiteLinkModel> sitelinksDataFromExcel = new List<SiteLinkModel>();
            if (File.Exists(siteLinkFilePath))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(siteLinkFilePath)))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                  
                    for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                    {
                        SiteLinkModel siteLinkModel = new SiteLinkModel();
                        try
                        {
                            siteLinkModel.FeedId = long.Parse(worksheet.Cells[i, 1].Value.ToString().Trim().Replace("\r\n", " "));
                            siteLinkModel.FeedItemId = long.Parse(worksheet.Cells[i, 2].Value.ToString().Trim().Replace("\r\n", " "));
                            siteLinkModel.AccountId = worksheet.Cells[i, 3].Value.ToString().Trim().Replace("\r\n", " ");
                            siteLinkModel.Text = worksheet.Cells[i, 4].Value.ToString().Trim().Replace("\r\n", " ");
                            siteLinkModel.DescriptionLine1 = worksheet.Cells[i, 5].Value.ToString().Trim().Replace("\r\n", " ");
                            siteLinkModel.DescriptionLine2 = worksheet.Cells[i, 6].Value.ToString().Trim().Replace("\r\n", " ");
                            siteLinkModel.Url = worksheet.Cells[i, 7].Value.ToString().Trim().Replace("\r\n", " ");
                            if(siteLinkModel.Text.Trim().Length==0 || siteLinkModel.DescriptionLine1.Trim().Length == 0 || siteLinkModel.DescriptionLine2.Trim().Length == 0 || siteLinkModel.Url.Trim().Length == 0)
                            {
                                siteLinkModel.IsDataValid = false;
                            }
                            else
                            {
                                siteLinkModel.IsDataValid = true;
                            }                            
                        }
                        catch (Exception ex)
                        {
                            siteLinkModel.IsDataValid = false;
                            Logger.Log(Logger.LogType.EXCEPTION, "Reading Row ("+(i+2)+") "+ex.Message + Environment.NewLine + ex.InnerException);
                        }
                        sitelinksDataFromExcel.Add(siteLinkModel);
                    }
                }
            }
            else
            {
                Logger.Log(Logger.LogType.ERROR, "Excel File with Site Link Does not Found at Path: " + siteLinkFilePath);
            }
            return sitelinksDataFromExcel;
        }
    }
}
