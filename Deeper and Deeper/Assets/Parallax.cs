using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float paralaxEffect;
    [SerializeField] private float yPos;

    private float startPos;
    private float length;

    private Transform thisTransform;
    private Transform cameraTransform;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
        cameraTransform = Camera.main.GetComponent<Transform>();
        startPos = thisTransform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    private void Start()
    {
        thisTransform.position = cameraTransform.position;
    }

    private void FixedUpdate()
    {
        float temp = cameraTransform.position.x * (1 - paralaxEffect);
        float dist = cameraTransform.position.x * paralaxEffect;
        thisTransform.position = new Vector3(startPos + dist, Player.S.transform.position.y + yPos, -2);

        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}
