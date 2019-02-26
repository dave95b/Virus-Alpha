using System;
using UnityEngine;

[CreateAssetMenu(order = 160)]
public class NativeEvent : ScriptableObject
{

    private Action _event;


    public void Invoke()
    {
        if (_event != null)
            _event();
    }

    public void AddListener(Action listener)
    {
        _event += listener;
    }

    public void RemoveListener(Action listener)
    {
        _event -= listener;
    }
}

public abstract class NativeEvent<T> : ScriptableObject {

    private Action<T> _event;


    public void Invoke(T data)
    {
        if (_event != null)
            _event(data);
    }

    public void AddListener(Action<T> listener)
    {
        _event += listener;
    }

    public void RemoveListener(Action<T> listener)
    {
        _event -= listener;
    }
}

public abstract class NativeEvent<T1, T2> : ScriptableObject
{

    private Action<T1, T2> _event;


    public void Invoke(T1 data1, T2 data2)
    {
        if (_event != null)
            _event(data1, data2);
    }

    public void AddListener(Action<T1, T2> listener)
    {
        _event += listener;
    }

    public void RemoveListener(Action<T1, T2> listener)
    {
        _event -= listener;
    }
}