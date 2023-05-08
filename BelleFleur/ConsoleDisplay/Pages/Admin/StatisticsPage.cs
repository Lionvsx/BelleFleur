using BelleFleur.Database.Structures;

namespace BelleFleur.
    ConsoleDisplay.Pages.Admin;

public class StatisticsPage : Menu
{
    private Database.Structures.User _activeUser;
    
    public StatisticsPage(Database.Structures.User user)
    {
        _activeUser = user;
        Description = "Voici les statistiques :";
        Options = new List<Option>()
        {
            new("Moyenne des ventes par magasin", MeanSales),
            new("Retour", ExitMenu)
        };
    }
    
    public void MeanSales()
    {
        Database.Database.MeanPriceCommand();
        Invoke();
    }
}