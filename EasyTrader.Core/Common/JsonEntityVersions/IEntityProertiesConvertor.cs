namespace EasyTrader.Core.Common.JsonEntityVersions
{
    public interface IEntityProertiesConvertor
    {
        TDerived CopyPropertiesTo<TDerived>() where TDerived : IEntityProertiesConvertor;
    }
}
