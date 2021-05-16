using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioClip steps;
    [SerializeField] private AudioClip attack;
    //[SerializeField] private AudioClip
    private Animator characterAnimator;
    private AudioSource thisAudoiSource;

    private void Awake()
    {
        thisAudoiSource = GetComponent<AudioSource>();
        characterAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (characterAnimator.GetBool("Run") && !thisAudoiSource.isPlaying)
            PlayRun();
    }

    public void PlayRun()
    {
        thisAudoiSource.pitch = Random.Range(0.85f, 1.3f);
        thisAudoiSource.PlayOneShot(steps);
        if (!characterAnimator.GetBool("Run"))
            StopRun();
    }
    public void StopRun()
    {
        thisAudoiSource.Stop();
    }
    public void PlayAttack()
    {
        thisAudoiSource.PlayOneShot(attack);
    }
}
