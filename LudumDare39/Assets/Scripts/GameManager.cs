using UnityEngine;

public class GameManager : MonoBehaviour {

    public int numberOfBatteries { get; private set; }

    [SerializeField]
    private GameObject outside;
    [SerializeField]
    private Inside inside;

    private float powerLeft = 100;
    private float powerDrainSpeed = 0.5f;

    private HUD hud;

    private void Start()
    {
        hud = FindObjectOfType<HUD>();
    }

    private void Update()
    {
        powerLeft -= Time.deltaTime * powerDrainSpeed;
        hud.SetPowerGauge(powerLeft / 100);
        if (inside.isActiveAndEnabled)
        {
            inside.PowerChanged(powerLeft);
        }
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
      

    }

    public void GoInside()
    {
        outside.SetActive(false);
        inside.gameObject.SetActive(true);
    }
}
