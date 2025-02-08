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

    public void Attack1()
    {

    }

    public IEnumerator PlayAttack1Anim()
    {
        yield return Hero.PlayAttack1Anim();
    }
}
