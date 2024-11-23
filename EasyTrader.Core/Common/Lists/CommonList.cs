namespace EasyTrader.Core.Common.Lists
{
    public abstract class CommonList<T> : List<T>
    {

        public abstract bool OnBeforeAdd(T item);
        public abstract void OnAfterAdd(T item, bool isItemAdded);
        public abstract void OnClear();

        public CommonEventArgs CommonEventArgs;

        public CommonList() : base() { }

        public CommonList(CommonEventArgs? commonEventArgs = null) : this()
        { CommonEventArgs = commonEventArgs; }

        public new void Add(T item)
        {
            var isAdd = OnBeforeAdd(item);
            if (isAdd) { base.Add(item); }
            OnAfterAdd(item, isAdd);
        }

        public new void Clear()
        {
            base.Clear();
            OnClear();
        }
    }
}