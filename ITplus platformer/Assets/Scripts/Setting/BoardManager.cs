using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    [SerializeField] bool isRecordingMap = false;
    [SerializeField] ElementInfo[] elementList;
    [Tooltip("The recorded map file you want to load")]
    [SerializeField] string levelName;
    GameObject elementPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ExtractMapInfo();
    }

    private void ExtractMapInfo()
    {
        if (!isRecordingMap)
        {
            levelName = SceneManager.GetActiveScene().name;
        }
        
        string destination = Path.Combine(Application.persistentDataPath, levelName + ".txt");

        string content = File.ReadAllText(destination);
        Debug.Log(content);

        //Dictionary<int, BlockInfo> blockInforList = JsonConvert.DeserializeObject(content);

        Dictionary<int, BlockInfo> blockInforList = JsonConvert.DeserializeObject<Dictionary<int, BlockInfo>>(content);
        CreateEnv(blockInforList);
    }

    private void CreateEnv(Dictionary<int, BlockInfo> blockInforList)
    {
        if (elementList.Length == 0)
        {
            Debug.LogWarning("Empty element array, pls insert a new array!!");
            return;
        }

        foreach (KeyValuePair<int, BlockInfo> element in blockInforList)
        {
            BlockInfo blockInfo = element.Value;
            
            for (var index = 0; index < elementList.Length; index++)
            {
                if (elementList[index].GetBlockID() == blockInfo.id)
                {
                    elementPrefab = elementList[index].gameObject;
                }
            }

            Vector3 position = blockInfo.position;

            GameObject elementBlock = Instantiate(elementPrefab, position, Quaternion.identity);
            elementBlock.transform.SetParent(GameObject.Find("Board").transform);
        }
    }

    void Update()
    {
        
    }
}
