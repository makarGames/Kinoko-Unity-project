using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float attackDelay = 1f;

    private Transform thisTransform;
    private Animator thisAnimator;
    private void Awake()
    {
        thisTransform = GetComponent<Transform>();
        thisAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(RangeAttack());
    }

    private IEnumerator RangeAttack()
    {
        bool rigth = true;
        if (Random.Range(0, 100f) > 50f)
        {
            rigth = true;
            thisTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            rigth = false;
            thisTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        thisAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.2f);

        GameObject shot = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);

        if (!rigth)
            shot.GetComponent<EnemyShot>().speed *= -1f;

        yield return new WaitForSeconds(attackDelay);
        StartCoroutine(RangeAttack());
    }
}
