using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiAppWeather
{
    public class FavoriteCity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string CityName { get; set; }
    }
}
