using Newtonsoft.Json;
using Polllyakk.TaskPlanner.Domain.Models;
using Pollyakk.TaskPlanner.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using Pollyakk.TaskPlanner.DataAccess.Abstractions;
using Polllyakk.TaskPlanner.Domain.Models;

namespace Pollyakk.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "work-items.json";
        private readonly Dictionary<Guid, WorkItem> _items;

        public FileWorkItemsRepository()
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    var array = JsonConvert.DeserializeObject<WorkItem[]>(json);
                    _items = new Dictionary<Guid, WorkItem>();
                    foreach (var item in array)
                        _items[item.Id] = item;
                    return;
                }
            }

            _items = new Dictionary<Guid, WorkItem>();
        }

        public Guid Add(WorkItem workItem)
        {
            var clone = workItem.Clone();
            clone.Id = Guid.NewGuid();
            _items.Add(clone.Id, clone);
            return clone.Id;
        }

        public WorkItem Get(Guid id) =>
            _items.ContainsKey(id) ? _items[id] : null;

        public WorkItem[] GetAll() =>
            new List<WorkItem>(_items.Values).ToArray();

        public bool Update(WorkItem workItem)
        {
            if (!_items.ContainsKey(workItem.Id))
                return false;

            _items[workItem.Id] = workItem.Clone();
            return true;
        }

        public bool Remove(Guid id) =>
            _items.Remove(id);

        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(GetAll(), Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
    }
}
