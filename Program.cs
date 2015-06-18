using System;
using Mono.Data.Sqlite;
using Dapper;

class Program
{
    public static void Main()
    {
        var bld = new SqliteConnectionStringBuilder()
        {
            DataSource = @":memory:",
        };
        using (var conn = new SqliteConnection(bld.ToString()))
        {
            conn.Open();
            conn.Execute("DROP TABLE IF EXISTS author;");
            conn.Execute("CREATE TABLE IF NOT EXISTS author (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, age INTEGER);");
            var a = new Author { Name = "aa aaa", Age = 11 };
            var b = new Author { Name = "bb bbb", Age = 22 };
            conn.Execute("INSERT INTO author (name, age) VALUES (@Name, @Age);", a);
            conn.Execute("INSERT INTO author (name, age) VALUES (@Name, @Age);", b);
            foreach (var item in conn.Query<Author>("SELECT id AS Id, name AS Name, age AS Age from author;"))
            {
                Console.WriteLine(item);
            }
        }
    }
}

class Author
{
    public long Id { get; set; }
    public string Name{ get; set; }
    public int Age{ get; set; }
    public override string ToString()
    {
        return string.Format("[Author: Id={0}, Name={1}, Age={2}]", Id, Name, Age);
    }
}