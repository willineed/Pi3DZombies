using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This script has been made with the help of Github Copilot
public class UIManager : MonoBehaviour
{
    // Singleton instance
    public static UIManager Instance { get; private set; }

    // UI Text references
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI bulletsInMagText;
    [SerializeField] private TextMeshProUGUI overallAmmoText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI youLostText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image healthFill;
    [SerializeField] private Gradient gradient;

    private int _maxHealth = 100;

    // singleton pattern
    private void Awake()
    {
        // Ensure that there's only one instance of UIManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initialize the player's health bar
    public void InitialHealthBar(int maxHealth)
    {
        if (healthBar != null)
        {
            _maxHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
            healthFill.color = gradient.Evaluate(1f);
            healthText.text = "+" + maxHealth.ToString();
            healthText.color = Color.green;


        }
    }

    // Update the player's health bar
    public void UpdateHealthBar(int health)
    {
        if (healthBar != null)
        {
            healthBar.value = health;
            Debug.Log("Healthbar: " + healthBar.value);
            healthFill.color = gradient.Evaluate((float)health / (float)_maxHealth);
        }
    }

    // Update the player's health display
    public void UpdateHealthText(int health)
    {
        if (healthText != null)
        {
            healthText.text = "+" + health.ToString();
            healthText.color = Color.Lerp(Color.red, Color.green, (float)health / (float)_maxHealth);
        }
    }

    // Update the bullets in the magazine display
    public void UpdateBulletsInMagText(int bulletsInMag)
    {
        if (bulletsInMagText != null)
        {
            bulletsInMagText.text = bulletsInMag.ToString();
        }


    }
    // Update the overall ammo display
    public void UpdateOverallAmmoText(int overallAmmo)
    {
      
        if (overallAmmoText != null)
        {
            
            overallAmmoText.text = overallAmmo.ToString();
        }
}

    // Display the text when the player wins
    public void DisplayWinText()
    {
        // Display the win text
        Debug.Log("You win!");
        winText.gameObject.SetActive(true);
    }

    // Display the text when the player loses
    public void DisplayGameOverText()
    {
        
        youLostText.gameObject.SetActive(true);

    }
    }

