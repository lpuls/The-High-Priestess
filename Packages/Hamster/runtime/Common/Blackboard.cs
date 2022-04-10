using System;
using System.Collections.Generic;
using Hamster;

public interface IBackboardVar {
    Type GetValueType();
    string GetName();
    void SetName(string name);

#if UNITY_EDITOR
    void SetValue(object value);
    object GetValueObject();
#endif
}

public class BackboardVar<T> : IBackboardVar, IPool {
    protected T _value = default;
    protected string _name = string.Empty;

    public T GetValue() {
        return _value;
    }

    public void SetValue(T value) {
        _value = value;
    }

    public void SetName(string name) {
        _name = name;
    }

    public string GetName() {
        return _name;
    }

    public Type GetValueType() {
        return typeof(T);
    }

    public void Reset() {
        _value = default;
        _name = string.Empty;
    }

    public static BackboardVar<T> Malloc() {
        return ObjectPool<BackboardVar<T>>.Malloc();
    }

    public static void Free(IBackboardVar inst) {
        ObjectPool<BackboardVar<T>>.Free(inst as BackboardVar<T>);
    }

#if UNITY_EDITOR

    public void SetValue(object value) {
        _value = (T)value;
    }

    public object GetValueObject() {
        return _value;
    }

#endif
}

public class Blackboard {
    protected Dictionary<int, int> _data = new Dictionary<int, int>(new Int32Comparer());

    public bool TryGetValue(int key, out int value) {
        return _data.TryGetValue(key, out value);
    }

    public void SetValue(int key, int value) {
        _data[key] = value;
    }

    public bool HasValue(int key) {
        return _data.ContainsKey(key);
    }

    public void Clean() {
        _data.Clear();
    }
}
