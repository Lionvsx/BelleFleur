namespace BelleFleur.Database.Structures;

public class User
{
    private int _id;
    private string username;
    private string nom;
    private string prenom;
    private string telephone;
    private string email;
    private string addresse;
    private string credit_card;
    private string statut_fidelite;
    private bool is_admin;
    
    public User(int id, string username, string nom, string prenom, string telephone, string email, string addresse, string credit_card, string statut_fidelite, bool is_admin)
    {
        _id = id;
        this.username = username;
        this.nom = nom;
        this.prenom = prenom;
        this.telephone = telephone;
        this.email = email;
        this.addresse = addresse;
        this.credit_card = credit_card;
        this.statut_fidelite = statut_fidelite;
        this.is_admin = is_admin;
    }
    
    public void UpdatePassword()
    {
        
    }

    public void Save()
    {
        var commmand = Database.Connexion.CreateCommand();
        commmand.CommandText = $"UPDATE user SET username = '{username}', nom = '{nom}', prenom = '{prenom}', telephone = '{telephone}', email = '{email}', addresse = '{addresse}', credit_card = '{credit_card}', statut_fidelite = '{statut_fidelite}', is_admin = {is_admin} WHERE id = {_id};";
        commmand.ExecuteNonQuery();
    }
    
    public void Delete()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"DELETE FROM user WHERE id = {_id};";
        command.ExecuteNonQuery();
    }
}