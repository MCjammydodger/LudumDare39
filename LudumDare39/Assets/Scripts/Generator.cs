using System;
using UnityEngine;

public class Generator : Interactable {

    private Animator animator;
    private GameManager gameManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }
    public override void Interact()
    {
        if (gameManager.numberOfBatteries > 0)
        {
            gameManager.RemoveBattery();
            gameManager.AddPower(10);
            animator.SetTrigger("Open");
        }
    }
}
