using BelleFleur.ConsoleDisplay.Pages.User;

namespace BelleFleur.ConsoleDisplay.Pages;

public class UserPage : Menu
{
    private Database.Structures.User _activeUser;
    public UserPage(string username)
    {
        _activeUser = new Database.Structures.User(username);
        Options = new List<Option>()
        {
            new("Magasin", () => Shop(_activeUser)),
            new("Modifier le profil", EditProfile),
            new("Commandes", () => Orders(_activeUser)),
            new("Changer le mot de passe", ChangePassword),
            new("Me déconnecter", ExitMenu)
        };


        Description = "Bienvenue " + _activeUser.Username + " !";
    }

    public void Shop(Database.Structures.User user)
    {
        var shoppingPage = new ShoppingPage(user);
        shoppingPage.Invoke();
        Invoke();

    }

    public void EditProfile()
    {
        Console.WriteLine("Non implémenté");
        Console.ReadLine();
        Invoke();
    }

    public void Orders(Database.Structures.User user)
    {
        var ordersPage = new OrdersPage(user);
        ordersPage.Invoke();
        Invoke();
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
}