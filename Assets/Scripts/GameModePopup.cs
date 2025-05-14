using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameModePopup : MonoBehaviour
{
    public GameObject GameModeUi;
    public TextMeshProUGUI MessageText;
    public Button ContinueButton;

    void Start()
    {
        GameModeUi.SetActive(false);
        ContinueButton.onClick.AddListener(ResumeGame);
    }

    public void ShowPopup(string message)
    {
        MessageText.text = message;
        GameModeUi.SetActive(true);
        Time.timeScale = 0f; // Met le jeu en pause
    }

    public void ResumeGame()
    {
        GameModeUi.SetActive(false);
        Time.timeScale = 1f; // Reprend le jeu
    }
}
