using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 100;

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
                Destroy(gameObject);
        }
    }
}
