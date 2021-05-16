using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
            other.gameObject.GetComponent<Player>().Death();
        if (other.gameObject.GetComponent<Enemy>())
            Destroy(other.gameObject);
    }
}
