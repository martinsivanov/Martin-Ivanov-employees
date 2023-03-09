namespace Employees.Services
{
    using Employees.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class EmployeeService : IEmployeeService
    {
        public List<TeamModel> PairEmployeesWorkedTogether()
        {
            var lines = File.ReadAllLines(@"Resource\data.csv");

            var employees = new List<EmployeeModel>();

            foreach (var line in lines)
            {
                var values = line.Split(", ");

                employees.Add(new EmployeeModel
                {
                    EmpId = int.Parse(values[0]),
                    ProjectId = int.Parse(values[1]),
                    DateFrom = DateTime.Parse(values[2]),
                    DateTo = values[3] == "NULL" ? DateTime.Now : DateTime.Parse(values[3])
                });
            }

            var projects = employees
                .GroupBy(x => x.ProjectId)
                .Select(grp => grp.ToList())
                .Where(x => x.Count > 1)
                .ToList();

            var projectEmp = new List<TeamModel>();

            foreach (var item in projects)
            {
                var firstEmp = item[0];
                var secondEmp = item[1];

                var start = firstEmp.DateFrom <= secondEmp.DateFrom ? secondEmp.DateFrom : firstEmp.DateFrom;
                var end = firstEmp.DateTo <= secondEmp.DateTo ? firstEmp.DateTo : secondEmp.DateTo;

                if (end >= start)
                {
                    var daysWorkedTogether = (end.Date - start.Date).TotalDays;

                    projectEmp.Add(new TeamModel
                    {
                        ProjectId = firstEmp.ProjectId,
                        FirstEmployeeId = firstEmp.EmpId,
                        SecondEmployeeId = secondEmp.EmpId,
                        DaysWorked = (int)daysWorkedTogether
                    });
                }
            }

            return projectEmp;
        } 
    }
}
