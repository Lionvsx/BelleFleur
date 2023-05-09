using BelleFleur.Database.Structures;

namespace BelleFleur.ConsoleDisplay.Pages.User;

public class ShoppingPage : Menu
{
    private Database.Structures.User _activeUser;
    private List<Produit> _products;
    private List<Produit> _cart = null!;

    public ShoppingPage(Database.Structures.User activeUser)
    {
        _cart = new List<Produit>();
        _activeUser = activeUser;
        Description = "Bienvenue " + activeUser.Username + " !" +
                      "\n\nVoici les produits disponibles à l'achat :" +
                      "Veuillez selectionner un produit pour l'ajouter à votre panier.";


        Options = new List<Option>();
        
        _products = Database.Database.GetProducts();
        foreach (var product in _products)
        {
            Options.Add(new Option($"{product.Nom} - {product.Prix}€", () => AddToCart(product)));
        }
        
        Options.Add(new Option("Commande Personnalisée", CustomOrder));
        Options.Add(new Option("Acheter", Checkout));
        Options.Add(new Option("Retour", ExitMenu));
    }

    private void Checkout()
    {
        ConsoleFunctions.ClearConsole();
        Console.WriteLine("Dans combien de temps voulez-vous être livré ? (en jours)");
        int days = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        DateTime deliveryDate = DateTime.Now.AddDays(days);
        
        Console.WriteLine("Veuillez entrer un message pour le magasin");
        string message = Console.ReadLine() ?? throw new InvalidOperationException();
        
        Console.WriteLine("Dans quelle ville habitez vous ? (Paris, Nantes Lyon)");
        string city = Console.ReadLine() ?? throw new InvalidOperationException();
        
        var sqlRequest = Database.Database.GetData($"SELECT (id_magasin) FROM magasin WHERE nom_magasin = 'Fleuriste {city}'");
        sqlRequest.Read();
        int storeId = sqlRequest.GetInt32(0);
        sqlRequest.Close();

        var order = new Commande(deliveryDate, message, "VINV", _activeUser._id, storeId);
        
        foreach (var product in _cart)
        {
            var orderProduct = new CommandeProduit(order.Id, product.Id, 1);
            Database.Database.RemoveFromStock(product.Id, 1);
        }
        
        Console.WriteLine("Votre commande a bien été envoyée !");
        ExitMenu();
    }

    public void AddToCart(Produit product)
    {
        _cart.Add(product);
        Options[Index] = new Option(product.Nom + " - Dans le panier ✓", () => RemoveFromCart(product));
        Options[Options.Count - 2] = new Option("Acheter " + _cart.Count + " produits - " + _cart.Sum(p => p.Prix) + "€", Checkout);
        Invoke();
    }
    
    public void RemoveFromCart(Produit product)
    {
        _cart.Remove(product);
        Options[Index] = new Option(product.Nom + " - " + product.Prix + "€", () => AddToCart(product));
        Options[Options.Count - 2] = new Option("Acheter " + _cart.Count + " produits - " + _cart.Sum(p => p.Prix) + "€", Checkout);
        Invoke();
    }

    public void CustomOrder()
    {
        ConsoleFunctions.ClearConsole();
        Console.WriteLine("Veuillez entrer votre demande");
        string message = Console.ReadLine() ?? throw new InvalidOperationException();
        
        Console.WriteLine("Dans quelle ville habitez vous ? (Paris, Nantes Lyon)");
        string city = Console.ReadLine() ?? throw new InvalidOperationException();
        
        var sqlRequest = Database.Database.GetData($"SELECT (id_magasin) FROM magasin WHERE nom_magasin = 'Fleuriste {city}'");
        sqlRequest.Read();
        int storeId = sqlRequest.GetInt32(0);
        sqlRequest.Close();
        
        var order = new Commande(DateTime.Now, message, "VINV", _activeUser._id, storeId);
        Console.WriteLine("Votre commande a bien été envoyée !");
        ExitMenu();
    }
}