namespace BelleFleur.Database.Structures;

public class Commande
{
    private int id_commande;
    private DateTime date_commande;
    private DateTime date_livraison;
    private string message_commande;
    private string etat_commande;
    private int id_utilisateur;
    private int id_magasin;

    public int Id
    {
        get => id_commande;
        set => id_commande = value;
    }

    public DateTime DateCommande
    {
        get => date_commande;
        set => date_commande = value;
    }

    public DateTime DateLivraison
    {
        get => date_livraison;
        set => date_livraison = value;
    }

    public string MessageCommande
    {
        get => message_commande;
        set => message_commande = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string EtatCommande
    {
        get => etat_commande;
        set => etat_commande = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int IdUtilisateur
    {
        get => id_utilisateur;
        set => id_utilisateur = value;
    }

    public int IdMagasin
    {
        get => id_magasin;
        set => id_magasin = value;
    }

    public string Username
    {
        get
        {
            var command = Database.Connexion.CreateCommand();
            command.CommandText = $"SELECT username FROM user WHERE _id = {id_utilisateur};";
            var reader = command.ExecuteReader();
            reader.Read();
            var username = reader.GetString(0);
            reader.Close();
            return username;
        }
    }

    public List<Produit> Produits
    {
        get
        {
            var command = Database.Connexion.CreateCommand();
            command.CommandText = $"SELECT p.*\nFROM commande c\nJOIN commande_produit cp ON c.id_commande = cp.id_commande\nJOIN produit p ON cp.id_produit = p.id_produit\nWHERE c.id_commande = {id_commande};\n";
            var reader = command.ExecuteReader();
            var produits = new List<Produit>();
            while (reader.Read())
            {
                var produit = new Produit(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetFloat(3), reader.GetInt32(4));
                produits.Add(produit);
            }
            reader.Close();
            return produits;
        }
    }

    public float PrixTotal
    {
        get
        {
            var command = Database.Connexion.CreateCommand();
            command.CommandText = $"SELECT COALESCE(SUM(cp.quantite * p.prix_produit), 0) as prix_total_commande\nFROM commande c\nLEFT JOIN commande_produit cp ON c.id_commande = cp.id_commande\nLEFT JOIN produit p ON cp.id_produit = p.id_produit\nWHERE c.id_commande = {id_commande};";
            var reader = command.ExecuteReader();
            reader.Read();
            var prixTotal = reader.GetFloat(0);
            reader.Close();
            return prixTotal;
        }
    }

    public Commande(DateTime dateLivraison, string messageCommande, string etatCommande,int idUtilisateur, int idMagasin)
    {
        date_commande = DateTime.Now;
        date_livraison = dateLivraison;
        message_commande = messageCommande;
        etat_commande = etatCommande;
        id_utilisateur = idUtilisateur;
        id_magasin = idMagasin;
        
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"INSERT INTO commande (date_commande, date_livraison, message_commande, etat_commande, id_utilisateur, id_magasin) VALUES ('{date_commande:yyyy-MM-dd}', '{date_livraison:yyyy-MM-dd}', '{message_commande}', '{etat_commande}', {id_utilisateur}, {id_magasin});";
        command.ExecuteNonQuery();

        // Refresh id_commande
        command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT id_commande FROM commande WHERE date_commande = '{date_commande:yyyy-MM-dd}' AND date_livraison = '{date_livraison:yyyy-MM-dd}' AND message_commande = '{message_commande}' AND etat_commande = '{etat_commande}' AND id_utilisateur = {id_utilisateur} AND id_magasin = {id_magasin};";
        var reader = command.ExecuteReader();
        reader.Read();
        id_commande = reader.GetInt32(0);
        reader.Close();
    }
    public Commande(int id_commande, DateTime date_commande, DateTime date_livraison, string message_commande, string etat_commande, int id_utilisateur, int id_magasin)
    {
        this.id_commande = id_commande;
        this.date_commande = date_commande;
        this.date_livraison = date_livraison;
        this.message_commande = message_commande;
        CheckCommandeDate(etat_commande);
        this.id_utilisateur = id_utilisateur;
        this.id_magasin = id_magasin;
    }
    
    public Commande(int id_commande, DateTime date_commande, DateTime date_livraison, string etat_commande, int id_utilisateur, int id_magasin)
    {
        this.id_commande = id_commande;
        this.date_commande = date_commande;
        this.date_livraison = date_livraison;
        this.message_commande = "aucun message";
        CheckCommandeDate(etat_commande);
        this.id_utilisateur = id_utilisateur;
        this.id_magasin = id_magasin;
    }

    public void Save()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"UPDATE commande SET date_commande = '{date_commande:yyyy-MM-dd}', date_livraison = '{date_livraison:yyyy-MM-dd}', message_commande = '{message_commande}', etat_commande = '{etat_commande}', id_utilisateur = {id_utilisateur}, id_magasin = {id_magasin} WHERE id_commande = {id_commande};";
        command.ExecuteNonQuery();
    }
    
    public void Delete()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"DELETE FROM commande WHERE id_commande = {id_commande};";
        command.ExecuteNonQuery();
    }

    public void CheckCommandeDate(string etat_commande)
    {
        if (date_livraison < date_commande+TimeSpan.FromDays(3))
        {
            this.etat_commande = "VINV";
        }
        else
        {
            this.etat_commande = etat_commande;
        }
    }

    

    public void CheckStock()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = "SELECT quantité FROM stock WHERE " +
                              " id_produit = (SELECT cp.id_produit FROM commande_produit cp INNER JOIN commande c " +
                              "ON cp.id_commande = c.id_commande);";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var quantite = reader.GetInt32(0);
            var command2 = Database.Connexion.CreateCommand();
            command2.CommandText = "SELECT p.seuil_alerte FROM produit p " +
                                   "INNER JOIN commande_produit cp ON p.id_produit=cp.id_produit" +
                                   " INNER JOIN commande c ON c.id_commande=cp.id_commande;";
            var reader2 = command2.ExecuteReader();
            var seuil_alerte = reader2.GetInt32(0);
            if (quantite < seuil_alerte)
            {
                Console.WriteLine("Le stock est en dessous du seuil d'alerte");
            }
        }
        reader.Close();
    }
}