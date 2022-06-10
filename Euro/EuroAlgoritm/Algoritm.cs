using System;
using System.Collections.Generic;

namespace EuroAlgoritm
{
	class Algoritm
	{
		public List<MainData> mainDatas = new List<MainData>();
		public Map map = new Map();
		public void Initialization()
		{
			ParseData parseData = new ParseData();
			parseData.ParseFile();
			parseData.CheckCaseCount();
			parseData.PutData();
			this.mainDatas = parseData.mainDatas;
		}
		public void CoinsTransfer()
		{
			foreach (MainData data in mainDatas)
			{
				if (data.CountryCount > 1)
                {
					while (true)
					{
						//Console.WriteLine($"--------Day {mainDatas[indDatas].AllDays}----------");
						foreach (Country country in data.countries)
						{
							foreach (City city in country.cities)
							{
								city.OperationDay();
								city.CheckFlagCity();
							}
							country.CheckFlagCountry();
							if (country.ReadyFlagCountry == false)
                            {
								country.Days++;
                            }
						}
						foreach (Country country in data.countries)
                        {
							country.EndDay();
                        }
						data.AllDays++;
						data.CheckAllReadyCase();
						if (data.ReadyCase == true)
                        {
							break;
                        }
					}
                }
			}
		}
		public void Output()
        {
			int indDatas = 1;
			foreach (MainData data in mainDatas)
            {
				data.countries.Sort();
				Console.WriteLine($"Case Number {indDatas}");
				foreach (Country country in data.countries)
                {
					Console.WriteLine($"\t{country.CountryName} " +
						$"{country.Days}");
                }
				indDatas++;
			}
		}
	}
}
