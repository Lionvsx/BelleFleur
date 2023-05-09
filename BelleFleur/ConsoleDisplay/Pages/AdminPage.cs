using BelleFleur.ConsoleDisplay.Pages.Admin;

namespace BelleFleur.ConsoleDisplay.Pages;

public class AdminPage : Menu
{
    private Database.Structures.User _activeUser;
    public AdminPage(string username)
    {
        _activeUser = new Database.Structures.User(username);
        Options = new List<Option>
        {
            new("Statistiques des ventes et des magasins", () =>StatisticsPage(_activeUser)),
            new("Vérifier les stocks", CheckStocks),
            new("Toutes les commandes", CheckOrders),
            new("Tous les utilisateurs", CheckUsers),
            new("Tous les produits", CheckProducts),
            new("Changer le mot de passe", ChangePassword),
            new("Quitter", ExitMenu)
        };
        
        Description = "Bienvenue sur le pannel admin " + _activeUser.Username + " !";
    }
    
    public void ChangePassword()
    {
        ConsoleFunctions.ClearConsole();
        Console.WriteLine("Veuillez entrer votre nouveau mot de passe :");
        string newPassword = Console.ReadLine() ?? throw new InvalidOperationException();
        _activeUser.UpdatePassword(newPassword);
        Console.WriteLine("Votre mot de passe a bien été changé !");
        Console.ReadLine();
        Invoke();
    }
    
    public void CheckStocks()
    {
        var stocksPage = new AllStocks(_activeUser);
        stocksPage.Invoke();
        Invoke();
    }
    
    public void CheckOrders()
    {
        var allOrdersPage = new AllOrders(_activeUser);
        allOrdersPage.Invoke();
        Invoke();
    }
    
    public void CheckUsers()
    {
        var usersPage = new AllUsers(_activeUser);
        usersPage.Invoke();
        Invoke();
    }
    
    public void CheckProducts()
    {
        var productsPage = new AllProducts(_activeUser);
        productsPage.Invoke();
        Invoke();
        
    }
    
    public void StatisticsPage(Database.Structures.User user)
    {
        var statisticsPage = new StatisticsPage(user);
        statisticsPage.Invoke();
        Invoke();
    }
}