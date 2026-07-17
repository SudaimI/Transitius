using System.Collections;

using UnityEngine;


public class VinePlant : MonoBehaviour
{
    [SerializeField] private float dropDistance = 1f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float cycleTime = 3f;

    private Vector3 startingPosition;
    private bool isDropped;


    private void Start()
    {
        startingPosition = transform.position;
        StartCoroutine(PlantRoutine());
    }

    private IEnumerator PlantRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(cycleTime);

            if (isDropped)
            {
                yield return LiftRoutine();
            }
            else
            {
                yield return DropRoutine();
            }
        }
    }

    private IEnumerator DropRoutine()
    {
        Vector3 targetPosition = startingPosition + Vector3.down * dropDistance;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isDropped = true;
    }

    private IEnumerator LiftRoutine()
    {
        while (Vector3.Distance(transform.position, startingPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = startingPosition;
        isDropped = false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with the vine plant!");
        }
    }
}
