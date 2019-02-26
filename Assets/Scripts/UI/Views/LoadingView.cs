using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingView : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NativeEvent onMapGenerated;

    private CanvasGroupFader fader;

    private void Start()
    {
        fader = GetComponent<CanvasGroupFader>();
    }

    public void StopLoading()
    {
        animator.SetTrigger("LoadingEnd");
        fader.Fade(0f);
    }

    private void OnEnable()
    {
        onMapGenerated.AddListener(StopLoading);
    }

    private void OnDisable()
    {
        onMapGenerated.RemoveListener(StopLoading);
    }
}
