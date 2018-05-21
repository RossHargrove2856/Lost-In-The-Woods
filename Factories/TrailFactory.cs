using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Extensions.Options;
using Dapper;
using MySql.Data.MySqlClient;
using LostInTheWoods.Models;

namespace LostInTheWoods.Factories {
    public class TrailFactory : IFactory<Trail> {
        private readonly IOptions<MySqlOptions> MySqlConfig;
        public TrailFactory(IOptions<MySqlOptions> config) {
            MySqlConfig = config;
        }
        internal IDbConnection Connection {
            get {
                return new MySqlConnection(MySqlConfig.Value.ConnectionString);
            }
        }
        public void Add(Trail trail) {
            using (IDbConnection dbConnection = Connection) {
                string query = "INSERT INTO trails (name, description, length, elevation, latitude, longitude) VALUES (@Name, @Description, @Length, @Elevation, @Latitude, @Longitude)";
                dbConnection.Open();
                dbConnection.Execute(query, trail);
            }
        }
        public List<Trail> Index() {
            using (IDbConnection dbConnection = Connection) {
                using (IDbCommand command = dbConnection.CreateCommand()) {
                    string query = "SELECT * FROM trails";
                    dbConnection.Open();
                    return dbConnection.Query<Trail>(query).ToList();
                }
            }
        }
        public Trail FindByID(int id) {
            using (IDbConnection dbConnection = Connection) {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails WHERE id = @Id", new {Id = id}).FirstOrDefault();
            }
        }
    }
}