namespace BelleFleur.Database.Structures;

public class Produit
{
    private int _id;
    private string nom;
    private string type;
    private float prix;
    private int seuil;

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Nom
    {
        get => nom;
        set => nom = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Type
    {
        get => type;
        set => type = value ?? throw new ArgumentNullException(nameof(value));
    }

    public float Prix
    {
        get => prix;
        set => prix = value;
    }

    public int Seuil
    {
        get => seuil;
        set => seuil = value;
    }

    public Produit(int id, string nom, string type, float prix, int seuil)
    {
        _id = id;
        this.nom = nom;
        this.type = type;
        this.prix = prix;
        this.seuil = seuil;
    }

    public Produit(int _id)
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT * FROM produit WHERE id = {_id};";
        var reader = command.ExecuteReader();
        reader.Read();
        nom = reader.GetString(1);
        type = reader.GetString(2);
        prix = reader.GetFloat(3);
        seuil = reader.GetInt32(4);
        reader.Close();
    }

    public void Save()
    {
        var commmand = Database.Connexion.CreateCommand();
        commmand.CommandText = $"UPDATE produit SET nom = '{nom}', type = '{type}', prix = '{prix}', seuil = '{seuil}' WHERE id = '{_id}';";
        commmand.ExecuteNonQuery();
    }
    
    public void Delete()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"DELETE FROM produit WHERE id = {_id};";
        command.ExecuteNonQuery();
    }
    

    public override string ToString()
    {
        return "Produit: " + nom + " " + type + " " + prix + " " + seuil;
    }
}