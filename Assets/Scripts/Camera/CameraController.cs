using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 ZoomRange = new Vector2(1, 8);
    [SerializeField] float MobilePinchZoomModifier = 0.01f;

    new Camera camera;

    Vector2 MousePositionWorld { get {return camera.ScreenToWorldPoint(Input.mousePosition) ; } }
    Vector2 touchStart;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        //Mobile
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Input.touchCount == 2)
            {
                //Panning Mobile
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    touchStart = MousePositionWorld;
                }
                if (Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    Vector2 direction = touchStart - MousePositionWorld;
                    transform.position += (Vector3)direction;
                }

                //Zooming Mobile
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                Zoom(difference * MobilePinchZoomModifier);
            }
        }

        //Desktop
        else if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            //Panning Desktop
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                touchStart = MousePositionWorld;
            }
            if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                Vector2 direction = touchStart - MousePositionWorld;
                transform.position += (Vector3)direction;
            }

            //Zooming Desktop
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }


    }

    void Zoom(float increment)
    {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - increment, ZoomRange.x, ZoomRange.y);
    }
}
