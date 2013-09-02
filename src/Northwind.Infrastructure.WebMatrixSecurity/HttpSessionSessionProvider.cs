using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Northwind.MusicStore.BusinessInterfaces;

namespace Northwind.Infrastructure.WebMatrixSecurity
{
    //todo: Move to SSW.Common.Web
    public class HttpSessionSessionProvider : ISessionProvider
    {

        public object this[string name]
        {
            get { return HttpContext.Current.Session[name]; }
            set { HttpContext.Current.Session[name] = value; }
        }

        public IEnumerator GetEnumerator()
        {
            return HttpContext.Current.Session.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            HttpContext.Current.Session.CopyTo(array, index);
        }

        public int Count { get { return HttpContext.Current.Session.Count; } }
        public object SyncRoot { get { return HttpContext.Current.Session.SyncRoot; } }
        public bool IsSynchronized { get { return HttpContext.Current.Session.IsSynchronized; } }
    }
}
