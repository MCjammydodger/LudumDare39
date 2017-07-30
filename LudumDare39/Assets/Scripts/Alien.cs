using UnityEngine;

public class Alien : MonoBehaviour {

    private Spaceman spaceman;
    private GameManager gameManager;

    private World world;

    private float movementSpeed = 10;

    private enum MovementMode { Attack, Carry, Flee };
    private MovementMode currentMode;

    private float timeSinceGrabbed = 0;
    private float timeSinceChangedDirection = 0;
    private float timeBeforeChange;

    private Rigidbody2D rigidBody;

    Vector3 direction;

    private void Awake()
    {
        spaceman = FindObjectOfType<Spaceman>();
        world = FindObjectOfType<World>();
        currentMode = MovementMode.Attack;
        rigidBody = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        gameManager.PlayClip(gameManager.alienLoop);
    }

    private void OnDisable()
    {
        gameManager.PlayClip(gameManager.normalLoop);

    }

    private void Update()
    {
        if (currentMode == MovementMode.Attack) {
            if (spaceman != null)
            {
                direction = (spaceman.transform.position - transform.position).normalized;
            }
        }
        if(currentMode == MovementMode.Carry)
        {
            timeSinceGrabbed += Time.deltaTime;
            timeSinceChangedDirection += Time.deltaTime;

            if (timeSinceChangedDirection >= timeBeforeChange)
            {
                timeBeforeChange = Random.Range(1, 4);
                direction = (new Vector3(Random.Range(world.minX + GetComponent<SpriteRenderer>().bounds.size.x, world.maxX - GetComponent<SpriteRenderer>().bounds.size.x), Random.Range(world.minY + GetComponent<SpriteRenderer>().bounds.size.y, world.maxY - GetComponent<SpriteRenderer>().bounds.size.y)) - transform.position).normalized;
                timeSinceChangedDirection = 0;
            }           

            if(timeSinceGrabbed >= 10)
            {
                DropSpaceman();
            }
        }
        if(currentMode == MovementMode.Flee)
        {
            direction = Vector3.up;
            if(transform.position.y > world.maxY)
            {
                Destroy(gameObject);
            }
        }

        rigidBody.velocity = (direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(currentMode == MovementMode.Attack && other.gameObject.GetComponent<Spaceman>() != null)
        {
            other.gameObject.GetComponent<SpacemanMovement>().enabled = false;
            other.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            other.transform.SetParent(transform);
            other.transform.localPosition = new Vector3(0, -2, 0);
            currentMode = MovementMode.Carry;
        }
    }

    private void DropSpaceman()
    {
        spaceman.transform.SetParent(GameObject.Find("Outside").transform);
        spaceman.GetComponent<SpacemanMovement>().enabled = true;
        spaceman.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        spaceman.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        currentMode = MovementMode.Flee;

    }

    private void OnDestroy()
    {
        world.AlienFled();
    }
}
