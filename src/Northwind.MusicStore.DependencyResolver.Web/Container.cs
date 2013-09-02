using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using Ninject;

namespace Northwind.MusicStore.DependencyResolver.Web
{
    /// <summary>
    /// Container required to get dependencies that don't get injected
    /// See http://www.jaltiere.com/index.php/2010/05/04/ninject-with-mvc-and-validationattributes/ for more information
    /// </summary>
    public static class Container
    {
        private static IKernel _kernel;

        public static void Initialize(IKernel kernel)
        {
            _kernel = kernel;
        }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static object Get(Type type)
        {
            return _kernel.Get(type);
        }
    }
}
