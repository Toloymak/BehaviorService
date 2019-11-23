using System;
using System.Collections.Generic;
using System.Text;

namespace Dao.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
