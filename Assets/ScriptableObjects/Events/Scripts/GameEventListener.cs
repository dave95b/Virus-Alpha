using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameEventListener : MonoBehaviour {

    public NativeEvent Event;
    public UnityEvent Response;


    private void OnEnable() {
        if (Event != null)
            Event.AddListener(Response.Invoke);
    }

    private void OnDisable() {
        if (Event != null)
            Event.AddListener(Response.Invoke);
    }
}
