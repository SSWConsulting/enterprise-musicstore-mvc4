//using System;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Threading;
//using System.Web.Mvc;
//using System.Web.Security;
//using SSW.MusicStore.CodeFirstData;
//using WebMatrix.WebData;
//using MvcMusicStore.Models;
//namespace SSW.SimpleMembership.Data
//{
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
//    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
//    {
//        private static SimpleMembershipInitializer _initializer;
//        private static object _initializerLock = new object();
//        private static bool _isInitialized;

//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            // Ensure ASP.NET Simple Membership is initialized only once per app start
//            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
//        }

//        private class SimpleMembershipInitializer
//        {
//            public SimpleMembershipInitializer()
//            {
//                //todo: move userscontext to this project.
//                Database.SetInitializer<UsersContext>(null);

//                try
//                {
//                    using (var context = new UsersContext())
//                    {
//                        if (!context.Database.Exists())
//                        {
//                            // Create the SimpleMembership database without Entity Framework migration schema
//                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
//                        }
//                    }

//                    WebSecurity.InitializeDatabaseConnection("MusicStoreEntities", "UserProfile", "UserId", "UserName", autoCreateTables: true);

//                    var roleProvider = (SimpleRoleProvider)Roles.Provider;
//                    if (!roleProvider.RoleExists("Administrator"))
//                    {
//                        roleProvider.CreateRole(("Administrator"));
//                    }
//                    var membershipProvider = (SimpleMembershipProvider)Membership.Provider;
//                    if (membershipProvider.GetUser("StoreAdmin", false) == null)
//                    {
//                        membershipProvider.CreateUserAndAccount("StoreAdmin", "Pass@word1");
//                    }
//                    var userRoles = roleProvider.GetRolesForUser("StoreAdmin").ToList();
//                    if (!userRoles.Contains("Administrator"))
//                    {
//                        roleProvider.AddUsersToRoles(new[] { "StoreAdmin" }, new[] { "Administrator" });
//                    }

//                }
//                catch (Exception ex)
//                {
//                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
//                }
//            }
//        }
//    }
//}
