using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    public Transform farBackground, middleBackground;
    [SerializeField] private float middleParallaxEffectX, middleParallaxEffectY, farParallaxEffectX, farParallaxEffectY;

    [SerializeField] private GameObject cam;

    private float lastXPos;
    private float lastYPos;

    void Start()
    {
        lastXPos = cam.transform.position.x;
        lastYPos = cam.transform.position.y;

    }

    void Update()
    {

        float amountToMoveX = cam.transform.position.x - lastXPos;
        float amountToMoveY = cam.transform.position.y - lastYPos;

        farBackground.position    += new Vector3(amountToMoveX * farParallaxEffectX, amountToMoveY * farParallaxEffectY);
        middleBackground.position += new Vector3(amountToMoveX * middleParallaxEffectX, amountToMoveY * middleParallaxEffectY);

        lastXPos = cam.transform.position.x;
        lastYPos = cam.transform.position.y;

    }
}
