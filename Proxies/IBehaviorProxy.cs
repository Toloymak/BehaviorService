using Proxies.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proxies
{
    public interface IBehaviorProxy
    {
        VerdictResult GetVerdict(string fio, int age);
    }
}
