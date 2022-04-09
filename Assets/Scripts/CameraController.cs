using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public Transform target;

    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight;

    public bool stopFollow;

    private float lastXPos;
    private float lastYPos;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        lastXPos = transform.position.x;
        lastYPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopFollow) { 
        transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);

        float clampedY = Mathf.Clamp (transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        float amountToMoveX = transform.position.x - lastXPos;
        float amountToMoveY = transform.position.y - lastYPos;

        farBackground.position = farBackground.position + new Vector3(amountToMoveX, amountToMoveY * 0.5f, 0);
        middleBackground.position += new Vector3(amountToMoveX*0.5f, amountToMoveY * 0.2f, 0);

        lastXPos = transform.position.x;
        lastYPos = transform.position.y;
        }
    }
}
