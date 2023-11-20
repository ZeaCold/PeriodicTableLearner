namespace ZC.PeriodicTableLearner.Resources.Models
{
    /// <summary>
    /// Element model class
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Atomic number of the element
        /// </summary>
        public int AtomicNumber {  get; set; }

        /// <summary>
        /// Name of the element
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Symbol of the element
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Atomic weight of the element
        /// </summary>
        public double AtomicWeight { get; set; }

        /// <summary>
        /// Electro-negativity of the element
        /// </summary>
        public double ElectroNegativity {  get; set; }

        /// <summary>
        /// Chemical group of the element
        /// </summary>
        public ChemicalGroup ChemicalGroup => ChemicalGroup.GetChemicalGroupForElement(this);

        /// <summary>
        /// Constructor of the element
        /// </summary>
        /// <param name="atomicNumber">Atomic number of the element</param>
        /// <param name="name">Name of the element</param>
        /// <param name="symbol">Symbol of the element</param>
        /// <param name="atomicWeight">Atomic weight of the element</param>
        /// <param name="electroNegativity">Electro-negativity of the element</param>
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
