using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camp : MonoBehaviour
{
    [SerializeField] Reigon reigon;
    [SerializeField] 
    public void NextLevel()
    {
        //Increment level
        reigon.LoadLevel(PlayerPrefs.GetInt("level") + 1);
        DestroyAllDontDestroyOnLoad();
        SceneManager.LoadScene("RoadScene");

    }

    private void DestroyAllDontDestroyOnLoad()
    {
        GameObject temp = new GameObject();
        DontDestroyOnLoad(temp);
        Scene dontDestroyScene = temp.scene;
        Destroy(temp);

        List<GameObject> dontDestroyObjects = new List<GameObject>();
        dontDestroyScene.GetRootGameObjects(dontDestroyObjects);

        foreach (GameObject obj in dontDestroyObjects)
        {
            Destroy(obj);
        }
    }
}
