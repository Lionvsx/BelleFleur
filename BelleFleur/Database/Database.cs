using System.Text.Json;
using BelleFleur.Database.Structures;
using MySql.Data.MySqlClient;

namespace BelleFleur.Database;

public static class Database
{
    private static MySqlConnection _connexion;

    public static MySqlConnection Connexion
    {
        get => _connexion;
    }

    public static void Connect(string user, string password)
    {
        _connexion = new MySqlConnection($"server=localhost;user={user};password={password};");
        _connexion.Open();
        CreateDatabase("belle_fleur");
        CreateTables();
    }


    public static void CreateDatabase(string name)
    {
        var command = _connexion.CreateCommand();
        command.CommandText = $"CREATE DATABASE IF NOT EXISTS {name};use {name};";
        command.ExecuteNonQuery();
    }
    
    public static void CreateTables()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = File.ReadAllText("../../../Database/schema.sql");
        command.ExecuteNonQuery();
    }
    
    
    //Export database to json
    // public static void ExportToJson()
    // {
    //     var flowers = GetFlowers();
    //     var json = JsonSerializer.Serialize(flowers);
    //     File.WriteAllText("flowers.json", json);
    // }

    public static bool Authentificate(string user, string password, bool admin = false)
    {
        var command = _connexion.CreateCommand();
        // Validate user inputs
        user = MySqlHelper.EscapeString(user);
        password = MySqlHelper.EscapeString(password);
        command.CommandText = $"SELECT * FROM user WHERE username = '{user}' AND password = '{password}' AND is_admin = {(admin ? 1 : 0)};";
        var reader = command.ExecuteReader();
        var result = reader.HasRows;
        reader.Close();
        return result;
    }

    public static bool CreateAccount(string? user, string password)
    {
        var command = _connexion.CreateCommand();
        // Validate user inputs
        user = MySqlHelper.EscapeString(user);
        password = MySqlHelper.EscapeString(password);
        command.CommandText = $"INSERT INTO user (username, password) VALUES ('{user}', '{password}');";
        var result = command.ExecuteNonQuery();
        return result > 0;
    }

    public static int Populate()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = File.ReadAllText("../../../Database/populate.sql");
        var result = command.ExecuteNonQuery();
        return result;
    }

    public static List<Produit> GetProducts()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT * FROM produit;";
        var reader = command.ExecuteReader();
        var result = new List<Produit>();
        while (reader.Read())
        {
            result.Add(new Produit(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetDateTime(5), reader.GetDateTime(6)));
        }
        reader.Close();
        return result;
    }

    public static User GetUserInfo(string id)
    {
        var command = _connexion.CreateCommand();
        command.CommandText = $"SELECT * FROM user WHERE id = {id};";
        var reader = command.ExecuteReader();
        reader.Read();
        var result = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetString(4),
            reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetBoolean(10)); 
        reader.Close();
        return result;
    }

    public static void ClearTables()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "DELETE FROM produit;" +
                              "DELETE FROM magasin;" +
                              "DELETE FROM user;" +
                              "DELETE FROM commande;" +
                              "DELETE FROM commande_produit;" +
                              "DELETE FROM user;" +
                              "DELETE FROM stocks;";

    }

    public static void DropTables()
    {
        
    }
}