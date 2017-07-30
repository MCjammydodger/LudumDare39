using UnityEngine;

public class Meteorite : MonoBehaviour {

    private GameManager gameManager;

    private float fallSpeed = 5;
	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-40, 40)));
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, -fallSpeed * fallSpeed * Time.deltaTime));
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Block>())
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.GetComponent<Spaceman>())
        {
            gameManager.GameOver("You were hit by a meteorite and perished!");
        }
        if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Base")
        {
            Destroy(gameObject);
        }
    }
}
