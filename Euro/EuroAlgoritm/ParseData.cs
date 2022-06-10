using System;
using System.Collections.Generic;
using System.IO;

namespace EuroAlgoritm
{
	class ParseData
	{
		public int CaseCount { get; set; }
		private string[] InputData;
		public List<MainData> mainDatas { get; set; } = new List<MainData>();
		private static int i = 0;
		private char[] nums = {'1', '2', '3', '4',
					'0', '5', '6', '7', '8', '9'};
		
	   
		private void PutInputData(ref MainData Data)
		{
			int CountryCount;

			if (int.TryParse((InputData[i]), out CountryCount))
            {
				Data.CountryCount = CountryCount;
            }
			else
			{
				Console.WriteLine("Error - incorrect case number");
				Environment.Exit(0);
			}
			CheckCaseNumber(i);
			i++;
			for (int tmp = 0; tmp < Data.CountryCount; tmp++)
			{
				string[] CountryData = InputData[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
				Country newCountry = new Country(CountryData, tmp + 1, CountryCount);
				newCountry.ParseDataCountry();
				newCountry.AddCityInCountry();
				Data.countries.Add(newCountry);
				i++;
			}
		}
		public void PutData()
		{
			for (int i = 0; i < CaseCount; i++)
			{
				Map DataInMap = new Map();
				MainData Data = new MainData();

				PutInputData(ref Data);
				Data.CountryMap = DataInMap.PutCountryInMap(Data);
				foreach (Country country in Data.countries)
				{
					foreach (City city in country.cities)
                    {
						city.FindNeighbours(Data.CountryMap);
                    }
					if (country.ForeignCount > 1)
                    {
						country.CheckBorders();
                    }
					else
                    {
						country.Days = 0;
                    }
				}
				mainDatas.Add(Data);
			}
		}
		public void CheckCaseCount()
		{
			int i;

			i = 0;
			foreach (string str in InputData)
			{
				if (Char.IsNumber(str, 0))
				{
					if (str[0] == '0')
                    {
						Console.WriteLine(" ");
                    }
					else
                    {
						CaseCount++;
                    }
				}
				i++;
			}
		}
		public void CheckCaseNumber(int ind)
        {
			int tempNum = Convert.ToInt32(InputData[ind][0]) + 1 - 48 + ind;
			if (tempNum < InputData.Length)
            {
				if (!char.IsDigit(InputData[tempNum][0]))
				{
					Console.WriteLine("Error - incorrect case count");
					Environment.Exit(0);
				}
            }
        }
		public void ParseFile()
		{
			InputData = File.ReadAllLines(@"..\..\..\Euro.in");
		}
	}
}
