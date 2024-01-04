using App;

Console.Clear();
var meanTasks = TimeSpan.Zero;
var meanAsync = TimeSpan.Zero;
var pageSize = 40;
foreach (var i in Enumerable.Range(1, 10))
{
    var taskTime = await Run.RunTasks(pageSize);
    Console.WriteLine("////////////////");
    var asyncTime = await Run.RunAsync(pageSize);

    meanTasks = meanTasks.Add(taskTime);
    meanAsync = meanAsync.Add(asyncTime);
}
Console.WriteLine("----------------");
Console.WriteLine($"Tasks Mean: {meanTasks} | Async Mean: {meanAsync}");
Console.WriteLine("---------------");

// Tasks Total: 00:02:21.9282862 | Async Total: 00:13:59.3457858
// pagesize = 40; range = 1, 10;
// Tasks Mean: 14sec --- Async mean: 1m24sec