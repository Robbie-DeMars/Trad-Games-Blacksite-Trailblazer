using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Camp : MonoBehaviour
{
    [SerializeField] Reigon reigon;
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;

    private void Start()
    {
        if (PlayerPrefs.GetInt("visitedLevel1") != 0)
            if (PlayerPrefs.GetInt("visitedLevel2") != 0)
                if (PlayerPrefs.GetInt("visitedLevel3") != 0)
                    SceneManager.LoadScene("Win");

        if (PlayerPrefs.GetInt("visitedLevel1") == 0)
            button1.SetActive(true);
        else 
            button1.SetActive(false);

        if (PlayerPrefs.GetInt("visitedLevel2") == 0)
            button2.SetActive(true);
        else
            button2.SetActive(false);

        if (PlayerPrefs.GetInt("visitedLevel3") == 0)
            button3.SetActive(true);
        else
            button3.SetActive(false);
    }

    //Loads level 1
    public void Level1()
    {
        //Increment level
        reigon.LoadLevel(1);
        PlayerPrefs.SetInt("visitedLevel1", 1);
        DestroyAllDontDestroyOnLoad();
        SceneManager.LoadScene("RoadScene");
    }

    //Loads level 2
    public void Level2()
    {
        //Increment level
        reigon.LoadLevel(2);
        PlayerPrefs.SetInt("visitedLevel2", 1);
        DestroyAllDontDestroyOnLoad();
        SceneManager.LoadScene("RoadScene");
    }

    //Loads level 3
    public void Level3()
    {
        //Increment level
        reigon.LoadLevel(3);
        PlayerPrefs.SetInt("visitedLevel3", 1);
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
