namespace BelleFleur.Database.Structures;

public class Produit
{
    private int _id;
    private string nom;
    private string type;
    private string prix;
    private int seuil;
    private DateTime debut_dispo;
    private DateTime fin_dispo;
    
    public Produit(int id, string nom, string type, string prix, int seuil, DateTime debut_dispo, DateTime fin_dispo)
    {
        _id = id;
        this.nom = nom;
        this.type = type;
        this.prix = prix;
        this.seuil = seuil;
        this.debut_dispo = debut_dispo;
        this.fin_dispo = fin_dispo;
    }

    public Produit(int _id)
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT * FROM produit WHERE id = {_id};";
        var reader = command.ExecuteReader();
        reader.Read();
        nom = reader.GetString(1);
        type = reader.GetString(2);
        prix = reader.GetString(3);
        seuil = reader.GetInt32(4);
        debut_dispo = reader.GetDateTime(5);
        fin_dispo = reader.GetDateTime(6);
        reader.Close();
    }

    public void Save()
    {
        var commmand = Database.Connexion.CreateCommand();
        commmand.CommandText = $"UPDATE produit SET nom = '{nom}', type = '{type}', prix = '{prix}', seuil = {seuil}, debut_dispo = '{debut_dispo:yyyy-MM-dd}', fin_dispo = '{fin_dispo:yyyy-MM-dd}' WHERE id = {_id};";
    }
    
    public void Delete()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"DELETE FROM produit WHERE id = {_id};";
        command.ExecuteNonQuery();
    }
    

    public override string ToString()
    {
        return "Produit: " + nom + " " + type + " " + prix + " " + seuil + " " + debut_dispo + " " + fin_dispo;
    }
}