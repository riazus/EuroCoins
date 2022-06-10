using System.Collections.Generic;

namespace EuroAlgoritm
{
    class MainData
    {
        public List<Country> countries = new List<Country>();
        public int CountryCount { get; set; }
        public City[][] CountryMap = new City[10][];
        public bool ReadyCase;
        public int AllDays = 1;

        public void CheckAllReadyCase()
        {
            bool changed = false;
            for (int i = 0; i < countries.Count; i++)
            {
                if (!countries[i].ReadyFlagCountry)
                {
                    ReadyCase = false;
                    changed = true;
                }
            }
            if (!changed)
            {
                ReadyCase = true;
            }
        }
    }
}
