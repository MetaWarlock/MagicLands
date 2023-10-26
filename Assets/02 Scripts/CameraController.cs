using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public bool stopFollow;

    [SerializeField] private Transform target;

    [SerializeField] private float minHeight, maxHeight;

    [SerializeField] private GameObject cam;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        stopFollow = false;
    }

    void Update()
    {
        if (!stopFollow) { 
        cam.transform.position = new Vector3 (target.position.x, target.position.y, cam.transform.position.z);

        float clampedY = Mathf.Clamp (cam.transform.position.y, minHeight, maxHeight);
        cam.transform.position = new Vector3(cam.transform.position.x, clampedY, cam.transform.position.z);

        }
    }
}
