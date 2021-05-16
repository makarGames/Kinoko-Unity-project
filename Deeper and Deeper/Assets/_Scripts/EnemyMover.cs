using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackDistance;
    [SerializeField] private float jumpForce;
    [SerializeField] private float attackDelay;
    [SerializeField] private float repulsiveForce = 7f;
    [SerializeField] private float distanceToMove = 30f;
    private bool attacking = false;
    private bool takingDamage = false;

    private Animator thisAnimator;
    private Transform thisTransform;
    private Rigidbody2D thisRigidbody;
    private Vector2 direction;

    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(Player.S.playerTransform.position, transform.position);

        if (!takingDamage && distance < distanceToMove)
        {
            Move();

            if (!attacking && Vector2.Distance(Player.S.playerTransform.position, thisTransform.position) < attackDistance)
                StartCoroutine(Attack());

        }
    }

    private void Move()
    {
        Vector2 normalizedDirection = (Player.S.playerTransform.position - thisTransform.position).normalized;
        if (normalizedDirection.x < 0)
            thisTransform.rotation = Quaternion.Euler(0, 180f, 0);
        else
            thisTransform.rotation = Quaternion.Euler(0, 0, 0);

        thisRigidbody.velocity = new Vector2(normalizedDirection.x * speed, thisRigidbody.velocity.y);

        if (Player.S.playerTransform.position.y - thisTransform.position.y > 1.5f)
        {
            thisRigidbody.velocity = new Vector2(thisRigidbody.velocity.x, jumpForce);
        }
        if (Player.S.playerTransform.position.y - thisTransform.position.y < 0.5f)
        {
            attacking = true;
        }

    }

    private IEnumerator Attack()
    {
        attacking = true;
        thisAnimator.SetBool("Attack", attacking);
        thisRigidbody.velocity = new Vector2(thisRigidbody.velocity.x, jumpForce);

        thisTransform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(attackDelay);

        attacking = false;
        thisTransform.GetChild(1).GetComponent<BoxCollider2D>().enabled = attacking;
        thisAnimator.SetBool("Attack", attacking);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionObject = other.gameObject;

        if (collisionObject.GetComponent<Weapon>())
        {
            StartCoroutine(TackingDamage());
            Vector2 heading = (transform.position - collisionObject.transform.position).normalized;
            thisRigidbody.velocity = heading * repulsiveForce;
        }
    }

    private IEnumerator TackingDamage()
    {
        takingDamage = true;
        yield return new WaitForSeconds(1f);
        takingDamage = false;
    }
}
