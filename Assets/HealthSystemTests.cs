using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemTests : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        RunAllUnitTests();
    }

    void RunAllUnitTests()
    {
        // Take damage tests
        Test_TakeDamage_OnlyShield();
        Test_TakeDamage_ShieldAndHealth();
        Test_TakeDamage_DepletedShield();
        Test_TakeDamage_NegativeDamageInput();
        Test_TakeDamage_ReduceHealthToZero();
        Test_TakeDamage_ReduceShieldAndHealthToZero();

        // Healing tests
        Test_Heal_NormalHealing();
        Test_Heal_AlreadyMaxHealth();
        Test_Heal_NegativeInput();

        // Regenerate Shield Tests
        Test_RegenerateShield_Regeneration();
        Test_RegenerateShield_MaxShield();
        Test_RegenerateShield_NegativeInput();

        // Revive Test
        Test_Revive();

        // XP Tests
        Test_IncreaseXP_Add();
        Test_IncreaseXP_Max();
        Test_IncreaseXP_NegativeInput();
    }

    public void Test_TakeDamage_OnlyShield()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(10);

        // Shield should deplete, not health.
        Debug.Assert(90 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(3 == system.lives);
    }

    public void Test_TakeDamage_ShieldAndHealth()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 5;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(10);

        // Shield should be depleted and health
        // should decrement remainder
        Debug.Assert(0 == system.shield);
        Debug.Assert(95 == system.health);
        Debug.Assert(3 == system.lives);
    }

    public void Test_TakeDamage_DepletedShield()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 0;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(10);

        // Shield should be depleted and health should decrement
        Debug.Assert(0 == system.shield);
        Debug.Assert(90 == system.health);
        Debug.Assert(3 == system.lives);
    }

    public void Test_TakeDamage_ReduceHealthToZero()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 0;
        system.health = 10;
        system.lives = 3;

        system.TakeDamage(10);

        // Player should be revived and lives should decrement.
        Debug.Assert(100 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(2 == system.lives);
    }

    public void Test_TakeDamage_ReduceShieldAndHealthToZero()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(200);

        // Player should die and revive.
        Debug.Assert(100 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(2 == system.lives);
    }

    public void Test_TakeDamage_NegativeDamageInput()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.TakeDamage(-10);

        // If damage is negative, nothing happens.
        Debug.Assert(100 == system.shield, "Shield should not reduce");
        Debug.Assert(100 == system.health, "Health should not reduce");
        Debug.Assert(3 == system.lives, "Lives should not reduce");
    }

    public void Test_Heal_NormalHealing()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 80;
        system.lives = 3;

        system.Heal(20);

        // If damage is negative, nothing happens.
        Debug.Assert(100 == system.shield, "Shield should not reduce");
        Debug.Assert(100 == system.health, "Health should not reduce");
        Debug.Assert(3 == system.lives, "Lives should not reduce");
    }

    public void Test_Heal_AlreadyMaxHealth()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.Heal(20);

        // If damage is negative, nothing happens.
        Debug.Assert(100 == system.shield, "Shield should not reduce");
        Debug.Assert(100 == system.health, "Health should not reduce");
        Debug.Assert(3 == system.lives, "Lives should not reduce");
    }

    public void Test_Heal_NegativeInput()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.Heal(-20);

        // If damage is negative, nothing happens.
        Debug.Assert(100 == system.shield, "Shield should not reduce");
        Debug.Assert(100 == system.health, "Health should not reduce");
        Debug.Assert(3 == system.lives, "Lives should not reduce");
    }

    public void Test_RegenerateShield_Regeneration()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 80;
        system.health = 100;
        system.lives = 3;

        system.RegenerateShield(20);

        Debug.Assert(100 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(3 == system.lives);
    }

    public void Test_RegenerateShield_MaxShield()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.RegenerateShield(20);

        Debug.Assert(100 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(3 == system.lives);
    }

    public void Test_RegenerateShield_NegativeInput()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 100;
        system.health = 100;
        system.lives = 3;

        system.RegenerateShield(-20);

        Debug.Assert(100 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(3 == system.lives);
    }

    public void Test_Revive()
    {
        HealthSystem system = new HealthSystem();
        system.shield = 0;
        system.health = 0;
        system.lives = 3;

        system.Revive();

        Debug.Assert(100 == system.shield);
        Debug.Assert(100 == system.health);
        Debug.Assert(2 == system.lives);
    }

    public void Test_IncreaseXP_Add()
    {
        HealthSystem system = new HealthSystem();
        system.level = 1;
        system.xp = 0;

        system.IncreaseXP(100);

        Debug.Assert(2 == system.level);
        Debug.Assert(0 == system.xp);
    }

    public void Test_IncreaseXP_Max()
    {
        HealthSystem system = new HealthSystem();
        system.level = 1;
        system.xp = 80;

        system.IncreaseXP(40);

        Debug.Assert(2 == system.level);
        Debug.Assert(20 == system.xp);
    }

    public void Test_IncreaseXP_NegativeInput()
    {
        HealthSystem system = new HealthSystem();
        system.level = 1;
        system.xp = 0;

        system.IncreaseXP(-20);

        Debug.Assert(1 == system.level);
        Debug.Assert(0 == system.xp);
    }
}
