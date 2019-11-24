using Dao.Entities;

namespace Dao.Core.Persons
{
    public class PersonWriter : WriterBase<Person>
    {
        public PersonWriter(BehaviorContext behaviorContext) : base(behaviorContext)
        {
        }
    }
}