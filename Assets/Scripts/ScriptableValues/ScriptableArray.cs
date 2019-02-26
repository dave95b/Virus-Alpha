using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableArray<T> : ScriptableObject, IEnumerable<T>
{
    public T[] Array;

    public int Length { get { return Array.Length; } }

    public T this[int i]
    {
        get { return Array[i]; }
        set { Array[i] = value; }
    }


    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }

    public IEnumerator<T> GetEnumerator()
    {
        int length = Array.Length;
        for (int i = 0; i < length; i++)
        {
            yield return Array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
