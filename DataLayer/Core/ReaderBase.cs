using System.Linq;
using Dao.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dao.Core
{
    public interface IReader<T> where T : Entity
    {
        IQueryable<T> All { get; }
    }

    public class ReaderBase<T> : IReader<T> where T : Entity 
    {
        protected BehaviorContext BehaviorContext { get; set; }

        protected ReaderBase(BehaviorContext behaviorContext)
        {
            BehaviorContext = behaviorContext;
        }

        public virtual IQueryable<T> All => BehaviorContext.Set<T>().AsNoTracking();
    }
}