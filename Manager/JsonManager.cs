

using Newtonsoft.Json;

public static class JsonManager
{

    public static Object LoadFromJson(String data, Type c)
    {
        return JsonConvert.DeserializeObject(data, c);
    }

    public static String ToJson(Object o)
    {
        return JsonConvert.SerializeObject(o);
    }
}
