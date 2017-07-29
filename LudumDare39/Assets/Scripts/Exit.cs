using System;
using UnityEngine;

public class Exit : Interactable {
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  
    }

    public override void Interact()
    {
        gameManager.GoOutside();    
    }
}
