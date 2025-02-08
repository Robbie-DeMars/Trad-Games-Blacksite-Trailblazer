using System.Collections;
using UnityEngine;

public abstract class Hero : ScriptableObject
{
    public abstract void Attack1();

    public abstract void Attack2();

    public abstract void Attack3();

    public abstract IEnumerator PlayAttack1Anim();
}
