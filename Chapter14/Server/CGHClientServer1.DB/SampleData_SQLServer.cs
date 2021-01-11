using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using CGHClientServer1.DTO;
using Newtonsoft.Json;

namespace CGHClientServer1.DB
{
    public static class SampleData_SQLServer
    {
        public static List<CitySelectorDataItem> DataItems { get; set; } = new List<CitySelectorDataItem>();

        public static void InsertCities(SqlConnection dbConnection)
        {
            string sql;
            SqlCommand command;

            var uniqueItems = DataItems.Select(x => new { x.CityId, x.StateId, x.CityName })
                .Distinct().ToList();
            uniqueItems.ForEach(x =>
            {
                sql = $"INSERT INTO City ([CityId], [StateId], [Name]) VALUES ('{x.CityId}', '{x.StateId}', '{x.CityName.Replace("'", "''")}')";
                command = new SqlCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            });
        }

        public static void InsertCountries(SqlConnection _dbConnection)
        {
            string sql;
            SqlCommand command;

            var uniqueItems = DataItems.Select(x => new { x.CountryId, x.CountryName })
                .Distinct().ToList();
            uniqueItems.ForEach(x =>
            {
                sql = $"INSERT INTO Country ([CountryId], [Name]) VALUES ('{x.CountryId}', '{x.CountryName.Replace("'", "''")}')";
                command = new SqlCommand(sql, _dbConnection);
                command.ExecuteNonQuery();
            });
        }

        public static void InsertStates(SqlConnection _dbConnection)
        {
            string sql;
            SqlCommand command;

            var uniqueItems = DataItems.Select(x => new { x.StateId, x.CountryId, x.StateName })
                .Distinct().ToList();
            uniqueItems.ForEach(x =>
            {
                sql = $"INSERT INTO State ([StateId], [CountryId], [Name]) VALUES ('{x.StateId}', '{x.CountryId}', '{x.StateName.Replace("'", "''")}')";
                command = new SqlCommand(sql, _dbConnection);
                command.ExecuteNonQuery();
            });
        }

        public static void LoadData(SqlConnection _dbConnection)
        {
            DataItems.Clear();

            Console.WriteLine($"Loading sample data....");
            DirectoryInfo diDataFile = new DirectoryInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data"));
            using (var reader = new StreamReader(@$"{diDataFile.FullName}\SampleData.json"))
            {
                string json = reader.ReadToEnd();
                var dataItems = JsonConvert.DeserializeObject<IList<CitySelectorDataItem>>(json, new JsonSerializerSettings());
                DataItems.AddRange(dataItems);
            }

            //Console.WriteLine($"Creating table schemas");
            //CreateTableSchemas(_dbConnection);

            Console.WriteLine($"Inserting country data....");
            InsertCountries(_dbConnection);

            Console.WriteLine($"Inserting state data....");
            InsertStates(_dbConnection);

            Console.WriteLine($"Inserting city data....");
            InsertCities(_dbConnection);
        }

        private static void CreateTableSchemas(SqlConnection dbConnection)
        {
            string sql = "CREATE TABLE IF NOT EXISTS Country (CountryId uniqueidentifier NOT NULL PRIMARY KEY, Code text, Name text NOT NULL, PhoneCode text) WITHOUT ROWID";

            SqlCommand command = new SqlCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE IF NOT EXISTS State (StateId uniqueidentifier NOT NULL PRIMARY KEY, CountryId uniqueidentifier NOT NULL, Abbreviation text, Name text NOT NULL, FOREIGN KEY(CountryId) REFERENCES Country(CountryId)) WITHOUT ROWID";

            command = new SqlCommand(sql, dbConnection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE IF NOT EXISTS City (CityId uniqueidentifier NOT NULL PRIMARY KEY, StateId uniqueidentifier NOT NULL, Name text NOT NULL, FOREIGN KEY(StateId) REFERENCES State(StateId)) WITHOUT ROWID";

            command = new SqlCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }
    }
}