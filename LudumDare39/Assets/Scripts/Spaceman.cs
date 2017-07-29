using UnityEngine;

public class Spaceman : MonoBehaviour {

    private World world;
    private int numberOfBatteries = 0;

	// Use this for initialization
	void Start () {
        world = FindObjectOfType<World>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Battery")
        {
            numberOfBatteries++;
            Destroy(other.gameObject);
            world.SpawnBattery();
        }
    }
}
