using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float zoom;
    [Header("Camera Settings")]
    [SerializeField] private float zoomMultiplier = 4f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] float maxZoom = 15f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    [SerializeField] private Camera cam;

    private void Start()
    {
        zoom = cam.orthographicSize;
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}