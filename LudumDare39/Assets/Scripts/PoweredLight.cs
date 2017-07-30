using System.Collections;
using UnityEngine;

public class PoweredLight : PoweredItem
{
    private Light lightSource;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        isPowered = true;
        lightSource = GetComponentInChildren<Light>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public override void GainedPower()
    {
        lightSource.intensity = 1;
        meshRenderer.material.EnableKeyword("_EMISSION");
        isPowered = true;
    }

    public override void LostPower()
    {
        lightSource.intensity = 0;
        meshRenderer.material.DisableKeyword("_EMISSION");
        isPowered = false;
    }
}
