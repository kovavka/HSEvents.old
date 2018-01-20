using Domain.Events;
using Domain.IEntity;
using FluentNHibernate.Mapping;

namespace Infrastructure.Mappings.Events
{
    class EventMap<T> : NamedEntityMap<T> where T: IEvent
    {
        public EventMap()
        {
            Map(x => x.Comment).Nullable();
            Map(x => x.Info);
            //Map(x => x.Name);
            //Map(x => x.Name);
        }
    }

    class EventsMap : EventMap<Event> 
    {
        public EventsMap()
        {
        }
    }

}
