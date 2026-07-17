using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RocketExit : MonoBehaviour
{
    [Header("Scene")]
    public Object nextScene;

    [Header("Requirements")]
    public bool missionComplete = false;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip exitSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.CompareTag("Player")
            && missionComplete
        )
        {
            StartCoroutine(
                FadeAndLoad()
            );
        }
    }

    IEnumerator FadeAndLoad()
    {
        if (exitSound != null)
        {
            audioSource.PlayOneShot(
                exitSound
            );
        }

        float elapsed = 0f;

        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(
                0,
                1,
                elapsed / fadeDuration
            );

            fadeImage.color = color;

            yield return null;
        }

        SceneManager.LoadScene(
            nextScene.name
        );
    }

    public void CompleteMission()
    {
        missionComplete = true;
    }
}