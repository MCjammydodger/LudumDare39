using UnityEngine;

public class SpacemanForcefield : MonoBehaviour {
    [SerializeField]
    private GameObject forcefield;

    private float currentEnergy;
    private float maxEnergy = 100;
    private float drainRate = 10;
    private float gainRate = 5;

    private bool losingEnergy = false;
    private bool gainingEnergy = false;

    private HUD hud;

    private void Awake()
    {
        hud = FindObjectOfType<HUD>();
    }

    private void OnEnable()
    {
        hud.SetForcefieldUI(true);
        currentEnergy = maxEnergy;
    }

    private void Update()
    {
        if (gainingEnergy)
        {
            currentEnergy += gainRate * Time.deltaTime;
            currentEnergy = Mathf.Min(maxEnergy, currentEnergy);
            if(currentEnergy == maxEnergy)
            {
                gainingEnergy = false;
            }
        }
        if (losingEnergy)
        {
            currentEnergy -= drainRate * Time.deltaTime;
            currentEnergy = Mathf.Max(0, currentEnergy);
            if (currentEnergy == 0)
            {
                losingEnergy = false;
                gainingEnergy = true;
                forcefield.SetActive(false);
            }
        }
        hud.SetForcefieldEnergy(currentEnergy / maxEnergy);

    }

    public void UseForcefield()
    {
        if (currentEnergy == maxEnergy)
        {
            losingEnergy = true;
            forcefield.SetActive(true);
        }
    }
}
