using BelleFleur.Database.Structures;

namespace BelleFleur.ConsoleDisplay.Pages.User;

public class OrdersPage : Menu
{
    private Database.Structures.User _activeUser;
    private List<Commande> _orders;

    public OrdersPage(Database.Structures.User user)
    {
        _activeUser = user;
        Description = $"Bonjour {_activeUser.Username} !\n\nVoici vos commandes :";
        _orders = new List<Commande>();
        _orders = Database.Database.GetUserOrders(_activeUser);
        Options = new List<Option>();
        foreach (var order in _orders)
        {
            Options.Add(new Option($"Commande n°{order.Id}", () => OrderDetails(order)));
        }
        Options.Add(new Option("Retour", ExitMenu));
    }
    
    public void OrderDetails(Commande order)
    {
        ConsoleFunctions.ClearConsole();
        Console.WriteLine($"Commande n°{order.Id} :");
        Console.WriteLine($"Date de la commande : {order.DateCommande}");
        Console.WriteLine($"Date de livraison : {order.DateLivraison}");
        Console.WriteLine($"Statut : {order.EtatCommande}");
        Console.WriteLine($"Prix total : {order.PrixTotal}");
        Console.WriteLine($"Produits :");

        foreach (var product in order.Produits)
        {
            Console.WriteLine($"- {product.Nom} - {product.Prix}€");
        }
        Console.ReadLine();
        Invoke();
    }
}