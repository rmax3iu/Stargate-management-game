<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                         LANGUAGE                                          | -->
<!-- |-------------------------------------------------------------------------------------------| -->
<div align="right">
  <a href="README.md">
    <img src="https://img.shields.io/badge/🇫🇷 Français-1e3a5f?style=for-the-badge" alt="Français"/>
  </a>
  <a href="README.en.md">
    <img src="https://img.shields.io/badge/🇬🇧 English-555555?style=for-the-badge" alt="English"/>
  </a>
</div>

<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                          HEADER                                           | -->
<!-- |-------------------------------------------------------------------------------------------| -->
<h1 align="left">🛸 Stargate Management Game</h1>

<p align="justify">
Ce projet est une application de gestion des missions spatiales, développée en C#, dans le cadre du projet Stargate. L'application permet de gérer les missions initiées dans le cadre du projet : création d'une mission, affectation d'un équipage, définition des objectifs de capture, suivi du budget et des dépenses, ainsi qu'un journal de bord avec les contacts effectués auprès des informateurs.
</p>

<p align="justify">
L'application propose également la consultation des races répertoriées dans la galaxie, des informations sur les planètes, ainsi qu'une page de statistiques. La création d'une nouvelle mission est réservée aux administrateurs.
</p>

<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                        APERÇU                                             | -->
<!-- |-------------------------------------------------------------------------------------------| -->
## 📸 Aperçu

**Image du tableau de bord :**
<div align="center">
  <img src="img/Readme/image1.png" alt="Tableau de bord" width="600"/>
</div>
<br>

**Image de la création d'une nouvelle mission :**
<div align="center">
  <img src="img/Readme/image2.png" alt="Nouvelle mission" width="600"/>
</div>
<br>

**Image des races répertoriées :**
<div align="center">
  <img src="img/Readme/image3.png" alt="Races répertoriées" width="600"/>
</div>
<br>

**Image des informations sur les planètes :**
<div align="center">
  <img src="img/Readme/image4.png" alt="Informations planètes" width="600"/>
</div>
<br>

**Image des statistiques :**
<div align="center">
  <img src="img/Readme/image5.png" alt="Statistiques" width="600"/>
</div>
<br>

**Image du changement de couleur de fond :**
<div align="center">
  <img src="img/Readme/image6.png" alt="Personnalisation de la couleur de fond" width="600"/>
</div>
<br>

<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                     BASE DE DONNÉES                                       | -->
<!-- |-------------------------------------------------------------------------------------------| -->
## 🗄️ Base de données

<p align="justify">
L'ensemble des données de l'application est stocké dans une base SQLite intitulée Stargate.db. Elle contient les tables suivantes : Planete, Espece, Habiter, Allie, Ennemi, Membre, Civil, Militaire, Mission, Composer, Depense, TypeDepense, JournalDeBord, Contact, Informateur, Capturer, ObjectifCapture, Negocier et Admin.
</p>

<div align="center">
  <img src="img/Readme/image7.png" alt="Schéma de la base de données" width="600"/>
</div>
<br>

<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                        INSTALLATION                                       | -->
<!-- |-------------------------------------------------------------------------------------------| -->
## 🚀 Installation et lancement

<p align="justify">
Pour installer et lancer le projet, commencez par cloner le dépôt GitHub puis déplacez-vous dans le dossier du projet avec la commande cd Stargate-management-game. Ouvrez ensuite la solution SAE24_Stargate.sln avec Visual Studio. Une fois la solution ouverte dans Visual Studio, lancez le projet en appuyant sur la touche F5 de votre clavier ou en cliquant sur la flèche verte située en haut de l'interface.
</p>

<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                   ACCÈS ADMINISTRATEUR                                    | -->
<!-- |-------------------------------------------------------------------------------------------| -->
## 🔑 Accès administrateur

<p align="justify">
La création d'une nouvelle mission est réservée aux utilisateurs disposant de droits d'administration. Un compte de test est disponible :
</p>

|   Login   |   Mot de passe   |
|-----------|------------------|
| admin | &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;admin |

<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                    STRUCTURE DU PROJET                                    | -->
<!-- |-------------------------------------------------------------------------------------------| -->
## 📁 Structure du projet

```text
Stargate-management-game/
├── Modeles/                   # Connexion à la base de données et gestion du DataSet
├── Properties/                # Ressources et paramètres du projet
├── Resources/                 # Images utilisées dans l'interface
├── Librairies/                # Bibliothèques externes 
├── SharpZipLib/               # Bibliothèque de compression
├── img/                       # Images des planètes et images pour le README
├── Form1.cs                   # Formulaire de démarrage
├── frmLogin.cs                # Authentification administrateur
├── frmChargement.cs           # Écran de chargement
├── ucVueTableauDeBord.cs      # Volet tableau de bord
├── ucVueNvlMission.cs         # Volet création d'une nouvelle mission
├── ucVueAliens.cs             # Volet races répertoriées
├── ucVueInfosPlanetes.cs      # Volet informations sur les planètes
├── ucVueStatistiques.cs       # Volet statistiques
├── ucVueParametres.cs         # Volet personnalisation 
├── SAE24_Stargate.csproj
├── SAE24_Stargate.sln
└── README.md
```

<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                      DÉPENDANCES                                          | -->
<!-- |-------------------------------------------------------------------------------------------| -->
## 📦 Dépendances

| Package | Usage |
|---------|-------|
| **BCrypt.Net-Core** | Hachage et sécurisation des mots de passe |
| **LiveCharts** | Création de graphiques et visualisation de données |
| **LiveCharts.WinForms** | Intégration de graphiques dans l'interface Windows Forms |
| **LiveCharts.Wpf** | Intégration de graphiques dans l'interface WPF |
| **PdfSharp.MigraDoc.Standard** | Création et mise en forme de documents PDF |
| **PdfSharp.MigraDoc.Standard.DocumentObjectModel** | Gestion de la structure et du contenu des documents PDF |
| **PDFSharp.Standard** | Génération et manipulation de fichiers PDF |
| **PDFSharp.Standard.Charting** | Création de graphiques destinés aux documents PDF |

<!-- |-------------------------------------------------------------------------------------------| -->
<!-- |                                       CONTRIBUTEURS                                       | -->
<!-- |-------------------------------------------------------------------------------------------| -->
## 👥 Contributeurs

Travail réalisé en trinôme dans le cadre d'un projet à l'IUT Robert Schuman.

<div align="center">

[![rmax3iu](https://img.shields.io/badge/rmax3iu-1e3a5f?style=for-the-badge&logo=github&logoColor=white)](https://github.com/rmax3iu)
[![marwaaan212](https://img.shields.io/badge/marwaaan212-1e3a5f?style=for-the-badge&logo=github&logoColor=white)](https://github.com/marwaaan212)
[![GalatiMaxime](https://img.shields.io/badge/GalatiMaxime-1e3a5f?style=for-the-badge&logo=github&logoColor=white)](https://github.com/GalatiMaxime)

</div>
