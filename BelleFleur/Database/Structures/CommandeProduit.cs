namespace BelleFleur.Database.Structures;

public class CommandeProduit
{
    private int id_commande_produit;
    private int id_commande;
    private int id_produit;
    private int quantity;

    public CommandeProduit(int id_commande, int id_produit, int quantity)
    {
        this.id_commande = id_commande;
        this.id_produit = id_produit;
        this.quantity = quantity;
        
        
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"INSERT INTO commande_produit (id_commande, id_produit, quantite) VALUES ({id_commande}, {id_produit}, {quantity});";
        command.ExecuteNonQuery();
        
        // Refresh id_commande_produit
        command.CommandText = $"SELECT id_commande_produit FROM commande_produit WHERE id_commande = {id_commande} AND id_produit = {id_produit} AND quantite = {quantity};";
        var reader = command.ExecuteReader();
        reader.Read();
        id_commande_produit = reader.GetInt32(0);
        reader.Close();
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