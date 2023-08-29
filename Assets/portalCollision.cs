using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCollision : MonoBehaviour
{
    // for the gameboard
    public GameObject gameBoard;

    //speed of rotation (once activated)
    public float rotationSpeed;

    //for detecting player collision, etc
    public GameObject playerPeice;
    public GameObject key;
    public GameObject counter;

    public bool hasEntered;
    public bool isOpen;

    //this is for board X and Y (these are set by the level designer)
    public float portalBoardX;
    public float portalBoardY;

    public float translucentValue;

    // Start is called before the first frame update
    void Start()
    {
        // setting gameBoard to parent object
        transform.parent = gameBoard.transform;


        //set animation to stop
        translucentValue = .35f;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, translucentValue);
    }

    // Update is called once per frame
    void Update()
    {
        //rotating the portal by rotationSpeed
        Quaternion theRotation = transform.localRotation;
        theRotation.z *= rotationSpeed;
        transform.localRotation = theRotation;

        //checking if key is taken
        if (key.GetComponent<keyCollision>().isTaken)
        {
            //resume animation, open
            GetComponent<Animator>().enabled = true;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            isOpen = true;

            

        }
        //checking if key is not taken
        if (!key.GetComponent<keyCollision>().isTaken)
        {
            //stop animation, close
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, translucentValue);
            isOpen = false;
        }

        
    }

    public void OnTriggerEnter2D(Collider2D target)
    {
        //if we are colliding with the player and the player is set for that position and the player is not supposed to be dead
        if (target.gameObject.tag.Equals("Player") && !target.gameObject.GetComponent<playerMovement>().isTargeted && !target.gameObject.GetComponent<playerMovement>().isMoving && isOpen && target.gameObject.GetComponent<playerMovement>().playerBoardX == portalBoardX && target.gameObject.GetComponent<playerMovement>().playerBoardY == portalBoardY)
        {
            //make it taken (this will signal to the game's logic that it has been taken...processes will continue within gamelogic)
            hasEntered = true;

            //TODO : open the popup window for going to the next level

            //make it so that the game text changes to the win message
            counter.GetComponent<counterScript>().hasWon = true;

        }

    }

    public void portalReset()
    {
        // setting gameBoard to parent object
        transform.parent = gameBoard.transform;


        //set animation to stop
        translucentValue = .35f;
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, translucentValue);
        counter.GetComponent<counterScript>().hasWon = false;
    }


}
