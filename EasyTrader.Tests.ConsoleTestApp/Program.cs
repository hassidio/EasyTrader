// See https://aka.ms/new-console-template for more information

using System.Threading.Tasks;

MyConsole.WriteLine("Starting Program...");

int milliseconds1 = 3000;
int milliseconds2 = 1000;
int milliseconds3 = 2000;

var t = new Tasks3(milliseconds1, milliseconds2, milliseconds3);
var r = t.RunTasks();

Thread.Sleep(milliseconds1 + milliseconds2 + milliseconds3 + 1000);
MyConsole.WriteLine("End Program...");



    public class Tasks3
{
    int _milliseconds1;
    int _milliseconds2;
    int _milliseconds3;

    public Tasks3(int milliseconds1, int milliseconds2, int milliseconds3)
    {
        _milliseconds1 = milliseconds1;
        _milliseconds2 = milliseconds2;
        _milliseconds3 = milliseconds3;
    }

    public async Task RunTasks()
    {
        MyConsole.WriteLine("Starting operation...");
        Console.WriteLine($"");


        MyConsole.WriteLine("Starting task 1...");
        var result1 =  LongRunningOperationAsync(_milliseconds1,"Task1");
        MyConsole.WriteLine("End task 1");
        Console.WriteLine($"");

        MyConsole.WriteLine("Starting task 2...");
        var result2 =  LongRunningOperationAsync(_milliseconds2, "Task2");
        MyConsole.WriteLine("End task 2");
        Console.WriteLine($"");

        MyConsole.WriteLine("Starting task 3...");
        var result3 = LongRunningOperationAsync(_milliseconds3, "Task3");
        MyConsole.WriteLine("End task 3");
        Console.WriteLine($"");

        var r1 = await result1;
        var r2 = await result2;

        MyConsole.WriteLine("Result1: " +  r1.Name);
        MyConsole.WriteLine("Result2: " + r2.Name);
        MyConsole.WriteLine("Result3: " + result3.Result.Name);
        Console.WriteLine($"");

        MyConsole.WriteLine("Operations completed!");

    }

    public async Task<MyClass> LongRunningOperationAsync(int milliseconds, string taskName)
    {
        MyConsole.WriteLine($"Starting delay...{milliseconds}: {taskName}");
        await Task.Delay(milliseconds);
        MyConsole.WriteLine($"end delay {milliseconds}: {taskName}");
        Console.WriteLine($"");

        return new MyClass() { Name = $"{taskName} delayed for {milliseconds}" };
    }
}





public class TasksBasicCall
{
    int _milliseconds1;
    int _milliseconds2;

    public TasksBasicCall(int milliseconds1, int milliseconds2)
    {
        _milliseconds1 = milliseconds1;
        _milliseconds2 = milliseconds2;
    }
    public static string GetMessage(string txt)
    {
        var time = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
        return $"{time}: {txt}";
    }

    public void ApiSend() // not async
    {
        Console.WriteLine(GetMessage($"Starting ApiSend.."));

        Console.WriteLine(GetMessage($"Starting Task 1..."));
        Task.Run(() =>
        {
            AsyncCall(_milliseconds1);
            Console.WriteLine(GetMessage($"End Task 1..."));

        });

        Console.WriteLine(GetMessage($"Starting Task 2..."));
        Task.Run(() =>
        {
            AsyncCall(_milliseconds2);
            Console.WriteLine(GetMessage($"End Task 2..."));

        });

        Console.WriteLine(GetMessage($"End ApiSend"));
    }

    protected MyClass AsyncCall(int milliseconds)
    {
        MyConsole.WriteLine(GetMessage($"Starting AsyncCall...{milliseconds}"));

        MyConsole.WriteLine(GetMessage($"Going to sleep {milliseconds}"));
        Thread.Sleep(milliseconds);
        MyConsole.WriteLine(GetMessage($"Wokeup {milliseconds}"));

        MyConsole.WriteLine(GetMessage($"Task completed after {milliseconds} milliseconds"));

        var myclass = new MyClass { Name = "GetData" };

        MyConsole.WriteLine(GetMessage($"End AsyncCall {milliseconds}"));

        return myclass;
    }
}


public class MyConsole
{
    public static string GetMessage(string txt)
    {
        var time = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
        return $"{time}: {txt}";
    }

    public static void WriteLine(string txt)
    {
        Console.WriteLine(GetMessage(txt));
    }
}

public class MyClass
{
    public string Name { get; set; }
}