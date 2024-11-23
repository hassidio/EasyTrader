using EasyTrader.Core.Models.Entities;

namespace EasyTrader.Core.PersistenceServices
{

    public interface IPersistenceService
    {
        T Get<T>(string id, string? parentId = null)
            where T : IEntity;

        void Save<T>(T item, bool? isArchive = false) where T : IEntity;
        void Delete<T>(T item) where T : IEntity;

        void Archive<T>(T item) where T : IEntity;

        //void ArchiveFragmentedDurations<TParent>(TParent item) where TParent : IEntity;
    }
}