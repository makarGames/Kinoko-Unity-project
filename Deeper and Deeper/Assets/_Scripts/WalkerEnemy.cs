using System.Collections;
using UnityEngine;

public class WalkerEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackDistance;
    [SerializeField] private float attackDelay;
    [SerializeField] private float repulsiveForce = 7f;
    [SerializeField] private float distanceToMove = 30f;

    private bool attacking = false;
    private bool takingDamage = false;
    private bool canAttack = true;

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

            if (canAttack && !attacking && distance < attackDistance)
                StartCoroutine(Attack());
        }
    }

    private void Move()
    {
        Vector2 normalizedDirection = (Player.S.playerTransform.position - thisTransform.position).normalized;
        if (normalizedDirection.x < 0)
            thisTransform.rotation = Quaternion.Euler(0, 0, 0);
        else
            thisTransform.rotation = Quaternion.Euler(0, 180, 0);

        thisRigidbody.velocity = new Vector2(normalizedDirection.x * speed, thisRigidbody.velocity.y);
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        attacking = true;
        thisAnimator.SetBool("Attack", attacking);

        yield return new WaitForSeconds(0.2f);


        thisTransform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(attackDelay);

        attacking = false;
        thisTransform.GetChild(1).GetComponent<BoxCollider2D>().enabled = attacking;
        thisAnimator.SetBool("Attack", attacking);
        yield return new WaitForSeconds(0.5f);

        canAttack = true;
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
