using Npgsql;
using System.Data;

var your_password = "Version#1";
var conn = new NpgsqlConnectionStringBuilder($"Server=c.monseratti-cluster.postgres.database.azure.com;" +
    $"Database=citus;Port=5432;User Id=citus;Password={your_password};Ssl Mode=Require;" +
    $"Pooling = true; Minimum Pool Size=0; Maximum Pool Size =50;");
conn.TrustServerCertificate = true;

DataTable dt = new DataTable();

using (var connect = new NpgsqlConnection(conn.ToString()))
{
    connect.Open();
    //NpgsqlCommand cmd = new NpgsqlCommand(
    //    "create table students (id serial primary key, st_name varchar(50), st_surname varchar(50), st_age int)", connect);
    //cmd.ExecuteNonQuery();
    //Console.WriteLine("PostgreDB table was created");
    //NpgsqlCommand cmd = new NpgsqlCommand(
    //    "insert into students(st_name, st_surname,st_age) " +
    //    "values (@st_name, @st_surname, @st_age)",
    //    connect
    //    );
    //cmd.Parameters.AddWithValue("st_name", "Test2" );
    //cmd.Parameters.AddWithValue("st_surname", "Tester2" );
    //cmd.Parameters.AddWithValue("st_age", 31 );
    //var i = cmd.ExecuteNonQuery();
    //NpgsqlCommand cmd = new NpgsqlCommand(
    //    "select *  from students",
    //    connect
    //    );
    //var res = await cmd.ExecuteReaderAsync();
    //while (res.Read())
    //{
    //    Console.WriteLine(string.Format("Data: {0}, name: {1}, surname: {2}, age: {3}",
    //        res.GetInt32(0).ToString(),
    //        res.GetString(1),
    //        res.GetString(2),
    //        res.GetInt32(3).ToString()
    //        ));
    //}
    //await res.CloseAsync();
    //NpgsqlCommand cmd2 = new NpgsqlCommand(
    //    "update students set st_name=@st_name " +
    //    "where id=@id",
    //    connect
    //    );
    //cmd2.Parameters.AddWithValue("st_name", "Test5");
    //cmd2.Parameters.AddWithValue("id", 2);
    //cmd2.ExecuteNonQuery();

    
    //NpgsqlCommand cmd1 = new NpgsqlCommand(
    //    "delete from students " +
    //    "where id=@id",
    //    connect
    //    );
    //cmd1.Parameters.AddWithValue("id", 1);
    //var i = cmd1.ExecuteNonQuery();

    //res = await cmd.ExecuteReaderAsync();
    //while (res.Read())
    //{
    //    Console.WriteLine(string.Format("Data: {0}, name: {1}, surname: {2}, age: {3}",
    //        res.GetInt32(0).ToString(),
    //        res.GetString(1),
    //        res.GetString(2),
    //        res.GetInt32(3).ToString()
    //        ));
    //}
    //await res.CloseAsync();

   
    NpgsqlCommand cmd = new NpgsqlCommand(
        "select *  from students",
        connect
        );
    NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
    dataAdapter.Fill(dt);
    await connect.CloseAsync();
}

if (dt.Rows.Count > 0)
{
    for (int i = 0; i < dt.Columns.Count; i++)
    {
        Console.Write($"{dt.Columns[i].ColumnName} | ");
    }
    Console.WriteLine();
    for (int i = 0; i < dt.Rows.Count; i++)
    {
        for (int y = 0; y < dt.Columns.Count; y++)
        {
            Console.Write($"{dt.Rows[i][y]} | ");
        }
        Console.WriteLine();
    }
}

Console.ReadLine();
