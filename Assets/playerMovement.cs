using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    //reference to the mapboard parent object
    public GameObject gameBoard;

    //list of enemies trying to kill this piece
    public List<GameObject> enemiesOnBoard;

    //id of the peice attacking
    public int attackID;

    //knight history
    public List<float> knightHistoryX;
    public List<float> knightHistoryY;

    //particles to emit upon death
    //public ParticleSystem deathParticles;

    //so we can change the counter...
    public GameObject counter;

    //whether more than one enemy is attacking at the same time (so that only one person attacks at a time)
    public bool someoneElseAttacking;
    public int peopleAttacking;

    //for the exit, getting exit info
    public GameObject exit;

    //for if playerKnight is locked on to (an enemy is ready to kill you)
    //in this case the playerKnight will not move
    public bool isTargeted;

    //player position in the matrix, 
    //these will be set in the unity editor, not here...
    public float playerBoardX;
    public float playerBoardY;

    //booleans for player movement
    public bool movingRight;
    public bool movingLeft;
    public bool movingUp;
    public bool movingDown;

    public bool isMoving;
    public bool canMove;

    //speed for various directions
    public float speedX;
    public float speedY;
    public float speedZ;

    //for controlling directions
    private float zeroSpeed;
    private float spaceToMove;
    private float spaceMoved;

    //for making invisible/visible
    public float invisibleLayer;
    public float visibleLayer;

    //for holding start and end position
    public float startX;
    public float startY;

    //different L shaped moves, listed from A - H in clockwise order, starting from 1:00 
    //see diagram A for better illustration
    public bool movingA;
    public bool movingB;
    public bool movingC;
    public bool movingD;
    public bool movingE;
    public bool movingF;
    public bool movingG;
    public bool movingH;


    // Start is called before the first frame update
    void Start()
    {

        // setting gameBoard to parent object
        transform.parent = gameBoard.transform;


        enemiesOnBoard = new List<GameObject>();

        attackID = 0;
        peopleAttacking = 0;

        //obstantiating and adding the knights initial location in the player history in counter
        counter.GetComponent<counterScript>().playerHistoryX = new List<float>();
        counter.GetComponent<counterScript>().playerHistoryY = new List<float>();

        counter.GetComponent<counterScript>().playerHistoryX.Add(playerBoardX);
        counter.GetComponent<counterScript>().playerHistoryY.Add(playerBoardY);

        //pause the particles upon start
        //deathParticles.Pause();

        //assume no one is attacking player yet
        someoneElseAttacking = false;

        //setting locked on to false
        isTargeted = false;

        //setting our start position
        startX = transform.position.x;
        startY = transform.position.y;

        //placeholder for 0
        zeroSpeed = 0f;

        //for controlling space moved for each board space
        spaceToMove = 2.9f;
        spaceMoved = 0f;

        //setting all movements to false at start
        movingA = false;
        movingB = false;
        movingC = false;
        movingD = false;
        movingE = false;
        movingF = false;
        movingG = false;
        movingH = false;

        movingRight = false;
        movingLeft = false;
        movingUp = false;
        movingDown = false;

        isMoving = false;

        //setting visible/invisiblelayers
        invisibleLayer = 100f;
        visibleLayer = -3f;

        //makeing sure it's visible
        makeVisible();

        //make sure you can have movement
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {
        //scrapped reference #1

        //up,up,right...movementA
        if (movingA && canMove)
        {
            //Checking for the three steps of movement

            //move up
            if (spaceMoved < 1f)
            {
                transform.position += new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;
                
            }
            //move up
            if (spaceMoved >= 1f && spaceMoved < 2f)
            {
                transform.position += new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;
            }
            //move right
            if (spaceMoved >= 2f && spaceMoved < spaceToMove)
            {
                transform.position += new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //if moved enough, stop
            if (spaceMoved > spaceToMove)
            {
                
                stop();

            }
        }

        //up, right, right...movementB
        if (movingB && canMove)
        {
            //Checking for the three steps of movement

            //move up
            if (spaceMoved < 1f)
            {
                transform.position += new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move up
            if (spaceMoved >= 1f && spaceMoved < 2f)
            {
                transform.position += new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //move right
            if (spaceMoved >= 2f && spaceMoved < spaceToMove)
            {
                transform.position += new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //if moved enough, stop
            if (spaceMoved > spaceToMove)
            {
                stop();
            }
        }

        // down, right, right...movement C
        if (movingC && canMove)
        {
            //Checking for the three steps of movement

            //move down
            if (spaceMoved < 1f)
            {
                transform.position -= new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move right
            if (spaceMoved >= 1f && spaceMoved < 2f)
            {
                transform.position += new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //move right
            if (spaceMoved >= 2f && spaceMoved < spaceToMove)
            {
                transform.position += new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //if moved enough, stop
            if (spaceMoved > spaceToMove)
            {
                stop();
            }
        }

        // down, down, right...movement D
        if (movingD && canMove)
        {
            //Checking for the three steps of movement

            //move down
            if (spaceMoved < 1f)
            {
                transform.position -= new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move down
            if (spaceMoved >= 1f && spaceMoved < 2f)
            {
                transform.position -= new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move right
            if (spaceMoved >= 2f && spaceMoved < spaceToMove)
            {
                transform.position += new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //if moved enough, stop
            if (spaceMoved > spaceToMove)
            {
                stop();
            }
        }

        // down, down, left...movement E
        if (movingE && canMove)
        {
            //Checking for the three steps of movement

            //move down
            if (spaceMoved < 1f)
            {
                transform.position -= new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move down
            if (spaceMoved >= 1f && spaceMoved < 2f)
            {
                transform.position -= new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move left
            if (spaceMoved >= 2f && spaceMoved < spaceToMove)
            {
                transform.position -= new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //if moved enough, stop
            if (spaceMoved > spaceToMove)
            {
                stop();
            }
        }

        // down, left, left...movement F
        if (movingF && canMove)
        {
            //Checking for the three steps of movement

            //move down
            if (spaceMoved < 1f)
            {
                transform.position -= new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move left
            if (spaceMoved >= 1f && spaceMoved < 2f)
            {
                transform.position -= new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //move left
            if (spaceMoved >= 2f && spaceMoved < spaceToMove)
            {
                transform.position -= new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //if moved enough, stop
            if (spaceMoved > spaceToMove)
            {
                stop();
            }
        }

        // up, left, left...movement G
        if (movingG && canMove)
        {
            //Checking for the three steps of movement

            //move up
            if (spaceMoved < 1f)
            {
                transform.position += new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move left
            if (spaceMoved >= 1f && spaceMoved < 2f)
            {
                transform.position -= new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //move left
            if (spaceMoved >= 2f && spaceMoved < spaceToMove)
            {
                transform.position -= new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //if moved enough, stop
            if (spaceMoved > spaceToMove)
            {
                stop();
            }
        }

        // up, up, left...movement H
        if (movingH && canMove)
        {
            //Checking for the three steps of movement

            //move up
            if (spaceMoved < 1f)
            {
                transform.position += new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move up
            if (spaceMoved >= 1f && spaceMoved < 2f)
            {
                transform.position += new Vector3(zeroSpeed, speedY, zeroSpeed);
                spaceMoved += speedY;

            }
            //move left
            if (spaceMoved >= 2f && spaceMoved < spaceToMove)
            {
                transform.position -= new Vector3(speedX, zeroSpeed, zeroSpeed);
                spaceMoved += speedX;
            }
            //if moved enough, stop
            if (spaceMoved > spaceToMove)
            {
                stop();
            }
        }

        //if we are colliding with the portal (exit) and the player is set for that position
        if (!isTargeted && canMove && !isMoving && exit.GetComponent<portalCollision>().isOpen && exit.GetComponent<portalCollision>().portalBoardX == playerBoardX && exit.GetComponent<portalCollision>().portalBoardY == playerBoardY)
        {
            //make it invisible (by effectively taking it off of the stage)
            //TODO : possible alternative would be to set it's opacity to zero. This may be easier to work with.
            makeInvisible();
            //make it so that the game text changes to the win message

            //make it so that the game text changes to the win message
            counter.GetComponent<counterScript>().hasWon = true;

        }




    }


    public void makeInvisible()
    {
        //make the player invisible
        transform.position = new Vector3(transform.position.x, transform.position.y, invisibleLayer);
        
        //win/lose conditions for displaying the counter message
        if (!isTargeted)
        {
            //make it so that the game text changes to the win message
            counter.GetComponent<counterScript>().hasWon = true;
            counter.GetComponent<counterScript>().hasLost = false;
        }
        else if (isTargeted)
        {
            //make it so that the game text changes to the win message
            counter.GetComponent<counterScript>().hasLost = true;
            counter.GetComponent<counterScript>().hasWon = false;


            //emit the particles!!!
            //deathParticles.Play();

        }

    }

    public void addPosititionToHistory()
    {
       
    }

    public void makeVisible()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y, visibleLayer);
    }

    //These functions allow other objects to call playerMovements
    //see document A for more details on movements A - H

    //up, up, right
    public void movementA()
    {

        //making sure it's not moving at beginning of movement
        if (!isMoving && canMove && !isTargeted)
        {
            isMoving = true;
            
            //moving the playerboard position
            playerBoardX += 1f;
            playerBoardY -= 2f;
            movingA = true;

            //adding the current position to the history
            addPosititionToHistory();


        }
    }

    //up, right, right
    public void movementB()
    {

        //making sure it's not moving at beginning of movement
        if (!isMoving && canMove && !isTargeted)
        {
            isMoving = true;

            //moving the playerboard position
            playerBoardX += 2f;
            playerBoardY -= 1f;
            movingB = true;

            //adding the current position to the history
            addPosititionToHistory();

        }
    }

    //down, right, riht
    public void movementC()
    {

        //making sure it's not moving at beginning of movement
        if (!isMoving && canMove && !isTargeted)
        {
            isMoving = true;

            //moving the playerboard position
            playerBoardX += 2f;
            playerBoardY += 1f;
            movingC = true;

            //adding the current position to the history
            addPosititionToHistory();
        }
    }

    //down, down, right
    public void movementD()
    {

        //making sure it's not moving at beginning of movement
        if (!isMoving && canMove && !isTargeted)
        {
            isMoving = true;

            //moving the playerboard position
            playerBoardX += 1f;
            playerBoardY += 2f;
            movingD = true;

            //adding the current position to the history
            addPosititionToHistory();
        }
    }

    //down, down, left
    public void movementE()
    {

        //making sure it's not moving at beginning of movement
        if (!isMoving && canMove && !isTargeted)
        {
            isMoving = true;

            //moving the playerboard position
            playerBoardX -= 1f;
            playerBoardY += 2f;
            movingE = true;

            //adding the current position to the history
            addPosititionToHistory();
        }
    }

    //down, left, left
    public void movementF()
    {

        //making sure it's not moving at beginning of movement
        if (!isMoving && canMove && !isTargeted)
        {
            isMoving = true;

            //moving the playerboard position
            playerBoardX -= 2f;
            playerBoardY += 1f;
            movingF = true;

            //adding the current position to the history
            addPosititionToHistory();
        }
    }

    //up, left, left
    public void movementG()
    {

        //making sure it's not moving at beginning of movement
        if (!isMoving && canMove && !isTargeted)
        {
            isMoving = true;

            //moving the playerboard position
            playerBoardX -= 2f;
            playerBoardY -= 1f;
            movingG = true;

            //adding the current position to the history
            addPosititionToHistory();
        }
    }

    //up, up, left
    public void movementH()
    {

        //making sure it's not moving at beginning of movement
        if (!isMoving && canMove && !isTargeted)
        {
            isMoving = true;

            //moving the playerboard position
            playerBoardX -= 1f;
            playerBoardY -= 2f;
            movingH = true;

            //adding the current position to the history
            addPosititionToHistory();
        }
    }


    //moving a single step...
    public void moveLeft()
    {
        //code for moving left one space
        movingRight = false;
        movingLeft = true;
        movingUp = false;
        movingDown = false;

        isMoving = true;
        /*
        if (playerBoardX != null && playerBoardX >= 0)
        {
            playerBoardX -= 1;   
        }
        */
    
    }

    public void moveRight()
    {
        //code for moving right one space
        movingRight = true;
        movingLeft = false;
        movingUp = false;
        movingDown = false;

        isMoving = true;

        /*
        if (playerBoardX != null && playerBoardX >= 0)
        {
            playerBoardX += 1;
        }
        */
    }

    public void moveUp()
    {
        //code for moving up one space
        movingRight = false;
        movingLeft = false;
        movingUp = true;
        movingDown = false;

        isMoving = true;
        /*
        if (playerBoardY != null && playerBoardX >= 0)
        {
            playerBoardY -= 1;
        }
        */
    }

    public void moveDown()
    {
        //code for moving down one space
        movingRight = false;
        movingLeft = false;
        movingUp = false;
        movingDown = true;

        isMoving = true;
        /*
        if (playerBoardY != null && playerBoardX >= 0)
        {
            playerBoardY += 1;
        }
        */
    }

    //stops all movement
    public void stop()
    {

        movingRight = false;
        movingLeft = false;
        movingUp = false;
        movingDown = false;
        
        isMoving = false;

        movingA = false;
        movingB = false;
        movingC = false;
        movingD = false;
        movingE = false;
        movingF = false;
        movingG = false;
        movingH = false;

        spaceMoved = 0;

        knightHistoryX.Add(playerBoardX);
        knightHistoryY.Add(playerBoardY);

        counter.GetComponent<counterScript>().playerHistoryX.Add(playerBoardX);
        counter.GetComponent<counterScript>().playerHistoryY.Add(playerBoardY);

    }

    //if it collides with any object
    public void OnTriggerEnter2D(Collider2D target)
    {
        //if we are colliding with the portal (exit) and the player is set for that position
        if (target.gameObject.tag.Equals("Exit") && canMove && !isMoving && target.gameObject.GetComponent<portalCollision>().isOpen && target.gameObject.GetComponent<portalCollision>().portalBoardX == playerBoardX && target.gameObject.GetComponent<portalCollision>().portalBoardY == playerBoardY)
        {
            //make it invisible (by effectively taking it off of the stage)
            //TODO : possible alternative would be to set it's opacity to zero. This may be easier to work with.
            transform.position = new Vector3(startX, startY, invisibleLayer);
        }

    }

    //resets the position of the player to whatever the level designer specified it as in the beginning.
    public void putBackToStartPosition()
    {
        //setting back to original position as gotten from the start method
        transform.position = new Vector3(startX, startY, visibleLayer);
        canMove = true;

    }


    public void playerReset()
    {

        // setting gameBoard to parent object
        transform.parent = gameBoard.transform;


        enemiesOnBoard = new List<GameObject>();

        attackID = 0;
        peopleAttacking = 0;

        //obstantiating and adding the knights initial location in the player history in counter
        counter.GetComponent<counterScript>().playerHistoryX = new List<float>();
        counter.GetComponent<counterScript>().playerHistoryY = new List<float>();

        counter.GetComponent<counterScript>().playerHistoryX.Add(playerBoardX);
        counter.GetComponent<counterScript>().playerHistoryY.Add(playerBoardY);

        //pause the particles upon start
        //deathParticles.Pause();

        //assume no one is attacking player yet
        someoneElseAttacking = false;

        //setting locked on to false
        isTargeted = false;

        //setting our start position
        startX = transform.position.x;
        startY = transform.position.y;

        //placeholder for 0
        zeroSpeed = 0f;

        //for controlling space moved for each board space
        spaceToMove = 2.9f;
        spaceMoved = 0f;

        //setting all movements to false at start
        movingA = false;
        movingB = false;
        movingC = false;
        movingD = false;
        movingE = false;
        movingF = false;
        movingG = false;
        movingH = false;

        movingRight = false;
        movingLeft = false;
        movingUp = false;
        movingDown = false;

        isMoving = false;

        //setting visible/invisiblelayers
        invisibleLayer = 100f;
        visibleLayer = -3f;

        //makeing sure it's visible
        makeVisible();

        //make sure you can have movement
        canMove = true;

    }




}


//SCRAPPED REFERENCES:

//scrapped reference #1
//transform.position += new Vector3(speedX, speedY, speedZ);
/*
//testing moving right
if (movingRight)
{
    //moving
    transform.position += new Vector3(speedX, zeroSpeed, zeroSpeed);
    spaceMoved += speedX;

    //if moved enough, stop
    if (spaceMoved >= spaceToMove)
    {
        stop();
    }
}
//testing moving left
if (movingLeft)
{
    //moving
    transform.position -= new Vector3(speedX, zeroSpeed, zeroSpeed);
    spaceMoved += speedX;

    //if moved enough, stop
    if (spaceMoved >= spaceToMove)
    {
        stop();
    }
}
//testing moving up
if (movingUp)
{
    //moving
    transform.position -= new Vector3(zeroSpeed, speedY, zeroSpeed);
    spaceMoved += speedY;

    //if moved enough, stop
    if (spaceMoved >= spaceToMove)
    {
        stop();
    }
}

if (movingDown)
{
    //moving
    transform.position += new Vector3(zeroSpeed, speedY, zeroSpeed);
    spaceMoved += speedY;

    //if moved enough, stop
    if (spaceMoved >= spaceToMove)
    {
        stop();   
    }

}
*/
//scrapped reference #2

