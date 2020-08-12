using OfficeOpenXml;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace GASiteLinkDBConnector
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(ConfigurationManager.AppSettings["ExportFilePath"],"");
            string connectionString = ConfigurationManager.AppSettings["DBConnectionString"];
            string sqlQuery = ConfigurationManager.AppSettings["SQLStatement"];
            ExportDbToSiteLinkExcel(filePath,connectionString,sqlQuery);
        }

        private static void ExportDbToSiteLinkExcel(string filePath, string connectionString, string sqlQuery)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, sqlConnection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable dataTable = new DataTable();
            dataTable = (dataSet.Tables[0]);

            var newFile = new FileInfo(filePath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage(newFile))
            {
                ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.Add("MasterSiteLink");
                workSheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                excelPackage.Save();
            }

        }
    }
}
