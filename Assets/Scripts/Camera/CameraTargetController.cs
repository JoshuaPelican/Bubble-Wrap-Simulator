using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTargetController : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] Vector2 ZoomRange = new Vector2(-10f, 10f);
    [SerializeField] float ZoomSpeed = 1f;

    MouseControls mouseControls;
    Camera mainCam;

    Vector2 CenterScreenPixelPosition { get { return new Vector2(Screen.width / 2, Screen.height / 2); } }

    private void Awake()
    {
        mainCam = Camera.main;
        mouseControls = new MouseControls();
        mouseControls.Mouse.Enable();
        mouseControls.Mouse.Zoom.performed += Zoom;
    }

    private void Update()
    {
        Vector2 mousePosPixels = mouseControls.Mouse.Pan.ReadValue<Vector2>();
        if (mousePosPixels == Vector2.zero)
            mousePosPixels = CenterScreenPixelPosition;

        Vector2 mousePosWorld = mainCam.ScreenToWorldPoint(mousePosPixels);

        transform.position = new Vector3(mousePosWorld.x, mousePosWorld.y, transform.position.z);
    }

    void Zoom(InputAction.CallbackContext context)
    {
        float scrollYDelta = context.ReadValue<float>();

        if (scrollYDelta == 0)
            return;

        Vector3 newPosition = transform.position + Vector3.forward * scrollYDelta * Time.deltaTime * ZoomSpeed;
        newPosition = new Vector3(newPosition.x, newPosition.y, Mathf.Clamp(newPosition.z, ZoomRange.x, ZoomRange.y));
        transform.position = newPosition;

    }
}
