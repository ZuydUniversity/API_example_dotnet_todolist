// See https://aka.ms/new-console-template for more information
using ToDoListModel.Models;


Console.WriteLine("Tasks testprogramma!");
Console.WriteLine("In dit programma worden alle CRUD operaties uitgevoerd");
Console.WriteLine();
// ophalen alle taken
Console.WriteLine("Alle taken:");
foreach (var task in ToDoTask.ReadAll())
{
    Console.WriteLine($"Id: {task.Id.ToString().PadLeft(5)}, Description: {task.Description.PadRight(30)}, Assigned:{task.AssignedName?.PadRight(10)}, Created: {task.DateTimeCreated}, Finished: {task.DateTimeFinished}");
}

// ophalen specifieke taak
Console.WriteLine();
Console.WriteLine("Ophalen alleen taak 5");
ToDoTask? bestaandeTask = ToDoTask.Read(5);
if (bestaandeTask != null)
{
    Console.WriteLine($"Id: {bestaandeTask.Id.ToString().PadLeft(5)}, Description: {bestaandeTask.Description.PadRight(30)}, Assigned:{bestaandeTask.AssignedName?.PadRight(10)}, Created: {bestaandeTask.DateTimeCreated}, Finished: {bestaandeTask.DateTimeFinished}");
    // update een taak
    Console.WriteLine();
    Console.WriteLine("Assign Rob aan de bierdrinken taak");
    bestaandeTask.AssignPerson("Rob");
    Console.WriteLine($"Id: {bestaandeTask.Id.ToString().PadLeft(5)}, Description: {bestaandeTask.Description.PadRight(30)}, Assigned:{bestaandeTask.AssignedName?.PadRight(10)}, Created: {bestaandeTask.DateTimeCreated}, Finished: {bestaandeTask.DateTimeFinished}");
}


// maak een taak
Console.WriteLine();
Console.WriteLine("We maken een nieuwe taak");
ToDoTask nieuweTaak = new("Ramen lappen");
nieuweTaak.Create();

// ophalen alle taken
Console.WriteLine();
Console.WriteLine("Alle taken (zie de nieuwe taak toegevoegd:");
foreach (var task in ToDoTask.ReadAll())
{ 
    Console.WriteLine($"Id: {task.Id.ToString().PadLeft(5)}, Description: {task.Description.PadRight(30)}, Assigned:{task.AssignedName?.PadRight(10)}, Created: {task.DateTimeCreated}, Finished: {task.DateTimeFinished}");
}

// verwijderen de laatste taak
Console.WriteLine();
Console.WriteLine("Verwjder de laatste taak");
ToDoTask deleteTaak = ToDoTask.ReadAll().Last();
deleteTaak.FinishTask(); // alleen afgeronde taken mogen verwijderd
deleteTaak.Delete();
Console.WriteLine("Alle taken (zie de laatste taak weg):");
foreach (var task in ToDoTask.ReadAll())
{
    Console.WriteLine($"Id: {task.Id.ToString().PadLeft(5)}, Description: {task.Description.PadRight(30)}, Assigned:{task.AssignedName?.PadRight(10)}, Created: {task.DateTimeCreated}, Finished: {task.DateTimeFinished}");
}

