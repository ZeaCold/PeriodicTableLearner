using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZC.PeriodicTableLearner.Resources.Models
{
    public class Element
    {
        public int AtomicNumber {  get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public double AtomicWeight { get; set; }

        public double Electronegativity {  get; set; }

        public Element(int atomicNumber, string name, string symbol, double atomicWeight, double electronegativity) 
        {
            AtomicNumber = atomicNumber;
            Name = name;
            Symbol = symbol;
            AtomicWeight = atomicWeight;
            Electronegativity = electronegativity;
        }
    }
}
