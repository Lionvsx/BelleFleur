INSERT INTO user (username, nom_utilisateur, prenom_utilisateur, telephone_utilisateur, email_utilisateur, password, adresse_utilisateur, carte_credit_utilisateur, statut_fidelite ,is_admin)
VALUES ('MrBellefleur','Bellefleur', 'Michel', '0658003235', 'michel.bellefleur@mail.com', 'password', '2 chemin du chemin', '1234534567678912', NULL, 1),
       ('LucieD','Dufour', 'Lucie', '0612345678', 'lucie.dufour@mail.com', 'password123', '12 rue des Fleurs', '1234567890123456', 'OR', 0),
       ('JulesM','Martin', 'Jules', '0698765432', 'jules.martin@mail.com', 'password456', '8 avenue des Roses', '2345678901234567', 'Bronze', 0),
       ('SophieP','Petit', 'Sophie', '0734567890', 'sophie.petit@mail.com', 'password789', '6 boulevard des Orchidées', '3456789012345678', NULL, 0),
       ('PaulL','Legrand', 'Paul', '0778901234', 'paul.legrand@mail.com', 'password321', '14 impasse des Marguerites', '4567890123456789', 'OR', 0),
       ('EmmaG','Garnier', 'Emma', '0712348901', 'emma.garnier@mail.com', 'password654', '22 rue des Tulipes', '5678901234567890', 'Bronze', 0),
       ('HugoL','Laroche', 'Hugo', '0765432189', 'hugo.laroche@mail.com', 'password987', '28 place des Pivoines', '6789012345678901', NULL, 0),
       ('LouisM','Moreau', 'Louis', '0723456789', 'louis.moreau@mail.com', 'password222', '44 avenue des Iris', '9012345678901234', 'Bronze', 0),
       ('EvaD','Dupont', 'Eva', '0767890123', 'eva.dupont@mail.com', 'password333', '52 boulevard des Jonquilles', '0123456789012345', NULL, 0),
       ('AdamL','Lefevre', 'Adam', '0789012345', 'adam.lefevre@mail.com', 'password444', '60 impasse des Coquelicots', '1234567890123451', 'OR', 0);

INSERT INTO magasin (nom_magasin, adresse_magasin)
VALUES ('Fleuriste Paris ', ' 2 bis avenue garnier'),
       ('Fleuriste Lyon', '35 avenue de la gare'),
       ('Fleuriste Nantes', '1 boulevard des champs');

INSERT INTO produit (nom_produit, type_produit, prix_produit, seuil_alerte, debut_disponibilite, fin_disponibilite)
VALUES
    ('Gros Merci', 'bouquet', 45, 5, NULL, NULL),
    ('L’amoureux', 'bouquet', 65, 5, NULL, NULL),
    ('L’Exotique', 'bouquet', 40, 5, NULL, NULL),
    ('Maman', 'bouquet', 80, 5, NULL, NULL),
    ('Vive la mariée', 'bouquet', 120, 5, NULL, NULL),
    ('Gerbera', 'fleur', 5, 10, NULL, NULL),
    ('Ginger', 'fleur', 4, 10, NULL, NULL),
    ('Glaïeul', 'fleur', 1, 10, '2023-05-01', '2023-11-30'),
    ('Marguerite', 'fleur', 2.25, 10, NULL, NULL),
    ('Rose rouge', 'fleur', 2.50, 10, NULL, NULL),
    ('Vase en verre', 'accessoire', 15, 5, NULL, NULL),
    ('Boîte en bois', 'accessoire', 20, 5, NULL, NULL),
    ('Ruban en satin', 'accessoire', 3, 20, NULL, NULL);

INSERT INTO stocks (quantite, id_produit, id_magasin)
VALUES
    (10, 1, 1),
    (18, 1, 2),
    (15, 1, 3),
    (4, 2, 1),
    (48, 2, 2),
    (20, 2, 3),
    (2, 3, 1),
    (15, 3, 2),
    (32, 3, 3),
    (10, 4, 1),
    (8, 4, 2),
    (12, 4, 3),
    (5, 5, 1),
    (10, 5, 2),
    (5, 5, 3),
    (17, 6, 1),
    (25, 6, 2),
    (11, 6, 3),
    (10, 7, 1),
    (29, 7, 2),
    (1, 7, 3),
    (16, 8, 1),
    (20, 8, 2),
    (15, 8, 3),
    (10, 9, 1),
    (20, 9, 2),
    (15, 9, 3),
    (10, 10, 1),
    (20, 10, 2),
    (15, 10, 3),
    (10, 11, 1),
    (20, 11, 2),
    (15, 11, 3),
    (10, 12, 1),
    (20, 12, 2),
    (15, 12, 3),
    (10, 13, 1),
    (20, 13, 2),
    (15, 13, 3);