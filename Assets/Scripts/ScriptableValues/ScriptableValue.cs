using System;
using UnityEngine;
using UnityEngine.Events;


public class ScriptableValue<T> : ScriptableObject
{
    [SerializeField]
    private T value;

    public event Action<T> OnValueChanged;

#if UNITY_EDITOR
    public T Value
    {
        get { return keepPlaymodeChanges ? value : savedValue; }
        set
        {
            if (keepPlaymodeChanges)
                this.value = value;
            else
                savedValue = value;

            if (OnValueChanged != null)
                OnValueChanged.Invoke(value);
        }
    }

    [SerializeField]
    private bool keepPlaymodeChanges;

    [SerializeField, HideInInspector]
    private T savedValue;

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
        savedValue = value;
    }

#else
    public T Value {
        get { return value; }
        set {
            this.value = value;

            if (OnValueChanged != null)
                OnValueChanged.Invoke(value);
        }
    }

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
#endif
}