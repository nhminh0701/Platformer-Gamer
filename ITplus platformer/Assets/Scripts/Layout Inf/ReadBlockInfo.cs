using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ReadBlockInfo : MonoBehaviour
{
    [Tooltip("Name of the file you want to save/overwrite")]
    [SerializeField] string levelName = "Level 1";

    Dictionary<int, BlockInfo> blockInforList = new Dictionary<int, BlockInfo>();

    public void ReadEnvironmentInfo()
    {
        ElementInfo[] currentBlocks = FindObjectsOfType<ElementInfo>();

        for (var index = 0; index < currentBlocks.Length; index ++)
        {
            BlockInfo newBlock = new BlockInfo(currentBlocks[index].GetBlockID(), currentBlocks[index].transform.position.x, currentBlocks[index].transform.position.y);
            blockInforList.Add(index, newBlock);
        }

        string destination = Path.Combine(Application.persistentDataPath, levelName + ".txt");
        Debug.Log(JsonConvert.SerializeObject(blockInforList));
        File.WriteAllText(destination, JsonConvert.SerializeObject(blockInforList));
        Debug.Log(destination);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class BlockInfo
{
    public int id;
    public Vector2 position;

    public BlockInfo(int _id, float _xPos, float _yPos)
    {
        id = _id;
        position = new Vector2(_xPos, _yPos);
    }


}

