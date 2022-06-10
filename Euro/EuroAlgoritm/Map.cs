using System;

namespace EuroAlgoritm
{
    class Map
    {
        private void InitializingArray(City[][] used_coord)
        {
            for (int k = 0; k < 11; k++)
            {
                used_coord[k] = new City[11];
            }
            for (int i = 0; i < used_coord.Length; i++)
            {
                for (int j = 0; j < used_coord[i].Length; j++)
                {
                    City city = new City(i, j, 0);
                    used_coord[i][j] = city;
                }
            }
        }
        public City[][] PutCountryInMap(MainData mainData)
        {
            City[][] used_coord = new City[11][];
            int tempX;
            int tempY;

            InitializingArray(used_coord);
            foreach(Country country in mainData.countries)
            {
                foreach(City city in country.cities)
                {
                    tempX = city.x;
                    tempY = city.y;
                    if (used_coord[tempX][tempY].NumberOfCityInMap != 0)
                    {
                        Console.WriteLine("Error - country is imposed on each other");
                        Environment.Exit(0);
                    }
                    else
                    {
                        used_coord[tempX][tempY] = city;
                    }
                }
            }
            return used_coord;
        }
        public void ShowMap(City[][] used_coord)
        {
            foreach (var row in used_coord)
            {
                foreach (var x in row)
                {
                    Console.Write($"{x} ");
                }
                Console.WriteLine();
            }
        }
    }
}
