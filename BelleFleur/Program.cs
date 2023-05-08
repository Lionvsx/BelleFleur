// See https://aka.ms/new-console-template for more information

using BelleFleur.ConsoleDisplay.Pages;
using BelleFleur.Database;

Console.WriteLine("Hello, World!");

Database.Connect("root", "root");
Database.Populate();
var menu = new Login();
menu.Invoke();