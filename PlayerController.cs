// Do people even read these comments?
// anyways subscribe to suddy gaming on youtube
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float crouchMultiplier = 0.5f;
    public float climbSpeed = 3f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundRadius = 0.2f;

    [Header("Spawn")]
    public Transform spawnPoint;

    [Header("Rocket")]
    public RocketExit rocketExit;

    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite walkSprite;
    public Sprite jumpSprite;
    public Sprite fallSprite;
    public Sprite crouchSprite;
    public Sprite climbSprite;

    [Header("UI")]
    public TextMeshProUGUI deathText;
    public string deathMessage = "You Died!";
    public float deathMessageDuration = 2f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip walkSound;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private bool isClimbing;
    private bool isDead;

    public bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (deathText != null)
        {
            deathText.text = "";
        }
    }

    public void SetMovement(bool value)
{
    canMove = value;
}

    private void Update()
    {
        if (isDead)
            return;

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        Move();
        Jump();
        Climb();
        UpdateSprite();
    }

    private void Move()
    {
        if (!canMove)
    return;
        
        float horizontal = Input.GetAxisRaw("Horizontal");

        float currentSpeed = moveSpeed;

        bool crouching =
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow);

        if (crouching)
        {
            currentSpeed *= crouchMultiplier;
        }

        rb.linearVelocity = new Vector2(
            horizontal * currentSpeed,
            rb.linearVelocity.y
        );



        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

       

        if (
            horizontal != 0 &&
            isGrounded &&
            walkSound != null &&
            !audioSource.isPlaying
        )
        {
            audioSource.PlayOneShot(walkSound);
        }
    }

    private void Jump()
    {

        if (!canMove)
    return;

        if (
            Input.GetKeyDown(KeyCode.Space) &&
            isGrounded
        )
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            if (jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void Climb()
    {
        if (!canMove)
    return;
        
        if (!isClimbing)
            return;

        float vertical =
            Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            vertical * climbSpeed
        );
    }

    private void UpdateSprite()
    {
        if (isClimbing)
        {
            spriteRenderer.sprite = climbSprite;
            return;
        }

        if (
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow)
        )
        {
            spriteRenderer.sprite = crouchSprite;
            return;
        }

        if (rb.linearVelocity.y > 0.1f)
        {
            spriteRenderer.sprite = jumpSprite;
            return;
        }

        if (rb.linearVelocity.y < -0.1f)
        {
            spriteRenderer.sprite = fallSprite;
            return;
        }

        if (
            Mathf.Abs(
                rb.linearVelocity.x
            ) > 0.1f
        )
        {
            spriteRenderer.sprite = walkSprite;
            return;
        }

        spriteRenderer.sprite = idleSprite;
    }

    public IEnumerator Die()
    {
        isDead = true;

        if (deathSound != null)
        {
            audioSource.PlayOneShot(
                deathSound
            );
        }

        if (deathText != null)
        {
            deathText.text = deathMessage;
        }

        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(
            deathMessageDuration
        );

        if (deathText != null)
        {
            deathText.text = "";
        }

        transform.position =
            spawnPoint.position;

        isDead = false;
    }

    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if (
            other.CompareTag("Enemy")
            && !isDead
        )
        {
            StartCoroutine(
                Die()
            );
        }

        if (
            other.CompareTag(
                "Climbable"
            )
        )
        {
            isClimbing = true;
            rb.gravityScale = 0;
        }

        if (
            other.CompareTag(
                "Objective"
            )
        )
        {
            if (rocketExit != null)
            {
                rocketExit.CompleteMission();
            }
        }
    }

    private void OnTriggerExit2D(
        Collider2D other
    )
    {
        if (
            other.CompareTag(
                "Climbable"
            )
        )
        {
            isClimbing = false;
            rb.gravityScale = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundRadius
        );
    }
}