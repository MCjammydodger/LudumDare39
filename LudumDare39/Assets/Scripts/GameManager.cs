using UnityEngine;

public class GameManager : MonoBehaviour {

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
    }

    public void AddPower(float amount)
    {
        powerLeft = Mathf.Min(100, powerLeft + amount);
    }

}
