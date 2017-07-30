using UnityEngine;

public class CameraFollow2D : MonoBehaviour {

    private World world;
    private Spaceman spaceman;
    private Camera cam;
    private Transform arrow;

    private float arrowWidth;
    private float arrowHeight;

    private Transform target;

    private Vector3 bottomLeft;
    private Vector3 topLeft;

    [SerializeField]
    private Rocket rocket;

    // Use this for initialization
    void Start () {
        world = FindObjectOfType<World>();
        spaceman = FindObjectOfType<Spaceman>();
        cam = GetComponent<Camera>();
        arrow = transform.GetChild(0);
        arrowWidth = arrow.GetComponent<SpriteRenderer>().size.x;
        arrowHeight = arrow.GetComponent<SpriteRenderer>().size.y;
        target = spaceman.transform;
    }

    // Update is called once per frame
    void Update () {
        FollowTarget(target.position);
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
    }

    void UpdateArrow()
    {
        Vector3 targetPosition = world.battery.transform.position;

        if (rocket.isActiveAndEnabled)
        {
            targetPosition = rocket.transform.position;
            arrow.GetComponent<SpriteRenderer>().color = Color.white;
            if (rocket.IsEntered())
            {
                arrow.gameObject.SetActive(false);
                return;
            }
        }
        Vector3 newArrowPos = arrow.position;

        bool withinViewX = false;
        bool withinViewY = false;

        arrow.gameObject.SetActive(true);


        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        topLeft = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        if (targetPosition.x < bottomLeft.x)
        {
            newArrowPos.x = bottomLeft.x + arrowWidth/2;
        }else if(targetPosition.x > topLeft.x)
        {
            newArrowPos.x = topLeft.x - arrowWidth/2;
        }
        else
        {
            newArrowPos.x = targetPosition.x;
            withinViewX = true;
        }

        if(targetPosition.y < bottomLeft.y)
        {
            newArrowPos.y = bottomLeft.y + arrowHeight / 2;
        }else if(targetPosition.y > topLeft.y)
        {
            newArrowPos.y = topLeft.y - arrowHeight / 2;
        }
        else
        {
            newArrowPos.y = targetPosition.y;
            withinViewY = true;
        }

        if(withinViewX && withinViewY)
        {
            arrow.gameObject.SetActive(false);
        }
        else
        {
            Vector3 dir = targetPosition - arrow.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrow.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        }
        arrow.position = newArrowPos;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
