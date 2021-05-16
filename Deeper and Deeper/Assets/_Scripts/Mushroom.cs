using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            MushroomController.S.numberOfMushrooms++;
            Destroy(gameObject);
        }
    }
}
