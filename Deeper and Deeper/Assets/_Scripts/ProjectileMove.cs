using System.Collections;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform thisTransform;
    private bool rigth = true;


    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
        if (Player.S.playerTransform.rotation.y != 0)
            rigth = false;
    }

    private void FixedUpdate()
    {
        thisTransform.position += new Vector3(Time.deltaTime * speed * (rigth ? 1f : -1f), 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>())
            Destroy(gameObject);
    }
}
