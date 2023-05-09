using System.Diagnostics;

namespace BelleFleur.ConsoleDisplay.Pages;

public class Login : Menu
{
    public Login()
    {
        Options = new List<Option>()
        {
            new("Me connecter", UserAccess),
            new("Créer un compte", CreateAccount),
            new("Quitter", () => Environment.Exit(0))
        };
        Description = "Veuillez selectionner un type d'utilisateur";
    }

    public void CreateAccount()
    {
        ConsoleFunctions.ClearConsole();
        Console.Write("Addresse mail: ");
        var email = Console.ReadLine();
        Console.Write("Nom d'utilisateur: ");
        var user = Console.ReadLine();
        Console.Write("Mot de passe: ");
        var password = ConsoleFunctions.ReadPassword();
        // Type password 2 times
        Console.WriteLine("Veuillez confirmer le mot de passe: ");
        var password2 = ConsoleFunctions.ReadPassword();
        // Ask for prénom, nom, adresse, telephone, carte crédit
        Console.Write("Prénom: ");
        var firstName = Console.ReadLine();
        Console.Write("Nom: ");
        var lastName = Console.ReadLine();
        Console.Write("Adresse: ");
        var address = Console.ReadLine();
        Console.Write("Téléphone: ");
        var phone = Console.ReadLine();
        Console.Write("Carte de crédit: ");
        var creditCard = Console.ReadLine();

        if (password != password2)
        {
            Console.WriteLine("Les mots de passe ne correspondent pas, veuillez réessayer.");
            Console.ReadKey();
            Invoke();
        }
        
        Console.WriteLine("Création du compte en cours...");
        var result = Database.Database.CreateAccount(user, password, firstName, lastName, address, phone, creditCard, email);
        if (result == false)
        {
            Console.WriteLine("Une erreur est survenue lors de la création du compte, veuillez réessayer.");
            Console.ReadKey();
            Invoke();
        }
        Console.WriteLine("Compte créé avec succès !");
        Console.ReadKey();
        Invoke();
    }

    private void UserAccess()
    {
        AuthUser();
    }

    public void AuthUser()
    {
        ConsoleFunctions.ClearConsole();
        Console.Write("Utilisateur: ");
        var user = Console.ReadLine();
        Console.Write("Mot de passe: ");
        // Password displays with only * characters
        string password = ConsoleFunctions.ReadPassword();
        Console.WriteLine("Connexion en cours...");
        // Check if null
        if (user == null || password == null) return;
        var adminMode = Database.Database.Authentificate(user, password, true);
        var result= adminMode ? true : Database.Database.Authentificate(user, password);
        
        if (result == false)
        {
            Console.WriteLine("Utilisateur ou mot de passe incorrect, veuillez réessayer.");
            Console.ReadKey();
            Invoke();
        }
        if (adminMode)
        {
            AdminPage adminPage = new AdminPage(user);
            adminPage.Invoke();
        }
        UserPage userPage = new UserPage(user);
        userPage.Invoke();
        Invoke();
    }
}