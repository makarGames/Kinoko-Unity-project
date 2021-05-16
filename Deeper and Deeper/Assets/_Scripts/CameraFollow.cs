using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 posOffset = new Vector3(0, 0f, -140f);
    private Transform playerTransform;
    private Transform thisTransform;
    private void Awake()
    {
        playerTransform = player.GetComponent<Transform>();
        thisTransform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        thisTransform.position = (playerTransform.position + posOffset);
    }
}
