using App;

Console.Clear();
var meanParallel = TimeSpan.Zero;
var meanAsync = TimeSpan.Zero;
var pages = 1;

foreach (var i in Enumerable.Range(1, 1))
{
    var parallelTime = await Run.RunProgram(pages);
    Console.WriteLine("////////////////");
    var asyncTime = await Run.RunProgram(pages, false);

    meanParallel = meanParallel.Add(parallelTime);
    meanAsync = meanAsync.Add(asyncTime);
}
Console.WriteLine("----------------");
Console.WriteLine($"Parallel Total Time: {meanParallel} | Async Total Time: {meanAsync}");
Console.WriteLine("---------------");

// Parallel Total: 00:02:21.9282862 | Async Total: 00:13:59.3457858
// pagesize = 40; range = 1, 10;
// Tasks Mean: 14sec --- Async mean: 1m24sec