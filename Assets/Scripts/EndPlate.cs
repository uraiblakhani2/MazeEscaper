using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndPlate : MonoBehaviour
{
    public TMP_Text score;

    void Start()
    {
        score.text = GameManager.Instance.score.ToString();
        Cursor.lockState = CursorLockMode.None;
    }

    public void MainMenu()
    {
        GameManager.Instance.ResetAll();
        Destroy(UIManager.Instance);
        SceneManager.LoadScene("MainMenu");
    }

    public void Reset()
    {
        GameManager.Instance.ResetAll();
        //SceneManager.LoadScene("Stage2");
        Cursor.lockState = CursorLockMode.None;
        UIManager.Instance.StartGame();
    }
}
