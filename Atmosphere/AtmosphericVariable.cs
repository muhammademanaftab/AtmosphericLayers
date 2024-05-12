using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFile;


namespace Atmosphere_prj
{
    interface IAtmosphericVariable
    {
        Layer ChangeZ(Ozone p);
        Layer ChangeX(Oxygen p);
        Layer ChangeC(CarbonDioxide p);
    }


    // class of thunderstorm
    class Thunderstorm : IAtmosphericVariable
    {

        public Layer ChangeZ(Ozone p)
        {
            return null;
        }
        public Layer ChangeX(Oxygen p)
        {
            double thickness = p.getThickness * 0.50;
            p.ModifyThickness(-1.0 * thickness);
            return new Ozone('Z', thickness);
        }
        public Layer ChangeC(CarbonDioxide p)
        {
            return null;
        }
        private Thunderstorm() { }

        private static Thunderstorm instance = null;
        public static Thunderstorm Instance()
        {
            if (instance == null)
            {
                instance = new Thunderstorm();
            }
            return instance;
        }
    }


    // class of sunshine
    class Sunshine : IAtmosphericVariable
    {

        public Layer ChangeZ(Ozone p)
        {
            return null;
        }
        public Layer ChangeX(Oxygen p)
        {
            double thickness = p.getThickness * 0.05;
            p.ModifyThickness(-1.0 * thickness);
            return new Ozone('Z', thickness);
        }
        public Layer ChangeC(CarbonDioxide p)
        {
            double thickness = p.getThickness * 0.05;
            p.ModifyThickness(-1.0 * thickness);
            return new Oxygen('X', thickness);
        }
        private Sunshine() { }

        private static Sunshine instance = null;
        public static Sunshine Instance()
        {
            if (instance == null)
            {
                instance = new Sunshine();
            }
            return instance;
        }
    }

    // class of Others
    class Others : IAtmosphericVariable
    {

        public Layer ChangeZ(Ozone p)
        {
            double thickness = p.getThickness * 0.05;
            p.ModifyThickness(-1.0 * thickness);
            return new Oxygen('X', thickness);
        }
        public Layer ChangeX(Oxygen p)
        {
            double thickness = p.getThickness * 0.10;
            p.ModifyThickness(-1.0 * thickness);
            return new CarbonDioxide('C', thickness);
        }
        public Layer ChangeC(CarbonDioxide p)
        {
            return null;
        }
        private Others() { }

        private static Others instance = null;
        public static Others Instance()
        {
            if (instance == null)
            {
                instance = new Others();
            }
            return instance;
        }
    }

}
