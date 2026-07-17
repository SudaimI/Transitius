using UnityEngine;

public class StarScroller : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;

    [Header("Bounds")]
    public float topY = 6f;
    public float bottomY = -6f;

    [Header("Random X")]
    public bool randomizeX = true;
    public float minX = -10f;
    public float maxX = 10f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip ambience;

    private void Start()
    {
        if (ambience != null)
        {
            audioSource.clip = ambience;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void Update()
    {
        foreach (Transform star in transform)
        {
            star.Translate(
                Vector3.down *
                speed *
                Time.deltaTime
            );

            if (star.position.y < bottomY)
            {
                Vector3 pos = star.position;

                pos.y = topY;

                if (randomizeX)
                {
                    pos.x = Random.Range(
                        minX,
                        maxX
                    );
                }

                star.position = pos;
            }
        }
    }
}