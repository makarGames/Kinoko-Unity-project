using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health thisHealth;
    private Rigidbody2D thisRigidbody;

    private void Awake()
    {
        thisHealth = GetComponent<Health>();
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Weapon>())
        {
            float damage = other.gameObject.GetComponent<Weapon>().damage;
            thisHealth.health -= damage;
        }
    }
}
