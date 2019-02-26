using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusPositionTracker : MonoBehaviour {

    [SerializeField]
    private VirusValue virus;

    [SerializeField]
    private NativeEvent onTurnChanged;


    private void OnEnable()
    {
        onTurnChanged.AddListener(JumpToVirusPosition);
    }

    private void OnDisable()
    {
        onTurnChanged.RemoveListener(JumpToVirusPosition);
    }


    private void JumpToVirusPosition()
    {
        Vector3 virusPosition = virus.Value.transform.position;
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = virusPosition.x;
        cameraPosition.y = virusPosition.y;

        transform.position = cameraPosition;
    }
}
