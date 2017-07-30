
using UnityEngine;

public class GeneratorLight : MonoBehaviour {

    private float timeSinceChange;
    private float timeBeforeChange = 1;

    private MeshRenderer meshRenderer;

    [SerializeField]
    private Material redLight;
    [SerializeField]
    private Material greenLight;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update () {
        timeSinceChange += Time.deltaTime;
        if(timeSinceChange >= timeBeforeChange)
        {
            meshRenderer.material = SwitchMaterial();
            timeSinceChange = 0;
        }
	}

    Material SwitchMaterial()
    {
        if (meshRenderer.material.color == redLight.color)
        {
            return greenLight;
        }
        else
        {
            return redLight;
        }
    }
}
