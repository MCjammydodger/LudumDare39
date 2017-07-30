using UnityEngine;

public class Forcefield : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Meteorite>() != null || other.GetComponent<Alien>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
