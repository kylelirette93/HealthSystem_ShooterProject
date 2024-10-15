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
}
