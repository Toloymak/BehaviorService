using Api.Structs;

namespace Api.Models
{
    public class VerdictDto
    {
        public string Fio { get; set; }
        public int Age { get; set; }
        public VerdictType Verdict { get; set; }
    }
}