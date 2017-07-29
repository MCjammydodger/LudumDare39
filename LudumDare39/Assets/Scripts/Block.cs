using UnityEngine;

public class Block : MonoBehaviour {

    public bool isBeingHeld { get; private set; }
    private float fallSpeed = 10;

    private Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isBeingHeld)
        {
            rigidBody.velocity = new Vector3(0, -fallSpeed, 0);
        }
    }

    public void PickUp(bool pickedUp)
    {
        GetComponent<BoxCollider2D>().isTrigger = pickedUp;
        rigidBody.isKinematic = pickedUp;
        isBeingHeld = pickedUp;
        if (isBeingHeld)
        {
            rigidBody.velocity = Vector3.zero;
        }
    }
}
