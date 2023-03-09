namespace Employees.Controllers
{
    using Employees.Models;
    using Employees.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var files = GetListFiles();
            return View(files);
        }

        [HttpGet]
        public IActionResult GetFileData(string fileName)
        {
            var teams = _employeeService.PairEmployeesWorkedTogether(fileName);
            return View(teams);
        }

        private static List<FileModel> GetListFiles()
        {
            var filePaths = Directory.GetFiles(Path.Combine("Resource/"));

            List<FileModel> files = new List<FileModel>();
            foreach (string filePath in filePaths)
            {
                files.Add(new FileModel { FileName = Path.GetFileName(filePath) });
            }

            return files;
        }
    }
}
