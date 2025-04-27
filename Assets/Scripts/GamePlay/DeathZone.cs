using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioSource.Play();
            PongManager.Instance.TakeDamage();
            DamageFeedback.Instance.TriggerFeedback();
        }
    }
}
