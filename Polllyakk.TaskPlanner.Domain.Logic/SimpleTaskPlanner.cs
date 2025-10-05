

using System;
using System.Collections.Generic;
using System.Linq;
using Polllyakk.TaskPlanner.Domain.Models;

namespace Polllyakk.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] items)
        {
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
}
