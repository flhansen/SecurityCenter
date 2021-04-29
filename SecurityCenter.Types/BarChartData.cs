using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Types
{
    public class BarChartData
    {
        public IEnumerable<float> Values { get; set; }
        public string Category { get; set; }
    }
}
