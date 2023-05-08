using BelleFleur.Database.Structures;

namespace BelleFleur.ConsoleDisplay.Pages.Admin;

public class AllProducts : Menu
{
    private Database.Structures.User _activeUser;
    private List<Produit> _products;
    
    public AllProducts(Database.Structures.User user)
    { 
        _activeUser = user;
        _products = new List<Produit>();
        _products = Database.Database.GetProducts();
        Options = new List<Option>();
        Description = $"Bonjour {_activeUser.Username} !\n\nVoici tous les produits, veuillez en selectionner un pour le supprimer :";
        foreach (var product in _products)
        {
            Options.Add(new Option($"Supprimer {product.Nom} - {product.Prix}€ ?", () => RemoveFromProducts(product)));
        }
        
        Options.Add(new Option("Ajouter un produit", AddProduct));
        Options.Add(new Option("Retour", ExitMenu));
        
    }

    public void AddProduct()
    {
        ConsoleFunctions.ClearConsole();
        Console.WriteLine("Nom du produit :");
        string name = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Prix du produit :");
        double price = double.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        Console.WriteLine("Type du produit :");
        string type = Console.ReadLine() ?? throw new InvalidOperationException();
        Console.WriteLine("Seuil de disponibilité du produit :");
        int threshold = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
        
        Database.Database.AddProduct(name, price, type, threshold);
        Options.Add(new Option($"Supprimer {name} - {price}€ ?", () => RemoveFromProducts(_products.First(p => p.Nom == name))));
        Invoke();
    }

    public void RemoveFromProducts(Produit produit)
    {
        _products.Remove(produit);
        produit.Delete();
        Options.Remove(Options[Index]);
        Invoke();
    }
}