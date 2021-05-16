using UnityEngine;
using UnityEngine.UI;
public class Portal : MonoBehaviour
{

    [SerializeField] private Button contine;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            contine.interactable = false;
            ButtonController.S.Pause();
        }
    }
}
