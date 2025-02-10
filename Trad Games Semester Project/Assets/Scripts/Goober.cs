using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Goober", menuName = "Scriptable Objects/Goober")]
public class Goober : Hero 
{
    public override GameObject[] Attack1(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        GameObject temp = heroes[currentActor];
        heroes[currentActor] = heroes[4];
        heroes[4] = temp;
        return heroes;
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

    public override IEnumerator PlayAttack2Anim()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack3()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator PlayAttack3Anim()
    {
        throw new System.NotImplementedException();
    }
}
