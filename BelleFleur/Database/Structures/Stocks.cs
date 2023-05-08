namespace BelleFleur.Database.Structures;

public class Stocks
{
    private int id_stocks;
    private int quantite;
    private int id_produit;
    private int id_magasin;
    
    public Stocks(int id_stocks, int quantite, int id_produit, int id_magasin)
    {
        this.id_stocks = id_stocks;
        this.quantite = quantite;
        this.id_produit = id_produit;
        this.id_magasin = id_magasin;
    }

    public void Save()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"UPDATE stocks SET quantite = {quantite}, id_produit = {id_produit}, id_magasin = {id_magasin} WHERE id_stocks = {id_stocks};";
        command.ExecuteNonQuery();
    }
    
    public void Delete()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"DELETE FROM stocks WHERE id_stocks = {id_stocks};";
        command.ExecuteNonQuery();
    }
    
    public void CheckStocksQuantite(int quantite)
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT p.seuil_alerte FROM produit p INNER JOIN stocks s ON p.id_produit ==s.id_produit;";
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var seuil_alerte = reader.GetInt32(0);
            if (quantite < seuil_alerte)
            {
                Console.WriteLine("Le stock est en dessous du seuil d'alerte");
            }
        }
        
    }
}