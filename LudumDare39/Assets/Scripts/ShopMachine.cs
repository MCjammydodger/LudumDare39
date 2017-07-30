using UnityEngine;

public class ShopMachine : MonoBehaviour, Interactable {
    [SerializeField]
    private Shop shopUI;

    public string GetButtonPromptMessage()
    {
        return "Press [E] to use the shop machine";
    }

    public void Interact()
    {
        shopUI.gameObject.SetActive(true);
    }

}
