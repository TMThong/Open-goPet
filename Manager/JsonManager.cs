package manager;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import java.lang.reflect.Type;

public class JsonManager {

    public static  Gson GSON = new GsonBuilder().
//                registerTypeAdapter(ClanMember.class, new ClanMemberAdapter()).
//                registerTypeAdapter(ClanBuff.class, new ClanBuffAdapter()).
                create();

    public JsonManager() {
         
    }

    public static Object LoadFromJson(String data, Class c) {
        return GSON.fromJson(data, c);
    }

    public static Object LoadFromJson(String data, Type c) {
        return GSON.fromJson(data, c);
    }

    public static String ToJson(Object o) {
        return GSON.toJson(o);
    }
}
