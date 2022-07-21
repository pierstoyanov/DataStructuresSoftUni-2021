using System.Collections.Generic;

namespace Exam.Management
{
    public class Management : IManagement
    {

        public int Count => 0;

        private void AddEmployeeInternal(Employee employee)
        {
        }

        public void AddEmployee(Employee employee)
        {
        }

        public bool Contains(Employee employee)
        {
            return false;
        }

        public IEnumerable<Employee> GetAllEmployeesOrderedByCountOfSubordinatesThenByTimeServedThenByName()
        {
            return null;
        }

        public IEnumerable<Employee> GetCLevelManagement()
        {
            return null;
        }

        public Employee GetEmployee(string employeeId)
        {
            return null;
        }

        public IEnumerable<Employee> GetEmployeesInTimeServedRange(int lowerBound, int upperBound)
        {
            return null;
        }

        public IEnumerable<Employee> GetManagerEmployees(string managerId)
        {
            return null;
        }

        public void RemoveEmployee(string employeeId)
        {
            
        }
    }
}
