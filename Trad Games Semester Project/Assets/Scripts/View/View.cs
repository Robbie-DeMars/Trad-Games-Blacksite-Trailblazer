using System.Collections;
using TMPro;
using UnityEngine;

public class View : StateMachine
{
    [SerializeField] Transform[] heroBattlePositions;
    [SerializeField] GameObject[] heroReticles;
    [SerializeField] GameObject[] heroHighlights;

    [SerializeField] Transform[] enemyBattlePositions;
    [SerializeField] GameObject[] enemyReticles;

    [SerializeField] PsiHeatBar psiHeatBar;
    [SerializeField] ActionPointBar APBar;

    [SerializeField] TextMeshProUGUI moveText;

    [SerializeField] ParticleSystem[] particlesHero;
    [SerializeField] ParticleSystem[] particlesEnemy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set all highlighted markers to inactive
        for (int i = 0; i < heroHighlights.Length; i++)
            heroHighlights[i].SetActive(false);

        //Same for hero reticles
        for (int i = 0; i < heroReticles.Length; i++)
            heroReticles[i].SetActive(false);

        //Same for enemy reticles
        for (int i = 0; i < enemyReticles.Length; i++)
            enemyReticles[i].SetActive(false);

        //Turn particle emitters off
        for (int i = 0; i < particlesHero.Length; i++) {
            var emission = particlesHero[i].emission;
            emission.enabled = false;
        }
        for (int i = 0; i < particlesEnemy.Length; i++)
        {
            var emission = particlesEnemy[i].emission;
            emission.enabled = false;
        }
    }


    private void Update()
    {
        if (currentState == State.MOVE)
        {
            moveText.color = Color.red;
            moveText.text = "Move (ON)";
        }
        else
        {
            moveText.color = Color.white;
            moveText.text = "Move (OFF)";
        }
    }


    /// <summary>
    /// Activate the highlighter under the given party position.
    /// </summary>
    /// <param name="space"></param>
    public void HighlightPartyPosition(int space)
    {
        heroHighlights[space].SetActive(true);
    }

    /// <summary>
    /// Clears all highlights on the party side. 
    /// </summary>
    public void ClearPartyHighlights()
    {
        for (int i = 0; i < heroHighlights.Length; i++)
            heroHighlights[i].SetActive(false);
    }

    /// <summary>
    /// Activates the reticle at the given position on the party side.
    /// </summary>
    /// <param name="position"></param>
    public void ActivatePartyReticle(int position)
    {
        heroReticles[position].SetActive(true);
    }

    /// <summary>
    /// Activates the reticle at the given position on the enemy side.
    /// </summary>
    /// <param name="position"></param>
    public void ActivateEnemyReticle(int position)
    {
        enemyReticles[position].SetActive(true);
    }

    /// <summary>
    /// Clears all reticles on both sides of combat.
    /// </summary>
    public void ClearReticles()
    {
        for (int i = 0; i < heroReticles.Length; i++)
            heroReticles[i].SetActive(false);

        for (int i = 0; i < enemyReticles.Length; i++)
            enemyReticles[i].SetActive(false);
    }


    /// <summary>
    /// Execute the animation for this specific character's attack, then update battle positions.
    /// </summary>
    /// <param name="hero"> Specific hero behavior reference </param>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of executing attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack1Anim(HeroBehavior hero, GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        yield return hero.PlayAttack1Anim(currentActor);
        yield return UpdateBattlePositions(heroes, enemies);
    }

    /// <summary>
    /// Execute the animation for this specific character's attack, then update battle positions.
    /// </summary>
    /// <param name="hero"> Specific hero behavior reference </param>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of executing attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack2Anim(HeroBehavior hero, GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        yield return hero.PlayAttack2Anim(currentActor);
        yield return UpdateBattlePositions(heroes, enemies);
    }

    /// <summary>
    /// Execute the animation for this specific character's attack, then update battle positions.
    /// </summary>
    /// <param name="hero"> Specific hero behavior reference </param>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of executing attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack3Anim(HeroBehavior hero, GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        yield return hero.PlayAttack3Anim(currentActor);
        yield return UpdateBattlePositions(heroes, enemies);
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
    public IEnumerator PlayEnemyAttackAnim(EnemyBehavior enemy, GameObject[] heroes, GameObject[] enemies, int position, Enemy.Attack attack)
    {
        yield return enemy.PlayNextAttackAnim(position, attack);
        yield return UpdateBattlePositions(heroes, enemies);
    }

    /// <summary>
    /// Moves all enemies and heroes to their positions in battle over the course of one second
    /// 
    /// Does nothing if all heroes and enemies are in their correct positions
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <returns></returns>
    public IEnumerator UpdateBattlePositions(GameObject[] heroes, GameObject[] enemies)
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

    /// <summary>
    /// Sets the current psionic heat in the UI
    /// </summary>
    /// <param name="ammount"></param>
    public void SetHeat(int ammount)
    {
        psiHeatBar.SetHeat(ammount);
    }

    /// <summary>
    /// Sets the maximum psionic heat
    /// </summary>
    /// <param name="ammount"></param>
    public void SetMaxHeat(int ammount)
    {
        psiHeatBar.SetMaxHeat(ammount);
    }

    /// <summary>
    /// Sets the current AP in the UI
    /// </summary>
    /// <param name="ammount"></param>
    public void SetAP(int ammount)
    {
        APBar.SetAP(ammount);
        Debug.Log(ammount);
    }

    /// <summary>
    /// Sets the maximum AP
    /// </summary>
    /// <param name="ammount"></param>
    public void SetMaxAP(int ammount)
    {
        APBar.SetMaxAP(ammount);
    }

    /// <summary>
    /// Activates the particle at the given position on the hero side.
    /// </summary>
    /// <param name="position"></param>
    public void ActivateHeroParticle(int position)
    {
        var emission = particlesHero[position].emission;
        emission.enabled = true;
    }

    /// <summary>
    /// Activates the particle at the given position on the hero side.
    /// </summary>
    /// <param name="position"></param>
    public void ActivateEnemyParticle(int position)
    {
        var emission = particlesEnemy[position].emission;
        emission.enabled = true;
    }

    /// <summary>
    /// Deactivates all particles on screen.
    /// </summary>
    public void clearParticles()
    {
        for (int i = 0; i < particlesHero.Length; i++)
        {
            var emission = particlesHero[i].emission;
            emission.enabled = false;
        }
        for (int i = 0; i < particlesEnemy.Length; i++)
        {
            var emission = particlesEnemy[i].emission;
            emission.enabled = false;
        }
    }

    /// <summary>
    /// Plays the animation for the psionic heat effect.
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayPsiHeatAnimation()
    {
        //Turn particle emitters on
        for (int i = 0; i < particlesHero.Length; i++)
        {
            var emission = particlesHero[i].emission;
            emission.enabled = true;
        }
        yield return new WaitForSeconds(1f);
        //Turn particle emitters off
        for (int i = 0; i < particlesHero.Length; i++)
        {
            var emission = particlesHero[i].emission;
            emission.enabled = false;
        }
    }

}
