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
            new("Meilleur client annuel", ()=>ShowBestClientAnnual()), // ne fonctionne pas tant qu aucune commande n a ete passee en 2023
            new("Meilleur client mensuel", ()=>ShowBestClientMonthly()), // idem pour le mois actuel de 2023
            new("Comparaison des ventes entre bouquet Maman et Vive la mariée", ()=>ShowComparaisonSales()),
            new("Bouquet le plus populaire", ()=>ShowBestBouquet()),
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
    
    public void ShowBestClientAnnual()
    {
        var bestClient = Database.Database.BestClient();
        Options[Index] = new Option("Le meilleur client annuel est " + bestClient[0].Item1 + " " + bestClient[0].Item2 + " avec un total de " + bestClient[0].Item3 + " euros", ()=>HideBestClientAnnual());
        Invoke();
    }
    
    public void HideBestClientAnnual()
    {
        Options[Index] = new Option("Meilleur client annuel", ()=>ShowBestClientAnnual());
        Invoke();
    }
    
    public void ShowBestClientMonthly()
    {
        var bestClient = Database.Database.BestClient();
        Options[Index] = new Option("Le meilleur client mensuel est " + bestClient[1].Item1 + " " + bestClient[1].Item2 + " avec un total de " + bestClient[1].Item3 + " euros", ()=>HideBestClientMonthly());
        Invoke();
    }
    
    public void HideBestClientMonthly()
    {
        Options[Index] = new Option("Meilleur client mensuel", ()=>ShowBestClientMonthly());
        Invoke();
    }

    public void ShowComparaisonSales()
    {
        var bestBouquet = Database.Database.ComparisonBetweenBouquet();
        Options[Index] = new Option("Comparaison : \n" + bestBouquet[0].Item1 + " : " + bestBouquet[0].Item2 + 
                                    "euros généré \n" + bestBouquet[1].Item1 + " : " + bestBouquet[1].Item2 + 
                                    "euros généré", ()=>HideComparaisonSales());
        Invoke();
    }
    
    public void HideComparaisonSales()
    {
        Options[Index] = new Option("Comparaison des ventes", ()=>ShowComparaisonSales());
        Invoke();
    }
    
    public void ShowBestBouquet()
    {
        var bestBouquet = Database.Database.BestBouquet();
        Options[Index] = new Option("Le bouquet le plus populaire est " + bestBouquet.Item1 + " avec un total de " + bestBouquet.Item2 + " ventes" + " et un total de " + bestBouquet.Item3 + " euros", ()=>HideBestBouquet());
        Invoke();
    }
    
    public void HideBestBouquet()
    {
        Options[Index] = new Option("Meilleur bouquet", ()=>ShowBestBouquet());
        Invoke();
    }
}