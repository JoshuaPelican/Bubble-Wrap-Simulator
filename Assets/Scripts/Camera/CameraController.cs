using UnityEngine;

public class CameraController : MonoBehaviour
{
    new Camera camera;
    MouseControls mouseControls;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        mouseControls = new MouseControls();
        mouseControls.Enable();
    }

    private void Update()
    {
        
    }
}
