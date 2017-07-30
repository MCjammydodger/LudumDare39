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

    [SerializeField]
    private GameObject baseObject;

    [SerializeField]
    private Transform spacemanStart;

    [SerializeField]
    private Meteorite meteoritePrefab;

    [SerializeField]
    private Alien alienPrefab;

    [SerializeField]
    private Rocket rocket;

    private float batteryWidth;
    private float batteryHeight;

    private float blockSpawnRate = 3;
    private float timeSinceBlockSpawn;

    private int maxNumberOfBlocks = 10;
    private int numberOfBlocks;

    private float timeSinceMeteoriteSpawn;
    private float meteoriteSpawnRate = 6;

    private float timeSinceAlienFled = 12;
    private float alienSpawnRate = 12;
    private bool alienFled = true;

    private Spaceman spaceman;

    private GameManager gameManager;

    void Awake()
    {
        batteryWidth = batteryPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        batteryHeight = batteryPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        baseObject.transform.position = new Vector3(maxX, baseObject.transform.position.y);
        spaceman = FindObjectOfType<Spaceman>();
        gameManager = FindObjectOfType<GameManager>();
        SpawnBattery();
    }

    private void OnEnable()
    {
        spaceman.transform.position = spacemanStart.position;
    }
    void Update()
    {
        timeSinceBlockSpawn += Time.deltaTime;
        if(timeSinceBlockSpawn >= blockSpawnRate && numberOfBlocks < maxNumberOfBlocks)
        {
            timeSinceBlockSpawn = 0;
            SpawnBlock();
        }

        if (gameManager.meteorsStarted) { 
            timeSinceMeteoriteSpawn += Time.deltaTime;
            if (timeSinceMeteoriteSpawn >= meteoriteSpawnRate)
            {
                SpawnMeteorite();
                timeSinceMeteoriteSpawn = 0;
            }
        }

        if (gameManager.aliensStarted)
        {
            if (alienFled)
            {
                timeSinceAlienFled += Time.deltaTime;
                if (timeSinceAlienFled >= alienSpawnRate)
                {
                    SpawnAlien();
                    alienFled = false;
                    timeSinceAlienFled = 0;
                }
            }
        }
        if (gameManager.rocketSpawned)
        {
            if (!rocket.isActiveAndEnabled)
            {
                rocket.gameObject.SetActive(true);
            }
        }
    }

    public void BlockDestroyed()
    {
        numberOfBlocks--;
        timeSinceBlockSpawn = 0;
    }

    private void SpawnMeteorite()
    {
        Meteorite meteorite = Instantiate(meteoritePrefab);
        meteorite.transform.position = SpawnObject(meteorite.GetComponent<SpriteRenderer>());
        meteorite.transform.SetParent(transform);
    }

    private Vector3 SpawnObject(SpriteRenderer spriteRenderer)
    {
        float blockWidth = Mathf.Max(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
        float xPosition = Random.Range(minX + blockWidth, maxX - blockWidth);
        return new Vector3(xPosition, maxY + blockWidth, 0);
    }
    public void SpawnBattery()
    {
        if(battery != null)
        {
            Destroy(battery);
        }
        battery = Instantiate(batteryPrefab);
        float randomX = Random.Range(minX + batteryWidth * 2, maxX - batteryWidth * 2);
        float randomY = Random.Range(minY + batteryHeight * 2, maxY - batteryHeight * 2);

        battery.transform.position = new Vector3(randomX, randomY, 0);
        battery.transform.SetParent(transform);
    }

    void SpawnBlock()
    {
        Block newBlock = Instantiate(blockPrefabs[Random.Range(0, blockPrefabs.Length)]);
        newBlock.transform.position = SpawnObject(newBlock.GetComponent<SpriteRenderer>());
        newBlock.transform.SetParent(transform);
        newBlock.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
        numberOfBlocks++;
    }

    void SpawnAlien()
    {
        Alien alien = Instantiate(alienPrefab);
        alien.transform.position = SpawnObject(alien.GetComponent<SpriteRenderer>());
        alien.transform.SetParent(transform);
    }

    public void AlienFled()
    {
        alienFled = true;
    }
}
