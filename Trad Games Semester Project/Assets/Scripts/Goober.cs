using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Goober", menuName = "Scriptable Objects/Goober")]
public class Goober : Hero 
{
    public override void Attack1()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator PlayAttack1Anim()
    {
        Debug.Log("Animation Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Animation Over");
    }

    public override void Attack2()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack3()
    {
        throw new System.NotImplementedException();
    }
}
