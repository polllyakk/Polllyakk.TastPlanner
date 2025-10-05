using System;
using System.Collections.Generic;
using Polllyakk.TaskPlanner.Domain.Logic;
using Polllyakk.TaskPlanner.Domain.Models.Enums;
using Polllyakk.TaskPlanner.Domain.Models;


internal static class Program
{
    public static void Main(string[] args)
    {
        var items = new List<WorkItem>();

        Console.WriteLine("=== Task Planner ===");

        while (true)
        {
            Console.Write("Enter task title (or empty to finish): ");
            string? title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
                break;

            Console.Write("Enter description: ");
            string description = Console.ReadLine() ?? "";

            Console.Write("Enter due date (dd.MM.yyyy): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString());

            Console.Write("Enter priority (None, Low, Medium, High, Urgent): ");
            Priority priority = Enum.Parse<Priority>(Console.ReadLine() ?? "None", true);

            Console.Write("Enter complexity (None, Minutes, Hours, Days, Weeks): ");
            Complexity complexity = Enum.Parse<Complexity>(Console.ReadLine() ?? "None", true);

            items.Add(new WorkItem
            {
                Title = title,
                Description = description,
                CreationDate = DateTime.Now,
                DueDate = dueDate,
                Priority = priority,
                Complexity = complexity,
                IsCompleted = false
            });

            Console.WriteLine("Task added!\n");
        }

        if (items.Count == 0)
        {
            Console.WriteLine("No tasks entered.");
            return;
        }

        var planner = new SimpleTaskPlanner();
        var plan = planner.CreatePlan(items.ToArray());

        Console.WriteLine("\n=== Sorted Plan ===");
        foreach (var item in plan)
        {
            Console.WriteLine(item.ToString());
        }
    }
}
