using Npgsql;
using System.Data;

namespace HW1303.Models
{
    public static class DBController
    {
        static string your_password = "Version#1";
        static NpgsqlConnectionStringBuilder conn = new NpgsqlConnectionStringBuilder(
            $"Server=c.monseratti-cluster.postgres.database.azure.com;Database=citus;Port=5432;User Id=citus;" +
            $"Password={your_password};Ssl Mode=Require;"
            )
        {
            TrustServerCertificate = true
        };

        public static async Task CreateTable()
        {
            using (var connect = new NpgsqlConnection(conn.ToString()))
            {
                await connect.OpenAsync();
                NpgsqlCommand address = new NpgsqlCommand(
                    "create table if not exists " +
                    "address(id serial unique, " +
                    "country varchar(50) not null, " +
                    "city varchar(50) not null, " +
                    "street varchar(50) not null, " +
                    "building_no int not null)"
                    , connect);
                NpgsqlCommand book_info = new NpgsqlCommand(
                    "create table if not exists " +
                    "book_info(id serial, " +
                    "book_title varchar(150) not null, " +
                    "authors varchar(150) not null, " +
                    "publishing_name varchar(50) null, " +
                    "publishing_address_id int null references address(id), " +
                    "publishing_date date null)"
                    , connect);
                address.ExecuteNonQueryAsync().Wait();
                book_info.ExecuteNonQueryAsync().Wait();
            }
        }

        public static async Task AddData(string tableName, params string[] tableValue)
        {
            if (tableName == "address")
            {
                using (var connect = new NpgsqlConnection(conn.ToString()))
                {
                    await connect.OpenAsync();
                    NpgsqlCommand address = new NpgsqlCommand(
                       "insert into " +
                       "address(country, city, street, building_no) values" +
                       "(@country, @city, @street, @building_no)"
                       , connect);
                    address.Parameters.AddWithValue("country", tableValue[0]);
                    address.Parameters.AddWithValue("city", tableValue[1]);
                    address.Parameters.AddWithValue("street", tableValue[2]);
                    address.Parameters.AddWithValue("building_no", int.Parse(tableValue[3]));

                    address.ExecuteNonQueryAsync().Wait();
                }
            }
            else
            {
                using (var connect = new NpgsqlConnection(conn.ToString()))
                {
                    await connect.OpenAsync();
                    NpgsqlCommand book_info = new NpgsqlCommand(
                       "insert into " +
                       "book_info(book_title, authors, publishing_name, publishing_address_id, publishing_date) values" +
                       "(@book_title, @authors, @publishing_name, @publishing_address_id, @publishing_date)"
                       , connect);
                    book_info.Parameters.AddWithValue("book_title", tableValue[0]);
                    book_info.Parameters.AddWithValue("authors", tableValue[1]);
                    book_info.Parameters.AddWithValue("publishing_name", tableValue[2]);
                    book_info.Parameters.AddWithValue("publishing_address_id", int.Parse(tableValue[3]));
                    book_info.Parameters.AddWithValue("publishing_date", DateTime.Parse(tableValue[4]));
                    book_info.Parameters[4].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;

                    book_info.ExecuteNonQueryAsync().Wait();
                }
            }
        }

        public static async Task UpdateData(string tableName, int id, params string[] tableValue)
        {
            if (tableName == "address")
            {
                using (var connect = new NpgsqlConnection(conn.ToString()))
                {
                    await connect.OpenAsync();
                    NpgsqlCommand address = new NpgsqlCommand(
                       "update address " +
                       "set " +
                       "country = @country, city = @city,street = @street,building_no = @building_no " +
                       "where id = @id"
                       , connect);
                    address.Parameters.AddWithValue("id", id);
                    address.Parameters.AddWithValue("country", tableValue[0]);
                    address.Parameters.AddWithValue("city", tableValue[1]);
                    address.Parameters.AddWithValue("street", tableValue[2]);
                    address.Parameters.AddWithValue("building_no", int.Parse(tableValue[3]));

                    address.ExecuteNonQueryAsync().Wait();
                }
            }
            else
            {
                using (var connect = new NpgsqlConnection(conn.ToString()))
                {
                    await connect.OpenAsync();
                    NpgsqlCommand book_info = new NpgsqlCommand(
                       "update book_info " +
                       "set " +
                       "book_title = @book_title, authors = @authors, publishing_name = @publishing_name, " +
                       "publishing_address_id = @publishing_address_id, publishing_date = @publishing_date " +
                       "where id = @id"
                       , connect);
                    book_info.Parameters.AddWithValue("id", id);
                    book_info.Parameters.AddWithValue("book_title", tableValue[0]);
                    book_info.Parameters.AddWithValue("authors", tableValue[1]);
                    book_info.Parameters.AddWithValue("publishing_name", tableValue[2]);
                    book_info.Parameters.AddWithValue("publishing_address_id", int.Parse(tableValue[3]));
                    book_info.Parameters.AddWithValue("publishing_date", DateTime.Parse(tableValue[3]));

                    book_info.ExecuteNonQueryAsync().Wait();
                }
            }
        }

        public static async Task DeleteData(string tableName, int id)
        {
            if (tableName == "address")
            {
                using (var connect = new NpgsqlConnection(conn.ToString()))
                {
                    await connect.OpenAsync();
                    NpgsqlCommand address = new NpgsqlCommand(
                       "delete from address " +
                       "where id = @id"
                       , connect);
                    address.Parameters.AddWithValue("id", id);

                    address.ExecuteNonQueryAsync().Wait();
                }
            }
            else
            {
                using (var connect = new NpgsqlConnection(conn.ToString()))
                {
                    await connect.OpenAsync();
                    NpgsqlCommand book_info = new NpgsqlCommand(
                       "delete from book_info " +
                       "where id = @id"
                       , connect);
                    book_info.Parameters.AddWithValue("id", id);

                    book_info.ExecuteNonQueryAsync().Wait();
                }
            }
        }

        public static async Task<DataTable> SelectData(string tableName)
        {
            DataTable dt = new DataTable();
            using (var connect = new NpgsqlConnection(conn.ToString()))
            {
                await connect.OpenAsync();
                NpgsqlCommand data = new NpgsqlCommand(
                   $"select * from {tableName}", connect);
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(data);
                da.Fill(dt);
            }
            return dt;
        }
    }

}

