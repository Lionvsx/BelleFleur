using System.Data;
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

    public static List<Stocks> GetStocksParis()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT * FROM stocks WHERE stocks.id_magasin = 1;";
        var reader = command.ExecuteReader();
        var result = new List<Stocks>();
        while (reader.Read())
        {
            result.Add(new Stocks(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3)));
        }
        reader.Close();
        return result;
    }
    public static List<Stocks> GetStocksLyon()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT * FROM stocks WHERE stocks.id_magasin = 2;";
        var reader = command.ExecuteReader();
        var result = new List<Stocks>();
        while (reader.Read())
        {
            result.Add(new Stocks(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3)));
        }
        reader.Close();
        return result;
    }
    
    public static List<Stocks> GetStocksNantes()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT * FROM stocks WHERE stocks.id_magasin = 3;";
        var reader = command.ExecuteReader();
        var result = new List<Stocks>();
        while (reader.Read())
        {
            result.Add(new Stocks(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3)));
        }
        reader.Close();
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
    
    public static void RemoveFromStock(int id, int quantite)
    {
        var command = _connexion.CreateCommand();
        command.CommandText = $"UPDATE stocks SET quantite = quantite - {quantite} WHERE id_produit = {id};";
        command.ExecuteNonQuery();
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

    public static List<Commande> GetUserOrders(User user)
    {
        var commmand = _connexion.CreateCommand();
        commmand.CommandText = $"SELECT * FROM commande WHERE id_utilisateur = {user._id};";
        var reader = commmand.ExecuteReader();
        var result = new List<Commande>();
        while (reader.Read())
        {
            if (reader.IsDBNull(3))
            {
                result.Add(new Commande(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetString(4), reader.GetInt16(5), reader.GetInt16(6)));
            }
            else
            {
                result.Add(new Commande(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2), reader.GetString(3), reader.GetString(4), reader.GetInt16(5), reader.GetInt16(6)));
            }
        }
        reader.Close();
        return result;
    }

    public static List<CommandeProduit> GetOrderProducts(Commande order)
    {
        var command = _connexion.CreateCommand();
        command.CommandText = $"SELECT * FROM commande_produit WHERE id_commande = {order.Id};";
        var reader = command.ExecuteReader();
        var result = new List<CommandeProduit>();
        while (reader.Read())
        {
            result.Add(new CommandeProduit(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2)));
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

    public static string GetNomProduit(int idproduit)
    {
        var command = _connexion.CreateCommand();
        command.CommandText = $"SELECT nom_produit FROM produit WHERE id_produit = {idproduit};";
        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetString(0);
        reader.Close();
        return result;
    }
    public static List<int> CheckStockParis()
    {
        var output = new List<int>();
        var produitstock = new List<(int, int)>(); // (idproduit, quantite)
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT id_produit,quantite FROM stocks WHERE id_magasin = 1;";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            produitstock.Add((reader.GetInt32(0), reader.GetInt32(1)));
        }
        reader.Close();
        
        var command2 = Database.Connexion.CreateCommand();
        foreach (var (idproduit, quantite) in produitstock)
        {
            command2.CommandText = $"SELECT seuil_alerte FROM produit WHERE id_produit = {idproduit};";
            var reader2 = command2.ExecuteReader();
            reader2.Read();
            var seuil_alerte = reader2.GetInt32(0);
            if (quantite < seuil_alerte)
            {
                output.Add(idproduit);
            }
            reader2.Close();
        }
        return output;
    }

    public static List<int> CheckStockLyon()
    {
        var output = new List<int>();
        var produitstock = new List<(int, int)>(); // (idproduit, quantite)
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT id_produit,quantite FROM stocks WHERE id_magasin = 2;";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            produitstock.Add((reader.GetInt32(0), reader.GetInt32(1)));
        }
        reader.Close();
        
        var command2 = Database.Connexion.CreateCommand();
        foreach (var (idproduit, quantite) in produitstock)
        {
            command2.CommandText = $"SELECT seuil_alerte FROM produit WHERE id_produit = {idproduit};";
            var reader2 = command2.ExecuteReader();
            reader2.Read();
            var seuil_alerte = reader2.GetInt32(0);
            if (quantite < seuil_alerte)
            {
                output.Add(idproduit);
            }
            reader2.Close();
        }
        return output;
    }
    
    public static List<int> CheckStockNantes()
    {
        var output = new List<int>();
        var produitstock = new List<(int, int)>(); // (idproduit, quantite)
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT id_produit,quantite FROM stocks WHERE id_magasin = 3;";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            produitstock.Add((reader.GetInt32(0), reader.GetInt32(1)));
        }
        reader.Close();
        
        var command2 = Database.Connexion.CreateCommand();
        foreach (var (idproduit, quantite) in produitstock)
        {
            command2.CommandText = $"SELECT seuil_alerte FROM produit WHERE id_produit = {idproduit};";
            var reader2 = command2.ExecuteReader();
            reader2.Read();
            var seuil_alerte = reader2.GetInt32(0);
            if (quantite < seuil_alerte)
            {
                output.Add(idproduit);
            }
            reader2.Close();
        }
        return output;
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
    /// <returns>
    /// Une liste de tuple contenant le nom du magasin et le prix moyen des commandes de ce magasin
    /// (magasin 1, moyenne prix 1), (magasin 2, moyenne prix 2), ...
    /// </returns>
    public static List<(string,double)> MeanPriceCommand()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT magasin.id_magasin, magasin.nom_magasin, AVG(produit.prix_produit*commande_produit.quantite) as " +
                              "moyenne_prix_commandes FROM magasin " +
                              "INNER JOIN commande ON magasin.id_magasin = commande.id_magasin " +
                              "INNER JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "INNER JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "GROUP BY magasin.id_magasin";
        var reader = command.ExecuteReader();
        List<(string, double)> result = new List<(string, double)>();
        while (reader.Read())
        {
            result.Add((reader.GetString(1), reader.GetDouble(2)));
            //Console.WriteLine(reader.GetString(1) + " " + reader.GetDouble(2));
        }
        reader.Close();
        return result;
    }

    /// <summary>
    /// moyenne de toutes les commandes effectué quelque soit le magasin
    /// </summary>
    /// <returns>
    /// moyenne_prix_commandes
    /// </returns>
    public static double MeanPriceAllCommand()
    { 
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT AVG(produit.prix_produit*quantite) as moyenne_prix_commandes FROM commande_produit " +
                              "INNER JOIN produit ON commande_produit.id_produit = produit.id_produit ";
        var reader = command.ExecuteReader();
        var result = 0.0;
        while (reader.Read())
        {
            result = reader.GetDouble(0);
        }
        reader.Close();
        return result;
    }

    /// <summary>
    /// Meilleur client de l'annee et du mois
    /// </summary>
    /// <returns>
    /// une liste contenant des tuples (nom, prenom, prix total)
    /// le premier tuple est le meilleur client de l'annee
    /// le deuxieme tuple est le meilleur client du mois
    /// </returns>
    /// <remarks>
    /// Ne marche pas avec le peuplement actuel, aucune commande effectué en 2023
    /// </remarks>
    public static List<(string,string,double)> BestClient()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT user._id, user.nom_utilisateur, user.prenom_utilisateur, SUM(produit.prix_produit) as prix_total " +
                              "FROM user " +
                              "INNER JOIN commande ON user._id = commande.id_utilisateur " +
                              "INNER JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "INNER JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "WHERE YEAR(commande.date_commande) = YEAR(CURDATE()) " +
                              "GROUP BY user._id " +
                              "ORDER BY prix_total DESC " +
                              "LIMIT 1;";
        var reader = command.ExecuteReader();
        List<(string, string, double)> result = new List<(string, string, double)>();
        while (reader.Read())
        {
            result.Add((reader.GetString(1), reader.GetString(2), reader.GetDouble(3)));
            //Console.WriteLine("meilleur client de l'année : " + reader.GetString(1) + " " + reader.GetString(2) + " avec un montant de " + reader.GetDouble(3));
        }
        reader.Close();
        command.CommandText = "SELECT user._id, user.nom_utilisateur, user.prenom_utilisateur, SUM(produit.prix_produit) as prix_total " +
                              "FROM user " +
                              "INNER JOIN commande ON user._id = commande.id_utilisateur " +
                              "INNER JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "INNER JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "WHERE YEAR(commande.date_commande) = YEAR(CURDATE()) " +
                              "GROUP BY user._id, MONTH(commande.date_commande) " +
                              "ORDER BY prix_total DESC " +
                              "LIMIT 1;";
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add((reader.GetString(1), reader.GetString(2), reader.GetDouble(3)));
            //Console.WriteLine("meilleur client du mois : " + reader.GetString(1) + " " + reader.GetString(2) + " avec un montant de " + reader.GetDouble(3));
        }
        reader.Close();
        return result;
    }
    
    /// <summary>
    /// Compare le total de ventes entre les deux bouquets Maman et Vive la mariée
    /// Utilise une union pour faire la requete en une seule fois
    /// </summary>
    /// <returns>
    /// Une liste contenant des tuples (type_bouquet, total_ventes)
    /// </returns>
    public static List<(string, double)> ComparisonBetweenBouquet()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT 'bouquet Maman' AS type_bouquet, SUM(produit.prix_produit) " +
                              "AS total_ventes FROM commande " +
                              "JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "WHERE produit.nom_produit = 'Maman' " +
                              "UNION " +
                              "SELECT 'Bouquet Vive la mariée' AS type_bouquet, SUM(produit.prix_produit) " +
                              "AS total_ventes FROM commande " +
                              "JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "WHERE produit.nom_produit = 'Vive la mariée'";
        var reader = command.ExecuteReader();
        List<(string, double)> result = new List<(string, double)>();
        while (reader.Read())
        {
            result.Add((reader.GetString(0), reader.GetDouble(1)));
            //Console.WriteLine(reader.GetString(0) + " " + reader.GetDouble(1));
        }
        reader.Close();
        return result;
    }

    /// <summary>
    /// Donne le nom du bouquet ayant été le plus vendu avec le nombre de ventes et le revenu total
    /// </summary>
    /// <returns>
    /// Un tuple avec (nom_bouquet, quantite vendu, revenu_total)
    /// </returns>
    public static (string, int, double) BestBouquet()
    {
        var command = _connexion.CreateCommand();
        command.CommandText = "SELECT produit.nom_produit, COUNT(produit.nom_produit), SUM(produit.prix_produit) " +
                              "FROM commande " +
                              "JOIN commande_produit ON commande.id_commande = commande_produit.id_commande " +
                              "JOIN produit ON commande_produit.id_produit = produit.id_produit " +
                              "GROUP BY produit.nom_produit " +
                              "ORDER BY COUNT(produit.nom_produit) DESC " +
                              "LIMIT 1;";
        var reader = command.ExecuteReader();
        (string, int, double) result = ("", 0, 0.0);
        while (reader.Read())
        {
            result = (reader.GetString(0), reader.GetInt32(1), reader.GetDouble(2));
            //Console.WriteLine("meilleur bouquet : " + reader.GetString(0) + " avec " + reader.GetInt32(1) + " ventes pour un total de " + reader.GetDouble(2));
        }
        reader.Close();
        return result;
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