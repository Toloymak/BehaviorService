
namespace Dao.Entities
{
    public class Person : Entity
    {
        public string Fio { get; set; }
        public int Age { get; set; }
        public VerdictType Verdict { get; set; }
    }
}