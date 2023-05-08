CREATE TABLE IF NOT EXISTS user (
    _id int(11) NOT NULL AUTO_INCREMENT,
    username varchar(250) NOT NULL,
    password varchar(250) NOT NULL,
    nom_utilisateur varchar(250) NOT NULL,
    prenom_utilisateur varchar(250) NOT NULL,
    telephone_utilisateur varchar(10) NOT NULL,
    email_utilisateur varchar(250) NOT NULL,
    adresse_utilisateur varchar(250) NOT NULL,
    carte_credit_utilisateur varchar(250) NOT NULL,
    statut_fidelite varchar(250) NOT NULL,
    is_admin boolean NOT NULL,
    PRIMARY KEY (_id)
    ) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS magasin (
    id_magasin int(11) NOT NULL AUTO_INCREMENT,
    nom_magasin varchar(250) NOT NULL,
    adresse_magasin varchar(250) NOT NULL,
    PRIMARY KEY (id_magasin)
    ) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS commande (
    id_commande int(11) NOT NULL AUTO_INCREMENT,
    date_commande date NOT NULL,
    date_livraison date NOT NULL,
    message_commande varchar(250),
    etat_commande varchar(250) NOT NULL,
    id_utilisateur int(11) NOT NULL,
    id_magasin int(11) NOT NULL,
    PRIMARY KEY (id_commande),
    FOREIGN KEY (id_utilisateur) REFERENCES user(_id)
    ) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS produit (
    id_produit int(11) NOT NULL AUTO_INCREMENT,
    nom_produit varchar(250) NOT NULL,
    type_produit varchar(250) NOT NULL, # bouquet, fleur, accessoire
    prix_produit float NOT NULL,
    seuil_alerte int(11) NOT NULL,
    debut_disponibilite date,
    fin_disponibilite date,
    PRIMARY KEY (id_produit)
    ) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS stocks (
    id_stocks int(11) NOT NULL AUTO_INCREMENT,
    quantite int(11) NOT NULL,
    id_produit int(11) NOT NULL,
    id_magasin int(11) NOT NULL,
    PRIMARY KEY (id_stocks),
    FOREIGN KEY (id_produit) REFERENCES produit(id_produit),
    FOREIGN KEY (id_magasin) REFERENCES magasin(id_magasin)
    ) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS commande_produit (
    id_commande_produit int(11) NOT NULL AUTO_INCREMENT,
    id_commande int(11) NOT NULL,
    id_produit int(11) NOT NULL,
    quantite int(11) NOT NULL,
    PRIMARY KEY (id_commande_produit),
    FOREIGN KEY (id_commande) REFERENCES commande(id_commande),
    FOREIGN KEY (id_produit) REFERENCES produit(id_produit)
    ) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1;