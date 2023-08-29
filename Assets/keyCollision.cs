using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCollision : MonoBehaviour
{
    // for the gameboard
    public GameObject gameBoard;

    //for detecting player collision, etc
    public GameObject playerPeice;

    public bool isTaken;

    //this is for board X and Y (these are set by the level designer)
    public float keyBoardX;
    public float keyBoardY;

    //these are the objects "real" x and y. For resetting board if retry is necesary.
    public float startX;
    public float startY;

    //for setting it's default layer (set by level designer)
    public float visibleLayer;
    public float invisibleLayer;

    // Start is called before the first frame update
    void Start()
    {
        // setting gameBoard to parent object
        transform.parent = gameBoard.transform;


        isTaken = false;

        //setting visibility layers
        visibleLayer = -3;
        invisibleLayer = 100;
        
        //setting our start position
        startX = transform.position.x;
        startY = transform.position.y;
        
        //making sure that it's visible
        transform.position = new Vector3(startX, startY, visibleLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //for when the key collides with any object
    public void OnTriggerEnter2D(Collider2D target)
    { 
        //if we are colliding with the player and the player is set for that position
        if (target.gameObject.tag.Equals("Player") && !isTaken && target.gameObject.GetComponent<playerMovement>().playerBoardX == keyBoardX && target.gameObject.GetComponent<playerMovement>().playerBoardY == keyBoardY)
        {
            //make it taken (this will signal to the game's logic that it has been taken...processes will continue within gamelogic)
            isTaken = true;
            
            //make invisible (by taking it off of the screen)
            //TODO: figure out how to make this a specific location
            transform.position = new Vector3(transform.position.x, transform.position.y, invisibleLayer);
        }

    }

    //resets the position of the key to whatever the level designer specified it as in the beginning. Will be called by the gameLogic object
    public void putBackToStartPosition()
    {
        //setting back to original position as gotten from the start method
        transform.position = new Vector3(startX, startY, visibleLayer);

    }

    public void keyReset()
    {
        // setting gameBoard to parent object
        transform.parent = gameBoard.transform;

        isTaken = false;

        //setting visibility layers
        visibleLayer = -3;
        invisibleLayer = 100;

        //putBackToStartPosition();

        //making sure that it's visible
        transform.position = new Vector3(transform.position.x, transform.position.y, visibleLayer);
    }
}
