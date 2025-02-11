using Unity.VisualScripting;
using UnityEngine;

public class Controller : StateMachine
{
    [SerializeField] Model model;
    [SerializeField] View view;

    [SerializeField] Transform[] heroPositions;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
                    view.clearPartyHighlights();
                    view.highlightPartyPosition(closestTransform);
                    view.highlightPartyPosition(model.getCurrentActor());

                    //Move hero to highlighted position
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        model.MoveHeroFromTo(model.getCurrentActor(), closestTransform);
                        view.clearPartyHighlights();
                        view.highlightPartyPosition(model.getCurrentActor());
                    }
                }
                else
                {
                    view.clearPartyHighlights();
                    view.highlightPartyPosition(model.getCurrentActor());
                }
            }
            else
            {
                view.clearPartyHighlights();
                view.highlightPartyPosition(model.getCurrentActor());
            }

        }










    }
}
