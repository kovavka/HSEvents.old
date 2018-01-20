using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IEntity;

namespace Domain
{
    public class Address: Entity
    {
        public virtual HouseDto House { get; set; }
        
        public virtual CountryDto Country
        {
            get { return Region.Country; }
        }

        public virtual RegionDto Region
        {
            get { return CityType.Region; }
        }

        public virtual CityTypeDto CityType
        {
            get { return City.CityType; }
        }

        public virtual CityDto City
        {
            get { return Street.City; }
        }

        public virtual StreetDto Street
        {
            get { return House.Street; }
        }
        
        public virtual string FullAddress
        {
            get
            {
                return string.Format("{0}, {1}, {2} {3}, {4}, {5}",
                    Country.Name,
                    Region.Name,
                    CityType.Name,
                    City.Name,
                    Street.Name,
                    House.Name);
            }
        }
    }


    public class CountryDto : Country
    {
    }
    public class RegionDto : Region
    {
        public virtual CountryDto Country { get; set; }
    }
    public class CityTypeDto : CityType
    {
        public virtual RegionDto Region { get; set; }
    }
    public class CityDto : City
    {
        public virtual CityTypeDto CityType { get; set; }
    }
    public class StreetDto : Street
    {
        public virtual CityDto City { get; set; }
    }
    public class HouseDto : House
    {
        public virtual StreetDto Street { get; set; }
    }



    public class Country : NamedEntity
    {
    }
    public class Region : NamedEntity
    {
    }
    public class City : NamedEntity
    {
    }
    public class CityType : NamedEntity
    {
    }
    public class Street : NamedEntity
    {
    }
    public class House : NamedEntity
    {
    }

}
