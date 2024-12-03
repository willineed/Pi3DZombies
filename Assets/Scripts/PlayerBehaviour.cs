using UnityEngine;

// This script has been made with the help of Github Copilot
public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 3.5f;
    public float jumpForce = 5f;
    public int maxHealth = 100;
    private int currentHealth;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void Jump()
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.001f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
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
