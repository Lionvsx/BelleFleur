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
    
    
    public int GetIdStocks()
    {
        return id_stocks;
    }
    
    public int GetQuantite()
    {
        return quantite;
    }
    
    public int GetIdProduit()
    {
        return id_produit;
    }
}