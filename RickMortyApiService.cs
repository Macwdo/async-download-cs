using System.Text.Json;

namespace App;


// TODO: Refactor
public class RickMortyApiService{

    private readonly HttpClient _httpClient;

    public RickMortyApiService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://rickandmortyapi.com/api/")
        };
    }

    public async Task<Character> GetCharacterAsync(int id)
    {
        var response = await _httpClient.GetAsync($"character/{id}");
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();

        var character = JsonSerializer.Deserialize<Character>(responseContent);
        return character;
    }

    public async Task DownloadImage(string character, string url)
    {
        var imageByte = await _httpClient.GetByteArrayAsync(url);
        await File.WriteAllBytesAsync($"./files/{Guid.NewGuid()}.jpeg", imageByte);
    }

    public async Task DownloadImagesAsync(IEnumerable<Character> characters)
    {

        var taskGetImages = new List<Task<byte[]>>();
        var dict = new Dictionary<Character, Task<byte[]>>();
        foreach (var character in characters)
        {
            var request = _httpClient.GetByteArrayAsync(character.Image);
            taskGetImages.Add(request);
        }

        await Task.WhenAll(taskGetImages);
        var imagesResults = taskGetImages.Select(s => s.Result);

        var taskWriteFiles = new List<Task>();
        foreach (var result in imagesResults)
        {
            var task = File.WriteAllBytesAsync($"./files/{Guid.NewGuid()}.jpeg", result);
            taskWriteFiles.Add(task);
        }

        await Task.WhenAll(taskWriteFiles);
    }
    public async Task<IEnumerable<Character>> GetCharactersAsync(int pages)
    {
        var characters = new List<IEnumerable<Character>>();
        foreach (var i in Enumerable.Range(1, pages))
        {
            var charactersResult = await GetCharactersAsyncByPage(i);
            characters.Add(charactersResult);
        }
        return characters.SelectMany(s => s);
    }

    public async Task<IEnumerable<Character>> GetCharactersAsyncByTasks(int pages)
    {
        return await GetCharactersAsyncByPageUsingTasks(pages);
    }

    public async Task<IEnumerable<Character>> GetCharactersAsyncByPageUsingTasks(int pages)
    {
        var tasks = new List<Task<IEnumerable<Character>>>();
        foreach (var i in Enumerable.Range(1, pages))
        {
            var charactersResponse = GetCharactersAsyncByPage(i);
            tasks.Add(charactersResponse);
        }

        await Task.WhenAll(tasks);
        var results = tasks.Select(s => s.Result);
        return results.SelectMany(s => s);
    }


    public async Task<IEnumerable<Character>> GetCharactersAsyncByPage(int page)
    {
        var response = await _httpClient.GetAsync($"character/?page={page}");
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var characterResponse = JsonSerializer.Deserialize<CharacterList>(responseContent);
        return characterResponse.Characters;
    }



}