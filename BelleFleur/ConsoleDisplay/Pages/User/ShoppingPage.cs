namespace BelleFleur.ConsoleDisplay.Pages.User;

public class ShoppingPage : Menu
{
    private Database.Structures.User _activeUser;
    
    public ShoppingPage(Database.Structures.User activeUser)
    {
        this._activeUser = activeUser;
        
        
    }
}