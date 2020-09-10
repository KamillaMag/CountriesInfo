using System.Data;
using System.Data.SqlClient;

namespace CountriesInfo
{
    internal class DBWriter
    {
        private static string connString;
        private static Country newCountry;
        private static int cityId;
        private static int regionId;
        internal static void SaveCountryInfo(string connectionString, Country country)
        {
            connString = connectionString;
            newCountry = country;
            string sqlSearchCity = "SELECT Id FROM Cities WHERE Cities.Name=@cityName";
            string sqlSearchRegion = "SELECT Id FROM Regions WHERE Regions.Name=@regionName";
            string sqlAddCity = "INSERT INTO Cities (Name) VALUES (@cityName)";
            string sqlAddRegion = "INSERT INTO Regions (Name) VALUES (@regionName)";
            string sqlSearchCountry = "SELECT Id FROM Countries WHERE Code=@countryCode";
            string sqlAddCountry = "INSERT INTO Countries (Name, Code, Capital, Area, Population, Region)" +
                " VALUES (@countryName,@countryCode,@cityId, @area, @population, @regionId)";
            string sqlUpdateCountry = "UPDATE Countries SET Name = @countryName, Code = @countryCode, Capital = @cityId," +
                "Area = @area, Population = @population, Region = @regionId WHERE Code=@countryCode";

            cityId = GetId(sqlSearchCity);
            if (cityId < 0)
            {
                InsertUpdateData(sqlAddCity);
                cityId = GetId(sqlSearchCity);
            }

            regionId = GetId(sqlSearchRegion);
            if (regionId < 0)
            {
                InsertUpdateData(sqlAddRegion);
                regionId = GetId(sqlSearchRegion);
            }

            if (GetId(sqlSearchCountry) < 0)
            {
                InsertUpdateData(sqlAddCountry);
            }
            else { InsertUpdateData(sqlUpdateCountry); }
        }

        private static int GetId(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.Add("@cityName", SqlDbType.VarChar).Value = newCountry.Capital;
                command.Parameters.Add("@regionName", SqlDbType.VarChar).Value = newCountry.Region;
                command.Parameters.Add("@countryCode", SqlDbType.VarChar).Value = newCountry.Code;
                var response = command.ExecuteScalar() ?? -1;
                return (int)response;
            }
        }

        private static void InsertUpdateData(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.Add("@cityName", SqlDbType.VarChar).Value = newCountry.Capital;
                command.Parameters.Add("@regionName", SqlDbType.VarChar).Value = newCountry.Region;
                command.Parameters.Add("@countryCode", SqlDbType.VarChar).Value = newCountry.Code;
                command.Parameters.Add("@countryName", SqlDbType.VarChar).Value = newCountry.Name;
                command.Parameters.Add("@cityId", SqlDbType.Int).Value = cityId;
                command.Parameters.Add("@area", SqlDbType.Decimal).Value = newCountry.Area;
                command.Parameters.Add("@population", SqlDbType.Int).Value = newCountry.Population;
                command.Parameters.Add("@regionId", SqlDbType.Int).Value = regionId;
                command.ExecuteNonQuery();
            }
        }
    }
}