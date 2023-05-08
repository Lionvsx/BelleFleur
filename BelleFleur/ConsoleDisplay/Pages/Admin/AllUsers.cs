namespace BelleFleur.ConsoleDisplay.Pages.Admin;

public class AllUsers : Menu
{
    private Database.Structures.User _activeUser;

    public AllUsers(Database.Structures.User user)
    {
        _activeUser = user;
        Options = new List<Option>();
        Description = $"Bonjour {_activeUser.Username} !\n\nVoici la liste des utilisateurs, appuyez sur entrée pour en supprimer un :";
        foreach (var currentUser in Database.Database.GetUsers())
        {
            if (currentUser._id == _activeUser._id) continue;
            Options.Add(new Option($"{currentUser.Username} - {currentUser.Email}", () => RemoveFromUsers(currentUser)));
        }
        Options.Add(new Option("Retour", ExitMenu));
    }
    
    public void RemoveFromUsers(Database.Structures.User user)
    {
        user.Delete();
        Options.Remove(Options[Index]);
        Invoke();
    }
}