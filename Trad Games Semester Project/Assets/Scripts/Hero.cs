using System.Collections;
using UnityEngine;

public abstract class Hero : ScriptableObject
{
    public abstract GameObject[] Attack1(GameObject[] heroes, GameObject[] enemies, int currentActor);

    public abstract void Attack2();

    public abstract void Attack3();

    public abstract IEnumerator PlayAttack1Anim();
    public abstract IEnumerator PlayAttack2Anim();
    public abstract IEnumerator PlayAttack3Anim();
}
