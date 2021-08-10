using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Muszilla.Models;
using System.Data.SqlClient;

namespace Muszilla.Controllers
{
    public class SongsController : Controller //Not being used, this conroller had dealt with showing all the songs
                                              // inside the songs table. Currently only one shows inside a playlist.
                                              // **         This means that this was not implemented yet          **
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<SongsModel> songs = new List<SongsModel>();

        private readonly ILogger<SongsController> _logger;

        public SongsController(ILogger<SongsController> logger)
        {
            _logger = logger;
            con.ConnectionString = Muszilla.Properties.Resources.ConnectionString;
        }

        public IActionResult Songs()
        {
            FetchData();
            return View(songs);
        }

        private void FetchData()
        {
            if (songs.Count > 0)
            {
                songs.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Select TOP(100)[Song_ID],[Song_Name],[Song_Artist],[Song_Length],[Song_Genre] from Songs";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    songs.Add(new SongsModel()
                    {
                        Song_ID = dr["Song_ID"].ToString()
                    ,
                        Song_Name = dr["Song_Name"].ToString()
                    ,
                        Song_Artist = dr["Song_Artist"].ToString()
                    ,
                        Song_Length = dr["Song_Length"].ToString()
                    ,
                        Song_Genre = dr["Song_Genre"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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