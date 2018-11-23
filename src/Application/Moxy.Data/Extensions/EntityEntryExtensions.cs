using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moxy.Data.Extensions
{
    public static class EntityEntryExtensions
    {
        public static void ApplyEntityAuditable(this IEnumerable<EntityEntry> entries, string operationUser)
        {
            operationUser = operationUser ?? string.Empty;
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is ICreatable creatable)
                    {
                        creatable.CreatedAt = DateTime.Now;
                        creatable.CreatedBy = operationUser;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is IUpdatable updatable)
                    {
                        updatable.UpdatedAt = DateTime.Now;
                        updatable.UpdatedBy = operationUser;
                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    if (entry.Entity is ISoftDeletable softDeletable)
                    {
                        softDeletable.IsDeleted = true;
                        softDeletable.DeletedBy = operationUser;
                        softDeletable.DeletedAt = DateTime.Now;
                        entry.State = EntityState.Modified;
                    }
                }
            }
        }
    }
}
