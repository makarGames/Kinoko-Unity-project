using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public float speed;

    [SerializeField] private float damage;
    private Rigidbody2D thisRigidbody;

    private void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        thisRigidbody.velocity = new Vector2(speed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ImmovableEnemy>()) return;
        if (other.gameObject.GetComponent<Player>())
            Player.S.health -= damage;

        Destroy(gameObject);
    }
}
