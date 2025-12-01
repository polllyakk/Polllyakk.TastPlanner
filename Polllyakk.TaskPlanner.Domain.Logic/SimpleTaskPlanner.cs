

using Polllyakk.TaskPlanner.Domain.Models;
using Pollyakk.TaskPlanner.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Polllyakk.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        private readonly IWorkItemsRepository _repository;

        public SimpleTaskPlanner(IWorkItemsRepository repository)
        {
            _repository = repository;
        }

        public WorkItem[] CreatePlan()
        {
            var items = _repository.GetAll();
            var itemsAsList = items.ToList();
            itemsAsList.Sort(CompareWorkItems);
            return itemsAsList.ToArray();
        }

        private static int CompareWorkItems(WorkItem first, WorkItem second)
        {
            int priorityCompare = second.Priority.CompareTo(first.Priority);
            if (priorityCompare != 0)
                return priorityCompare;

            int dateCompare = first.DueDate.CompareTo(second.DueDate);
            if (dateCompare != 0)
                return dateCompare;

            return string.Compare(first.Title, second.Title, StringComparison.OrdinalIgnoreCase);
        }
    }
