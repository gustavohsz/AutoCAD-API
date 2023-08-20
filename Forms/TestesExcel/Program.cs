using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace TestesExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var excelApp = new Application();
            var workbook = excelApp.Workbooks.Open(@"C:\Users\gusta\OneDrive - Instituto Federal de Santa Catarina\9º Fase\Projeto Integrador 3\Plugin AutoCad\Projeto\Excel\Pasta1.xlsx");
            var worksheet = (Worksheet)workbook.Worksheets[1];

            var headers = new List<string>();
            var usedRange = worksheet.UsedRange;
            var headerRange = (Range)usedRange.Rows[1];
            foreach (Range cell in headerRange.Cells)
            {
                headers.Add(cell.Value2.ToString());
            }

            var results = new List<Dictionary<string, object>>();

            for (int row = 2; row <= usedRange.Rows.Count; row++)
            {
                var item = new Dictionary<string, object>();

                for (int column = 1; column <= headers.Count; column++)
                {
                    var cell = (Range)worksheet.Cells[row, column];

                    object value;
                    if (cell.MergeCells)
                    {
                        var mergedRange = worksheet.Range[cell.MergeArea.Address];
                        value = mergedRange.Cells[1, 1].Value2;
                    }
                    else
                    {
                        value = cell.Value2;
                    }

                    item.Add(headers[column - 1], value);
                }

                results.Add(item);
            }

            foreach (var item in results)
            {
                foreach (var key in item.Keys)
                    Console.WriteLine($"{key} = {item[key]}");

                Console.WriteLine();
            }

            workbook.Close();
            excelApp.Quit();
            Console.ReadLine();


        }
    }
}
