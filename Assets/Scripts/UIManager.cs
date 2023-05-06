using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    // void Update()
    // {
    //     // SetScore(GameManager.Instance.score);
    //     if (timerIsActive)  UpdateTimer();
    //     yellowPickupsText.text = GameManager.Instance.yellowPickups.ToString();
    //     bluePickupsText.text = GameManager.Instance.bluePickups.ToString();

    //     healthBar.fillAmount = (float)GameManager.Instance.playerHealth / 3;
    // }

    void Update()
    {
        // SetScore(GameManager.Instance.score);
        if (timerIsActive) UpdateTimer();
        if (GameManager.Instance != null)
        {
            yellowPickupsText.text = GameManager.Instance.yellowPickups.ToString();
            bluePickupsText.text = GameManager.Instance.bluePickups.ToString();
            healthBar.fillAmount = (float)GameManager.Instance.playerHealth / 3;
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
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
        timer -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timer <= 0)
        {
            timer = 0;
            GameManager.Instance.OnDeath();
        }
    }

    public void HidePickupCounters()
    {
        yellowPickupsText.gameObject.SetActive(false);
        bluePickupsText.gameObject.SetActive(false);
    }
}
