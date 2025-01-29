using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CharacterCombatManager 
{
    public IEnumerator Attack1();
    public List<int> DisplayAttack1Targets();
    public IEnumerator Attack2();
    public List<int> DisplayAttack2Targets();
    public IEnumerator Attack3();
    public List<int> DisplayAttack3Targets();
    public void incrementHP(int ammount);
    public IEnumerator die();
    public IEnumerator moveLeft();
    public IEnumerator moveRight();

}
