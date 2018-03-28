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
using HSEvents.Web.Authentification;
using Infrastructure.Repositories;

namespace HSEvents.Web.Controllers
{
    [Auth]
    public class AttendeesController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["Attendees"]= new NHGetAllRepository<Attendee>().GetAll().ToList();
            return View();
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var attendee = new NHRepository<Attendee>().Get(id);
            if (attendee.Type == AttendeeType.Pupil)
            {
                List<School> schools;
                using (var repository = new NHGetAllRepository<School>())
                {
                    schools = repository.GetAll().ToList();
                }

                ViewData["Schools"] = new SelectList(schools, "Id", "Name");

                List<AcademicProgram> programs;
                using (var repository = new NHGetAllRepository<AcademicProgram>())
                {
                    programs = repository.GetAll().ToList();
                }

                ViewData["Programs"] = new SelectList(programs, "Id", "Name");

                var list = new AcademicProgram { Id = -1, Name = "не выбрано" }.AsEnumerable().Concat(programs).ToList();
                ViewData["NullablePrograms"] = new SelectList(list, "Id", "Name");


                var pupil = attendee as Pupil;
                
                return View("EditPupil", pupil);
            }
            
            return View(attendee);
        }

        [HttpPost]
        public ActionResult EditPupil(Pupil pupil, int school, IEnumerable<int> intrestingPrograms,
            IEnumerable<int> registrarionPrograms, int enterProgram)
        {
            List<School> schools;
            using (var repository= new NHGetAllRepository<School>())
            {
                schools= repository.GetAll().ToList();
            }

            ViewData["Schools"] = new SelectList(schools, "Id", "Name");

            List<AcademicProgram> programs;
            using (var repository = new NHGetAllRepository<AcademicProgram>())
            {
                programs = repository.GetAll().ToList();
            }
            
            ViewData["Programs"] = new SelectList(programs, "Id", "Name");

            var list = new AcademicProgram {Id = -1, Name = "не выбрано"}.AsEnumerable().Concat(programs).ToList();
             ViewData["NullablePrograms"] = new SelectList(list, "Id", "Name");

            IsValid = true;

            if (pupil.ContactInfo.FullName.IsNullOrEmpty())
                AddModelError("FullName", "Введите ФИО");
            if (!pupil.ContactInfo.Email.IsCorrectEmail())
                AddModelError("Email", "Неверный формат Email");
            if (!pupil.ContactInfo.PhoneNumber.IsCorrectPhone())
                AddModelError("PhoneNumber", "Неверный формат номера");
            if (pupil.YearOfGraduation <= 2010 || pupil.YearOfGraduation >= 2030)
                AddModelError("Year", "Неверный формат года");

            if (!IsValid)
                return View();
            

            new PupilRepository().Update(pupil, school, intrestingPrograms, registrarionPrograms, enterProgram);

            return RedirectToAction("Index");
        }

        private bool IsValid;
        private void AddModelError(string key, string message)
        {
            ModelState.AddModelError(key, message);
            IsValid = false;
        }

        [HttpPost]
        public ActionResult Edit(Attendee attendee)
        {
            if (attendee.ContactInfo.FullName.IsNullOrEmpty())
                ModelState.AddModelError("FullName", "Введите ФИО");
            if (!attendee.ContactInfo.Email.IsCorrectEmail())
                ModelState.AddModelError("Email", "Неверный формат Email");
            if (!attendee.ContactInfo.PhoneNumber.IsCorrectPhone())
                ModelState.AddModelError("PhoneNumber", "Неверный формат номера");

            if (!ModelState.IsValid)
                return View();
            
            new NHRepository<Attendee>().Update(attendee);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Add()
        {
            List<School> schools;
            using (var repository = new NHGetAllRepository<School>())
            {
                schools = repository.GetAll().ToList();
            }

            ViewData["Schools"] = new SelectList(schools, "Id", "Name");

            List<AcademicProgram> programs;
            using (var repository = new NHGetAllRepository<AcademicProgram>())
            {
                programs = repository.GetAll().ToList();
            }

            ViewData["Programs"] = new SelectList(programs, "Id", "Name");

            var list = new AcademicProgram {Id = -1, Name = "не выбрано"}.AsEnumerable().Concat(programs).ToList();
            ViewData["NullablePrograms"] = new SelectList(list, "Id", "Name");

            return View();
        }

        public ActionResult Delete(int id)
        {
            new NHRepository<Attendee>().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Add(Pupil pupil, int school, IEnumerable<int> intrestingPrograms, IEnumerable<int> registrarionPrograms, int enterProgram)
        {
            List<School> schools;
            using (var repository = new NHGetAllRepository<School>())
            {
                schools = repository.GetAll().ToList();
            }

            ViewData["Schools"] = new SelectList(schools, "Id", "Name");

            List<AcademicProgram> programs;
            using (var repository = new NHGetAllRepository<AcademicProgram>())
            {
                programs = repository.GetAll().ToList();
            }
            ViewData["Programs"] = new SelectList(programs, "Id", "Name");

            var list = new AcademicProgram { Id = -1, Name = "не выбрано" }.AsEnumerable().Concat(programs).ToList();
            ViewData["NullablePrograms"] = new SelectList(list, "Id", "Name");

            IsValid = true;

            if (pupil.ContactInfo.FullName.IsNullOrEmpty())
                AddModelError("FullName", "Введите ФИО");
            if (!pupil.ContactInfo.Email.IsCorrectEmail())
                AddModelError("Email", "Неверный формат Email");
            if (!pupil.ContactInfo.PhoneNumber.IsCorrectPhone())
                AddModelError("PhoneNumber", "Неверный формат номера");
            if ((pupil.YearOfGraduation<=2010 || pupil.YearOfGraduation >= 2030) && pupil.Type == AttendeeType.Pupil)
                AddModelError("Year", "Неверный формат года");

            if (!IsValid)
                return View();


            if (pupil.Type == AttendeeType.Pupil)
            {
              new  PupilRepository().Save(pupil, school, intrestingPrograms, registrarionPrograms, enterProgram);
            }
            else
            {
                var attendee = new Attendee()
                {
                    ContactInfo = pupil.ContactInfo,
                    Type = pupil.Type,
                };
                new NHRepository<Attendee>().Add(attendee);
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save()
        {
            var data = (IEnumerable<Attendee>)TempData["Import"];

            var repository = new NHRepository<Attendee>();
            foreach (var attendee in data)
            {
                repository.Add(attendee);
            }

            return RedirectToAction("Index");
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

            ViewData["Import"] = ReadFile(file).ToList();
            TempData["Import"] = ViewData["Import"];

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

                    School school;
                    using (var repository = new NHRepository<School>())
                    {
                        school = repository.Get(id);
                    }
                    
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

                element.Type = type;
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