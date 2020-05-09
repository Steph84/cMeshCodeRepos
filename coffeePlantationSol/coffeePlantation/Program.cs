using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Compute coffee plantation in Stardew Valley");
        
        List<CoffeePlant> Plantation = new List<CoffeePlant>();
        int TotDaysAvail = 56;
        int BeanStock = 1;

        for (int day = 1; day <= TotDaysAvail; day++) // 56 = 2 seasons of 28 days
        {
            // if we have bean in stock AND if we have more than 10 days before the end, then plant it
            if (BeanStock > 0 && day < TotDaysAvail - 10)
            {
                for (int bean = 1; bean <= BeanStock; bean++)
                {
                    CoffeePlant tempPlant = new CoffeePlant();
                    Plantation.Add(tempPlant);
                }
                BeanStock = 0;
            }

            // check the plantation if we can harvest
            foreach (CoffeePlant plant in Plantation)
            {
                // check the day

                // harvest and put in the stock

            }
        }

        Console.ReadKey();
    }
}

public class CoffeePlant
{

}
