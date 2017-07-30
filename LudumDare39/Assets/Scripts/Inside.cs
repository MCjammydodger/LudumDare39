using UnityEngine;

public class Inside : MonoBehaviour {

    [SerializeField]
    private PoweredItem[] poweredItems;

    public void PowerChanged(float power)
    {
        if (poweredItems.Length == 0)
            return;
        int interval = 90 / poweredItems.Length;
        for(int i = 0; i < poweredItems.Length; i++)
        {
            int powerNeeded = 100 - ((i + 1) * interval);
            if(power >= powerNeeded && !poweredItems[i].isPowered)
            {
                poweredItems[i].GainedPower();
            }else if(power < powerNeeded && poweredItems[i].isPowered)
            {
                poweredItems[i].LostPower();
            }
        }
    }
}
