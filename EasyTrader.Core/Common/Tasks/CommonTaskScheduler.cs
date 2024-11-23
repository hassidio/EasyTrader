using System.Diagnostics;

namespace EasyTrader.Core.Common.Tasks
{
    public class CommonTaskScheduler
    {
        public CommonTaskScheduler(bool isRunOnQueue = true)
        {
            _scheduledTasks = new TasksList();
            _scheduledTasks.TaskAdded += ScheduledTasks_TaskAdded;

            IsRunOnQueue = isRunOnQueue;
        }

        //private Object _lockObj = new Object();

        private TasksList _scheduledTasks;

        private bool _canRunNextTask
        {
            get
            {
                //lock (_lockObj)
                {
                    if (_scheduledTasks.Count == 0) { return false; }

                    if (CurrentTask is not null)
                    {
                        if (!CurrentTask.IsCompleted)
                        {
                            // previous task still running
                            return false;
                        }
                    }
                    return true;
                }
            }
        }

        public bool IsRunOnQueue { get; set; }
        public Task CurrentTask { get; private set; }
        public Task ScheduleRunnerTask { get; private set; }

        public bool IsRunning
        {
            get { return ScheduleRunnerTask is not null && CurrentTask is not null; }
        }

        public IEnumerable<Task>? GetScheduledTasks()
        {
            return _scheduledTasks;
        }

        public string TimeStamp { get { return $"{DateTime.Now.ToString("mm:ss.fff")} Thread[{Thread.CurrentThread.ManagedThreadId}]"; } }


        private void ScheduledTasks_TaskAdded(object? sender, TraderTaskSchedulerEventArgs e)
        {
            if (IsRunOnQueue) { RunScheduleAsync(); }
        }

        public void RunScheduleAsync()
        {
            if (!IsRunning)
            {
                ScheduleRunnerTask = Task.Run(RunSchedule);
            }
        }

        private void RunSchedule()
        {
            //lock (_lockObj)
            {
                while (_canRunNextTask)
                {
                    RunTask();

                    CurrentTask.Wait();
                }
                CurrentTask = null;
                ScheduleRunnerTask = null;
            }
        }

        private void RunTask()
        {
            //lock (_lockObj)
            {
                if (_scheduledTasks.Count == 0) { /* no more tasks in the list*/ return; }

                CurrentTask = _scheduledTasks.FirstOrDefault();

                _scheduledTasks.Remove(CurrentTask);

                Debug.WriteLine($"{TimeStamp}: CommonTaskScheduler start task current task id [{CurrentTask.Id}]");
                CurrentTask.Start();
            }
        }

        public void QueueTask(Task task)
        {
            //lock (_lockObj)
            {
                Thread.Sleep(1); //to avoid racing the queue

                Debug.WriteLine($"{TimeStamp}: CommonTaskScheduler Queue task id [{task.Id}]");

                if (_scheduledTasks.Where(e => e.Id == task.Id).FirstOrDefault() is null)
                {
                    _scheduledTasks.Add(task);
                }
            }
        }

        public void WaitAll()
        {
            if (ScheduleRunnerTask is null) { return; }

            Debug.WriteLine($"{TimeStamp}: CommonTaskScheduler Waiting for tasks to finish...");
            ScheduleRunnerTask.Wait();
            Debug.WriteLine($"{TimeStamp}: CommonTaskScheduler Finished !!!");
        }
    }

    public class TraderTaskSchedulerEventArgs : EventArgs
    {
        public Task LastTaskAdded { get; set; }
    }
}