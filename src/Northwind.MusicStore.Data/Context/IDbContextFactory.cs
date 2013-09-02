namespace Northwind.MusicStore.Data.Context
{
    public interface IDbContextFactory
    {
        MusicStoreDbContext Get();
    }
}