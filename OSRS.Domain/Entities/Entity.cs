using System;
using System.Collections.Generic;
using System.Text;

namespace OSRS.Domain.Entities
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }

    }
}
