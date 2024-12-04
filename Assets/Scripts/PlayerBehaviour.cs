using UnityEngine;

// This script has been made with the help of Github Copilot
public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public int maxHealth = 100;
    private int currentHealth;
    private CharacterController controller;
    public Transform cameraTransform;
    private Animator anim;
    

    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        UIManager.Instance.InitialHealthBar(maxHealth);
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Move();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("isWalking", true);
            // Calculate the direction the player should face based on the camera's forward direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Move the player in the calculated direction
            controller.Move(moveDirection * speed * Time.deltaTime);

            // Rotate the player to face the movement direction
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }
        else
        {
            // Ensure the player stops moving when no input is detected
            controller.Move(Vector3.zero);
            anim.SetBool("isWalking", false);
        }

    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UIManager.Instance.UpdateHealthBar(currentHealth);
        UIManager.Instance.UpdateHealthText(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., respawn, game over)
        Debug.Log("Player died");
    }
}
