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

    [SerializeField]
    private Block[] blockPrefabs;

    private float batteryWidth;
    private float batteryHeight;

    private float blockSpawnRate = 3;
    private float timeSinceBlockSpawn;

    private int maxNumberOfBlocks = 10;
    private int numberOfBlocks;

    void Start()
    {
        batteryWidth = batteryPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        batteryHeight = batteryPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        SpawnBattery();
    }

    void Update()
    {
        timeSinceBlockSpawn += Time.deltaTime;
        if(timeSinceBlockSpawn >= blockSpawnRate && numberOfBlocks < maxNumberOfBlocks)
        {
            timeSinceBlockSpawn = 0;
            SpawnBlock();
        }
    }

    public void SpawnBattery()
    {
        battery = Instantiate(batteryPrefab);
        float randomX = Random.Range(minX + batteryWidth * 2, maxX - batteryWidth * 2);
        float randomY = Random.Range(minY + batteryHeight * 2, maxY - batteryHeight * 2);

        battery.transform.position = new Vector3(randomX, randomY, 0);
    }

    void SpawnBlock()
    {
        Block newBlock = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)]);
        float blockWidth = Mathf.Max(newBlock.GetComponent<SpriteRenderer>().bounds.size.x, newBlock.GetComponent<SpriteRenderer>().bounds.size.y);
        float xPosition = Random.Range(minX + blockWidth, maxX - blockWidth);
        newBlock.transform.position = new Vector3(xPosition, maxY + blockWidth, 0);
        newBlock.transform.SetParent(transform);
        numberOfBlocks++;
    }
}
