namespace App;

public class GetCharactersResponse(TimeSpan finishTime, IEnumerable<Character> characters)
{
    public TimeSpan FinishTime { get; set; } = finishTime;
    public IEnumerable<Character> Characters { get; set; } = characters;
}

public class GetCharactersDependencies
{
    public bool Parallel { get; set; }
    public RickMortyApiService Service { get; set; }
    public int Pages { get; set; }
}

public class DownloadDependencies
{
    public bool Parallel { get; set; }
    public RickMortyApiService Service { get; set; }
    public IEnumerable<Character> Characters { get; set; }

}