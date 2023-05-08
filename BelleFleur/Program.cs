// See https://aka.ms/new-console-template for more information

using BelleFleur.ConsoleDisplay.Pages;
using BelleFleur.Database;

Console.WriteLine("Hello, World!");

Database.Connect("root", "root");

//Populate if empty
if (Database.GetUsers().Count == 0)
{
    Database.Populate();
}

var loginPage = new Login();
loginPage.Invoke();
