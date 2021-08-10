using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Muszilla.Models;
using Sitecore.FakeDb;

namespace Muszilla.Controllers
{
    public class RegisterController : Controller
    {
        // GET: RegisterController
        public IActionResult Index()
        {
            return View();
        }

        // GET: RegisterController/Create
        //Code for register page
        /*
        [HttpPost]
        public IActionResult Create(Register add)
        {
            string connection = Muszilla.Properties.Resources.ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                string query = "insert into Consumer(FirstName, LastName, Email, Pass_word) values('" + add.FirstName + "', '" + add.LastName + "', '" + add.Email + "', '" + add.Pass_word + "')";
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    com.ExecuteNonQuery();
                    ViewBag.Message = "New User inserted succesfully!";
                }
                con.Close();
                return View("Index");
            }
        }
        */
    }
}
