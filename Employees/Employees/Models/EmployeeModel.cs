﻿namespace Employees.Models
{
    using System;

    public class EmployeeModel
    {
        public int EmpId { get; set; }
        public int ProjectId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
