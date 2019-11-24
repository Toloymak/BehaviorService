using System.Linq;
using Dao.Entities;

namespace Dao.Core.Persons
{
    public class PersonReader : ReaderBase<Person>
    {
        public PersonReader(BehaviorContext behaviorContext) : base(behaviorContext)
        {
        }

        public Person GetByFioAndAge(string fio, int age)
        {
            return this.All
                .FirstOrDefault(x => 
                    x.Fio == fio 
                    && x.Age == age);
        }
    }
}