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

    public static bool CreateAccount(string? user, string password, string nom, string prenom, string telephone, string email, string addresse, string creditCard)
    {
        var command = _connexion.CreateCommand();
        // Validate user inputs
        user = MySqlHelper.EscapeString(user);
        password = MySqlHelper.EscapeString(password);
        command.CommandText = $"INSERT INTO user (username, password, nom_utilisateur, prenom_utilisateur, telephone, email, addresse, credit_card, statut_fidelite, is_admin) VALUES ('{user}', '{password}', '{nom}', '{prenom}', '{telephone}', '{email}', '{addresse}', '{creditCard}', 'bronze', 0);";
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
        // Get all products where debut_disponibilite <= today and fin_disponibilite >= today or fin_disponibilite is null and debut_disponibilite is null
        command.CommandText = "SELECT * FROM produit WHERE (debut_disponibilite <= CURDATE() AND fin_disponibilite >= CURDATE()) OR (debut_disponibilite IS NULL AND fin_disponibilite IS NULL);";
        var reader = command.ExecuteReader();
        var result = new List<Produit>();
        while (reader.Read())
        {
            result.Add(new Produit(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetFloat(3), reader.GetInt32(4)));
        }

        reader.Close();
        return result;
    }

    public static List<User> GetUsers()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT * FROM user;";
        var reader = command.ExecuteReader();
        var result = new List<User>();
        while (reader.Read())
        {
            result.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetString(4),
                reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetBoolean(10)));
        }
        reader.Close();
        return result;
    }

    public static List<Commande> GetOrders()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT * FROM commande;";
        var reader = command.ExecuteReader();
        var result = new List<Commande>();
        while (reader.Read())
        {
            result.Add(new Commande(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetString(3), reader.GetString(4), reader.GetInt16(5), reader.GetInt16(6)));
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
    
    /// <summary>
    /// Le prix moyen des commandes effectué par magasin
    /// </summary>
    public static void MeanPriceCommand()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT magasin.id_magasin, magasin.nom_magasin, AVG(produit.prix_produit) as " +
                              "moyenne_prix_commandes FROM magasin " +
                              "INNER JOIN commande ON magasin.id_magasin = commande.id_magasin " +
                              "INNER JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "INNER JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "GROUP BY magasin.id_magasin";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Magasin: {reader.GetString(1)} - Moyenne prix commandes: {reader.GetDouble(2)}");
        }
        reader.Close();
    }

    /// <summary>
    /// moyenne de toutes les commandes effectué quelque soit le magasin
    /// </summary>
    public static void MeanPriceAllCommand()
    { 
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT AVG(produit.prix_produit) as moyenne_prix_commandes FROM commande_produit " +
                              "INNER JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "GROUP BY commande_produit.id_commande";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Moyenne prix commandes: {reader.GetDouble(0)}");
        }
    }

    /// <summary>
    /// Meilleur client de l'annee et du mois
    /// </summary>
    public static void BestClient()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT user.id_user, user.nom_utilisateur, user.prenom_utilisateur, SUM(produit.prix_produit) as prix_total " +
                              "FROM user " +
                              "INNER JOIN commande ON user.id_user = commande.id_user " +
                              "INNER JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "INNER JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "WHERE YEAR(commande.date_commande) = YEAR(CURDATE()) " +
                              "GROUP BY user.id_user " +
                              "ORDER BY prix_total DESC " +
                              "LIMIT 1;";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Meilleur client de l'année: {reader.GetString(1)} {reader.GetString(2)} avec {reader.GetDouble(3)} euros");
        }
        reader.Close();
        command.CommandText = "SELECT user.id_user, user.nom_utilisateur, user.prenom_utilisateur, SUM(produit.prix_produit) as prix_total " +
                              "FROM user " +
                              "INNER JOIN commande ON user.id_user = commande.id_user " +
                              "INNER JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "INNER JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "WHERE YEAR(commande.date_commande) = YEAR(CURDATE()) " +
                              "GROUP BY user.id_user, MONTH(commande.date_commande) " +
                              "ORDER BY prix_total DESC " +
                              "LIMIT 1;";
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Meilleur client du mois: {reader.GetString(1)} {reader.GetString(2)} avec {reader.GetDouble(3)} euros");
        }
        reader.Close();
    }

    public static void DropTables()
    {
        
    }

    public static MySqlDataReader GetData(string request)
    {
        var command = _connexion.CreateCommand();
        command.CommandText = request;
        return command.ExecuteReader();
    }

    public static void AddProduct(string name, double price, string type, int threshold)
    {
        var command = _connexion.CreateCommand();
        command.CommandText = $"INSERT INTO produit (nom_produit, prix_produit, type_produit, seuil) VALUES ('{name}', {price}, '{type}', {threshold});";
        command.ExecuteNonQuery();
    }
}