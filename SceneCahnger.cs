using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    [Header("Scene")]
    public Object nextScene;

    [Header("Timing")]
    public float delayBeforeFade = 5f;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip fadeSound;

    private void Start()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        // Wait before fading
        yield return new WaitForSeconds(
            delayBeforeFade
        );

        // Play sound
        if (fadeSound != null)
        {
            audioSource.PlayOneShot(
                fadeSound
            );
        }

        // Fade to black
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

        // Load next scene
        SceneManager.LoadScene(
            nextScene.name
        );
    }
}