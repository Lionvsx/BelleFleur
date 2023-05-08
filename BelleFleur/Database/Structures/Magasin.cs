namespace BelleFleur.Database.Structures;

public class Magasin
{
    private int id_magasin;
    private string nom_magasin;
    private string addresse_magasin;
    
    public Magasin(int id_magasin, string nom_magasin, string addresse_magasin)
    {
        this.id_magasin = id_magasin;
        this.nom_magasin = nom_magasin;
        this.addresse_magasin = addresse_magasin;
    }

    public void Save()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"UPDATE magasin SET nom_magasin = '{nom_magasin}', addresse_magasin = '{addresse_magasin}' WHERE id_magasin = {id_magasin};";
        command.ExecuteNonQuery();
    }
    
    public void Delete()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"DELETE FROM magasin WHERE id_magasin = {id_magasin};";
        command.ExecuteNonQuery();
    }
}