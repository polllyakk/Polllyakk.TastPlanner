using Polllyakk.TaskPlanner.Domain.Models;
using System;

namespace Pollyakk.TaskPlanner.DataAccess.Abstractions
{
    public interface IWorkItemsRepository
    {
        Guid Add(WorkItem item);
        WorkItem Get(Guid id);
        WorkItem[] GetAll();
        bool Update(WorkItem item);
        bool Remove(Guid id);
        void SaveChanges();
    }
}
