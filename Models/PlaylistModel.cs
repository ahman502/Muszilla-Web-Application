using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muszilla.Models
{
    public class PlaylistModel
    {
        public string Playlist_ID { get; set; }
        public string Playlist_Name { get; set; }
        public string User_ID_FK { get; set; }
    }
}
