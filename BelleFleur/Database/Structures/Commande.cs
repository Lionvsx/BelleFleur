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
    
    public Commande(int id_commande, DateTime date_commande, DateTime date_livraison, string message_commande, string etat_commande, int id_utilisateur, int id_magasin)
    {
        this.id_commande = id_commande;
        this.date_commande = date_commande;
        this.date_livraison = date_livraison;
        this.message_commande = message_commande;
        this.etat_commande = etat_commande;
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
}