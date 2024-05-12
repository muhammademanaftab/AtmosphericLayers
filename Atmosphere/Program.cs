using TextFile;
using System;
using System.Collections.Generic;

namespace Atmosphere_prj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filename = "input.txt";
                TextFileReader reader = new TextFileReader(filename);

                Atmosphere atmosphere = new Atmosphere(ref reader);

                int initialLayers = atmosphere.getLayersCount;
                Console.WriteLine(atmosphere.getState(0));
                for (int i = 1; atmosphere.getLayersCount >= 3 && atmosphere.getLayersCount != 3 * initialLayers; i++)
                {
                    atmosphere.simulate();
                    Console.WriteLine(atmosphere.getState(i));
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Layers Count are zero Please Provide the Layers.");
            }



            

        }
    }
}
