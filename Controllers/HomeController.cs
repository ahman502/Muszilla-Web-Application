using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Muszilla.Models;
using System.Data.SqlClient;
using Sitecore.FakeDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Options;
using Muszilla.Helpers;
using System.IO;

namespace Muszilla.Controllers                                                            //**This controller handles the CRUD functionalities**
{
    public class HomeController : Controller
    {
        //Connection for login
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        //Connection for upload music
        SqlConnection connect = new SqlConnection();
        SqlCommand command = new SqlCommand();
        SqlDataReader read;
        
        public IActionResult Index()
        {
            return View();
        }
        public void ConnectionString()
        {
            con.ConnectionString = Muszilla.Properties.Resources.ConnectionString;
            connect.ConnectionString = Muszilla.Properties.Resources.ConnectionString;
        }
        public IActionResult Verify(ConsumerModel acc, SongsModel song) //Checks if the user's email and password matches one inside the database
        {
            string fn = "";
            string ln = "";
            string url = "";
            string id = "";
            string song_name = "";
            string audio = "";
            ConnectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select User_ID, Email, Pass_word, FirstName, LastName, Picture from Consumer where Email = '" + acc.Email + "' and Pass_word = '"+acc.Pass_word+"'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                fn = dr["FirstName"].ToString();
                ln = dr["LastName"].ToString();
                url = dr["Picture"].ToString();
                id = dr["User_ID"].ToString();
                con.Close();
                HttpContext.Session.SetString("User_ID", id);
                HttpContext.Session.SetString("Email", acc.Email);
                HttpContext.Session.SetString("Pass_word", acc.Pass_word);
                HttpContext.Session.SetString("FirstName", fn);
                HttpContext.Session.SetString("LastName", ln);
                HttpContext.Session.SetString("Picture", url);
                
                //                                                                          ** Start logic for Showing Songs **
                ConnectionString();
                connect.Open();
                command.Connection = connect;
                command.CommandText = "select Song_Name, Song_Audio, Song_Owner  from Songs where Song_Owner = '" + id + "'";
                read = command.ExecuteReader();
                if (read.Read())
                {
                    song_name = read["Song_Name"].ToString();
                    audio = read["Song_Audio"].ToString();
                    ViewBag.Message = "Hi!";
                }
                else
                {
                    connect.Close();
                    ViewBag.Message = "Query did not work";                                 
                    return RedirectToAction("Homepage");                                                                                   
                }

                connect.Close();
                HttpContext.Session.SetString("Song_Name", song_name);
                HttpContext.Session.SetString("Song_Audio", audio);
                //                                                                          ** End logic for Showing Songs **


                return RedirectToAction("Homepage");
            }
            else
            {
                con.Close();
                ViewBag.Message = "Email or Password Incorrect";
                return View("Index");
            }
        }

        public IActionResult Homepage() // Gets all the strings to show the user's information in their session
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                ViewBag.Email = HttpContext.Session.GetString("Email");
                ViewBag.Pass_word = HttpContext.Session.GetString("Pass_word");
                ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
                ViewBag.LastName = HttpContext.Session.GetString("LastName");
                ViewBag.Picture = HttpContext.Session.GetString("Picture");
                ViewBag.SongName = HttpContext.Session.GetString("Song_Name");
                ViewBag.Song_Audio = HttpContext.Session.GetString("Song_Audio");
                return View("User_Homepage");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(Register add) // Adding a new user inside the database
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
        public IActionResult Logout() //Returns to the login page and clears the session
        {
            string empty = "";
            HttpContext.Session.SetString("User_ID", empty);
            HttpContext.Session.SetString("Email", empty);
            HttpContext.Session.SetString("Pass_word", empty);
            HttpContext.Session.SetString("FirstName", empty);
            HttpContext.Session.SetString("LastName", empty);
            HttpContext.Session.SetString("Picture", empty);
            HttpContext.Session.SetString("Song_Name", empty);
            HttpContext.Session.SetString("Song_Audio", empty);
            ViewBag.Message = "Log out successful!";
            return View("Index");
        }

        public IActionResult Update(ConsumerModel edit) //Updates fields inside the database
        {
            string id = "";
            id = HttpContext.Session.GetString("User_ID");
            ConnectionString();
            con.Open();
            com.Connection = con;
            if (edit.FirstName != null)
            {
                com.CommandText = "update Consumer set FirstName = '" + edit.FirstName + "'  where User_ID ='" + id + "'";
                com.ExecuteNonQuery();
                HttpContext.Session.SetString("FirstName", edit.FirstName);
            }
            if (edit.LastName != null)
            {
                com.CommandText = "update Consumer set LastName = '" + edit.LastName + "'  where User_ID ='" + id + "'";
                com.ExecuteNonQuery();
                HttpContext.Session.SetString("LastName", edit.LastName);
            }
            if (edit.Email != null)
            {
                com.CommandText = "update Consumer set Email = '" + edit.Email + "'  where User_ID ='" + id + "'";
                com.ExecuteNonQuery();
                HttpContext.Session.SetString("Email", edit.Email);
            }
            if (edit.Pass_word != null)
            {
                com.CommandText = "update Consumer set Pass_word = '" + edit.Pass_word + "'  where User_ID ='" + id + "'";
                com.ExecuteNonQuery();
                HttpContext.Session.SetString("Pass_word", edit.Pass_word);
            }
            else
            {
                con.Close();
                return RedirectToAction("Homepage");
            }
            con.Close();
            return RedirectToAction("Homepage");
        }
    }
}
       