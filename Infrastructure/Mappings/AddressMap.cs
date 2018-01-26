using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Events;

namespace Infrastructure.Mappings
{
    class AddressMap : EntityMap<Address>
    {
        public AddressMap()
        {
            References(x => x.House).Cascade.SaveUpdate().Cascade.Delete().ForeignKey("FK_Address_House");
        }
    }
    class HouseMap : NamedEntityMap<House>
    {
        public HouseMap()
        {
            References(x => x.Street).Cascade.SaveUpdate().Cascade.Delete().ForeignKey("FK_House_Street");
        }
    }
    class StreetMap : NamedEntityMap<Street>
    {
        public StreetMap()
        {
            References(x => x.City).Cascade.SaveUpdate().Cascade.Delete().ForeignKey("FK_Street_City");
        }
    }
    class CityMap : NamedEntityMap<City>
    {
        public CityMap()
        {
            References(x => x.CityType).Cascade.SaveUpdate().Cascade.Delete().ForeignKey("FK_City_CityType");
        }
    }
    class CityTypeMap : NamedEntityMap<CityType>
    {
        public CityTypeMap()
        {
            References(x => x.Region).Cascade.SaveUpdate().Cascade.Delete().ForeignKey("FK_CityType_Region");
        }
    }
    class RegionMap : NamedEntityMap<Region>
    {
        public RegionMap()
        {
            References(x => x.Country).Cascade.SaveUpdate().Cascade.Delete().ForeignKey("FK_Region_Country");
        }
    }
    class CountryMap : NamedEntityMap<Country>
    {
        public CountryMap()
        {
        }
    }
}
