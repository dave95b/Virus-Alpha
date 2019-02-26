using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAwaker : MonoBehaviour {

    [SerializeField]
    private GameObject[] gameObjects;

    private IEnumerator Start()
    {
        foreach (var obj in gameObjects)
        {
            obj.SetActive(true);
        }
        yield return null;
        foreach (var obj in gameObjects)
        {
            obj.SetActive(false);
        }

        Destroy(this);
    }
}
