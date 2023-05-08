namespace BelleFleur.ConsoleDisplay.Pages;

public class Login : Menu
{
    public Login()
    {
        Options = new List<Option>()
        {
            new("Utilisateur", UserAccess),
            new("Administrateur", AdminAccess),
            new("Créer un compte", CreateAccount),
            new("Quitter", ExitMenu)
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
        
        if (password != password2)
        {
            Console.WriteLine("Les mots de passe ne correspondent pas, veuillez réessayer.");
            Console.ReadKey();
            Invoke();
        }
        
        Console.WriteLine("Création du compte en cours...");
        var result = Database.Database.CreateAccount(user, password);
    }

    private void UserAccess()
    {
        AuthUser();
    }
    private void AdminAccess()
    {
        AuthUser(true);
    }

    public void AuthUser(bool admin = false)
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
        var result= Database.Database.Authentificate(user, password, true);
        if (result == false)
        {
            Console.WriteLine("Utilisateur ou mot de passe incorrect, veuillez réessayer.");
            Console.ReadKey();
            Invoke();
        }
        UserPage userPage = new UserPage(user, admin);
    }
}