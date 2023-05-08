namespace BelleFleur.ConsoleDisplay.Pages;

public class AdminPage : Menu
{
    private Database.Structures.User _activeUser;
    public AdminPage(string username)
    {
        _activeUser = new Database.Structures.User(username);
        Options = new List<Option>
        {
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
        
    }
    
    public void CheckOrders()
    {
        
    }
    
    public void CheckUsers()
    {
        var usersPage = new Admin.AllUsers(_activeUser);
        usersPage.Invoke();
        Invoke();
    }
    
    public void CheckProducts()
    {
        var productsPage = new Admin.AllProducts(_activeUser);
        productsPage.Invoke();
        Invoke();
    }
}