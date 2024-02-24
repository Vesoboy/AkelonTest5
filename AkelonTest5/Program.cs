using AkelonTest5;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Random random = new Random();

    static void Main(string[] args)
    {
        int dayVacations = 28;// Общее количество дней отпуска для каждого сотрудника
        DictionaryName dictName = new DictionaryName();
        dictName.AddEmployee("Иванов Иван Иванович, id - 1232");
        dictName.AddEmployee("Солохин Иван Алексеевич, id - 1212");
        dictName.AddEmployee("Шальнова Соня Александровна, id - 2232");
        dictName.AddEmployee("Новожилов Алексей Алексеевич, id - 3232");
        dictName.AddEmployee("Новожилов Алексей Алексеевич, id - 1242");
        dictName.AddEmployee("Новожилов Алексей Алексеевич, id - 1242");
        Dictionary<string, List<DateTime>> employees = dictName.GetEmployees();

        List<DayOfWeek> workingDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
        List<DateTime> allVacationsDates = new List<DateTime>();

        // Основной алгоритм
        foreach (var employee in employees)
        {
            int VacationDays = dayVacations; 
            while (VacationDays > 0)
            {
                DateTime startDate = GenerateRandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31));//Возможные отпуска п период этого промежутка времени
                if (!workingDays.Contains(startDate.DayOfWeek))
                    continue;

                int vacationDuration = RemainingVacationDays(random.Next(2) == 0 ? 7 : 14, VacationDays); // Случайная длительность отпуска: 7 или 14 дней

                if (!IsVacationPossible(startDate, vacationDuration, employee.Value, allVacationsDates))
                    continue;

                for (int i = 0; i < vacationDuration; i++)
                {
                    DateTime vacationDate = startDate.AddDays(i);
                    employee.Value.Add(vacationDate);
                    allVacationsDates.Add(vacationDate);
                }

                VacationDays -= vacationDuration;
            }
        }

        // Вывод результатов
        foreach (var employee in employees)
        {
            Console.WriteLine($"\n{employee.Key}:");
            int vacationBlockCount = employee.Value.Count / 7; // Вычисляем количество блоков отпусков по 7 дней
            for (int i = 0; i < vacationBlockCount; i++)
            {
                Console.WriteLine(string.Join(", ", employee.Value.Skip(i * 7).Take(7).Select(date => date.ToShortDateString())));
            }
        }
    }

    static int RemainingVacationDays (int vacationDuration, int VacationDays)
    {
        if (vacationDuration == 14 && VacationDays >= 14)
        {
            vacationDuration = 14;
        }

        if (vacationDuration == 7 || VacationDays <= 7)
        {
            vacationDuration = 7;
        }
        return vacationDuration;
    }


    static DateTime GenerateRandomDate(DateTime start, DateTime end)
    {
        int range = (end - start).Days;
        return start.AddDays(random.Next(range));
    }

    static bool IsVacationPossible(DateTime startDate, int duration, List<DateTime> employeeVacations, List<DateTime> allVacations)
    {
        for (int i = 0; i < duration; i++)
        {
            DateTime date = startDate.AddDays(i);
            if (!employeeVacations.Contains(date) && !allVacations.Contains(date))
                continue;
            else
                return false;
        }
        return true;
    }
}
