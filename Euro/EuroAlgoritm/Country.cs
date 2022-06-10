using System;
using System.Collections.Generic;
using System.Linq;

namespace EuroAlgoritm
{
    class Country : IComparable<Country>
    {
        public int CityCount { get; private set; }
        public string[] CountryData { get; set; }
        public int[] CountryCoord { get; private set; } = new int[4];
        public string CountryName { get; set; }
        public  List<City> cities = new List<City>();
        public int NumberOfCountryInMap;
        public int ForeignCount;
        public bool ReadyFlagCountry;
        public int Days = 1;

        public Country(string[] CountryData, int NumberOfCountryInMap, int ForeignCount)
        {
            this.CountryData = CountryData;
            this.NumberOfCountryInMap = NumberOfCountryInMap;
            this.ForeignCount = ForeignCount;
        }
        public void ParseDataCountry()
        {
            CountryName = CountryData[0];
            int j = 1;
            for (int i = 0; i < CountryCoord.Length; i++, j++)
            {
                if(!int.TryParse(CountryData[j], out CountryCoord[i]))
                {
                    Console.WriteLine("Error - coordinates must be contain only numbers");
                    Environment.Exit(0);
                }
                if (CountryCoord[i] > 10 || CountryCoord[i] < 1)
                {
                    Console.WriteLine("Error - coordinates must be from 1 to 10");
                    Environment.Exit(0);
                }
            }
        }
        public void AddCityInCountry()
        {
            int xL = CountryCoord[0];
            int yL = CountryCoord[1];
            int xH = CountryCoord[2];
            int yH = CountryCoord[3];
            int tempXL = xL;
            while (yL <= yH)
            {
                xL = tempXL;
                while (xL <= xH)
                {
                    City city = new City(xL, yL, NumberOfCountryInMap);
                    city.ForeignCountryCount = ForeignCount + 1;
                    city.InitializingBalance();
                    cities.Add(city);
                    xL++;
                    CityCount++;
                }
                yL++;
            }
        }
        public void CheckFlagCountry()
        {
            int i = 0;
            bool changed = false;

            while (i < cities.Count)
            {
                if (!cities[i].ReadyFlagCity)
                {
                    ReadyFlagCountry = false;
                    changed = true;
                }
                i++;
            }
            if (!changed)
            {
                ReadyFlagCountry = true;
            }
        }
        public void EndDay()
        {
            for (int i = 0; i < cities.Count; i++)
            {
                for (int j = 0; j < cities[i].Balance.Length; j++)
                {
                    cities[i].Balance[j] = cities[i].TomorrowBalance[j];
                }
            }
        }
        public void CheckBorders()
        {
            int tmpBorders = 0;
            for (int i = 0; i < cities.Count; i++)
            {
                cities[i].FindBorders();
                tmpBorders += cities[i].BorderCity;
            }
            if (tmpBorders == 0)
            {
                Console.WriteLine("Error - country has no border towns");
                Environment.Exit(0);
            }
        }
        public int CompareTo(Country item)
        {
            if (item is null)
            {
                throw new ArgumentException("Error - incorrect sorting");
            }
            if (Days == item.Days)
            {
                return string.Compare(this.CountryName, item.CountryName);
            }
            return Days - item.Days;
        }
    }
}

