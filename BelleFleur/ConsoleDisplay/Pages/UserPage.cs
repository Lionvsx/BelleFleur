namespace BelleFleur.ConsoleDisplay.Pages;

public class UserPage : Menu
{
    public UserPage(string username)
    {
        var user = new Database.Structures.User(username);
        Options = new List<Option>()
        {
            new("Magasin", Shop),
            new("Modifier le profil", EditProfile),
            new("Commandes", Orders),
            new("Déconnexion", Logout),
            new("Changer le mot de passe", ChangePassword),
            new("Quitter", ExitMenu)
        };


        Description = "Bienvenue " + user.Username + " !";
    }

    public void Shop()
    {
        
    }

    public void EditProfile()
    {
        
    }

    public void Orders()
    {
        
    }
    
    public void Logout()
    {
        
    }

    public void ChangePassword()
    {
        
    }
    
    public void CheckStocks()
    {
        
    }
}