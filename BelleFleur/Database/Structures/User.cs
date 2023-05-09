namespace BelleFleur.Database.Structures;

public class User
{
    public readonly int _id;
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
    
    

    public User(string username)
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT * FROM user WHERE username = '{username}';";
        var reader = command.ExecuteReader();
        reader.Read();
        _id = reader.GetInt32(0);
        nom = reader.GetString(3);
        prenom = reader.GetString(4);
        telephone = reader.GetString(5);
        email = reader.GetString(6);
        addresse = reader.GetString(7);
        credit_card = reader.GetString(8);
        statut_fidelite = reader.GetString(9);
        is_admin = reader.GetBoolean(10);
        reader.Close();
        this.username = username;
    }

    public string Username
    {
        get => username;
        set => username = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Nom
    {
        get => nom;
        set => nom = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Prenom
    {
        get => prenom;
        set => prenom = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Telephone
    {
        get => telephone;
        set => telephone = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Email
    {
        get => email;
        set => email = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Addresse
    {
        get => addresse;
        set => addresse = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string CreditCard
    {
        get => credit_card;
        set => credit_card = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string StatutFidelite
    {
        get => statut_fidelite;
        set => statut_fidelite = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool IsAdmin
    {
        get => is_admin;
        set => is_admin = value;
    }

    public void UpdatePassword(string password)
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"UPDATE user SET password = '{password}' WHERE id = {_id};";
        command.ExecuteNonQuery();
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
        command.CommandText = $"DELETE FROM user WHERE _id = {_id};";
        command.ExecuteNonQuery();
    }
    
    public int[] GetCommandes()
    {
        var command = Database.Connexion.CreateCommand();
        command.CommandText = $"SELECT id_commande FROM commande WHERE id_utilisateur = {_id};";
        var reader = command.ExecuteReader();
        var commandes = new List<int>();
        while (reader.Read())
        {
            commandes.Add(reader.GetInt32(0));
        }
        reader.Close();
        return commandes.ToArray();
    }
}