using BelleFleur.Database.Structures;

namespace BelleFleur.ConsoleDisplay.Pages.Admin;

public class AllStocks : Menu
{
    private Database.Structures.User _activeUser;
    private List<Stocks> _stocks;

    public AllStocks(Database.Structures.User user)
    {
        _activeUser = user;
        Description = "Voici la liste des stocks selons les magasins pour tous les produits :";
        Options = new List<Option>()
        {
            new("Magasin Paris", ()=>ShowStocksParis()),
            new("Magasin Lyon", ()=>ShowStocksLyon()),
            new("Magasin Nantes", ()=>ShowStocksNantes()),
            new("Retour", ExitMenu)
        };
    }
    
    public void ShowStocksParis()
    {
        _stocks = Database.Database.GetStocksParis();
        string stock = "\n";
        foreach (var currentStock in _stocks)
        {
            string nomproduit = Database.Database.GetNomProduit(currentStock.GetIdProduit());
            stock += nomproduit + " : " + currentStock.GetQuantite() + "\n";
        }
        
        var missingproduit = Database.Database.CheckStockParis();
        foreach (var currentMissing in missingproduit)
        {
            stock += "Il manque " + Database.Database.GetNomProduit(currentMissing)+"\n";
        }
        Options[Index] = new Option("Le stock du magasin de Paris est :" + stock , ()=>HideStocksParis());
        Invoke();
    }
    
    public void HideStocksParis()
    {
        Options[Index] = new Option("Magasin Paris", ()=>ShowStocksParis());
        Invoke();
    }
    
    public void ShowStocksLyon()
    {
        _stocks = Database.Database.GetStocksLyon();
        string stock = "\n";
        foreach (var currentStock in _stocks)
        {
            string nomproduit = Database.Database.GetNomProduit(currentStock.GetIdProduit());
            stock += nomproduit + " : " + currentStock.GetQuantite() + "\n";
        }
        
        var missingproduit = Database.Database.CheckStockLyon();
        foreach (var currentMissing in missingproduit)
        {
            stock += "Il manque " + Database.Database.GetNomProduit(currentMissing)+"\n";
        }
        Options[Index] = new Option("Le stock du magasin de Lyon est :" + stock , ()=>HideStocksLyon());
        Invoke();
    }
    
    public void HideStocksLyon()
    {
        Options[Index] = new Option("Magasin Lyon", ()=>ShowStocksLyon());
        Invoke();
    }

    public void ShowStocksNantes()
    {
        _stocks = Database.Database.GetStocksNantes();
        string stock = "\n";
        foreach (var currentStock in _stocks)
        {
            string nomproduit = Database.Database.GetNomProduit(currentStock.GetIdProduit());
            stock += nomproduit + " : " + currentStock.GetQuantite() + "\n";
        }
        
        var missingproduit = Database.Database.CheckStockNantes();
        foreach (var currentMissing in missingproduit)
        {
            stock += "Il manque " + Database.Database.GetNomProduit(currentMissing)+"\n";
        }
        Options[Index] = new Option("Le stock du magasin de Lyon est :" + stock , ()=>HideStocksNantes());
        Invoke();
    }
    
    public void HideStocksNantes()
    {
        Options[Index] = new Option("Magasin Nantes", ()=>ShowStocksNantes());
        Invoke();
    }
    
}