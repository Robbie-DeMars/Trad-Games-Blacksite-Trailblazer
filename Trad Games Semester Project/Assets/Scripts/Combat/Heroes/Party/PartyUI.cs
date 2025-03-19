using UnityEngine;
using UnityEngine.UI;

public class PartyUI : MonoBehaviour
{
    [SerializeField] PartyState party;

    //Possible party member sprite representations
    [SerializeField] Sprite transparent; //For empty space
    [SerializeField] Sprite main; //For main character
    [SerializeField] Sprite side1; //For side1 character
    [SerializeField] Sprite side2; //For side2 character

    //Images to represent current spaces
    [SerializeField] Image[] onImages;
    [SerializeField] Transform[] onTransforms;
    [SerializeField] Image[] offImages;
    [SerializeField] Transform[] offTransforms;

    CanvasGroup canvasGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Make all spaces transparent
        for (int x = 0; x < onImages.Length; x++)
        {
            onImages[x].sprite = transparent;
        }
        for (int x = 0; x < offImages.Length; x++)
        {
            offImages[x].sprite = transparent;
        }

        //Get canvasgroup and set group invisible and not interactable
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        //Set main image
        onImages[party.GetMainPosition()].sprite = main;
        onImages[party.GetSide1Position()].sprite = side1;
        onImages[party.GetSide2Position()].sprite = side2;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canvasGroup.alpha != 0)
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
            else
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }

    public void UpdateParty(Image draggedImage)
    {
        //Find dragged character, if any (there should be at most one)
        int draggedImageIndex = 0;
        bool draggedOn = true;
        for (int x = 0; x < onImages.Length; x++)
        {
            if (onImages[x].transform.position != onTransforms[x].position)
            {
                draggedImage = onImages[x];
                draggedImageIndex = x;
                draggedOn = true;
            }
            else if (offImages[x].transform.position != offTransforms[x].position)
            {
                draggedImage = offImages[x];
                draggedImageIndex = x;
                draggedOn = false;
            }
        }

        //Update party positions in view
        if (draggedImage != null) {
            //Find closest transform
            float closestDist;
            int closestImageIndex = 0;
            bool closestOn = true;
            Image destinationImage = null;
            if (draggedImage != onImages[0])
            {
                closestDist = Vector2.Distance(draggedImage.transform.position, onImages[0].transform.position);
                closestImageIndex = 0;
                closestOn = true;
                destinationImage = onImages[0];
            }
            else
            {
                closestDist = Vector2.Distance(draggedImage.transform.position, onImages[1].transform.position);
                closestImageIndex = 1;
                closestOn = true;
                destinationImage = onImages[1];
            }

            //Loop through onImages
            for (int x = 0; x < onImages.Length; x++)
            {
                if (onImages[x] == draggedImage)
                    continue;
                if (closestDist > Vector2.Distance(draggedImage.transform.position, onImages[x].transform.position))
                {
                    closestDist = Vector2.Distance(draggedImage.transform.position, onImages[x].transform.position);
                    destinationImage = onImages[x];
                    closestImageIndex = x;
                    closestOn = true;
                }
            }

            //Loop through offImages
            for (int x = 0; x < offImages.Length; x++)
            {
                if (offImages[x] == draggedImage)
                    continue;
                if (closestDist > Vector2.Distance(draggedImage.transform.position, offImages[x].transform.position))
                {
                    closestDist = Vector2.Distance(draggedImage.transform.position, offImages[x].transform.position);
                    destinationImage = offImages[x];
                    closestImageIndex = x;
                    closestOn = false;
                }
            }

            //Swap both of their places in model
            if (draggedOn)
            {
                if (closestOn)
                {
                    Image temp = onImages[closestImageIndex];
                    onImages[closestImageIndex] = onImages[draggedImageIndex];
                    onImages[draggedImageIndex] = temp;
                }
                else
                {
                    Image temp = offImages[closestImageIndex];
                    offImages[closestImageIndex] = onImages[draggedImageIndex];
                    onImages[draggedImageIndex] = temp;
                }
            }
            else
            {
                if (closestOn)
                {
                    Image temp = onImages[closestImageIndex];
                    onImages[closestImageIndex] = offImages[draggedImageIndex];
                    offImages[draggedImageIndex] = temp;
                }
                else
                {
                    Image temp = offImages[closestImageIndex];
                    offImages[closestImageIndex] = offImages[draggedImageIndex];
                    offImages[draggedImageIndex] = temp;
                }
            }

            //Finally, loop through each array again, pairing the images with their corresponding transforms
            //Loop through onImages
            for (int x = 0; x < onImages.Length; x++)
            {
                onImages[x].transform.position = onTransforms[x].position;
            }
            //Loop through offImages
            for (int x = 0; x < offImages.Length; x++)
            {
                offImages[x].transform.position = offTransforms[x].position;
            }
        }

    }


}
