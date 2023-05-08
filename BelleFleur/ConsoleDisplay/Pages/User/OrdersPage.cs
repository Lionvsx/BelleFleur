namespace BelleFleur.ConsoleDisplay.Pages.User;

public class OrdersPage : Menu
{
    private Database.Structures.User _activeUser;

    public OrdersPage(Database.Structures.User user)
    {
        _activeUser = user;
        Description = "Voici vos commandes :";
        Options = new List<Option>()
        {
            new("Retour", ExitMenu)
        };
    }
}