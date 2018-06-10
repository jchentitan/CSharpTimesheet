using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ClassLibrary;

namespace CSharpTimesheet.Controllers
{
    public class TimesheetController : Controller
    {
        // GET: Timesheet
        public ActionResult Index()
        {
            return View();
        }

        /*
        [HttpPost]
        public ActionResult Create(int WeekId, FormCollection formCollection)
        {
            // View to display editor

            string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;

            // Connect to database to get posted data info database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveWeekOfData", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();

                // cmdtext 

            }



            // 1 project to M weekly unit.  1 week time instance to M 1:7 dayof unit.  1 day to 1 hour recording
            
            // Enumeral 1 to M projects to display projects in list view

            return RedirectToAction("Index");
        }
        */

        [HttpPost]
        public ActionResult CreateProject()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;

            List<Project> projects = new List<Project>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spAddProject", connection);

                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    Project project = new Project();
                    project.ProjectId = Convert.ToInt32(rdr["pid"]);
                    project.Name = rdr["name"].ToString();
                    project.YearId = Convert.ToInt32(rdr["yid"]);
                    project.WeekId = Convert.ToInt32(rdr["wid"]);
                    project.DayId = Convert.ToInt32(rdr["did"]);
                    project.BeginDate = Convert.ToDateTime(rdr["DateOfBirth"]);
                    project.EndDate = Convert.ToDateTime(rdr["DateOfBirth"]);
                    project.Status = Convert.ToBoolean(true);
                    projects.Add(project);
                }

            }
            return View(projects);
        }

        [HttpPost]
        public ActionResult UpdateHours(int ProjectId, int WeekNumber, FormCollection formCollection)
        {
            int projectid = ProjectId;
            int weeknum = WeekNumber;

            Week week = new Week();
            week.WeekNumber = WeekNumber;

            string connectionString = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("spUpdateProject", connection);

                command.CommandType = CommandType.StoredProcedure;
                connection.Open();

                SqlDataReader rdr = command.ExecuteReader();
                week.ProjectId = Convert.ToInt32(rdr["pid"]); //hidden input
                week.WeekNumber = weeknum;

                while (rdr.Read())
                {
                    week.Days.ElementAt(0) = formCollection["Sunday"];
                    week.Days.ElementAt(1) = formCollection["Monday"];
                    week.Days.ElementAt(2) = formCollection["Tuesday"];
                    week.Days.ElementAt(3) = formCollection["Wednesday"];
                    week.Days.ElementAt(4) = formCollection["Thursday"];
                    week.Days.ElementAt(5) = formCollection["Friday"];
                    week.Days.ElementAt(6) = formCollection["Saturday"];
                }
            }

            // 1 project to M users

            // 1 project to M weekly unit.  1 week time instance to M 1:7 dayof unit.  1 day to 1 hour recording

            // Enumeral 1 to M projects to display projects in list view

            // At update screen known already: 1 userid, 1 projectid, 1 underlying number for week, 7 days null or non-null hours recorded

            return View();
        }


    }
}
