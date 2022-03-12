using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public enum EEventActionSpawnType {
    Action,
    Condition
}

public interface IDrawInspector {
    void Draw(object args);
}

public class EventActionInfoAttribute : System.Attribute {
    public string DisplayName = string.Empty;
    public string Category = string.Empty;

    public EventActionInfoAttribute(string displayName, string category) {
        DisplayName = displayName;
        Category = category;
    }

#if UNITY_EDITOR
    public static Dictionary<string, Dictionary<string, System.Type>> CallbackAndCallbacks = new Dictionary<string, Dictionary<string, System.Type>>();
    
    public static void Clean() {
        CallbackAndCallbacks.Clear();
    }

    public static Dictionary<string, Type> GetTypesByCategory(string category) {
        CallbackAndCallbacks.TryGetValue(category, out Dictionary<string, Type> result);
        return result;
    }

    public static Type GetTypeByCategoryAndDisplayName(string category, string displayName) {
        if (CallbackAndCallbacks.TryGetValue(category, out Dictionary<string, Type> result)) {
            if (result.TryGetValue(displayName, out Type type))
                return type;
        }
        return null;
    }

    public static void Spawner(Assembly assembly) {
        Type[] types = assembly.GetTypes();
        for (int i = 0; i < types.Length; i++) {
            Type classType = types[i];

            EventActionInfoAttribute attribute = classType.GetCustomAttribute<EventActionInfoAttribute>();
            if (null == attribute)
                continue;

            if (!CallbackAndCallbacks.TryGetValue(attribute.Category, out Dictionary<string, Type> result)) {
                result = new Dictionary<string, Type>();
                CallbackAndCallbacks[attribute.Category] = result;
            }
            result.Add(attribute.DisplayName, classType);
        }
    }
#endif
}