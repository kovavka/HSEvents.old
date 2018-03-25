using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Domain;
using Domain.Events;
using Domain.IEntity;
using Infrastructure;
using Infrastructure.Repositories;

namespace HSEvents.Web.Api
{
    public class AttendeeNHController : ApiController
    {
        protected readonly IGetAllRepository<Attendee> repository;

        public AttendeeNHController(IGetAllRepository<Attendee> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<Attendee> GetAll()
        {
            return repository.GetAll();
        }

        [HttpGet]
        public IEnumerable<Attendee> GetAllByParams(bool attendeed, bool registered, bool currentYearGraduate,
            bool entered, bool participated)
        {
            IEnumerable<Attendee> result;

            if (participated)
                result = new NHGetAllRepository<AttendanceInfo>().GetAll().Select(x => x.Attendee);
            else if (attendeed)
                result = new NHGetAllRepository<AttendanceInfo>().GetAll().Where(x => x.Participated).Select(x => x.Attendee);
            else
                result = repository.GetAll();

            if (!currentYearGraduate && !entered && !participated)
                return result;

            var pupils = result.Where(x => x.Type == AttendeeType.Pupil).Cast<Pupil>();

            if (currentYearGraduate)
                pupils = pupils.Where(x => x.YearOfGraduation == DateTime.Today.Year);

            if (entered)
                pupils = pupils.Where(x => x.EnterProgram != null);

            if (registered)
                pupils = pupils.Where(x => x.RegistrarionPrograms.Any());

            return pupils;
        }
    }
}