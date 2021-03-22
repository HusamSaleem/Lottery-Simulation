
using OfficeOpenXml;
using System;
using System.IO;

namespace Lottery_Simulation
{
    class Excel
    {
        private string excelDirPath = "";
        private bool useExcel = false;
        public static int MAX_ROWS = 1048576; // Max rows in excel that is allowed before an error occurs
        ExcelPackage excel;
        ExcelWorksheet worksheet;

        public Excel()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
        }

        public void SetPath(string excelDirPath)
        {
            this.excelDirPath = excelDirPath;
        }

        public void UseExcel(bool useExcel)
        {
            this.useExcel = useExcel;
        }

        public void CreateNewExcelFile(string lotteryName)
        {
            excel = new ExcelPackage();
            worksheet = excel.Workbook.Worksheets.Add(lotteryName);

            SetUp();
            FileInfo filePath = new FileInfo(excelDirPath + "Lottery Data (" + GetRandomString(4) + ").xlsx");
            excel.SaveAs(filePath);
        }

        public void Save()
        {
            if (useExcel)
            {
                Console.WriteLine("Saving Data, Please be patient for larger simulations.");
                excel.Save();
                Console.WriteLine("\nSaved data successfully!");
            }
        }

        public void WriteToExcel(int row, int column, string msg)
        {
            if (useExcel)
            {
                worksheet.Cells[row, column].Value = msg;
            }
        }

        // Sets up the columns of the file
        private void SetUp()
        {
            if (!useExcel)
            {
                return;
            }

            worksheet.Cells["A1"].Value = "0 Matches";
            worksheet.Cells["B1"].Value = "1 Matches";
            worksheet.Cells["C1"].Value = "2 Matches";
            worksheet.Cells["D1"].Value = "3 Matches";
            worksheet.Cells["E1"].Value = "4 Matches";
            worksheet.Cells["F1"].Value = "5 Matches";
            worksheet.Cells["G1"].Value = "6 Matches";
            worksheet.Cells["H1"].Value = "Winning Numbers";

            worksheet.Column(1).AutoFit();
            worksheet.Column(2).AutoFit();
            worksheet.Column(3).AutoFit();
            worksheet.Column(4).AutoFit();
            worksheet.Column(5).AutoFit();
            worksheet.Column(6).AutoFit();
            worksheet.Column(7).AutoFit();
        }

        private string GetRandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
