using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atmosphere_prj
{
    abstract class Layer
    {
        protected double thickness;
        protected char type;

        public double getThickness { get => thickness; }
        public char getType { get => type; }
        protected Layer(char ch, double e = 0) { type = ch; thickness = e; }
        public void ModifyThickness(double e) { thickness += e; }


        public abstract Layer Simulate(IAtmosphericVariable atmosphericVariable);
    }

    // class of ozone
    class Ozone : Layer
    {
        public Ozone(char ch, double e = 0) : base(ch, e) { }
        public override Layer Simulate(IAtmosphericVariable atmosphericVariable)
        {
            return atmosphericVariable.ChangeZ(this);
        }
    }

    // class of oxygen
    class Oxygen : Layer
    {
        public Oxygen(char ch, double e = 0) : base(ch, e) { }
        public override Layer Simulate(IAtmosphericVariable atmosphericVariable)
        {
            return atmosphericVariable.ChangeX(this);
        }
    }

    // class of Carbon Dioxide
    class CarbonDioxide : Layer
    {
        public CarbonDioxide(char ch, double e = 0) : base(ch, e) { }
        public override Layer Simulate(IAtmosphericVariable atmosphericVariable)
        {
            return atmosphericVariable.ChangeC(this);
        }
    }
}
