using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Helper
    {
        public static IEnumerable<T> AsEnumerable<T>(this T self)
        {
            yield return self;
        }
    }
}
