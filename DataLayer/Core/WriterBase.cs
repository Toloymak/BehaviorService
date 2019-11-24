using System;
using Dao.Entities;

namespace Dao.Core
{
    public class WriterBase<T>  where T : Entity
    {
        protected BehaviorContext BehaviorContext { get; set; }

        protected WriterBase(BehaviorContext behaviorContext)
        {
            BehaviorContext = behaviorContext;
        }
        
        public Guid Create(T entity, bool needSave = true)
        {
            BehaviorContext.Add(entity);
            if (needSave)
            {
                this.BehaviorContext.SaveChanges();
            }
            
            return entity.Id;
        }
    }
}