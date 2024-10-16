using System;
using System.Diagnostics;

public class HealthSystem
{
    // Variables
    public int health;
    const int maxHealth = 100;
    public string healthStatus;
    public int shield;
    const int maxShield = 100;
    private int _lives;
    public int lives
    {
        get { return _lives; }
        set
        {
            UnityEngine.Debug.Log($"Lives was {_lives} now changing it to {value}");
            _lives = value;
        }
    }
    public int requiredXP = 100;
    

    // Optional XP system variables
    public int xp = 0;
    public int level = 1;


    public string HealthStatus(int hp)
    {
        return hp switch
        {
            > 90 => "Perfect Health",
            > 75 => "Healthy",
            > 50 => "Hurt",
            > 25 => "Badly Hurt",
            _ => "Imminent Danger",
        };
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
            $"\nHealth Status: {healthStatus}" +
            $"\nLevel: {level}" +
            $"\nXP: {xp}" +
            $"\nRequired XP to Level up: {requiredXP}";
    }

    public void TakeDamage(int damage)
    {
        // Prevent negative damage input
        if (damage < 0)
        {
            damage = 0;
        }


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

        if (health <= 0 && lives > 0)
        {
            Revive();
        }
    }


    public void Heal(int hp)
    {
        // Prevent negative healing input
        if (hp <= 0)
        {
            hp = 0;
        }
        // Take the value of health after hp has been added and clamp it
        health += hp;
        health = Math.Clamp(health, 0, maxHealth);
    }

    public void RegenerateShield(int hp)
    {
        // Prevent negative regen values
        if (hp <= 0)
        {
            hp = 0; 
        }
        // Clamp the shield just like health
        shield += hp;
        shield = Math.Clamp(shield, 0, maxShield);
    }

    public void Revive()
    {       
        Heal(100);
        RegenerateShield(100);
        lives--;
    }

    public void ResetGame()
    {
        // Reset all variables to default values
        health = maxHealth;
        shield = maxShield;
        lives = 3;
        xp = 0;
        level = 1;
    }

 
    public void IncreaseXP(int exp)
    {
        // Add the powerup xp to total xp
        xp += exp;

        if (xp < 0)
        {
            xp = 0;
        }

        // Level will increment every 100xp
        requiredXP = 100;

        // Check if total xp is greater than the required xp
        while (xp >= requiredXP && level < 99)
        {
            level++;

            // Keeps track of how much xp is left after leveling
            xp -= requiredXP;
        }

        if (level >= 99)
        {
            level = 99;
            xp = 0;
        }
    }
}