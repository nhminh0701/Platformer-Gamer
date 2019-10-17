using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInfo : MonoBehaviour
{
    [SerializeField] int blockID;
    
    public int GetBlockID() { return blockID; }
}
