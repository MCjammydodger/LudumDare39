using UnityEngine;

public class Spaceman : MonoBehaviour {

    public float jetpackFuel { get; private set; }

    private float jetpackBurnRate = 30;
    private float jetpackRecoveryRate = 30;

    private bool atBase = false;
    private bool atRocket = false;

    private Block currentBlock;

    private World world;
    private GameManager gameManager;
    private SpacemanTrigger trigger;
    private SpacemanMovement movement;
    private HUD hud;

    private Rocket rocket;

    // Use this for initialization
    void Start () {
        world = FindObjectOfType<World>();
        gameManager = FindObjectOfType<GameManager>();
        trigger = GetComponentInChildren<SpacemanTrigger>();
        movement = GetComponent<SpacemanMovement>();
        hud = FindObjectOfType<HUD>();
        jetpackFuel = gameManager.maxFuel;
	}

    void Update()
    {
        string dropBlockMessage = "Press [E] to drop block";
        string pickUpBlockMessage = "Press [E] to pick up block";

        if (currentBlock != null)
        {
            gameManager.ShowButtonPrompt(dropBlockMessage);
        }
        else if(currentBlock == null && trigger.GetLatestBlock() != null)
        {
            gameManager.ShowButtonPrompt(pickUpBlockMessage);
        }
        else
        {
            if(gameManager.IsButtonPrompt(dropBlockMessage) || gameManager.IsButtonPrompt(pickUpBlockMessage))
            {
                gameManager.CloseButtonPrompt();
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
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
        hud.SetFuelGauge(jetpackFuel/gameManager.maxFuel);

        string enterBaseMessage = "Press [E] to enter base";
        if (atBase)
        {
            gameManager.ShowButtonPrompt(enterBaseMessage);
            if (Input.GetKeyUp(KeyCode.E))
            {
                gameManager.GoInside();
            }
        }
        else
        {
            if (gameManager.IsButtonPrompt(enterBaseMessage))
            {
                gameManager.CloseButtonPrompt();
            }
        }

        string rocketMessage = "Press [E] to enter rocket and take off";
        if (atRocket)
        {
            gameManager.ShowButtonPrompt(rocketMessage);
            if (Input.GetKeyUp(KeyCode.E))
            {
                rocket.EnterRocket(this);
            }
        }
        else
        {
            if (gameManager.IsButtonPrompt(rocketMessage))
            {
                gameManager.CloseButtonPrompt();
            }
        }

        if (!GetComponent<SpacemanForcefield>().enabled)
        {
            GetComponent<SpacemanForcefield>().enabled = gameManager.unlockedForcefield;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Battery")
        {
            gameManager.AddBattery();
            world.SpawnBattery();
        }
        if(other.tag == "Base" )
        {
            atBase = true;
        }
        if(other.GetComponent<Rocket>() != null)
        {
            atRocket = true;
            rocket = other.GetComponent<Rocket>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Base")
        {
            atBase = false;
        }
        if (other.GetComponent<Rocket>())
        {
            atRocket = false;
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
        jetpackFuel = Mathf.Min(jetpackFuel + amount, gameManager.maxFuel);
        jetpackFuel = Mathf.Max(jetpackFuel + amount, 0);
    }
}
