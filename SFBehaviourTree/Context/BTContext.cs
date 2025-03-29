namespace SFBehaviourTree.Context
{
    public class BTContext
    {
        private Dictionary<string, object> data = new();

        public void Set<T>(string key, T value)
        {
            data[key] = value;
        }

        public T Get<T>(string key)
        {
            if (data.TryGetValue(key, out var value) && value is T typed)
                return typed;
            throw new InvalidCastException($"Cannot cast value of key '{key}' to {typeof(T)}");
        }

        public bool TryGet<T>(string key, out T value)
        {
            if (data.TryGetValue(key, out var obj) && obj is T typed)
            {
                value = typed;
                return true;
            }

            value = default;
            return false;
        }

        public void Clear()
        {
            data.Clear();
        }
    }
}