using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNuke.Collections;

namespace WebXMS.DAL.WebXMS.Models
{
    public class XMSChartModel
    {
        public List<string> labels { get; set; }
        public List<int> data { get; set; }
        public int max { get; set; } = 0;
        public int min { get; set; } = 0;
        public XMSChartModel() {
            labels = new  List<string>();
            data = new List<int>();
        }
    }
}
