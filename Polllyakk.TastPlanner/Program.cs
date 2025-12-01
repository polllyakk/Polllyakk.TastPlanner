using Polllyakk.TaskPlanner.Domain.Logic;
using Polllyakk.TaskPlanner.Domain.Models;
using Polllyakk.TaskPlanner.Domain.Models.Enums;
using Pollyakk.TaskPlanner.DataAccess.Abstractions;
using System;


class Program
{
    static void Main()
    {
        IWorkItemsRepository repo = new FileWorkItemsRepository();
        var planner = new SimpleTaskPlanner(repo);

        while (true)
        {
            Console.WriteLine("\nChoose operation:");
            Console.WriteLine("[A] Add work item");
            Console.WriteLine("[B] Build a plan");
            Console.WriteLine("[M] Mark item as completed");
            Console.WriteLine("[R] Remove work item");
            Console.WriteLine("[Q] Quit");
            Console.Write("Enter: ");
            var key = Console.ReadKey().Key;
            Console.WriteLine();

            switch (key)
            {
                case ConsoleKey.A:
                    AddWorkItem(repo);
                    break;

                case ConsoleKey.B:
                    BuildPlan(planner);
                    break;

                case ConsoleKey.M:
                    MarkCompleted(repo);
                    break;

                case ConsoleKey.R:
                    RemoveWorkItem(repo);
                    break;

                case ConsoleKey.Q:
                    repo.SaveChanges();
                    return;
            }
        }
    }

    static void AddWorkItem(IWorkItemsRepository repo)
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Description: ");
        string desc = Console.ReadLine();

        Console.Write("Duration (hours): ");
        int hours = int.Parse(Console.ReadLine());

        Console.Write("Priority (1=high): ");
        int prio = int.Parse(Console.ReadLine());

        var item = new WorkItem
        {
            Title = title,
            Description = desc,
            DurationHours = hours,
            Priority = (Priority)prio,
            IsCompleted = false
        };

        repo.Add(item);
        repo.SaveChanges();

        Console.WriteLine("Added!");
    }

    static void BuildPlan(SimpleTaskPlanner planner)
    {
        var plan = planner.CreatePlan();

        Console.WriteLine("\nPLAN:");
        foreach (var item in plan)
            Console.WriteLine($"{item.Title} ({item.DurationHours}h, priority {item.Priority})");
    }

    static void MarkCompleted(IWorkItemsRepository repo)
    {
        Console.Write("Enter item ID: ");
        Guid id = Guid.Parse(Console.ReadLine());

        var item = repo.Get(id);
        if (item == null)
        {
            Console.WriteLine("Not found!");
            return;
        }

        item.IsCompleted = true;
        repo.Update(item);
        repo.SaveChanges();

        Console.WriteLine("Marked completed.");
    }

    static void RemoveWorkItem(IWorkItemsRepository repo)
    {
        Console.Write("Enter item ID: ");
        Guid id = Guid.Parse(Console.ReadLine());

        if (repo.Remove(id))
            Console.WriteLine("Removed.");
        else
            Console.WriteLine("Not found.");

        repo.SaveChanges();
    }
}

