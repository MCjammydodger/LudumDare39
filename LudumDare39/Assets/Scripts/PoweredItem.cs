using UnityEngine;

public abstract class PoweredItem : MonoBehaviour {
    public bool isPowered { get; private set; }
    public abstract void LostPower();
    public abstract void GainedPower();
}
