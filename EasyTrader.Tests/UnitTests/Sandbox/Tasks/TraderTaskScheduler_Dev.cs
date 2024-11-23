using System.Diagnostics;
using EasyTrader.Core.Common.Tasks;

namespace EasyTrader.Tests.UnitTests.Sandbox
{
    public class TraderTaskScheduler_Dev //: TaskScheduler
    {
        public TraderTaskScheduler_Dev()
        {
            _scheduledTasks = new TasksList();
            _scheduledTasks.TaskAdded += ScheduledTasks_TaskAdded;
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
                        if (!CurrentTask.IsCompleted) //*/ .IsCompletedSuccessfully .IsCanceled .IsFaulted
                        {
                            // previous task still running
                            //Debug.WriteLine($"{TimeStamp}: current task [{CurrentTask.EntityNameId}] still running -> end RunTask");
                            return false;
                        }
                    }
                    return true;
                }
            }
        }


        public Task CurrentTask { get; private set; }
        public Task RunScheduleTask { get; private set; }

        public bool IsRunning
        {
            get
            {
                return RunScheduleTask is not null
                    && CurrentTask is not null ? true : false;
            }
        }

        public IEnumerable<Task>? GetScheduledTasks()
        {
            return _scheduledTasks;
        }
        public string TimeStamp { get { return $"{DateTime.Now.ToString("mm:ss.fff")} Thread[{Thread.CurrentThread.ManagedThreadId}]"; } }


        private void ScheduledTasks_TaskAdded(object? sender, TraderTaskSchedulerEventArgs e)
        {
            RunScheduleAsync();
        }

        private async void RunScheduleAsync()
        {
            //*/ task
            if (!IsRunning)
            {
                Debug.WriteLine($"{TimeStamp}: RunScheduleAsync Create new RunSchedule task");
                RunScheduleTask = Task.Run(RunSchedule);

                //ScheduleRunnerTask.Wait();
                //ScheduleRunnerTask =
                //    new Task(() => Debug.WriteLine($"{TimeStamp}: Placeholder RunSchedule task"));
                //RunSchedule();
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
                RunScheduleTask = null;
            }
        }



        private void RunTask()
        {
            //lock (_lockObj)
            {
                Debug.WriteLine($"{TimeStamp}: RunTask start =======");
                PrintSchedule_Tmp("RunTask Before");

                if (_scheduledTasks.Count == 0) { /* no more tasks in the list*/ return; }

                CurrentTask = _scheduledTasks.FirstOrDefault();

                Debug.WriteLine($"{TimeStamp}: RunTask remove task form list CurrentTask.EntityNameId[{CurrentTask.Id}]");
                _scheduledTasks.Remove(CurrentTask);

                Debug.WriteLine($"{TimeStamp}: RunTask Start task CurrentTask.EntityNameId[{CurrentTask.Id}]");
                CurrentTask.Start();

                PrintSchedule_Tmp("RunTask After");
                Debug.WriteLine($"{TimeStamp}: RunTask end =======");
            }
        }


        public void QueueTask(Task task, int i)
        {
            Thread.Sleep(1); //to avoid racing the queue

            //lock (_lockObj)
            {
                Debug.WriteLine($"{TimeStamp}: QueueTask i[{i}]");
                Debug.WriteLine($"{TimeStamp}: QueueTask task.EntityNameId[{task.Id}]");

                if (_scheduledTasks.Where(e => e.Id == task.Id).FirstOrDefault() is null)
                {
                    _scheduledTasks.Add(task);
                }
            }
        }

        public void WaitAll()
        {
            if (RunScheduleTask is null) { return; }

            Debug.WriteLine($"{TimeStamp}: Waiting for tasks to finish...");
            RunScheduleTask.Wait();
            Debug.WriteLine($"{TimeStamp}: Finished !!!");
        }

        private void PrintSchedule_Tmp(string title) //*/ remove
        {
            Debug.WriteLine($"{TimeStamp}: Print");
            Debug.WriteLine($"{TimeStamp}: {title}  Scheduled Tasks: ");
            foreach (var task in _scheduledTasks)
            {
                Debug.WriteLine($"{TimeStamp}: {title}  _scheduledTasks.task.EntityNameId: {task.Id}");
            }
            //Debug.WriteLine($"{TimeStamp}:Print ");
        }
    }

}