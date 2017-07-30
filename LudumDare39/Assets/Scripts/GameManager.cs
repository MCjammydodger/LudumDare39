using UnityEngine;

public class GameManager : MonoBehaviour {

    public AudioClip alienLoop;
    public AudioClip normalLoop;
    public AudioClip victoryTune;
    public AudioClip defeatTune;

    public int numberOfBatteries { get; private set; }
    public float maxFuel { get; private set;}


    private float scoreToGetRocket = 5000;
    private float scoreToGetExtraFuel = 400;
    private float scoreToGetForcefield = 1000;


    private float timeSinceStart;

    private float score;

    private float scorePerSecond = 10;

    [SerializeField]
    private GameObject outside;
    [SerializeField]
    private Inside inside;

    private float powerLeft = 100;
    private float powerDrainSpeed = 1f;

    private HUD hud;

    public bool meteorsStarted { get; private set; }
    public bool aliensStarted { get; private set; }
    public bool rocketSpawned { get; private set; }
    public bool unlockedForcefield { get; private set; }

    private float timeBeforeMeteorites = 20;
    private float timeBeforeAliens = 40;

    private AudioSource audioSource;

    private bool gameOverShown = false;

    private void Start()
    {
        Time.timeScale = 1;
        hud = FindObjectOfType<HUD>();
        maxFuel = 100;
        audioSource = GetComponent<AudioSource>();
        PlayClip(normalLoop);
        GoInside();
    }

    private void Update()
    {
        timeSinceStart += Time.deltaTime;
        powerLeft -= Time.deltaTime * powerDrainSpeed;
        powerLeft = Mathf.Max(0, powerLeft);
        if(powerLeft == 0)
        {
            GameOver("You ran out of power!");
        }
        score += scorePerSecond * Time.deltaTime;
        hud.SetScore(score);
        hud.SetPowerGauge(powerLeft / 100);
        if (inside.isActiveAndEnabled)
        {
            inside.PowerChanged(powerLeft);
        }
        if (GetTimeSinceStart() >= timeBeforeMeteorites)
        {
            if (!meteorsStarted)
            {
                ShowNotification("A meteor storm has started!");
                meteorsStarted = true;
            }
        }
        if (GetTimeSinceStart() >= timeBeforeAliens)
        {
            if (!aliensStarted)
            {
                ShowNotification("Aliens have started invading!");
                aliensStarted = true;
            }
        }

    }

    public float GetTimeSinceStart()
    {
        return timeSinceStart;
    }
    public void AddPower(float amount)
    {
        powerLeft = Mathf.Min(100, powerLeft + amount);
    }

    public void AddBattery()
    {
        numberOfBatteries++;
        hud.SetBatteryText(numberOfBatteries);
    }

    public void RemoveBattery()
    {
        numberOfBatteries--;
        hud.SetBatteryText(numberOfBatteries);
    }
    public void GoOutside()
    {
        inside.gameObject.SetActive(false);
        outside.SetActive(true);
        hud.SetOutsideHud(true);
        hud.CloseButtonPrompt();
        if (unlockedForcefield)
        {
            hud.SetForcefieldUI(true);
        }
    }

    public void GoInside()
    {
        outside.SetActive(false);
        inside.gameObject.SetActive(true);
        hud.SetOutsideHud(false);
        hud.CloseButtonPrompt();
        if (unlockedForcefield)
        {
            hud.SetForcefieldUI(false);
        }
    }

    public void GameOver(string message, bool victory = false)
    {
        if (!gameOverShown)
        {
            Time.timeScale = 0;
            if (victory)
            {
                AddScore(10000);
                PlayVictory();
            }
            else
            {
                PlayDefeat();
            }
            hud.GameOver(message, score);

            gameOverShown = true;
        }
    }
    
    public void AddScore(float amount)
    {
        score += amount;
    }

    public void ShowNotification(string message)
    {
        hud.ShowNotification(message);
    }

    public void ShowButtonPrompt(string message)
    {
        hud.ShowButtonPrompt(message);
    }

    public bool IsButtonPrompt(string message)
    {
        return hud.IsButtonPrompt(message);
    }
    public void CloseButtonPrompt()
    {
        hud.CloseButtonPrompt();
    }
    public float GetScore()
    {
        return score;
    }

    public void SetRocketActive()
    {
        if (!rocketSpawned)
        {
            rocketSpawned = true;
        }       
    }

    public float GetRocketPrice()
    {
        return scoreToGetRocket;
    }

    public float GetFuelPrice()
    {
        return scoreToGetExtraFuel;
    }

    public float GetForcefieldPrice()
    {
        return scoreToGetForcefield;
    }

    public void IncreaseFuel()
    {
        maxFuel = 200;
    }

    public void EnableForcefield()
    {
        unlockedForcefield = true;
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayVictory()
    {
        audioSource.Stop();
        audioSource.loop = false;
        PlayClip(victoryTune);
    }

    public void PlayDefeat()
    {
        audioSource.Stop();
        audioSource.loop = false;
        PlayClip(defeatTune);
    }

    public void SetMusic(bool on)
    {
        if (on)
        {
            audioSource.volume = 1;
        }
        else
        {
            audioSource.volume = 0;
        }
    }
}
