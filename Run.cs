namespace App;


// TODO: refactor for small methods
public static class Run
{
    public async static Task<TimeSpan> RunProgram(int pages=1, bool parallel=true)
    {
        var service = new RickMortyApiService();

        WritePagesInfo(pages);

        var getCharacters = await GetCharacters(new GetCharactersDependencies
        {
            Parallel = parallel,
            Pages = pages,
            Service = service
        });

        var downloadFilesTime = await DownloadFiles(
        new DownloadDependencies
        {
            Parallel = parallel,
            Characters = getCharacters.Characters,
            Service = service
        }
        );

        var cleanFilesTime = CleanFiles();

        WriteTimeResume(getCharacters.FinishTime, downloadFilesTime, cleanFilesTime);

        return getCharacters.FinishTime + downloadFilesTime + cleanFilesTime;
    }

    private static async Task<GetCharactersResponse> GetCharacters(GetCharactersDependencies dependencies)
    {
        var startGetCharactersTime = DateTime.Now;
        IEnumerable<Character> characters;
        if (dependencies.Parallel)
            characters = await dependencies.Service.GetCharactersAsyncParallel(dependencies.Pages);
        else
            characters = await dependencies.Service.GetCharactersAsync(dependencies.Pages);

        var getCharactersFinishTime = TimeSpent(startGetCharactersTime);
        Console.WriteLine($"Getted characters in: {getCharactersFinishTime}");
        Console.WriteLine($"First character {characters.First().Name} | Last character: {characters.Last().Name}");
        return new GetCharactersResponse(getCharactersFinishTime, characters);
    }

    private static async Task<TimeSpan> DownloadFiles(DownloadDependencies dependencies)
    {
        var startDownloadFilesTime = DateTime.Now;
        if (dependencies.Parallel)
            await dependencies.Service.DownloadImagesParallel(dependencies.Characters);
        else
            foreach (var character in dependencies.Characters)
                await dependencies.Service.DownloadImageAsync(character.Name, character.Image);

        var downloadFilesFinishTime = TimeSpent(startDownloadFilesTime);
        Console.WriteLine($"Downloaded files in: {downloadFilesFinishTime}");
        return downloadFilesFinishTime;

    }

    private static TimeSpan CleanFiles()
    {
        var startCleanFilesTime = DateTime.Now;
        var filesNumber = IoHandler.GetFilesNumber();
        IoHandler.CleanFilesDir();
        var cleanFilesFinishTime = TimeSpent(startCleanFilesTime);
        Console.WriteLine($"Cleaned {filesNumber} files in: {cleanFilesFinishTime}");
        return cleanFilesFinishTime;
    }


    private static TimeSpan TimeSpent(DateTime startTime) => DateTime.Now - startTime;
    private static void WritePagesInfo(int pages)
    {
        Console.WriteLine("Starting...");

        Console.WriteLine($"Pages: {pages}");
        Console.WriteLine($"Total requests to get all characters: {pages}");
        Console.WriteLine($"Total requests to download all characters images: {pages * 20}");

        Console.WriteLine("------");
    }

    private static void WriteTimeResume(
        TimeSpan getCharactersFinishTime,
        TimeSpan downloadFilesFinishTime,
        TimeSpan cleanFilesFinishTime
        )
    {

        Console.WriteLine("------");
        Console.WriteLine($"Time spended getting characters: {getCharactersFinishTime}");
        Console.WriteLine($"Time spended downloading files: {downloadFilesFinishTime}");
        Console.WriteLine($"Time spended cleaning files: {cleanFilesFinishTime}");
        Console.WriteLine("------");

        var totalTime = getCharactersFinishTime + downloadFilesFinishTime + cleanFilesFinishTime;
        Console.WriteLine($"Total time {totalTime}");
    }
}

