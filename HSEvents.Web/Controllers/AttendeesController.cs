using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Common;
using Domain;
using Infrastructure.Repositories;

namespace HSEvents.Web.Controllers
{
    public class AttendeesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Attendee attendee)
        {
            return View();

        }

        [HttpGet]
        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            if (file?.InputStream == null)
            {
                ModelState.AddModelError("file", "Файл не выбран");
                return View();
            }

            var extension = Path.GetExtension(file.FileName);

            if (extension != ".xlsx" && extension!= ".xls")
            {
                ModelState.AddModelError("file", "Расширение файла должно быть .xlsx или .xls");
                return View();
            }

            ViewData["Attendees"] = ReadFile(file).ToList();

            return View();
        }

        public IEnumerable<Attendee> ReadFile(HttpPostedFileBase file)
        {
            var tmpPath = Path.GetTempFileName();
            System.IO.File.Delete(tmpPath);
            file.SaveAs(tmpPath);

            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excelApp.Workbooks.Open(tmpPath);

            var workSheet = (Microsoft.Office.Interop.Excel.Worksheet) workbook.Worksheets.get_Item(1);
            
            var column = 1;

            do
            {
                column++;
                Attendee element;

                var str =(string) excelApp.Cells[2, column].Value.ToString();
                AttendeeType type;
                if (str == "Абитуриент")
                    type = AttendeeType.Pupil;
               else if (str == "Родитель")
                    type = AttendeeType.Parent;
               else if (str == "Учитель")
                    type = AttendeeType.Teacher;
                else continue;

                if (type == AttendeeType.Pupil)
                {
                    int id;
                    if (!int.TryParse((string) excelApp.Cells[7, column].Value.ToString(), out id))
                        continue;

                    var school = new NHRepository<School>().Get(id);
                    
                    str = (string)excelApp.Cells[5, column].Value.ToString();
                    Sex sex;
                    if (str == "мужской" || str == "м")
                        sex = Sex.Male;
                    else if (str == "женский" || str == "ж")
                        sex = Sex.Female;
                    else continue;

                    int year;
                    if (!int.TryParse((string)excelApp.Cells[6, column].Value.ToString(), out year))
                        continue;

                    element = new Pupil()
                    {
                        Sex= sex,
                        School = school,
                        YearOfGraduation = year,
                    };
                }
                else
                {
                    element = new Attendee();
                }

                element.ContactInfo.FullName = (string) excelApp.Cells[1, column].Value.ToString();

                var email = (string) excelApp.Cells[4, column].Value.ToString();
                if (!email.IsCorrectEmail())
                    continue;

                var phone = (string)excelApp.Cells[3, column].Value.ToString();
                if (!phone.IsCorrectPhone())
                    continue;

                element.ContactInfo.Email = email;
                element.ContactInfo.PhoneNumber = phone;

                yield return element;

            } while (excelApp.Cells[1, column+1].Value!=null);


            excelApp.Workbooks.Close();
            excelApp.Quit();
            Marshal.ReleaseComObject(workSheet);
            Marshal.ReleaseComObject(workbook);
            
            System.IO.File.Delete(tmpPath);
            
        }

    }
}