namespace ZC.PeriodicTableLearner.Resources.Models
{
    public class Element
    {
        public int AtomicNumber {  get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public double AtomicWeight { get; set; }

        public double ElectroNegativity {  get; set; }

        public ChemicalGroup ChemicalGroup => ChemicalGroup.GetChemicalGroupForElement(this);

        public Element(int atomicNumber, string name, string symbol, double atomicWeight, double electroNegativity) 
        {
            AtomicNumber = atomicNumber;
            Name = name;
            Symbol = symbol;
            AtomicWeight = atomicWeight;
            ElectroNegativity = electroNegativity;
        }
    }
}
