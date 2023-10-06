using System.Collections.Generic;

namespace ZC.PeriodicTableLearner.Resources.Models
{
    public class ChemicalGroup
    {
        public string Name { get; set; }

        public List<Element> Elements { get; set; }

        public ChemicalGroup(string name) 
        { 
            Name = name;
        }

        public void Add(Element element)
        {
            Elements.Add(element);
        }
    }
}
