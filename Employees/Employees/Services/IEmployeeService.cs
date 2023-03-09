namespace Employees.Services
{
    using Employees.Models;
    using System.Collections.Generic;

    public interface IEmployeeService
    {
        List<TeamModel> PairEmployeesWorkedTogether(string fileName);
    }
}
