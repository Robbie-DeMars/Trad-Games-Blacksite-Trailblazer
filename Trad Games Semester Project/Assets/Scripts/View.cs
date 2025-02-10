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
        for (int i = 0; i < heroHighlights.Length; i++)
            heroHighlights[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void highlightPartyPosition(int space)
    {
        Debug.Log("Highlighting position " + space);
        heroHighlights[space].SetActive(true);
    }

    public void clearPartyHighlights()
    {
        for (int i = 0; i < heroHighlights.Length; i++)
            heroHighlights[i].SetActive(false);
    }


    public IEnumerator playAttack1Anim(HeroBehavior hero, GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        yield return hero.PlayAttack1Anim();
        yield return updateBattlePositions(heroes, enemies);
    }

    public IEnumerator updateBattlePositions(GameObject[] heroes, GameObject[] enemies)
    {
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
