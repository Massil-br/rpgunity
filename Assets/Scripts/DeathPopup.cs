using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathPopup : MonoBehaviour
{
    public GameObject DeathScreenUi;
    public TextMeshProUGUI MessageText;
    public Button ResetButton;

    void Start()
    {
        DeathScreenUi.SetActive(false);
        ResetButton.onClick.AddListener(ResetGame); 
    }

    public void DeathScreen(string message)
    {
        MessageText.text = message;
        DeathScreenUi.SetActive(true);
        Time.timeScale = 0f; // Met le jeu en pause
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
