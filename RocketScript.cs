using System.Collections;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [Header("Positions")]
    public Vector3 startPosition;
    public Vector3 middlePosition;
    public Vector3 endPosition;

    [Header("Timing")]
    public float moveInTime = 2f;
    public float hoverTime = 3f;
    public float moveOutTime = 1f;

    [Header("Hover")]
    public float hoverAmount = 0.25f;
    public float hoverSpeed = 2f;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip enterSound;
    public AudioClip hoverSound;
    public AudioClip exitSound;

    private void Start()
    {
        transform.position = startPosition;
        StartCoroutine(AnimateRocket());
    }

    IEnumerator AnimateRocket()
    {
        // Enter

        if (enterSound != null)
            audioSource.PlayOneShot(enterSound);

        yield return MoveTo(
            startPosition,
            middlePosition,
            moveInTime
        );

        // Hover

        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);

        float timer = 0f;
        Vector3 center = transform.position;

        while (timer < hoverTime)
        {
            timer += Time.deltaTime;

            transform.position =
                center +
                Vector3.up *
                Mathf.Sin(timer * hoverSpeed * Mathf.PI)
                * hoverAmount;

            yield return null;
        }

        // Exit

        if (exitSound != null)
            audioSource.PlayOneShot(exitSound);

        yield return MoveTo(
            transform.position,
            endPosition,
            moveOutTime
        );
    }

    IEnumerator MoveTo(
        Vector3 from,
        Vector3 to,
        float duration
    )
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            transform.position =
                Vector3.Lerp(
                    from,
                    to,
                    elapsed / duration
                );

            yield return null;
        }

        transform.position = to;
    }
}