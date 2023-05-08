namespace BelleFleur.Database.Structures;

public class Commande_produit
{
    private int id_commande_produit;
    private int id_commande;
    private int id_produit;
    
    public Commande_produit(int id_commande_produit, int id_commande, int id_produit)
    {
        this.id_commande_produit = id_commande_produit;
        this.id_commande = id_commande;
        this.id_produit = id_produit;
    }

    public void Save()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"UPDATE commande_produit SET id_commande = {id_commande}, id_produit = {id_produit} WHERE id_commande_produit = {id_commande_produit};";
        command.ExecuteNonQuery();
    }

    public void Delete()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"DELETE FROM commande_produit WHERE id_commande_produit = {id_commande_produit};";
        command.ExecuteNonQuery();
    }
}