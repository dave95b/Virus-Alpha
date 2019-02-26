using UnityEngine;

public class TouchCamera : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float minZoom = 3f;
    [SerializeField]
    private float maxZoom = 12f;

    [SerializeField]
    private float boundX = 9f;
    [SerializeField]
    private float boundY = 5f;


	private Vector2?[] oldTouchPositions = { null, null };
    private Vector2 oldTouchVector;
    private float oldTouchDistance;

    private new Camera camera;
    private new Transform transform;


    private void Start() {
        camera = GetComponent<Camera>();
        transform = gameObject.transform;
    }

    private void OnValidate() {
        minZoom = Mathf.Max(0, Mathf.Min(minZoom, maxZoom));
        maxZoom = Mathf.Max(0, Mathf.Max(minZoom, maxZoom));

        boundX = Mathf.Max(0, boundX);
        boundY = Mathf.Max(0, boundY);
    }

    void Update() {
		if (Input.touchCount == 0) {
			oldTouchPositions[0] = null;
			oldTouchPositions[1] = null;
		}
		else if (Input.touchCount == 1) {
			if (oldTouchPositions[0] == null || oldTouchPositions[1] != null) {
				oldTouchPositions[0] = Input.GetTouch(0).position;
				oldTouchPositions[1] = null;
			}
			else
                MoveCamera();
		}
		else {
			if (oldTouchPositions[1] == null) {
				oldTouchPositions[0] = Input.GetTouch(0).position;
				oldTouchPositions[1] = Input.GetTouch(1).position;
				oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
				oldTouchDistance = oldTouchVector.magnitude;
			}
			else 
                ZoomCamera();
		}
	}


    private void MoveCamera() {
        Vector2 touchPosition = Input.GetTouch(0).position;
        Vector3 positionDifference = (Vector3) (oldTouchPositions[0] - touchPosition);
        float cameraSizeModifier = camera.orthographicSize / camera.pixelHeight;

        Vector3 newPosition = transform.position + transform.TransformDirection(positionDifference * cameraSizeModifier * moveSpeed);
        newPosition.x = Mathf.Clamp(newPosition.x, -boundX, boundX);
        newPosition.y = Mathf.Clamp(newPosition.y, -boundY, boundY);
        transform.position = newPosition;

        oldTouchPositions[0] = touchPosition;
    }

    private void ZoomCamera() {
        Vector2[] newTouchPositions = {
                    Input.GetTouch(0).position,
                    Input.GetTouch(1).position
                };
        Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
        float newTouchDistance = newTouchVector.magnitude;

        camera.orthographicSize *= oldTouchDistance / newTouchDistance;
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minZoom, maxZoom);

        // Ruch kamery razem z zoomem
        //Vector2 screen = new Vector2(camera.pixelWidth, camera.pixelHeight);
        //transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * camera.orthographicSize / screen.y));
        //transform.position -= transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * camera.orthographicSize / screen.y);


        oldTouchPositions[0] = newTouchPositions[0];
        oldTouchPositions[1] = newTouchPositions[1];
        oldTouchVector = newTouchVector;
        oldTouchDistance = newTouchDistance;
    }
}
