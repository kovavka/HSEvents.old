using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Common;
using Domain;
using HSEvents.Web.Authentification;

namespace HSEvents.Web.Controllers
{
    //[Auth]
    public class ReportsController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool Index(Attendee[] data)
        {
            if (data != null && data.Length != 0)
            {
                TempData["Report"] = data;
                return true;
            }

            return false;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return File(CreateReport((Attendee[])TempData["Report"]), "application/vnd.ms-excel");
        }

        private Stream CreateReport(Attendee[] data)
        {
            var tmpPath = Path.GetTempFileName();
            System.IO.File.Delete(tmpPath);

            string[] heads = new string[] { "ФИО", "Телефон", "Email", "Тип" };

            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            for (int i = 0; i < heads.Length; i++)
                ExcelApp.Cells[1, i + 1] = heads[i];
            for (int i = 0; i < data.Length; i++)
            {
                ExcelApp.Cells[i + 2, 1] = data[i].ContactInfo.FullName;
                ExcelApp.Cells[i + 2, 2] = data[i].ContactInfo.PhoneNumber;
                ExcelApp.Cells[i + 2, 3] = data[i].ContactInfo.Email;
                ExcelApp.Cells[i + 2, 4] = data[i].Type.GetDescription();

            }
            ExcelWorkSheet.Columns.AutoFit();


            object misValue = System.Reflection.Missing.Value;
            ExcelWorkBook.SaveAs(tmpPath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            ExcelApp.Workbooks.Close();
            ExcelApp.Quit();
            Marshal.ReleaseComObject(ExcelWorkSheet);
            Marshal.ReleaseComObject(ExcelWorkBook);
            
            var bytes = System.IO.File.ReadAllBytes(tmpPath);
            System.IO.File.Delete(tmpPath);

            return new MemoryStream(bytes);
        }
    }
}