using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkelonTest5
{
    internal class DictionaryName
    {
        private Dictionary<string, List<DateTime>> employees = new Dictionary<string, List<DateTime>>();

        public void AddEmployee(string name)
        {
            if (!employees.ContainsKey(name))
            {
                employees.Add(name, new List<DateTime>());
            }
            else
            {
                Console.WriteLine($"Сотрудник с именем '{name}' уже существует.");
            }
        }

        public Dictionary<string, List<DateTime>> GetEmployees()
        {
            return employees;
        }
    }
}
