using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNuke.Collections;

namespace WebXMS.DAL.WebXMS.Models
{
    public class XMSPagedList<T> 
    {
        public IPagedList<T> results;
        public T filter;
        public XMSPagedList() {

        }
    }


}
