using UnityEngine;

public class World : MonoBehaviour {

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    [HideInInspector]
    public GameObject battery;

    [SerializeField]
    private GameObject batteryPrefab;

    private float batteryWidth;
    private float batteryHeight;

    void Start()
    {
        batteryWidth = batteryPrefab.GetComponent<BoxCollider2D>().bounds.size.x;
        batteryHeight = batteryPrefab.GetComponent<BoxCollider2D>().bounds.size.y;
        SpawnBattery();
    }

    public void SpawnBattery()
    {
        battery = Instantiate(batteryPrefab);
        float randomX = Random.Range(minX + batteryWidth, maxX - batteryWidth);
        float randomY = Random.Range(minY + batteryHeight, maxY - batteryHeight);

        battery.transform.position = new Vector3(randomX, randomY, 0);
    }
}
