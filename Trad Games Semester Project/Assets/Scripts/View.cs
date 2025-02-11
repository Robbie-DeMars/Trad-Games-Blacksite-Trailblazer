using System.Collections;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] Transform[] heroBattlePositions;
    [SerializeField] Transform[] enemyBattlePositions;

    [SerializeField] GameObject[] heroHighlights;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set all highlighted markers to inactive
        for (int i = 0; i < heroHighlights.Length; i++)
            heroHighlights[i].SetActive(false);
    }

    /// <summary>
    /// Activate the highlighter under the given party position.
    /// </summary>
    /// <param name="space"></param>
    public void highlightPartyPosition(int space)
    {
        heroHighlights[space].SetActive(true);
    }

    /// <summary>
    /// Clears all highlights on the party side. 
    /// </summary>
    public void clearPartyHighlights()
    {
        for (int i = 0; i < heroHighlights.Length; i++)
            heroHighlights[i].SetActive(false);
    }

    /// <summary>
    /// Execute the animation for this specific character's attack, then update battle positions.
    /// </summary>
    /// <param name="hero"> Specific hero behavior reference </param>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of executing attack </param>
    /// <returns></returns>
    public IEnumerator playAttack1Anim(HeroBehavior hero, GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        yield return hero.PlayAttack1Anim(currentActor);
        yield return updateBattlePositions(heroes, enemies);
    }

    /// <summary>
    /// Execute the animation for this specific character's attack, then update battle positions.
    /// </summary>
    /// <param name="hero"> Specific hero behavior reference </param>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of executing attack </param>
    /// <returns></returns>
    public IEnumerator playAttack2Anim(HeroBehavior hero, GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        yield return hero.PlayAttack2Anim(currentActor);
        yield return updateBattlePositions(heroes, enemies);
    }

    /// <summary>
    /// Execute the animation for this specific character's attack, then update battle positions.
    /// </summary>
    /// <param name="hero"> Specific hero behavior reference </param>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of executing attack </param>
    /// <returns></returns>
    public IEnumerator playAttack3Anim(HeroBehavior hero, GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        yield return hero.PlayAttack3Anim(currentActor);
        yield return updateBattlePositions(heroes, enemies);
    }

    /// <summary>
    /// Executes the animation of the passed enemy at the given position, then updates battle positions.
    /// </summary>
    /// <param name="enemy"> Specific enemy behavior reference </param>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="position"> Current position of executing attack </param>
    /// <param name="attack"> Reference to the specific attack executing </param>
    /// <returns></returns>
    public IEnumerator playEnemyAttackAnim(EnemyBehavior enemy, GameObject[] heroes, GameObject[] enemies, int position, Enemy.Attack attack)
    {
        yield return enemy.PlayNextAttackAnim(position, attack);
        yield return updateBattlePositions(heroes, enemies);
    }

    /// <summary>
    /// Moves all enemies and heroes to their positions in battle over the course of one second
    /// 
    /// Does nothing if all heroes and enemies are in their correct positions
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <returns></returns>
    public IEnumerator updateBattlePositions(GameObject[] heroes, GameObject[] enemies)
    {
        //Check first to see if anyone needs to move
        bool noUpdateNeeded = true;
        for (int x = 0; x < heroes.Length; x++)
        {
            if (heroes[x] != null && heroes[x].transform.position != heroBattlePositions[x].position)
                noUpdateNeeded = false;
        }
        for (int x = 0; x < enemies.Length; x++)
        {
            if (enemies[x] != null && enemies[x].transform.position != enemyBattlePositions[x].position)
                noUpdateNeeded = false;
        }
        if (noUpdateNeeded)
            yield break;

        Debug.Log("Updating positions...");

        //Move all heroes / enemies to their positions
        float elapsedTime = 0f;
        float duration = 1f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            for (int x = 0; x < heroes.Length; x++)
            {
                if (heroes[x] != null)
                    heroes[x].transform.position = Vector3.Lerp(heroes[x].transform.position, heroBattlePositions[x].position, t);
            }
            for (int x = 0; x < enemies.Length; x++)
            {
                if (enemies[x] != null)
                    enemies[x].transform.position = Vector3.Lerp(enemies[x].transform.position, enemyBattlePositions[x].position, t);
            }

            yield return null;
        }


        for (int x = 0; x < heroes.Length; x++)
        {
            if (heroes[x] != null)
                heroes[x].transform.position = heroBattlePositions[x].position;
        }
        for (int x = 0; x < enemies.Length; x++)
        {
            if (enemies[x] != null)
                enemies[x].transform.position = enemyBattlePositions[x].position;
        }

    }

}
