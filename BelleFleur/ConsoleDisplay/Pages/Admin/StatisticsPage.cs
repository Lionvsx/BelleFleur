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
            new("Moyenne du prix d'une commande ", ()=>ShowStatisticMeanAll()),
            new("Moyenne des prix d'une commande par magasin", ()=>ShowStatisticMean()),
            new("Retour", ExitMenu)
        };
    }
    
    public void ShowStatisticMeanAll()
    {
        
        Options[Index] = new Option("La moyenne du prix d'une commande est de " + Database.Database.MeanPriceAllCommand() + " euros", ()=>HideStatisticMeanAll());
        Invoke();
    }
    
    public void HideStatisticMeanAll()
    {
        Options[Index] = new Option("Moyenne du prix d'une commande", ()=>ShowStatisticMeanAll());
        Invoke();
    }
    
    public void ShowStatisticMean()
    {
        string magasin = "\n"+string.Join(" euros \n", Database.Database.MeanPriceCommand())+" euros";
        Options[Index] = new Option("La moyenne des ventes par magasin est :" + magasin , ()=>HideStatisticMean());
        Invoke();
    }
    
    public void HideStatisticMean()
    {
        Options[Index] = new Option("Moyenne des ventes par magasin", ()=>ShowStatisticMean());
        Invoke();
    }
}