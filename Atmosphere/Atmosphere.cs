using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TextFile;

namespace Atmosphere_prj
{
    public class Atmosphere
    {
        public class LayerCountZero : Exception {  }; 


        private List<Layer> layers;
        private List<IAtmosphericVariable> atmosphericVariables;
        private int layersCount;
        private int atmosphericVariableIndex = 0;

        public int getLayersCount { get => layersCount; }
        public Atmosphere(ref TextFileReader reader)
        {
            populateLayers(reader);
            populateAtmosphericVariables(reader);
        }
        public void simulate()
        {
            for (int i = 0; i < layersCount; i++)
            {
                Layer outputLayer = layers[i].Simulate(atmosphericVariables[atmosphericVariableIndex]);

                if (outputLayer != null)
                {
                    bool absorbed = false;
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (layers[k].getType == outputLayer.getType)
                        {
                            layers[k].ModifyThickness(outputLayer.getThickness);
                            absorbed = true;
                            break;
                        }
                    }

                    if (!absorbed)
                    {
                        if (outputLayer.getThickness >= 0.5)
                        {
                            layers.Insert(0, outputLayer);
                            layersCount++;
                        }
                    }
                }

                if (layers[i].getThickness < 0.5)
                {
                    bool stayed = false;
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (layers[k].getType == layers[i].getType)
                        {
                            layers.Insert(k + 1, layers[i]);
                            layers.RemoveAt(i);
                            stayed = true;
                            break;
                        }
                    }

                    if (!stayed)
                    {
                        layers.RemoveAt(i);
                        layersCount--;

                    }
                }
            }

            atmosphericVariableIndex = (atmosphericVariableIndex + 1) % atmosphericVariables.Count;
        }
        public string getState(int i)
        {
            string output = "";
            output += $"Simulation Round {i}:\n";
            foreach (Layer layer in layers)
            {
                output += $"{layer.getType} {layer.getThickness}\n";
            }

            return output;
        }

        private void populateLayers(TextFileReader reader)
        {
            // Populating layers
            reader.ReadLine(out string line);
            layersCount = int.Parse(line);

            if (layersCount <= 0)
            {
                throw new LayerCountZero();
            }
  
            this.layers = new List<Layer>();
            for (int i = 0; i < layersCount; ++i)
            {
                if (reader.ReadLine(out line))
                {
                    string[] tokens = line.Split(' ');

                    char ch = char.Parse(tokens[0]);
                    double p = double.Parse(tokens[1]);

                    switch (ch)
                    {
                        case 'Z':
                            layers.Add(new Ozone(ch, p));
                            break;
                        case 'X':
                            layers.Add(new Oxygen(ch, p));
                            break;
                        case 'C':
                            layers.Add(new CarbonDioxide(ch, p));
                            break;
                    }
                }
            }
        }
        private void populateAtmosphericVariables(TextFileReader reader)
        {
            // Populating atmospheric variables
            this.atmosphericVariables = new List<IAtmosphericVariable>();
            if (reader.ReadLine(out string variables))
            {

                foreach (char c in variables)
                {
                    switch (c)
                    {
                        case 'T':
                            atmosphericVariables.Add(Thunderstorm.Instance());
                            break;
                        case 'S':
                            atmosphericVariables.Add(Sunshine.Instance());
                            break;
                        case 'O':
                            atmosphericVariables.Add(Others.Instance());
                            break;
                    }
                }
            }
        }


    }
}
