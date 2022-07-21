﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Management
{
    public class Management2 : IManagement
    {
        private Dictionary<string, Employee> IdEmploee = new Dictionary<string, Employee>();

        private Dictionary<Employee, List<Employee>> ManagerSubordinates = new Dictionary<Employee, List<Employee>>();   

        private Dictionary<string, string> EmploeeManager = new Dictionary<string, string>();

        public int Count => IdEmploee.Count;

        private void AddEmployeeInternal(Employee employee)
        {
        }
        //add
        public void AddEmployee(Employee employee)
        {
            IdEmploee.Add(employee.Id, employee);

            if (employee.Subordinates.Count != 0)
            {
                foreach (var emp in employee.Subordinates)
                {
                    AddEmployee(emp);
                    EmploeeManager.Add(emp.Id, employee.Id);
                }

                ManagerSubordinates.Add(employee, employee.Subordinates);
            }

        }

        public bool Contains(Employee employee)
        {
            return IdEmploee.ContainsKey(employee.Id);
        }
        //not cond
        private void NotContainsEmploeeId (string id)
        {
            if (!IdEmploee.ContainsKey(id))
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Employee> GetAllEmployeesOrderedByCountOfSubordinatesThenByTimeServedThenByName()
        {
            var emploeesToReturn = IdEmploee.Values
                .OrderByDescending(x => x.Subordinates.Count)
                .ThenByDescending(x => x.MonthsInService)
                .ThenBy(x => x.Name)
                .ToList();

            if (emploeesToReturn.Count == 0)
            {
                return new List<Employee>();
            }

            return emploeesToReturn;
        }

        public IEnumerable<Employee> GetCLevelManagement()
        {
            var emploeesToreturn = new List<Employee>();

            foreach (var kvp in ManagerSubordinates)
            {
                emploeesToreturn.Add(kvp.Key);
            }

            return emploeesToreturn
                .OrderByDescending(x => x.Subordinates.Count)
                .ThenBy(x => x.MonthsInService);
        }

        public Employee GetEmployee(string employeeId)
        {
            NotContainsEmploeeId(employeeId);

            return IdEmploee[employeeId];
        }

        public IEnumerable<Employee> GetEmployeesInTimeServedRange(int lowerBound, int upperBound)
        {
            var emploeesToReturn = IdEmploee.Values
                .Where(x => x.MonthsInService >= lowerBound && x.MonthsInService <= upperBound)
                .OrderByDescending(x => x.MonthsInService)
                .ThenBy(x => x.Name)
                .ToList();

            if (emploeesToReturn.Count == 0)
            {
                return new List<Employee>();
            }

            return emploeesToReturn;
        }

        public IEnumerable<Employee> GetManagerEmployees(string managerId)
        {
            NotContainsEmploeeId(managerId);

            if (IdEmploee[managerId].Subordinates.Count == 0)
            {
                throw new ArgumentException();
            }

            var SuboridinatesToReturn = IdEmploee[managerId].Subordinates
                .OrderByDescending(x => x.MonthsInService)
                .ToList();

            return SuboridinatesToReturn;
        }
        //remove
        public void RemoveEmployee(string employeeId)
        {
            NotContainsEmploeeId(employeeId);

            IdEmploee.Remove(employeeId);

            var temp = IdEmploee[employeeId];


            if (ManagerSubordinates.ContainsKey(temp))
            {
                ManagerSubordinates.Remove(IdEmploee[employeeId]);

                foreach (var subs in temp.Subordinates)
                {
                    ManagerSubordinates.Add(subs, subs.Subordinates);
                }
            }

            ManagerSubordinates[IdEmploee[EmploeeManager[employeeId]]].Remove(temp);


        }
    }
}
