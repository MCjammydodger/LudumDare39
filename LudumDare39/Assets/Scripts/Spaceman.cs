using UnityEngine;

public class Spaceman : MonoBehaviour {

    public float jetpackFuel { get; private set; }

    private int numberOfBatteries = 0;

    private float jetpackBurnRate = 30;
    private float jetpackRecoveryRate = 30;

    private Block currentBlock;

    private World world;
    private GameManager gameManager;
    private SpacemanTrigger trigger;
    private SpacemanMovement movement;
    private HUD hud;

    // Use this for initialization
    void Start () {
        world = FindObjectOfType<World>();
        gameManager = FindObjectOfType<GameManager>();
        trigger = GetComponentInChildren<SpacemanTrigger>();
        movement = GetComponent<SpacemanMovement>();
        hud = FindObjectOfType<HUD>();
        jetpackFuel = 100;
	}

    void Update()
    {
        if (Input.GetButtonUp("Submit"))
        {
            if(currentBlock != null)
            {
                DropBlock();
            }
            else
            {
                PickUpBlock();
            }
        }
        
        if (movement.isFlying)
        {
            ModifyFuel(-jetpackBurnRate * Time.deltaTime);
        }
        if (!movement.isFlying)
        {
            ModifyFuel(jetpackRecoveryRate * Time.deltaTime);
        }
        hud.SetFuelGauge(jetpackFuel/100);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Battery")
        {
            numberOfBatteries++;
            gameManager.AddPower(10);
            Destroy(other.gameObject);
            world.SpawnBattery();
        }
    }

    private void PickUpBlock()
    {
        Block block = trigger.GetLatestBlock();
        if(block != null)
        {
            currentBlock = block;
            currentBlock.PickUp(true);
            currentBlock.transform.SetParent(trigger.transform);
            currentBlock.transform.localPosition = Vector3.zero;
        }
    }

    private void DropBlock()
    {
        currentBlock.PickUp(false);
        currentBlock.transform.SetParent(world.transform);
        currentBlock = null;
    }

    private void ModifyFuel(float amount)
    {
        jetpackFuel = Mathf.Min(jetpackFuel + amount, 100);
        jetpackFuel = Mathf.Max(jetpackFuel + amount, 0);
    }
}
