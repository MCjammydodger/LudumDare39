using UnityEngine;

public abstract class PoweredItem : MonoBehaviour {
    public bool isPowered { get; protected set; }
    public abstract void LostPower();
    public abstract void GainedPower();
}
