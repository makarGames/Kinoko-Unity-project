using UnityEngine;
using System.Collections;
public class InfectedCell : MonoBehaviour
{
    [SerializeField] private float _health = 30;
    [SerializeField] private GameObject Elevator;
    //[SerializeField] private GameObject infectedZone;
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject[] infectionTree;

    private bool canDestroy = true;

    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                Elevator.GetComponent<Animation>().Play();
                //infectedZone.GetComponent<InfectedZone>().Fading();
                Destroy(spawner);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Weapon>() && canDestroy)
        {
            canDestroy = false;
            foreach (GameObject gO in infectionTree)
                StartCoroutine(infectionFade(gO));
            StartCoroutine(infectionFade(gameObject));
            float damage = other.gameObject.GetComponent<Weapon>().damage;
            health -= damage;
        }
    }

    private IEnumerator infectionFade(GameObject Tree)
    {
        print("LOL");
        float p = 1;
        Color tempColor = Color.white;

        while (p > 0)
        {
            tempColor.a = p;
            Tree.GetComponent<SpriteRenderer>().color = tempColor;
            p -= 0.01f;
            yield return new WaitForFixedUpdate();
        }
        Destroy(gameObject);
    }
}
