using System.Collections.Generic;
using System;
namespace EuroAlgoritm
{
	class City
	{
		public int[] Balance { get; set; }
		public int[] TomorrowBalance { get; set; }
		public int x;
		public int y;
		public int NumberOfCityInMap;
		public int ForeignCountryCount;
		public int BorderCity;
		public List<City> Neighbours { get; } = new List<City>();
		public bool ReadyFlagCity;
		public City(int x, int y, int NumberOfCityInMap)
		{
			this.x = x;
			this.y = y;
			this.NumberOfCityInMap = NumberOfCityInMap;
		}

		public void FindNeighbours(City[][] CityInMap)
		{
			x++;
			if (x < CityInMap[x].Length)
			{
				if (CityInMap[x][y].NumberOfCityInMap != 0)
                {
					Neighbours.Add(CityInMap[x][y]);
                }
			}
			x--;
			y++;
			if (y < 10)
			{
				if (CityInMap[x][y].NumberOfCityInMap != 0)
                {
					Neighbours.Add(CityInMap[x][y]);
                }
			}
			y--;
			x--;
			if (x > 0)
			{
				if (CityInMap[x][y].NumberOfCityInMap != 0)
				{
					Neighbours.Add(CityInMap[x][y]);
				}
			}
			x++;
			y--;
			if (y > 0)
			{
				if (CityInMap[x][y].NumberOfCityInMap != 0)
				{
					Neighbours.Add(CityInMap[x][y]);
				}
			}
			y++;
		}
		public void FindBorders()
        {
			for (int i = 0; i < Neighbours.Count; i++)
            {
				if (Neighbours[i].NumberOfCityInMap != this.NumberOfCityInMap)
                {
					BorderCity++;
                }
			}
        }
		public void InitializingBalance()
        {
			Balance = new int[ForeignCountryCount];
			TomorrowBalance = new int[ForeignCountryCount];
			Balance[0] = 1000000;
			TomorrowBalance[0] = 1000000;
			for (int i = 1; i < Balance.Length; i++)
            {
				Balance[i] = 0;
				TomorrowBalance[i] = 0;
            }
        }
		private int CreditEntry(int[] Balance, List<City> Neighbours)
		{
			int Turnovers = 0;
			for (int i = 0; i < Neighbours.Count; i++)
            {
				Turnovers += Balance[0] / 1000;
            }
			return Turnovers;
		}
		private int DebitEntry(List<City> Neighbours)
		{
			int Turnovers = 0;
			for (int i = 0; i < Neighbours.Count; i++)
			{
				if (Neighbours[i].NumberOfCityInMap == this.NumberOfCityInMap)
                {
					Turnovers += Neighbours[i].Balance[0] / 1000;
                }
				else
                {
					Turnovers += Neighbours[i].Balance[NumberOfCityInMap] / 1000;
                }
			}
			return Turnovers;
		}
		private int[] ForeignCreditEntry(int[] Balance, List<City> Neighbours)
		{
			int[] Turnovers = new int[ForeignCountryCount];
			int i = 0;
			int j;
			while (i < Neighbours.Count)
			{
				for (j = 1; j < ForeignCountryCount; j++)
                {
					Turnovers[j] += Balance[j] / 1000;
                }
				i++;
			}
			return Turnovers;
		}
		private int[] ForeignDebitEntry(List<City> Neighbours)
		{
			int[] Turnovers = new int[ForeignCountryCount];
			int j;
			for (int i = 0; i < Neighbours.Count; i++)
			{
				if (Neighbours[i].NumberOfCityInMap == this.NumberOfCityInMap)
				{
					for (j = 1; j < ForeignCountryCount; j++)
                    {
						Turnovers[j] += Neighbours[i].Balance[j] / 1000;
                    }
				}
				else
				{
					for (j = 1; j < ForeignCountryCount; j++)
					{
                        if (j == NumberOfCityInMap)
                        {
                            j++;
                        }
						if (j == ForeignCountryCount)
                        {
							break;
                        }
						Turnovers[j] += Neighbours[i].Balance[j] / 1000;
                    }
					Turnovers[Neighbours[i].NumberOfCityInMap] += Neighbours[i].Balance[0] / 1000;
				}
			}
			return Turnovers;
		}
		public void OperationDay()
		{
			//Console.WriteLine($"City x:{x} y:{y}");
			int DebitTurnover = DebitEntry(Neighbours);
			int CreditTurnover = CreditEntry(Balance, Neighbours);
			int[] ForeignCreditTurnover = ForeignDebitEntry(Neighbours);
			int[] ForeignDebitTurnover = ForeignCreditEntry(Balance, Neighbours);

			TomorrowBalance[0] += DebitTurnover - CreditTurnover;
			//Console.WriteLine($"{TomorrowBalance[0]} += {DebitTurnover} - {CreditTurnover}");
			for (int i = 1; i < ForeignCountryCount; i++)
			{
				TomorrowBalance[i] += ForeignCreditTurnover[i] - ForeignDebitTurnover[i];
				//Console.WriteLine($"{TomorrowBalance[i]} += {ForeignCreditTurnover[i]} - {ForeignDebitTurnover[i]}");
			}
			//Console.WriteLine("------------------------------");
		}
		public void CheckFlagCity()
		{
			int CityFullness = 0;
			foreach(int i in TomorrowBalance)
            {
				if (i > 0)
                {
					CityFullness++;
                }
            }
			int tempForeignCountryCount = ForeignCountryCount - 1;
			if (CityFullness == tempForeignCountryCount)
            {
				ReadyFlagCity = true;
            }
			else
            {
				ReadyFlagCity = false;
            }
		}
	}
}
