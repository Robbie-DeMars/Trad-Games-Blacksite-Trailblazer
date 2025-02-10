using System.Collections;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{

    [SerializeField] Hero Hero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject[] Attack1(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        return Hero.Attack1(heroes, enemies, currentActor);
    }

    public IEnumerator PlayAttack1Anim()
    {
        yield return Hero.PlayAttack1Anim();
    }
}
