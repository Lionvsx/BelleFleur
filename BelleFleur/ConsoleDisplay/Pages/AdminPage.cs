namespace BelleFleur.ConsoleDisplay.Pages;

public class AdminPage : Menu
{
    public AdminPage(string username)
    {
        var user = new Database.Structures.User(username);
        Options = new List<Option>
        {
            new("Vérifier les stocks", CheckStocks),
            new("Toutes les commandes", CheckOrders),
            new("Tous les utilisateurs", CheckUsers),
            new("Tous les produits", CheckProducts),
            new("Changer le mot de passe", ChangePassword),
            new("Quitter", ExitMenu)
        };
        
    }
    
    public void ChangePassword()
    {
        
    }
    
    public void CheckStocks()
    {
        
    }
    
    public void CheckOrders()
    {
        
    }
    
    public void CheckUsers()
    {
        
    }
    
    public void CheckProducts()
    {
        
    }
}