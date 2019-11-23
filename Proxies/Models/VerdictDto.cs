using Dao.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proxies.Models
{
    public class VerdictDto
    {
        public string Fio { get; set; }
        public int Age { get; set; }
        public VerdictType Verdict { get; set; }
    }
}
