using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ZC.PeriodicTableLearner.Resources.Models
{
    /// <summary>
    /// Chemical group model class
    /// </summary>
    public class ChemicalGroup
    {
        /// <summary>
        /// Index of the polyatomic nonmetal group
        /// </summary>
        public const int IDX_POLYATOMIC_NONMETAL = 0;

        /// <summary>
        /// Index of the alkaline metal group
        /// </summary>
        public const int IDX_ALKALIMETAL = 1;

        /// <summary>
        /// Index of the alkaline earth metal group
        /// </summary>
        public const int IDX_ALKALINE_EARTH_METAL = 2;

        /// <summary>
        /// Index of the transition metal group
        /// </summary>
        public const int IDX_TRANSITIONMETAL = 3;

        /// <summary>
        /// Index of the post transition metal group
        /// </summary>
        public const int IDX_POST_TRANSITION_METAL = 4;

        /// <summary>
        /// Index of the metalloid group
        /// </summary>
        public const int IDX_METALLOID = 5;

        /// <summary>
        /// Index of the diatomic nonmetal group
        /// </summary>
        public const int IDX_DIATOMIC_NONMETAL = 6;

        /// <summary>
        /// Index of the noble gas group
        /// </summary>
        public const int IDX_NOBLE_GAS = 7;

        /// <summary>
        /// Index of the lanthanide group
        /// </summary>
        public const int IDX_LANTHANIDE = 8;

        /// <summary>
        /// Index of the actinide group
        /// </summary>
        public const int IDX_ACTINIDE = 9;

        /// <summary>
        /// Index of the unknown group
        /// </summary>
        public const int IDX_UNKNOWN = 10;

        /// <summary>
        /// Name of the chemical group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Color of the group
        /// </summary>
        public SolidColorBrush GroupColor { get; set; }

        /// <summary>
        /// List of elements of the group
        /// </summary>
        public List<Element> Elements { get; set; } = new List<Element>();

        /// <summary>
        /// List of all chemicals groups
        /// </summary>
        public static List<ChemicalGroup> ChemicalGroups { get; set; } = new List<ChemicalGroup> { };

        /// <summary>
        /// Main constructor of the ChemicalGroup
        /// </summary>
        /// <param name="name">Name of the chemical group</param>
        /// <param name="color">Color of the chemical group</param>
        public ChemicalGroup(string name, SolidColorBrush color)
        {
            Name = name;
            GroupColor = color;
        }

        /// <summary>
        /// Add an element to the chemical group
        /// </summary>
        /// <param name="element">The element to add to the chemical group</param>
        public void Add(Element element) => Elements.Add(element);

        /// <summary>
        /// Get the chemical group for a specified element
        /// </summary>
        /// <param name="element">The element to search with</param>
        /// <returns>Returns the chemical group that contain the specified element</returns>
        public static ChemicalGroup GetChemicalGroupForElement(Element element) => ChemicalGroups.FirstOrDefault(chemicalGroup => chemicalGroup.Elements.Contains(element));

        /// <summary>
        /// Get the chemical group with the specified name
        /// </summary>
        /// <param name="name">The name of the chemical group</param>
        /// <returns>Returns the chemical group that corresponds to the specified name</returns>
        public static ChemicalGroup GetChemicalGroupFromName(string name) => ChemicalGroups.FirstOrDefault(chemicalGroup => chemicalGroup.Name.Equals(name));

        /// <summary>
        /// Create all elements and chemicals groups
        /// </summary>
        public static void CreateAll()
        {
            ChemicalGroups.Clear();
            CreateChemicalGroups();
            CreateElements();
        }

        /// <summary>
        /// Create the chemical groups
        /// </summary>
        public static void CreateChemicalGroups()
        {
            ChemicalGroups.Add(new ChemicalGroup("Polyatomic nonmetals", new SolidColorBrush(Color.FromArgb(128, 33, 191, 170))));
            ChemicalGroups.Add(new ChemicalGroup("Alkali Metal", new SolidColorBrush(Color.FromArgb(128, 209, 47, 36))));
            ChemicalGroups.Add(new ChemicalGroup("Alkaline Earth Metal", new SolidColorBrush(Color.FromArgb(128, 252, 78, 3))));
            ChemicalGroups.Add(new ChemicalGroup("Transition Metal", new SolidColorBrush(Color.FromArgb(128, 43, 139, 207))));
            ChemicalGroups.Add(new ChemicalGroup("Post-Transition Metal", new SolidColorBrush(Color.FromArgb(128, 42, 86, 209))));
            ChemicalGroups.Add(new ChemicalGroup("Metalloid", new SolidColorBrush(Color.FromArgb(128, 245, 245, 0))));
            ChemicalGroups.Add(new ChemicalGroup("Diatomic nonmetals", new SolidColorBrush(Color.FromArgb(128, 52, 235, 207))));
            ChemicalGroups.Add(new ChemicalGroup("Noble Gas", new SolidColorBrush(Color.FromArgb(128, 3, 252, 86))));
            ChemicalGroups.Add(new ChemicalGroup("Lanthanide", new SolidColorBrush(Color.FromArgb(128, 113, 44, 209))));
            ChemicalGroups.Add(new ChemicalGroup("Actinide", new SolidColorBrush(Color.FromArgb(128, 209, 146, 44))));
            ChemicalGroups.Add(new ChemicalGroup("Unknown", new SolidColorBrush(Color.FromArgb(128, 180, 179, 181))));
        }

        /// <summary>
        /// Create the elements
        /// </summary>
        public static void CreateElements()
        {
            ChemicalGroups[IDX_DIATOMIC_NONMETAL].Add(new Element(1, "Hydrogen", "H", 1.00784, 2.2));
            ChemicalGroups[IDX_NOBLE_GAS].Add(new Element(2, "Helium", "He", 4.00260, double.NaN));
            ChemicalGroups[IDX_ALKALIMETAL].Add(new Element(3, "Lithium", "Li", 6.9380, 0.98));
            ChemicalGroups[IDX_ALKALINE_EARTH_METAL].Add(new Element(4, "Beryllium", "Be", 9.01218, 1.57));
            ChemicalGroups[IDX_METALLOID].Add(new Element(5, "Boron", "B", 10.81, 2.04));
            ChemicalGroups[IDX_POLYATOMIC_NONMETAL].Add(new Element(6, "Carbon", "C", 12.011, 2.55));
            ChemicalGroups[IDX_DIATOMIC_NONMETAL].Add(new Element(7, "Nitrogen", "N", 14.007, 3.04));
            ChemicalGroups[IDX_DIATOMIC_NONMETAL].Add(new Element(8, "Oxygen", "O", 15.999, 3.44));
            ChemicalGroups[IDX_DIATOMIC_NONMETAL].Add(new Element(9, "Fluorine", "F", 18.998, 3.98));
            ChemicalGroups[IDX_NOBLE_GAS].Add(new Element(10, "Neon", "Ne", 20.180, double.NaN));
            ChemicalGroups[IDX_ALKALIMETAL].Add(new Element(11, "Sodium", "Na", 22.990, 0.93));
            ChemicalGroups[IDX_ALKALINE_EARTH_METAL].Add(new Element(12, "Magnesium", "Mg", 24.305, 1.31));
            ChemicalGroups[IDX_POST_TRANSITION_METAL].Add(new Element(13, "Aluminum", "Al", 26.982, 1.61));
            ChemicalGroups[IDX_METALLOID].Add(new Element(14, "Silicon", "Si", 28.085, 1.90));
            ChemicalGroups[IDX_POLYATOMIC_NONMETAL].Add(new Element(15, "Phosphorus", "P", 30.974, 2.19));
            ChemicalGroups[IDX_POLYATOMIC_NONMETAL].Add(new Element(16, "Sulfur", "S", 32.06, 2.58));
            ChemicalGroups[IDX_DIATOMIC_NONMETAL].Add(new Element(17, "Chlorine", "Cl", 35.453, 3.16));
            ChemicalGroups[IDX_NOBLE_GAS].Add(new Element(18, "Argon", "Ar", 39.948, double.NaN));
            ChemicalGroups[IDX_ALKALIMETAL].Add(new Element(19, "Potassium", "K", 39.098, 0.82));
            ChemicalGroups[IDX_ALKALINE_EARTH_METAL].Add(new Element(20, "Calcium", "Ca", 40.078, 1.00));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(21, "Scandium", "Sc", 44.956, 1.36));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(22, "Titanium", "Ti", 47.867, 1.54));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(23, "Vanadium", "V", 50.942, 1.63));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(24, "Chromium", "Cr", 51.996, 1.66));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(25, "Manganese", "Mn", 54.938, 1.55));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(26, "Iron", "Fe", 55.845, 1.83));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(27, "Cobalt", "Co", 58.933, 1.88));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(28, "Nickel", "Ni", 58.693, 1.91));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(29, "Copper", "Cu", 63.546, 1.90));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(30, "Zinc", "Zn", 65.38, 1.65));
            ChemicalGroups[IDX_POST_TRANSITION_METAL].Add(new Element(31, "Gallium", "Ga", 69.723, 1.81));
            ChemicalGroups[IDX_METALLOID].Add(new Element(32, "Germanium", "Ge", 72.630, 2.01));
            ChemicalGroups[IDX_METALLOID].Add(new Element(33, "Arsenic", "As", 74.922, 2.18));
            ChemicalGroups[IDX_POLYATOMIC_NONMETAL].Add(new Element(34, "Selenium", "Se", 78.971, 2.55));
            ChemicalGroups[IDX_DIATOMIC_NONMETAL].Add(new Element(35, "Bromine", "Br", 79.904, 2.96));
            ChemicalGroups[IDX_NOBLE_GAS].Add(new Element(36, "Krypton", "Kr", 83.798, double.NaN));
            ChemicalGroups[IDX_ALKALIMETAL].Add(new Element(37, "Rubidium", "Rb", 85.468, 0.82));
            ChemicalGroups[IDX_ALKALINE_EARTH_METAL].Add(new Element(38, "Strontium", "Sr", 87.620, 0.95));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(39, "Yttrium", "Y", 88.906, 1.22));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(40, "Zirconium", "Zr", 91.224, 1.33));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(41, "Niobium", "Nb", 92.906, 1.6));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(42, "Molybdenum", "Mo", 95.950, 2.16));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(43, "Technetium", "Tc", 98.000, double.NaN));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(44, "Ruthenium", "Ru", 101.07, 2.2));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(45, "Rhodium", "Rh", 102.91, 2.28));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(46, "Palladium", "Pd", 106.42, 2.20));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(47, "Silver", "Ag", 107.87, 1.93));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(48, "Cadmium", "Cd", 112.41, 1.69));
            ChemicalGroups[IDX_POST_TRANSITION_METAL].Add(new Element(49, "Indium", "In", 114.82, 1.78));
            ChemicalGroups[IDX_POST_TRANSITION_METAL].Add(new Element(50, "Tin", "Sn", 118.71, 1.96));
            ChemicalGroups[IDX_METALLOID].Add(new Element(51, "Antimony", "Sb", 121.76, 2.05));
            ChemicalGroups[IDX_METALLOID].Add(new Element(52, "Tellurium", "Te", 127.60, 2.1));
            ChemicalGroups[IDX_DIATOMIC_NONMETAL].Add(new Element(53, "Iodine", "I", 126.90, 2.66));
            ChemicalGroups[IDX_NOBLE_GAS].Add(new Element(54, "Xenon", "Xe", 131.29, 2.6));
            ChemicalGroups[IDX_ALKALIMETAL].Add(new Element(55, "Cesium", "Cs", 132.91, 0.79));
            ChemicalGroups[IDX_ALKALINE_EARTH_METAL].Add(new Element(56, "Barium", "Ba", 137.33, 0.89));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(57, "Lanthanum", "La", 138.90, 1.10));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(58, "Cerium", "Ce", 140.12, 1.12));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(59, "Praseodymium", "Pr", 140.91, 1.13));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(60, "Neodymium", "Nd", 144.24, 1.14));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(61, "Promethium", "Pm", 145.00, double.NaN));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(62, "Samarium", "Sm", 150.36, 1.17));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(63, "Europium", "Eu", 152.00, 1.20));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(64, "Gadolinium", "Gd", 157.25, 1.20));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(65, "Terbium", "Tb", 158.93, 1.20));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(66, "Dysprosium", "Dy", 162.50, 1.22));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(67, "Holmium", "Ho", 164.93, 1.23));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(68, "Erbium", "Er", 167.26, 1.24));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(69, "Thulium", "Tm", 168.93, 1.25));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(70, "Ytterbium", "Yb", 173.05, 1.10));
            ChemicalGroups[IDX_LANTHANIDE].Add(new Element(71, "Lutetium", "Lu", 175.00, 1.27));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(72, "Hafnium", "Hf", 178.49, 1.30));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(73, "Tantalum", "Ta", 180.95, 1.50));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(74, "Tungsten", "W", 183.84, 2.36));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(75, "Rhenium", "Re", 186.21, 1.90));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(76, "Osmium", "Os", 190.23, 2.20));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(77, "Iridium", "Ir", 192.22, 2.20));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(78, "Platinum", "Pt", 195.08, 2.28));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(79, "Gold", "Au", 196.97, 2.54));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(80, "Mercury", "Hg", 200.59, 2.00));
            ChemicalGroups[IDX_POST_TRANSITION_METAL].Add(new Element(81, "Thallium", "Tl", 204.38, 1.62));
            ChemicalGroups[IDX_POST_TRANSITION_METAL].Add(new Element(82, "Lead", "Pb", 207.2, 2.33));
            ChemicalGroups[IDX_POST_TRANSITION_METAL].Add(new Element(83, "Bismuth", "Bi", 208.98, 2.02));
            ChemicalGroups[IDX_POST_TRANSITION_METAL].Add(new Element(84, "Polonium", "Po", 209.00, 2.00));
            ChemicalGroups[IDX_METALLOID].Add(new Element(85, "Astatine", "At", 210.00, 2.20));
            ChemicalGroups[IDX_NOBLE_GAS].Add(new Element(86, "Radon", "Rn", 222.00, double.NaN));
            ChemicalGroups[IDX_ALKALIMETAL].Add(new Element(87, "Francium", "Fr", 223.00, double.NaN));
            ChemicalGroups[IDX_ALKALINE_EARTH_METAL].Add(new Element(88, "Radium", "Ra", 226.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(89, "Actinium", "Ac", 227.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(90, "Thorium", "Th", 232.04, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(91, "Protactinium", "Pa", 231.04, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(92, "Uranium", "U", 238.03, 1.38));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(93, "Neptunium", "Np", 237.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(94, "Plutonium", "Pu", 244.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(95, "Americium", "Am", 243.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(96, "Curium", "Cm", 247.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(97, "Berkelium", "Bk", 247.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(98, "Californium", "Cf", 251.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(99, "Einsteinium", "Es", 252.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(100, "Fermium", "Fm", 257.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(101, "Mendelevium", "Md", 258.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(102, "Nobelium", "No", 259.00, double.NaN));
            ChemicalGroups[IDX_ACTINIDE].Add(new Element(103, "Lawrencium", "Lr", 262.00, double.NaN));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(104, "Rutherfordium", "Rf", 267.00, double.NaN));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(105, "Dubnium", "Db", 270.00, double.NaN));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(106, "Seaborgium", "Sg", 271.00, double.NaN));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(107, "Bohrium", "Bh", 270.00, double.NaN));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(108, "Hassium", "Hs", 277.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(109, "Meitnerium", "Mt", 276.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(110, "Darmstadtium", "Ds", 281.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(111, "Roentgenium", "Rg", 280.00, double.NaN));
            ChemicalGroups[IDX_TRANSITIONMETAL].Add(new Element(112, "Copernicium", "Cn", 285.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(113, "Nihonium", "Nh", 284.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(114, "Flerovium", "Fl", 289.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(115, "Moscovium", "Mc", 288.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(116, "Livermorium", "Lv", 293.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(117, "Tennessine", "Ts", 294.00, double.NaN));
            ChemicalGroups[IDX_UNKNOWN].Add(new Element(118, "Oganesson", "Og", 294.00, double.NaN));
        }
    }
}
