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
    public class PlaylistController : Controller //Not fully implemented
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<PlaylistModel> playlists = new List<PlaylistModel>();

        private readonly ILogger<PlaylistController> _logger;

        public PlaylistController(ILogger<PlaylistController> logger)
        {
            _logger = logger;
            con.ConnectionString = Muszilla.Properties.Resources.ConnectionString; 
        }

        public IActionResult Playlist()
        {
            FetchData();
            return View(playlists);
        }

        private void FetchData()
        {
            if (playlists.Count > 0)
            {
                playlists.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "select TOP(100) Playlist_ID,Playlist_Name,User_ID_FK from Playlist";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    playlists.Add(new PlaylistModel() { Playlist_ID = dr["Playlist_ID"].ToString()
                    ,Playlist_Name = dr["Playlist_Name"].ToString()
                    ,User_ID_FK = dr["User_ID_FK"].ToString()
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
