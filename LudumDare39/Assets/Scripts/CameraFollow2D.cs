using UnityEngine;

public class CameraFollow2D : MonoBehaviour {

    private World world;
    private Spaceman spaceman;
    private Camera cam;
    private Transform arrow;

    private float arrowWidth;
    private float arrowHeight;

    Vector3 bottomLeft;
    Vector3 topLeft;
    // Use this for initialization
    void Start () {
        world = FindObjectOfType<World>();
        spaceman = FindObjectOfType<Spaceman>();
        cam = GetComponent<Camera>();
        arrow = transform.GetChild(0);
        arrowWidth = arrow.GetComponent<SpriteRenderer>().size.x;
        arrowHeight = arrow.GetComponent<SpriteRenderer>().size.y;

    }

    // Update is called once per frame
    void Update () {
        FollowTarget(spaceman.transform.position);
        UpdateArrow();
	}

    void FollowTarget(Vector3 target)
    {
        Vector3 oldPos = transform.position;
        Vector3 targetPos = new Vector3(target.x, target.y, -10);
        transform.position = targetPos;

        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        topLeft = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        Vector3 newPos = transform.position;

        if(bottomLeft.x < world.minX || topLeft.x > world.maxX)
        {
            newPos.x = oldPos.x;    
        }
        if(bottomLeft.y < world.minY || topLeft.y > world.maxY)
        {
            newPos.y = oldPos.y;
        }
        transform.position = newPos;

        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        topLeft = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
    }

    void UpdateArrow()
    {
        Vector3 batteryPosition = world.battery.transform.position;
        Vector3 newArrowPos = arrow.position;

        bool withinViewX = false;
        bool withinViewY = false;

        arrow.gameObject.SetActive(true);

        if (batteryPosition.x < bottomLeft.x + arrowWidth / 2)
        {
            newArrowPos.x = bottomLeft.x + arrowWidth/2;
        }else if(batteryPosition.x > topLeft.x - arrowWidth / 2)
        {
            newArrowPos.x = topLeft.x - arrowWidth/2;
        }
        else
        {
            newArrowPos.x = batteryPosition.x;
            withinViewX = true;
        }

        if(batteryPosition.y < bottomLeft.y + arrowHeight / 2)
        {
            newArrowPos.y = bottomLeft.y + arrowHeight / 2;
        }else if(batteryPosition.y > topLeft.y - arrowHeight / 2)
        {
            newArrowPos.y = topLeft.y - arrowHeight / 2;
        }
        else
        {
            newArrowPos.y = batteryPosition.y;
            withinViewY = true;
        }

        if(withinViewX && withinViewY)
        {
            arrow.gameObject.SetActive(false);
        }
        else
        {
            Vector3 dir = batteryPosition - arrow.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        }
        arrow.position = newArrowPos;
    }
}
