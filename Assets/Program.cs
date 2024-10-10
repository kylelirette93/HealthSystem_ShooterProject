using System;
using System.Diagnostics;
using Unity.PlasticSCM.Editor.WebApi;
public class HealthSystem
{
    // Variables
    public int health;
    const int maxHealth = 100;
    public string healthStatus;
    public int shield;
    const int maxShield = 100;
    public int lives;
    

    // Optional XP system variables
    public int xp;
    public int level;


    public string HealthStatus(int hp)
    {
        if (health > 90)
        {
            return "Perfect Health";
        }
        else if (health > 75)
        {
            return "Healthy";
        }
        else if (health > 50)
        {
            return "Hurt";
        }
        else if (health > 25)
        {
            return "Badly Hurt";
        }
        else
        {
            return "Imminent Danger";
        }

    }
    public HealthSystem()
    {
        ResetGame();
    }

    public string ShowHUD()
    {
        healthStatus = HealthStatus(health);
        // Implement HUD display
        return $"Health: {health}" +
            $"\nShield: {shield}" +
            $"\nLives: {lives}" +
            $"\nHealth Status: {healthStatus} ";
    }

    public void TakeDamage(int damage)
    {
        if (damage > shield)
        {
            // Remaining damage after the shield absorbs damage
            int remainingDamage = damage - shield;
            // Shield should be fully depleted
            shield = 0;
            health -= remainingDamage;

            // Clamp the health so it stays in range
            health = Math.Clamp(health, 0, maxHealth);
        }
        else
        {
            shield -= damage;

            // Clamp the shield so it stays in range
            shield = Math.Clamp(shield, 0, maxShield);
        }
    }


    public void Heal(int hp)
    {
        // Take the value of health after hp has been added and clamp it
        health = Math.Clamp(health + hp, 0, maxHealth);
    }

    public void RegenerateShield(int hp)
    {
        // Clamp the shield just like health
        shield = Math.Clamp(shield + hp, 0, maxShield);
    }

    public void Revive()
    {
        health = maxHealth;
        shield = maxShield;
        lives--;
        
        if (lives <= 0)
        {
            // Game Over.
            ResetGame();
        }
    }

    public void ResetGame()
    {
        // Reset all variables to default values
        health = maxHealth;
        shield = maxShield;
        lives = 3;
    }

    // Optional XP system methods
    public void IncreaseXP(int exp)
    {
        // Implement XP increase and level-up logic
    }
}