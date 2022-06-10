using System;

namespace EuroAlgoritm
{
    class Program
    {
        static void Main(string[] args)
        {
            Algoritm algoritm = new Algoritm();
            algoritm.Initialization();
            algoritm.CoinsTransfer();
            algoritm.Output();
        }
    }
}
