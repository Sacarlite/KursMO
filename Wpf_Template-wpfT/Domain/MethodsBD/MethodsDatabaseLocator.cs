using Bootstrapper.UserBd;
using Domain.UserBd;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MethodsBD
{
  
    public class MethodsDatabaseLocator : IMethodsDatabaseLocator
    {
        public MethodsDatabaseLocator(MethodsDbContext context)
        {
            Context = context;
        }

        public MethodsDbContext Context { get; set; }

        public void RollBack()
        {

            var changedEntries = Context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}
