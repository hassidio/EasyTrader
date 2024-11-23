namespace EasyTrader.Core.Common.JsonEntityVersions
{
    public interface IObjectVersionTransform
    {
        void UpdateNewVersionObjects(dynamic? oldVersion = null);
    }
}