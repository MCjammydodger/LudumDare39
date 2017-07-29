using UnityEngine;

public class SpacemanMovement : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
        animator = spriteRenderer.GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        world = FindObjectOfType<World>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movementVector = new Vector3(GetHorizontal(), GetVertical(), 0);
        rigidBody.velocity = movementVector;
	}

    float GetHorizontal()
    {
        float horizontalAmount = walkSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        if(horizontalAmount > 0 && transform.position.x < world.maxX)
        {
            spriteRenderer.flipX = true;
            jetpackRenderer.flipX = true;
        }else if(horizontalAmount < 0 && transform.position.x > world.minX)
        {
            spriteRenderer.flipX = false;
            jetpackRenderer.flipX = false;

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
            return verticalAmount;
        }
        if (verticalAmount > 0 && transform.position.y < world.maxY)
        {
            jetpackRenderer.enabled = true;
        }
        else
        {
            verticalAmount = 0;
        }

        return verticalAmount;
    }
}
