using System.Collections.Generic;
using UnityEngine;

public class SpacemanTrigger : MonoBehaviour {

    private List<Block> blocksInTrigger = new List<Block>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Block block = other.GetComponent<Block>();
        if (block != null && !blocksInTrigger.Contains(block) && !block.isBeingHeld)
        {
            blocksInTrigger.Add(block);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Block block = other.GetComponent<Block>();
        if (block != null && blocksInTrigger.Contains(block) && !block.isBeingHeld)
        {
            blocksInTrigger.Remove(block);
        }
    }

    public Block GetLatestBlock()
    {
        if (blocksInTrigger.Count > 0)
        {
            return blocksInTrigger[blocksInTrigger.Count - 1];
        }
        else
        {
            return null;
        }
    }
}
