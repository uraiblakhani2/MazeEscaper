using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton Instance

    TMP_Text score;
    TMP_Text timerText;
    TMP_Text yellowPickupsText;
    TMP_Text bluePickupsText;

    Image healthBar;
    GameObject healthBarContainer;

    float timer = 300f; // 5-minute timer (300 seconds)
    public bool timerIsActive = false; // Add this line

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

        score = transform.Find("Score").GetComponent<TMP_Text>();
        timerText = transform.Find("Timer").GetComponent<TMP_Text>();
        yellowPickupsText = transform.Find("YellowPickups").GetComponent<TMP_Text>();
        bluePickupsText = transform.Find("BluePickups").GetComponent<TMP_Text>();

        // Hide the counters by default
        yellowPickupsText.gameObject.SetActive(false);
        bluePickupsText.gameObject.SetActive(false);

        healthBar = transform.Find("HealthBarContainer/HealthBar").GetComponent<Image>();
        healthBarContainer = transform.Find("HealthBarContainer").gameObject;

        // Hide the health bar container by default
        healthBarContainer.SetActive(false);
    }

    void Update()
    {
        // SetScore(GameManager.Instance.score);
        if (timerIsActive)  UpdateTimer();
        if (GameManager.Instance != null)
        {
            yellowPickupsText.text = GameManager.Instance.yellowPickups.ToString();
            bluePickupsText.text = GameManager.Instance.bluePickups.ToString();
            healthBar.fillAmount = (float)GameManager.Instance.playerHealth / 3;
        }

        if (Keyboard.current.mKey.wasPressedThisFrame)
            GameManager.Instance.FinishStage();

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage2");
        Cursor.lockState = CursorLockMode.Locked;
        yellowPickupsText.gameObject.SetActive(true);
        bluePickupsText.gameObject.SetActive(true);

        // Show the health bar container when the game starts
        healthBarContainer.SetActive(true);

        timerIsActive = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetScore(int x)
    {
        score.text = x.ToString();
    }

    void UpdateTimer()
    {
        if (timer > 0f)
            timer -= Time.deltaTime;
        else
        {
            timerIsActive = false;
            GameManager.Instance.OnDeath();
        }

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void HidePickupCounters()
    {
        yellowPickupsText.gameObject.SetActive(false);
        bluePickupsText.gameObject.SetActive(false);
    }

    public void ShowPickupCounters()
    {
        yellowPickupsText.gameObject.SetActive(true);
        bluePickupsText.gameObject.SetActive(true);
    }

    public void ResetTimer()
    {
        timer = 300f;
        timerIsActive = true;
    }
}
