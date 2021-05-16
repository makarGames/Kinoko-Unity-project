using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player S;

    [HideInInspector] public Transform playerTransform;
    [SerializeField] private Text stringHealth;
    [SerializeField] private float _health = 100;
    [SerializeField] private Button contine;

    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            stringHealth.text = Mathf.RoundToInt(_health).ToString();

            if (_health <= 0)
                Death();
        }
    }
    private Rigidbody2D thisRigidbody;

    private void Awake()
    {
        health = 100;
        if (S == null)
            S = this;
        thisRigidbody = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
    }

    public void Death()
    {
        ButtonController.S.Pause();
        contine.interactable = false;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
