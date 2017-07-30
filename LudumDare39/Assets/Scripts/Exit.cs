using System;
using UnityEngine;

public class Exit : MonoBehaviour, Interactable {
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  
    }

    public void Interact()
    {
        gameManager.GoOutside();    
    }

    public string GetButtonPromptMessage()
    {
        return "Press [E] to exit base";
    }
}
