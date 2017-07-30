using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class HUD : MonoBehaviour {
    [SerializeField]
    private GameObject gameHUD;
    [SerializeField]
    private Slider powerGuage;
    [SerializeField]
    private Slider fuelGuage;
    [SerializeField]
    private Text batteryText;
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text gameOverScoreText;
    [SerializeField]
    private Text gameOverMessage;

    [SerializeField]
    private GameObject notificationPanel;
    [SerializeField]
    private Text notificationMessage;

    [SerializeField]
    private GameObject buttonPromptPanel;
    [SerializeField]
    private Text buttonPromptText;

    [SerializeField]
    private Slider forcefieldEnergy;
    [SerializeField]
    private GameObject forcefieldUI;
    [SerializeField]
    private GameObject forcefieldText;

    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private Toggle musicToggle;

    private GameManager gameManager;

    private Queue<string> notifications;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        notifications = new Queue<string>();
    }

    public void SetPowerGauge(float gaugeAmount)
    {
        powerGuage.value = gaugeAmount;
    }

    public void SetFuelGauge(float gaugeAmount)
    {
        fuelGuage.value = gaugeAmount;
    }

    public void SetBatteryText(int numberOfBatteries)
    {
        batteryText.text = numberOfBatteries.ToString();
    }

    public void SetScore(float score)
    {
        scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
    }

    public void SetOutsideHud(bool enable)
    {
        fuelGuage.gameObject.SetActive(enable);
    }

    public void GameOver(string message, float score)
    {
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = Mathf.RoundToInt(score).ToString();
        gameOverMessage.text = message;
        gameHUD.SetActive(false);
    }

    public void PlayAgainButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowNotification(string message)
    {
        notifications.Enqueue(message);
        if(notifications.Count == 1)
        {
            ShowNextNotification();
        }
    }

    private void ShowNextNotification()
    {
        if (notifications.Count != 0)
        {
            StartCoroutine(ShowNotificationPanel(notifications.Dequeue()));
        }
    }

    private IEnumerator ShowNotificationPanel(string message)
    {
        notificationPanel.SetActive(true);
        notificationMessage.text = message;
        yield return new WaitForSeconds(6);
        notificationPanel.SetActive(false);
        ShowNextNotification();
    }

    public void ShowButtonPrompt(string message)
    {
        buttonPromptPanel.SetActive(true);
        buttonPromptText.text = message;
    }

    public void CloseButtonPrompt()
    {
        buttonPromptPanel.SetActive(false);
    }

    public bool IsButtonPrompt(string message)
    {
        return buttonPromptText.text == message;
    }

    public void SetForcefieldEnergy(float energy)
    {
        forcefieldEnergy.value = energy;
        if(energy == 1)
        {
            forcefieldText.SetActive(true);
        }
        else
        {
            forcefieldText.SetActive(false);
        }
    }

    public void SetForcefieldUI(bool enable)
    {
        forcefieldUI.SetActive(enable);
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        pauseScreen.SetActive(pause);
    }

    public void MusicToggled()
    {
        gameManager.SetMusic(musicToggle.isOn);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }
}
