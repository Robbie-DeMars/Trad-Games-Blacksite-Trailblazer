using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton instance = null;
    [SerializeField] Pathway path;

    bool firstFrameInScene = true;

    public int dialogues = 0;
    public int choices = 0;
    public int combats = 0;
    public bool newLevel = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        dialogues = 0;
        choices = 0;
        combats = 0;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("RoadScene"))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }


        //Do next encounter upon entering this scene
        if (SceneManager.GetActiveScene().name.Equals("RoadScene"))
        {
            if (firstFrameInScene)
            {
                path.NextButtonClicked();
                firstFrameInScene = false;
            }
        }
        else
        {
            firstFrameInScene = true;
        }
    }





}
