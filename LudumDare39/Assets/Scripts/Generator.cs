using System;
using UnityEngine;

public class Generator : MonoBehaviour, Interactable {

    private Animator animator;
    private GameManager gameManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Interact()
    {
        if (gameManager.numberOfBatteries > 0)
        {
            gameManager.RemoveBattery();
            gameManager.AddPower(10);
            gameManager.AddScore(200);
            animator.SetTrigger("Open");
        }
    }
    public string GetButtonPromptMessage()
    {
        if (gameManager.numberOfBatteries > 0)
        {
            return "Press [E] to place battery in the generator";
        }
        else
        {
            return "You have no batteries to place";
        }
    }

}
