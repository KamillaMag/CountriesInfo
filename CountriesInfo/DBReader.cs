using System.Collections.Generic;
using System.Data.SqlClient;

namespace CountriesInfo
{
    internal static class DBReader
    {
        internal static List<Country> GetCountriesInfo(string connectionString)
        {
            List<Country> countries = new List<Country>();
            string sqlExpression = "SELECT Countries.Name, Code, Cities.Name, Area, Population," +
                " Regions.Name FROM Countries, Cities, Regions WHERE Cities.Id=Countries.Capital AND Regions.Id=Countries.Region";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Country country = new Country();
                        country.Name = reader.GetValue(0).ToString();
                        country.Code = reader.GetValue(1).ToString();
                        country.Capital = reader.GetValue(2).ToString();
                        country.Area = (decimal)reader.GetValue(3);
                        country.Population = (int)reader.GetValue(4);
                        country.Region = reader.GetValue(5).ToString();
                        countries.Add(country);
                    }
                }

                reader.Close();
            }

            return countries;
        }
    }
}