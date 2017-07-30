using UnityEngine;

public class SpacemanMovement : MonoBehaviour {

    public bool isFlying { get; private set; }


    private float walkSpeed = 1000f;
    private float jetpackSpeed = 1000f;
    private float gravitySpeed = 1000f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer jetpackRenderer;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private World world;
    private SpacemanTrigger trigger;
    private Spaceman spaceman;
    private SpacemanForcefield spacemanForcefield;

	// Use this for initialization
	void Start () {
        animator = spriteRenderer.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        world = FindObjectOfType<World>();
        trigger = GetComponentInChildren<SpacemanTrigger>();
        spaceman = GetComponent<Spaceman>();
        spacemanForcefield = GetComponent<SpacemanForcefield>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movementVector = new Vector3(GetHorizontal(), GetVertical(), 0);
        rigidBody.velocity = movementVector;
        if (spacemanForcefield.enabled && Input.GetKeyUp(KeyCode.Q))
        {
            spacemanForcefield.UseForcefield();
        }
	}
    
    float GetHorizontal()
    {
        float horizontalAmount = walkSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        if(horizontalAmount > 0 && transform.position.x < world.maxX)
        {
            spriteRenderer.flipX = true;
            jetpackRenderer.flipX = true;
            trigger.transform.localPosition = new Vector3(Mathf.Abs(trigger.transform.localPosition.x), trigger.transform.localPosition.y, trigger.transform.localPosition.z);
        }else if(horizontalAmount < 0 && transform.position.x > world.minX)
        {
            spriteRenderer.flipX = false;
            jetpackRenderer.flipX = false;
            trigger.transform.localPosition = new Vector3(Mathf.Abs(trigger.transform.localPosition.x) * -1, trigger.transform.localPosition.y, trigger.transform.localPosition.z);
        }
        else
        {
            horizontalAmount = 0;
        }
        if (Mathf.Abs(horizontalAmount) > 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        return horizontalAmount;
    }

    float GetVertical()
    {
        float verticalAmount = jetpackSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        if (verticalAmount <= 0)
        {
            jetpackRenderer.enabled = false;
            verticalAmount = -gravitySpeed * Time.deltaTime;
            isFlying = false;
            return verticalAmount;
        }
        if (verticalAmount > 0 && transform.position.y < world.maxY && spaceman.jetpackFuel > 0)
        {
            jetpackRenderer.enabled = true;
            isFlying = true;
        }
        else
        {
            isFlying = true;
            verticalAmount = 0;
        }

        return verticalAmount;
    }
}
