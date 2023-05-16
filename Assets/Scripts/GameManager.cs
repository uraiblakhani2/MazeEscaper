using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton Instance

    public int score = 0;
    public int lastScore;
    public int lastYellowScore;
    public int lastBlueScore;
    public bool HasKey { get; set; }
    public int yellowPickups { get; private set; }
    public int bluePickups { get; private set; }
    public int playerHealth { get; private set; }
    private const int maxPlayerHealth = 3;

    void Start()
    {
        // Initialize player health
        playerHealth = maxPlayerHealth;
    }

    void Awake() 
    {
        // If an Instance already exists and it's not this, destroy this
        if (Instance != null && Instance != this) 
        { 
            Destroy(gameObject);
            return;
        } 

        // Set Instance to this
        Instance = this;

        // Make this object persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        UIManager.Instance.SetScore(score);
    }

    public void AddScore(int x)
    {
        score += x;
    }

    // Use this for OnDeath, resets to the score the player had at the beginning of that level
    public void ResetScore()
    {
        score = lastScore;
        yellowPickups = lastYellowScore;
        bluePickups = lastBlueScore;
    }

    public void OnDeath()
    {
        ResetPlayerHealth();
        ResetScore();
        //ResetPickupCounts();
        UIManager.Instance.ResetTimer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishStage()
    {
        lastScore = score;
        lastYellowScore = yellowPickups;
        bluePickups = bluePickups;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            UIManager.Instance.HidePickupCounters();
            UIManager.Instance.timerIsActive = false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AddYellowPickup()
    {
        yellowPickups++;
    }

    public void AddBluePickup()
    {
        bluePickups++;
    }

    // Call this method in ResetAll() to reset the counters
    public void ResetPickupCounts()
    {
        yellowPickups = 0;
        bluePickups = 0;
        lastYellowScore = 0;
        lastBlueScore = 0;
    }

    public void ResetAll()
    {
        score = 0;
        lastScore = 0;
        ResetPlayerHealth();
        ResetPickupCounts();
        UIManager.Instance.ResetTimer();
    }

    public void TakeDamage()
    {
        playerHealth--;

        if (playerHealth <= 0)
        {
            OnDeath();
        }
    }

      public void healthBoost()
    {
        if (playerHealth <= 100)
        {
            playerHealth ++;

        }

        
        
    }

    // Call this method in ResetAll() to reset the player's health
    public void ResetPlayerHealth()
    {
        playerHealth = maxPlayerHealth;
    }
}
