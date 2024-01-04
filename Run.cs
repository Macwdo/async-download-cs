namespace App;

public static class Run
{
    public async static Task<TimeSpan> RunAsync(int pages=1)
    {
        var service = new RickMortyApiService();

        var startTime = DateTime.Now;
        Console.WriteLine("Starting...");

        Console.WriteLine($"Pages: {pages}");
        Console.WriteLine($"Total requests to get all characters: {pages}");
        Console.WriteLine($"Total requests to download all characters images: {pages * 20}");

        Console.WriteLine("------");

        var characters = await service.GetCharactersAsync(pages);
        var getCharactersFinishTime = DateTime.Now - startTime;
        Console.WriteLine($"Getted characters in: {getCharactersFinishTime}");
        Console.WriteLine($"First character {characters.First().Name} | Last character: {characters.Last().Name}");

        var startDownloadFilesTime = DateTime.Now;
        foreach (var character in characters)
            await service.DownloadImage(character.Name, character.Image);
        var downloadFilesFinishTime = DateTime.Now - startDownloadFilesTime;
        Console.WriteLine($"Downloaded files in: {downloadFilesFinishTime}");

        var startCleanFilesTime = DateTime.Now;
        var filesNumber = IoHandler.GetFilesNumber();
        IoHandler.CleanFilesDir();
        var cleanFilesFinishTime = DateTime.Now - startCleanFilesTime;
        Console.WriteLine($"Cleaned {filesNumber} files in: {cleanFilesFinishTime}");

        Console.WriteLine("------");
        Console.WriteLine($"Time spended getting characters: {getCharactersFinishTime}");
        Console.WriteLine($"Time spended downloading files: {downloadFilesFinishTime}");
        Console.WriteLine($"Time spended cleaning files: {cleanFilesFinishTime}");
        Console.WriteLine("------");

        var totalTime = getCharactersFinishTime + downloadFilesFinishTime + cleanFilesFinishTime;
        Console.WriteLine($"Total time {totalTime}");
        return totalTime;
    }

    public async static Task<TimeSpan> RunTasks(int pages=1)
    {
        var service = new RickMortyApiService();

        var startTime = DateTime.Now;
        Console.WriteLine("Starting...");

        Console.WriteLine($"Pages: {pages}");
        Console.WriteLine($"Total requests to get all characters: {pages}");
        Console.WriteLine($"Total requests to download all characters images: {pages * 20}");

        Console.WriteLine("------");

        var characters = await service.GetCharactersAsyncByTasks(pages);
        var getCharactersFinishTime = DateTime.Now - startTime;
        Console.WriteLine($"Getted characters in: {getCharactersFinishTime}");
        Console.WriteLine($"First character {characters.First().Name} | Last character: {characters.Last().Name}");

        var startDownloadFilesTime = DateTime.Now;
        await service.DownloadImagesAsync(characters);
        var downloadFilesFinishTime = DateTime.Now - startDownloadFilesTime;
        Console.WriteLine($"Downloaded files in: {downloadFilesFinishTime}");

        var startCleanFilesTime = DateTime.Now;
        var filesNumber = IoHandler.GetFilesNumber();
        IoHandler.CleanFilesDir();
        var cleanFilesFinishTime = DateTime.Now - startCleanFilesTime;
        Console.WriteLine($"Cleaned {filesNumber} files in: {cleanFilesFinishTime}");

        Console.WriteLine("------");
        Console.WriteLine($"Time spended getting characters: {getCharactersFinishTime}");
        Console.WriteLine($"Time spended downloading files: {downloadFilesFinishTime}");
        Console.WriteLine($"Time spended cleaning files: {cleanFilesFinishTime}");
        Console.WriteLine("------");

        var totalTime = getCharactersFinishTime + downloadFilesFinishTime + cleanFilesFinishTime;
        Console.WriteLine($"Total time {totalTime}");
        return totalTime;

    }
}

