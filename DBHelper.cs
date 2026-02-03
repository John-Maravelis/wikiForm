using System;
using System.Data;
using System.Data.SQLite; // Η βιβλιοθήκη που μόλις βάλαμε
using System.IO;

public class DbHelper
{
    private string connectionString = "Data Source=MyFavorites.db;Version=3;";

    // 1. Δημιουργία της Βάσης και του Πίνακα (αν δεν υπάρχουν)
    public void InitializeDatabase()
    {
        if (!File.Exists("MyFavorites.db"))
        {
            SQLiteConnection.CreateFile("MyFavorites.db");
        }

        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string sql = "CREATE TABLE IF NOT EXISTS Favorites (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT, Url TEXT)";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void AddFavorite(string title, string url)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string sql = "INSERT INTO Favorites (Title, Url) VALUES (@title, @url)";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@url", url);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public DataTable GetFavorites()
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string sql = "SELECT * FROM Favorites";
            using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, conn))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }

    public void DeleteFavorite(int id)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string sql = "DELETE FROM Favorites WHERE Id = @id";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // Ελέγχει αν υπάρχει ήδη το URL στη βάση
    public bool ArticleExists(string url)
    {
        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM Favorites WHERE Url = @url";
            using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@url", url);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; // Επιστρέφει true αν βρει έστω και ένα
            }
        }
    }
}