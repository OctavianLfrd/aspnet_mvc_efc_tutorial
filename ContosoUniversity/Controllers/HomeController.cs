using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models.SchoolViewModels;
using System.Data.Common;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            //IQueryable<EnrollmentDateGroup> data =
            //    from student in _context.Students
            //    group student by student.EnrollmentDate into dateGroup
            //    select new EnrollmentDateGroup()
            //    {
            //        EnrollmentDate = dateGroup.Key,
            //        StudentCount = dateGroup.Count()
            //    };
            //return View(await data.AsNoTracking().ToListAsync());

            // USING ADO.NET.

            List<EnrollmentDateGroup> groups = new List<EnrollmentDateGroup>();
            var conn = _context.Database.GetDbConnection(); // Here we are getting DB connection.
            try
            {
                await conn.OpenAsync(); // Here we are asynchronously openning DB connection.
                using (var command = conn.CreateCommand()) // Creating and returning DbCommand object associated with the current connection
                {
                    string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount " // Just a SQL query.
                        + "FROM Person "
                        + "WHERE Discriminator = 'Student' "
                        + "GROUP BY EnrollmentDate";
                    command.CommandText = query; // Setting the command text.
                    DbDataReader reader = await command.ExecuteReaderAsync(); // Executes the command.CommandText() against the connection and returns
                                                                              // the DbDataReader object.

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new EnrollmentDateGroup
                            {
                                EnrollmentDate = reader.GetDateTime(0),
                                StudentCount = reader.GetInt32(1)
                            };
                            groups.Add(row);
                        }
                        reader.Dispose();
                    }
                }
            }
            finally
            {
                conn.Close(); // Closing connection to the database.
            }
            return View(groups);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
