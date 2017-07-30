using UnityEngine;

public class FirstPersonController : MonoBehaviour {
    [SerializeField]
    private Transform startPosition;

    private CharacterController characterController;
    private Camera firstPersonCamera;
    private FirstPersonTrigger trigger;
    private GameManager gameManager;

    private float moveSpeed = 1000;
    private float rotateSpeed = 200;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
        firstPersonCamera = GetComponentInChildren<Camera>();
        trigger = GetComponentInChildren<FirstPersonTrigger>();
        gameManager = FindObjectOfType<GameManager>();
	}

    private void OnEnable()
    {
        transform.position = startPosition.position;
        transform.rotation = startPosition.rotation;
    }
    // Update is called once per frame
    void Update () {
        CheckForInteraction();
        Movement();
	}

    void CheckForInteraction()
    {
        Interactable interactable = trigger.GetLatestInteractable();
        if(interactable != null)
        {
            gameManager.ShowButtonPrompt(interactable.GetButtonPromptMessage());
            if (Input.GetKeyUp(KeyCode.E))
            {
                interactable.Interact();
            }
        }
        else
        {
            gameManager.CloseButtonPrompt();
        }
    }
    void Movement()
    {
        characterController.SimpleMove(transform.TransformVector(new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime)));
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
        firstPersonCamera.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);
    }
}
