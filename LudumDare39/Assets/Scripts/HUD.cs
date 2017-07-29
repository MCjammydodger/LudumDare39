using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    [SerializeField]
    private Slider powerGuage;
    [SerializeField]
    private Slider fuelGuage;
    [SerializeField]
    private Text batteryText;

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
}
