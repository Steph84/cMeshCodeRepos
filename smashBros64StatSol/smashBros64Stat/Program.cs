using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Compute N64 Smash Bros data");

        //ExtractData MyExtraction = new ExtractData();
        BuildTournament MyTournament = new BuildTournament();


        Console.ReadKey();
    }

    public enum Stage
    {
        Peach, // Château de Peach
        Jungle, // Jungle du Congo
        Hyrule, // Château d'Hyrule
        Zebes, // Planète Zebes
        Yoshi, // Ile Yoshi
        Reves, // Pays des Rêves
        SecteurZ,
        Safrania,
        Champignon, //Royaume Champignon
        None
    }

    public enum Character
    {
        Luigi = 1,
        Mario,
        DonkeyKong,
        Link,
        SamusAran,
        CFalcon,
        Ness,
        Yoshi,
        Kirby,
        Fox,
        Pikachu,
        Rondoudou
    }
}
