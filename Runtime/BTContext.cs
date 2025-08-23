using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimulFactory.BehaviourTree
{
    public class BTContext
    {
        private static readonly StringComparer Cmp = StringComparer.Ordinal;

        private readonly Dictionary<string, float>   floatValues   = new(64, Cmp);
        private readonly Dictionary<string, int>     intValues     = new(64, Cmp);
        private readonly Dictionary<string, bool>    boolValues    = new(64, Cmp);
        private readonly Dictionary<string, Vector3> vector3Values = new(64, Cmp);
        private readonly Dictionary<string, string>  stringValues  = new(64, Cmp);

        public void SetFloat(string key, float value)         => floatValues[key] = value;
        public bool TryGetFloat(string key, out float value)  => floatValues.TryGetValue(key, out value);
        public float GetFloat(string key)                     => floatValues.TryGetValue(key, out var v) ? v : 0f;

        public void SetInt(string key, int value)             => intValues[key] = value;
        public bool TryGetInt(string key, out int value)      => intValues.TryGetValue(key, out value);
        public int GetInt(string key)                         => intValues.TryGetValue(key, out var v) ? v : 0;

        public void SetBool(string key, bool value)           => boolValues[key] = value;
        public bool TryGetBool(string key, out bool value)    => boolValues.TryGetValue(key, out value);
        public bool GetBool(string key)                       => boolValues.TryGetValue(key, out var v) && v;

        public void SetVector3(string key, Vector3 value)     => vector3Values[key] = value;
        public bool TryGetVector3(string key, out Vector3 v)  => vector3Values.TryGetValue(key, out v);
        public Vector3 GetVector3(string key)                 => vector3Values.TryGetValue(key, out var v) ? v : Vector3.zero;

        public void SetString(string key, string value)       => stringValues[key] = value ?? string.Empty;
        public bool TryGetString(string key, out string v)    => stringValues.TryGetValue(key, out v);
        public string GetString(string key)                   => stringValues.TryGetValue(key, out var v) ? v : string.Empty;

        public void Clear()
        {
            floatValues.Clear();
            intValues.Clear();
            boolValues.Clear();
            vector3Values.Clear();
            stringValues.Clear();
        }

        public void Remove(string key)
        {
            floatValues.Remove(key);
            intValues.Remove(key);
            boolValues.Remove(key);
            vector3Values.Remove(key);
            stringValues.Remove(key);
        }
    }
}