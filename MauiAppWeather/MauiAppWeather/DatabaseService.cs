using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppWeather
{
    public class DatabaseService
    {
        private readonly SQLiteConnection _db;

        public DatabaseService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "weatherdata.db3");
            _db = new SQLiteConnection(dbPath);
            _db.CreateTable<FavoriteCity>(); 
        }

        public IEnumerable<FavoriteCity> GetFavoriteCities()
        {
            return _db.Table<FavoriteCity>().ToList();
        }

        public void AddFavoriteCity(string cityName)
        {
            var city = new FavoriteCity { CityName = cityName };
            _db.Insert(city);
        }

        public void RemoveFavoriteCity(FavoriteCity city)
        {
            _db.Delete(city);
        }
    }
}
