using System.Collections.Generic;

namespace SimulFactory.BehaviourTree
{
    public class BTContext
    {
        public BTContext() { }

        Dictionary<string, object> contextData = new Dictionary<string, object>();

        public void SetValue<T>(string key, T value)
        {
            if (contextData.ContainsKey(key))
            {
                contextData[key] = value;
            }
            else
            {
                contextData.Add(key, value);
            }
        }

        public T GetValue<T>(string key)
        {
            if (contextData.TryGetValue(key, out var value))
            {
                return (T)value;
            }
            return default;
        }
    }
}
