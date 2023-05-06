using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton Instance

    public int score = 0;
    public int lastScore;
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
        // Set Instance to this, Destroy if another instance is in the scene.

        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        UIManager.Instance.SetScore(score);
    }

    public void AddScore(int x)
    {
        score += x;
    }

    public void ResetScore()
    {
        score = lastScore;
    }

    public void OnDeath()
    {
        score = lastScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishStage()
    {
        lastScore = score;
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
    }

    public void ResetAll()
    {
        score = 0;
        lastScore = 0;
        ResetPickupCounts();
    }

    public void TakeDamage()
    {
        playerHealth--;

        if (playerHealth <= 0)
        {
            OnDeath();
        }
    }

    // Call this method in ResetAll() to reset the player's health
    public void ResetPlayerHealth()
    {
        playerHealth = maxPlayerHealth;
    }
}
