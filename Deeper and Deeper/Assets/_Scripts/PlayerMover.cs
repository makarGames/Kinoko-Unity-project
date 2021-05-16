using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDelay;

    private bool canJump = true;

    private Transform characterTransform;
    private Rigidbody2D characterRigidbody;
    private Animator characterAnimator;
    private Vector2 direction;
    private PlayerSound thisSounder;

    private void Awake()
    {
        characterTransform = GetComponent<Transform>();
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        thisSounder = GetComponent<PlayerSound>();
    }

    private void Update()
    {
        Walk();
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
            characterAnimator.SetTrigger("Jump");
        }
        if (Input.GetAxis("Horizontal") < 0)
            characterTransform.rotation = Quaternion.Euler(characterTransform.rotation.x, 180f, 0);

        else if (Input.GetAxis("Horizontal") > 0)
            characterTransform.rotation = Quaternion.Euler(characterTransform.rotation.x, 0, 0);
    }

    private void FixedUpdate()
    {
        characterAnimator.SetBool("Run", Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.1f);
    }

    private void Walk()
    {
        direction.x = Input.GetAxis("Horizontal");
        characterRigidbody.velocity = new Vector2(direction.x * speed, characterRigidbody.velocity.y);
    }

    private void Jump()
    {
        StartCoroutine(JumpDelaying());
        characterRigidbody.velocity = Vector2.up * jumpForce;
    }

    private IEnumerator JumpDelaying()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }
}
