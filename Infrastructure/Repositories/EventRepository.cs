using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Events;
using NHibernate;

namespace Infrastructure.Repositories
{
    public class EventRepository: IRepository<Event>
    {
        private readonly CourseRepository courseRepository;
        private readonly AcademicСompetitionRepository academicСompetitionRepository;
        private readonly SchoolWorkRepository schoolWorkRepository;

        public EventRepository()
        {
            courseRepository = new CourseRepository();
            academicСompetitionRepository = new AcademicСompetitionRepository();
            schoolWorkRepository = new SchoolWorkRepository();
        }

        public Event Get(int id)
        {
            var course = courseRepository.Get(id);
            if (course != null)
                return course;

            var academicСompetition = academicСompetitionRepository.Get(id);
            if (academicСompetition != null)
                return academicСompetition;

            return schoolWorkRepository.Get(id);
        }

        public IQueryable<Event> GetAll()
        {
            return new NHGetAllRepository<Event>().GetAll();
                
                //courseRepository.GetAll()
                //.OfType<Event>()
                //.Union(academicСompetitionRepository.GetAll())
                //.Union(schoolWorkRepository.GetAll());
        }

        public void Delete(Event entity)
        {
            //Todo: а надо ли вообще эти методы делать универсальными?

            if (entity == null)
                return;

            var course = entity as Course;
            if (course != null)
                courseRepository.Delete(course);

            var academicСompetition = entity as AcademicСompetition;
            if (academicСompetition != null)
                academicСompetitionRepository.Delete(academicСompetition);

            var courschoolWorkse = entity as SchoolWork;
            if (courschoolWorkse != null)
                schoolWorkRepository.Delete(courschoolWorkse);
        }

        public void Update(Event entity)
        {
            //ToDo
        }


        public void Delete(int id)
        {
            //ToDo
        }
        public object Add(Event entity)
        {
            //ToDo
            return 0;
        }

    }

    public class CourseRepository : NHRepository<Course>
    {
    }
    public class AcademicСompetitionRepository : NHRepository<AcademicСompetition>
    {
    }
    public class SchoolWorkRepository : NHRepository<SchoolWork>
    {
    }
}
