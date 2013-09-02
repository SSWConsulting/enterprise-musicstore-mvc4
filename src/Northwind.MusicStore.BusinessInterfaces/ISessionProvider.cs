using System.Collections;

namespace Northwind.MusicStore.BusinessInterfaces
{
    public interface ISessionProvider : ICollection
    {
        object this[string name] { get; set; }
    }
}
