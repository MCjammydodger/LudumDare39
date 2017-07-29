using System.Collections.Generic;
using UnityEngine;

public class FirstPersonTrigger : MonoBehaviour {

    private GameManager gameManager;

    private List<Interactable> objectsInTrigger = new List<Interactable>();
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        Debug.Log(other.name + "    " + interactable);
        if(interactable != null && !objectsInTrigger.Contains(interactable))
        {
            objectsInTrigger.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && objectsInTrigger.Contains(interactable))
        {
            objectsInTrigger.Remove(interactable);
        }
    }

    public Interactable GetLatestInteractable()
    {
        if(objectsInTrigger.Count > 0)
        {
            return objectsInTrigger[objectsInTrigger.Count-1];
        }
        else
        {
            return null;
        }
    }
}
