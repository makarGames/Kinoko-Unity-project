using UnityEngine;
using System.Collections;

public class PlayerAttaker : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject weapon;

    [SerializeField] private float meleeAttackDelay = 0.3f;
    [SerializeField] private float rangeAttackDelay = 0.7f;

    private bool meleeAttacking = false;
    private bool rangeAttacking = false;

    private PlayerSound thisSounder;
    private Transform weaponTransform;
    private Animator characterAnimator;

    private void Awake()
    {
        weaponTransform = weapon.GetComponent<Transform>();
        characterAnimator = GetComponent<Animator>();
        thisSounder = GetComponent<PlayerSound>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(MeleeAttack());
        if (Input.GetMouseButtonDown(1))
            StartCoroutine(RangeAttack());
    }

    private IEnumerator MeleeAttack()
    {
        if (meleeAttacking) yield break;

        characterAnimator.SetTrigger("MeleeAttack");
        yield return new WaitForSeconds(0.1f);
        thisSounder.PlayAttack();
        meleeAttacking = true;
        weaponTransform.GetComponentInChildren<BoxCollider2D>().enabled = true; ;

        yield return new WaitForSeconds(0.3f);
        weaponTransform.GetComponentInChildren<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(meleeAttackDelay);
        meleeAttacking = false;

    }

    private IEnumerator RangeAttack()
    {
        if (rangeAttacking) yield break;
        characterAnimator.SetTrigger("RangeAttack");

        rangeAttacking = true;
        Vector3 spawnPoint = weaponTransform.position;
        yield return new WaitForSeconds(0.1f);
        Instantiate(projectile, spawnPoint, Quaternion.identity);

        yield return new WaitForSeconds(rangeAttackDelay);
        rangeAttacking = false;
    }
}
