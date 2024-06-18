
using Newtonsoft.Json;

public class SettingsFile
{
    private string Name;

    public dynamic Data { get; set; }

    public SettingsFile(string nameFileConfig)
    {
        this.Name = nameFileConfig;
        init();
    }

    public void init()
    {
        string text = File.ReadAllText(Directory.GetCurrentDirectory() + "/config/" + this.Name);
        this.Data = JsonConvert.DeserializeObject(text);
    }
}