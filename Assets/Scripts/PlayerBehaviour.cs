using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;
        // Reset the y velocity if the player is on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // Call the Move and Jump methods
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        // Apply gravity to the player
        velocity.y += gravity * Time.deltaTime;
        // Move the player based on the velocity
        controller.Move(velocity * Time.deltaTime);
    }

    // Move the player based on input
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

            
        }
        else
        {
            // Ensure the player stops moving when no input is detected
            controller.Move(Vector3.zero);
            anim.SetBool("isWalking", false);
        }

    }
    // Make the player jump
    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    // Take damage and update the health bar
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
    // Handle player death
    private void Die()
    {
        
        Debug.Log("Player died");
        // Disable the player controller and display the game over text
        controller.enabled = false;
        UIManager.Instance.DisplayGameOverText();
        StartCoroutine(Respawn());

    }
   // Reloads the scene after a delay, resets initial health bar
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadScene("SampleScene");
        UIManager.Instance.InitialHealthBar(maxHealth);
    }
}
