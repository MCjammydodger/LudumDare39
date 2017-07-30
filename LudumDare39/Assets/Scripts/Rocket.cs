using UnityEngine;

public class Rocket : MonoBehaviour {

    private GameManager gameManager;
    private World world;
    private bool entered = false;

    private float speed = 10f;
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Sprite rocketFlyingSprite;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        rigidBody = GetComponent<Rigidbody2D>();
        world = FindObjectOfType<World>();
	}
	
	// Update is called once per frame
	void Update () {
        if (entered)
        {
            rigidBody.velocity = new Vector3(0, speed, 0);
            if(transform.position.y > world.maxY + GetComponent<SpriteRenderer>().bounds.size.y)
            {
                gameManager.GameOver("Congratulations! You Escaped!", true);
            }

        }
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Block>() != null || other.gameObject.GetComponent<Meteorite>() != null || other.gameObject.GetComponent<Alien>() != null)
        {
            Destroy(other.gameObject);
        }
    }

    public void EnterRocket(Spaceman spaceman)
    {
        entered = true;
        spaceman.gameObject.SetActive(false);
        rigidBody.isKinematic = false;
        FindObjectOfType<CameraFollow2D>().SetTarget(transform);
        GetComponent<SpriteRenderer>().sprite = rocketFlyingSprite;
        gameManager.CloseButtonPrompt();
    }

    public bool IsEntered()
    {
        return entered;
    }
}
