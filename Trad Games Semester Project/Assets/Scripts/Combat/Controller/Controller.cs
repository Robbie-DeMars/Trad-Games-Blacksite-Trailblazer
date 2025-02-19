using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : StateMachine
{
    [SerializeField] Model model;
    [SerializeField] View view;

    [SerializeField] Transform[] heroPositions;

    // Update is called once per frame
    void Update()
    {

        //Execute movement state behavior.
        if (currentState == State.MOVE)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 mouseWorldPos = new Vector3();
            if (Physics.Raycast(ray, out hit)) // Check if ray hits something
            {
                mouseWorldPos = hit.point; // Get the exact point where it hits
                float closestDist = 100f;
                int closestTransform = -1;
                float currentDist = 0f;
                for (int x = 0; x < heroPositions.Length; x++)
                {
                    currentDist = Vector3.Distance(mouseWorldPos, heroPositions[x].position);
                    if (currentDist < closestDist && currentDist <= 4f)
                    {
                        closestDist = currentDist;
                        closestTransform = x;
                    }
                }

                if (closestTransform != -1)
                {
                    view.ClearPartyHighlights();
                    view.HighlightPartyPosition(closestTransform);
                    view.HighlightPartyPosition(model.GetCurrentActor());

                    //Move hero to highlighted position
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        model.MoveHeroFromTo(model.GetCurrentActor(), closestTransform);
                        view.ClearPartyHighlights();
                        view.HighlightPartyPosition(model.GetCurrentActor());
                    }
                }
                else
                {
                    view.ClearPartyHighlights();
                    view.HighlightPartyPosition(model.GetCurrentActor());
                }
            }
            else
            {
                view.ClearPartyHighlights();
                view.HighlightPartyPosition(model.GetCurrentActor());
            }

        }


    }

    /// <summary>
    /// Activates the reticles corresponding to the position of the currently active party member's attack 1
    /// </summary>
    public void PointerEnterAttack1()
    {
        if (currentState == State.LOSE && currentState == State.WIN)
            return;

        List<List<int>> positions = model.heroes[model.GetCurrentActor()].GetComponent<HeroBehavior>().GetAttack1Targets(model.GetCurrentActor());

        List<int> heroes = positions[0];
        List<int> enemies = positions[1];

        for (int x = 0; x < heroes.Count; x++)
        {
            if (model.heroes.Length > heroes[x] && heroes[x] >= 0)
                view.ActivatePartyReticle(heroes[x]);
        }

        for (int x = 0; x < enemies.Count; x++)
        {
            if (model.heroes.Length > enemies[x] && enemies[x] >= 0)
                view.ActivateEnemyReticle(enemies[x]);
        }
    }

    /// <summary>
    /// Activates the reticles corresponding to the position of the currently active party member's attack 2
    /// </summary>
    public void PointerEnterAttack2()
    {
        List<List<int>> positions = model.heroes[model.GetCurrentActor()].GetComponent<HeroBehavior>().GetAttack2Targets(model.GetCurrentActor());

        List<int> heroes = positions[0];
        List<int> enemies = positions[1];

        for (int x = 0; x < heroes.Count; x++)
        {
            if (model.heroes.Length > heroes[x] && heroes[x] >= 0)
                view.ActivatePartyReticle(heroes[x]);
        }

        for (int x = 0; x < enemies.Count; x++)
        {
            if (model.heroes.Length > heroes[x] && heroes[x] >= 0)
                view.ActivateEnemyReticle(enemies[x]);
        }
    }

    /// <summary>
    /// Activates the reticles corresponding to the position of the currently active party member's attack 3
    /// </summary>
    public void PointerEnterAttack3()
    {
        List<List<int>> positions = model.heroes[model.GetCurrentActor()].GetComponent<HeroBehavior>().GetAttack3Targets(model.GetCurrentActor());

        List<int> heroes = positions[0];
        List<int> enemies = positions[1];

        for (int x = 0; x < heroes.Count; x++)
        {
            if (model.heroes.Length > heroes[x] && heroes[x] >= 0)
                view.ActivatePartyReticle(heroes[x]);
        }

        for (int x = 0; x < enemies.Count; x++)
        {
            if (model.heroes.Length > enemies[x] && enemies[x] >= 0)
                view.ActivateEnemyReticle(enemies[x]);
        }
    }

    /// <summary>
    /// Activates the reticles corresponding to the position of the currently active party member's attack 3
    /// </summary>
    public void PointerEnterEnemyAttack(List<List<int>> positions)
    {
        List<int> heroes = positions[0];
        List<int> enemies = positions[1];

        for (int x = 0; x < heroes.Count; x++)
        {
            if (model.heroes.Length > heroes[x] && heroes[x] >= 0)
                view.ActivatePartyReticle(heroes[x]);
        }

        for (int x = 0; x < enemies.Count; x++)
        {
            if (model.heroes.Length > heroes[x] && heroes[x] >= 0)
                view.ActivateEnemyReticle(enemies[x]);
        }
    }


    /// <summary>
    /// Clears all reticles
    /// </summary>
    public void PointerExit()
    {
        view.ClearReticles();
    }



}
