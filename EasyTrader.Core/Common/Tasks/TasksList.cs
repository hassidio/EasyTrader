using EasyTrader.Core.Common.Lists;

namespace EasyTrader.Core.Common.Tasks
{
    public class TasksList : CommonList<Task>
    {
        public TasksList() { }

        public event EventHandler<TraderTaskSchedulerEventArgs> TaskAdded;

        public override bool OnBeforeAdd(Task task) { return true; }

        public override void OnAfterAdd(Task task, bool isItemAdded)
        {
            TraderTaskSchedulerEventArgs args = new TraderTaskSchedulerEventArgs();
            args.LastTaskAdded = task;
            OnTaskAdded(args);
        }

        protected virtual void OnTaskAdded(TraderTaskSchedulerEventArgs e)
        {
            EventHandler<TraderTaskSchedulerEventArgs> handler = TaskAdded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public override void OnClear() { }
    }
}