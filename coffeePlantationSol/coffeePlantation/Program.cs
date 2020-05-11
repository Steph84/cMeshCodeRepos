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
        int GrowthTime = 10;
        int BeanStock = 1;

        for (int day = 1; day <= TotDaysAvail; day++) // 56 = 2 seasons of 28 days
        {
            // check the plantation if we can harvest
            foreach (CoffeePlant plant in Plantation)
            {
                plant.Age++; // aging
                if (plant.Age >= 10) // if mature
                {
                    if (IsEven(plant.Age)) // if even age
                    {
                        plant.Production = plant.Production + 4;
                        BeanStock = BeanStock + 4;
                    }
                }
            }

            // if we have bean in stock AND if we have more than 10 days before the end, then plant it
            if (BeanStock > 0 && day < TotDaysAvail - GrowthTime)
            {
                for (int bean = 1; bean <= BeanStock; bean++)
                {
                    CoffeePlant tempPlant = new CoffeePlant();
                    Plantation.Add(tempPlant);
                }
                BeanStock = 0;
            }
        }

        Console.ReadKey();
    }

    // if the number is even (pair)
    static public bool IsEven(int value)
    {
        return value % 2 == 0;
    }
}

public class CoffeePlant
{
    public int Age { get; set; }
    public int Production { get; set; }
}
