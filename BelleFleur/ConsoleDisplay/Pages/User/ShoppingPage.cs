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
        
        Options.Add(new Option("Acheter", Checkout));
        Options.Add(new Option("Retour", ExitMenu));

    }

    private void Checkout()
    {
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
}