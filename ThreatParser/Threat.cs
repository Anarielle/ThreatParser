using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreatParser
{
    public class Threat
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                if (value.Length > 60)
                {                    
                    name = value;
                }
                else
                {
                    name = value;
                }
            }
        }

        public string Description { get; set; }
        public string Source { get; set; }
        public string Object { get; set; }
        public bool isBreachPrivacy { get; set; }
        public bool isBreachIntegrity { get; set; }
        public bool isBreachAccess { get; set; }

        public Threat(int id, string name, string description, string source, string @object, bool isBreachPrivacy, bool isBreachIntegrity, bool isBreachAccessibility)
        {
            Id = id;
            Name = name;
            Description = description;
            Source = source;
            Object = @object;
            this.isBreachPrivacy = isBreachPrivacy;
            this.isBreachIntegrity = isBreachIntegrity;
            this.isBreachAccess = isBreachAccessibility;
        }

        public override string ToString()
        {
            return $"Идентификатор УБИ:\t{Id}" +
                $"\nНаименование УБИ:\t{Name}" +
                $"\nОписание:\t{Description}" +
                $"\nИсточник угрозы (характеристика и потенциал нарушителя):\t{Source}" +
                $"\nОбъект воздействия:\t{Object}" +
                $"\nНарушение конфиденциальности:\t{(isBreachPrivacy ? "да" : "нет") }" +
                $"\nНарушение целостности:\t{(isBreachIntegrity ? "да" : "нет")}" +
                $"\nНарушение доступности:\t{(isBreachAccess ? "да" : "нет")}";
        }
    }
}
