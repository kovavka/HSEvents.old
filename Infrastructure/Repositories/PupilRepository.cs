﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Domain;
using NHibernate;

namespace Infrastructure.Repositories
{
    public class PupilRepository:IDisposable
    {
        private ISession session = NHibernateHelper.OpenSession();

        public void Save(Pupil pupil, int school, IEnumerable<int> intrestingPrograms, IEnumerable<int> registrarionPrograms, int enterProgram)
        {

                pupil.School = session.Get<School>(school);


            if (enterProgram != -1)
            {
                    pupil.EnterProgram = session.Get<AcademicProgram>(enterProgram);
               
            }
            

            pupil.IntrestingPrograms = GetPrograms(intrestingPrograms).ToList();

            pupil.RegistrarionPrograms = GetPrograms(registrarionPrograms).ToList();

            session.Save(pupil);
            session.Flush();
        }

        public void Update(Pupil pupil, int school, IEnumerable<int> intrestingPrograms, IEnumerable<int> registrarionPrograms, int enterProgram)
        {
            pupil.School = session.Get<School>(school);

            if (enterProgram == -1)
                pupil.EnterProgram = null;
            else
            {
                pupil.EnterProgram = session.Get<AcademicProgram>(enterProgram);
            }

            pupil.IntrestingPrograms = GetPrograms(intrestingPrograms).ToList();
            pupil.RegistrarionPrograms = GetPrograms(registrarionPrograms).ToList();

            pupil.Type = AttendeeType.Pupil;

            session.Update(pupil);
            session.Flush();
        }

        private IEnumerable<AcademicProgram> GetPrograms(IEnumerable<int> programs)
        {
            foreach (var program in programs.WithEnumerable())
            {

                yield return session.Get<AcademicProgram>(program);
            }
        }

        public void Dispose()
        {
            session.Close();
        }
    }
}
