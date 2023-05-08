namespace BelleFleur.ConsoleDisplay.Pages.Admin;

public class AllStocks : Menu
{
    private Database.Structures.User _activeUser;
    
    public AllStocks(Database.Structures.User user)
    {
        _activeUser = user;
        Description = "Voici la liste des stocks selons les magasins pour tous les produits :";
        Options = new List<Option>()
        {
            new("Magasin Paris", ()=>ShowStocksParis()),
            new("Retour", ExitMenu)
        };
    }
    
    public void ShowStocksParis()
    {
        string stock = "\n"+string.Join(" \n", Database.Database.GetStocks())+" ";
        Options[Index] = new Option("Le stock du magasin de Paris est :" + stock , ()=>HideStocksParis());
        Invoke();
    }
    
    public void HideStocksParis()
    {
        Options[Index] = new Option("Magasin Paris", ()=>ShowStocksParis());
        Invoke();
    }
}