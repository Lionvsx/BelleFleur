namespace BelleFleur.ConsoleDisplay.Pages;

public class UserPage : Menu
{
    public UserPage(string username, bool admin = false)
    {
        Options = new List<Option>()
        {
        };
        
        Description = "Bienvenue " + username + " !";
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