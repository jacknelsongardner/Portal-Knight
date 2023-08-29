using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class rookLogic : MonoBehaviour
{
    // for the gameboard
    public GameObject gameBoard;
    
    //public static bool hasRestarted;
    //public static List<GameObject> otherEnemies = new List<GameObject>();
    //public List<GameObject> otherEnemiesCopy;

    public float speedFactor;

    // debugging variables
    public bool attackIdZero;
    public bool playerIsNotMoving;
    public bool queenIsInRange;
    public bool bishopIsInRange;
    public bool rookIsInRange;
    public bool onPlayerBoardSpace;
    public bool playerIsNotAlreadyTargetted;
    public bool otherEnemyNotInWay;
    public bool notPlayerSpace;
    public bool obstacleInTheWay;

    //for making a random ID for this piece between 1000 and 9999
    public int myID;
    public static List<int> listID = new List<int>();

    public static int attackID;
    public int nonStaticAttackID;

    private int maxIdValue;
    private int minIdValue;

    public System.Random randomID;

    //info about the object we're trying to kill
    public GameObject playerPiece;
    public float tempPlayerBoardX;
    public float tempPlayerBoardY;

    //front/back layers (in front of and behind the player)
    public float frontLayer;
    public float backLayer;

    //to make invisible
    public float invisibilityLayer;

    //info about board position
    public float enemyBoardX;
    public float enemyBoardY;

    //info for whether enemy is bishop, knight, etc
    public string enemyType;

    //whether rook/bishop/queen is attacking
    public bool attacking;

    //stats for 
    public float speedY;
    public float speedX;
    public float zeroSpeed;

    //four directions to move rook (or queen)
    public bool movingLeft;
    public bool movingRight;
    public bool movingUp;
    public bool movingDown;

    //four directions to move bishop (or queen)
    public bool movingUpRight;
    public bool movingUpLeft;
    public bool movingDownRight;
    public bool movingDownLeft;

    //eight moves to move knight

    //space to move one space
    public float spaceToMove;
    public float spaceMoved;

    //if it's dead or not
    public bool isDead;



    // Start is called before the first frame update
    void Start()
    {

        // setting gameBoard to parent object
        transform.parent = gameBoard.transform;

        

        speedFactor = 8;

        // adding self to the static list of enemies
        playerPiece.GetComponent<playerMovement>().enemiesOnBoard.Add(this.gameObject);

        //setting debug values to false
        attackIdZero = false;
        playerIsNotMoving = false;
        rookIsInRange = false;
        onPlayerBoardSpace = false;
        playerIsNotAlreadyTargetted = false;
        otherEnemyNotInWay = false;
        notPlayerSpace = false;

        minIdValue = 1000;
        maxIdValue = 9999;

        //generate this enemies ID
        generateID();

        //setting visibility layers at start
        invisibilityLayer = 100f;
        frontLayer = -4f;
        backLayer = -2f;

        //making visible at start

        zeroSpeed = 0f;

        spaceToMove = 1f;
        spaceMoved = 0f;

        //making sure its not dead
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        //calculateSpeed();

        //resetting the temp variables
        tempPlayerBoardX = playerPiece.GetComponent<playerMovement>().playerBoardX;
        tempPlayerBoardY = playerPiece.GetComponent<playerMovement>().playerBoardY;

        /*
        //test for attack...
        if (!movingRight && !movingUp && !movingDown && !movingLeft && !isDead)
        {
            testAttack();
        }
        */

        if (!attacking)
        {
            testAttack();
        }

        //testAttack();

        //ROOK MOVES (OR QUEEN)
        //if moving right
        if (movingRight && !isDead)
        {
            transform.position += new Vector3(speedX, zeroSpeed, zeroSpeed);
            spaceMoved += speedX;

            //if it's moved one full space...
            if (spaceMoved >= spaceToMove)
            {
                spaceMoved = 0f;
                enemyBoardX += 1f;
            }
        }
        else
        {
            movingRight = false;
        }

        //if moving left
        if (movingLeft && !isDead)
        {
            transform.position -= new Vector3(speedX, zeroSpeed, zeroSpeed);
            spaceMoved += speedX;

            //if it's moved one full space...
            if (spaceMoved >= spaceToMove)
            {
                spaceMoved = 0f;
                enemyBoardX -= 1f;
            }
        }
        else
        {
            movingLeft = false;
        }

        //if moving up
        if (movingUp && !isDead)
        {
            transform.position += new Vector3(zeroSpeed, speedY, zeroSpeed);
            spaceMoved += speedY;

            //if it's moved one full space...
            if (spaceMoved >= spaceToMove)
            {
                spaceMoved = 0f;
                enemyBoardY -= 1f;
            }
        }
        else
        {
            movingUp = false;
        }

        //if moving down
        if (movingDown && !isDead)
        {
            transform.position -= new Vector3(zeroSpeed, speedY, zeroSpeed);
            spaceMoved += speedY;

            //if it's moved one full space...
            if (spaceMoved >= spaceToMove)
            {
                spaceMoved = 0f;
                enemyBoardY += 1f;
            }
        }
        else
        {
            movingDown = false;
        }

        //BISHOP MOVES (or queen)
        //if moving up right
        if (movingUpRight && !isDead)
        {
            transform.position += new Vector3(speedX, speedY, zeroSpeed);
            spaceMoved += speedY;

            //if it's moved one full space...
            if (spaceMoved >= spaceToMove)
            {
                spaceMoved = 0f;
                enemyBoardY -= 1f;
                enemyBoardX += 1f;
            }
        }
        else
        {
            movingUpRight = false;
        }

        //if moving up left
        if (movingUpLeft && !isDead)
        {
            transform.position += new Vector3(-speedX, speedY, zeroSpeed);
            spaceMoved += speedY;

            //if it's moved one full space...
            if (spaceMoved >= spaceToMove)
            {
                spaceMoved = 0f;
                enemyBoardY -= 1f;
                enemyBoardX -= 1f;
            }
        }
        else
        {
            movingUpLeft = false;
        }

        //if moving down right
        if (movingDownRight && !isDead)
        {
            transform.position += new Vector3(speedX, -speedY, zeroSpeed);
            spaceMoved += speedY;

            //if it's moved one full space...
            if (spaceMoved >= spaceToMove)
            {
                spaceMoved = 0f;
                enemyBoardY += 1f;
                enemyBoardX += 1f;
            }
        }
        else
        {
            movingDownRight = false;
        }

        //if moving down left
        if (movingDownLeft && !isDead)
        {
            //moving diagonal
            transform.position += new Vector3(-speedX, -speedY, zeroSpeed);
            spaceMoved += speedY;

            //if it's moved one full space...set the board x and y
            if (spaceMoved >= spaceToMove)
            {
                spaceMoved = 0f;
                enemyBoardY += 1f;
                enemyBoardX -= 1f;
            }
        }
        else
        {
            movingDownLeft = false;
        }

        //testing if enemy is on the same space as the player AND the enemy is attacking the player
        if (onPlayerSpace() && attacking == true && playerNotMoving()/*!playerPiece.GetComponent<playerMovement>().isMoving && playerPiece.GetComponent<playerMovement>().isTargeted && !playerPiece.GetComponent<playerMovement>().canMove*/)
        {
            //stopping the enemy in place
            stop();
            //making player peice invisible (dissappearing it)
            playerPiece.GetComponent<playerMovement>().makeInvisible();

            //restart the scene
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //testing if enemy is one the same space as the player AND the player is the one attacking
        else if (onPlayerSpace() && attacking == false && playerNotMoving()/*!playerPiece.GetComponent<playerMovement>().isMoving && playerPiece.GetComponent<playerMovement>().canMove*/)
        {
            //make self invisible
            makeInvisible();

            //take self off the board
            enemyBoardX = 0f;
            enemyBoardY = 0f;

            //make it so player is not targetted
            playerPiece.GetComponent<playerMovement>().isTargeted = false;

            //make it so this peice is dead
            isDead = true;
        }

        /*
        if (myID != attackID)
        {
            attacking = false;
            stop();
        }
        */
        /*
        // testing if the player is in it's invisibility layer or not. If so, stop!
        if (playerPiece.GetComponent<playerMovement>().playerBoardX == enemyBoardX && playerPiece.GetComponent<playerMovement>().playerBoardY == enemyBoardY)
        {
            stop();
        }
        */
    }

    //for managing visiblility (by changing to a layer not visible to the player)
    public void makeInvisible()
    {
        // make sure the player is not set as being attacked by someone
        playerPiece.GetComponent<playerMovement>().someoneElseAttacking = false;
        playerPiece.GetComponent<playerMovement>().peopleAttacking = 0;

        // get this piece off the board
        transform.position = new Vector3(transform.position.x, transform.position.y, invisibilityLayer);

    }

    public void bringToFrontLayer()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y, frontLayer);
    }

    public void bringToBackLayer()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, backLayer);

    }

    //stop all movement of this peice
    public void stop()
    {
        //stopping rook movements (or queen)
        movingLeft = false;
        movingRight = false;
        movingUp = false;
        movingDown = false;

        //stopping bishop movements (or queen)
        movingUpRight = false;
        movingDownRight = false;
        movingUpLeft = false;
        movingDownLeft = false;
    }

    //OBSOLETE CODE : do not use. Destroy whenever able
    public void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag.Equals("Player"))
        {
            stop();
        }
    }

    public void finalizeAttack()
    {
        // setting someone attacking to true(so that more than one peice does not attack at once)
        playerPiece.GetComponent<playerMovement>().someoneElseAttacking = true;
        playerPiece.GetComponent<playerMovement>().peopleAttacking++;
        playerPiece.GetComponent<playerMovement>().isTargeted = true;

        // make it so knight can't move
        //playerPiece.GetComponent<playerMovement>().canMove = false;

        // make it so playerKnight has ID of this enemy
        playerPiece.GetComponent<playerMovement>().attackID = myID;

        // enabling enemy to attack
        attacking = true;
    }

    //testing for if this peice can attack
    public void testAttack()
    {

        //testing for if it's a rook
        if (enemyType.Equals("rook") || enemyType.Equals("r") || enemyType.Equals("Rook") /*|| enemyType.Equals("queen") || enemyType.Equals("q") || enemyType.Equals("Queen")*/)
        {
            if (rookInRange() && otherEnemyNotInTheWay() && playerNotAlreadyTargetted() && !onPlayerSpace() && playerNotMoving() && attackIdIsZero())
            {
                // determine what direction we need to attack
                determineRookDirection();
            }

        }

        //testing for if it's a bishop
        if (enemyType.Equals("bishop") || enemyType.Equals("b") || enemyType.Equals("Bishop") /*|| enemyType.Equals("queen") || enemyType.Equals("q") || enemyType.Equals("Queen")*/)
        {
            //testing for if player is in range (bishop or queen)
            if (bishopInRange() && otherEnemyNotInTheWay() && playerNotAlreadyTargetted() && !onPlayerSpace() && playerNotMoving() && attackIdIsZero())
            {
                determineBishopDirection();
            }
        }

        //testing for if it's a queen
        if (enemyType.Equals("queen") || enemyType.Equals("q") || enemyType.Equals("Queen"))
        {
            if (queenInRange() && otherEnemyNotInTheWay() && playerNotAlreadyTargetted() && !onPlayerSpace() && playerNotMoving() && attackIdIsZero())
            {
                determineQueenDirection();
            }
        }
    }
    /*
    attackIdZero = false;
        playerIsNotMoving = false;
        rookIsInRange = false;
        onPlayerBoardSpace = false;
        playerIsNotAlreadyTargetted = false;
        otherEnemyNotInWay = false;
        notPlayerSpace = false;
    */

    bool playerNotAlreadyTargetted()
    {
        if (playerPiece.GetComponent<playerMovement>().someoneElseAttacking == false /*&& playerPiece.GetComponent<playerMovement>().peopleAttacking < 1*/)
        {
            playerIsNotAlreadyTargetted = true;
            return true;
        }
        else
        {
            playerIsNotAlreadyTargetted = false;
            return false;
        }
    }

    bool onPlayerSpace()
    {
        if (tempPlayerBoardX == enemyBoardX && tempPlayerBoardY == enemyBoardY)
        {
            onPlayerBoardSpace = true;
            return true;
        }
        else
        {
            onPlayerBoardSpace = false;
            return false;
        }
    }
    bool notOnPlayerSpace()
    {
        if (tempPlayerBoardX != enemyBoardX || tempPlayerBoardY != enemyBoardX)
        {
            notPlayerSpace = true;
            return true;
        }
        else
        {
            notPlayerSpace = false;
            return false;
        }

    }

    // checkers for if an obstacle is in the way of attacking the player
    // for rook and queen
    bool checkObstacleInTheWayDown()
    {
        foreach (GameObject indexEnemy in playerPiece.GetComponent<playerMovement>().enemiesOnBoard)
        {
            float indexBoardY = indexEnemy.GetComponent<rookLogic>().enemyBoardY;
            float indexBoardX = indexEnemy.GetComponent<rookLogic>().enemyBoardX;

            if (enemyBoardY < indexBoardY && enemyBoardX == indexBoardX && indexBoardY < tempPlayerBoardY && enemyBoardX == tempPlayerBoardX)
            {
                obstacleInTheWay = true;
                return true;
            }
            else
            {
                // continue iterating, don't do anything else
            }
        }

        obstacleInTheWay = false;
        return false;
    }

    bool checkObstacleInTheWayUp()
    {
        foreach (GameObject indexEnemy in playerPiece.GetComponent<playerMovement>().enemiesOnBoard)
        {
            float indexBoardY = indexEnemy.GetComponent<rookLogic>().enemyBoardY;
            float indexBoardX = indexEnemy.GetComponent<rookLogic>().enemyBoardX;

            if (enemyBoardY > indexBoardY && enemyBoardX == indexBoardX && indexBoardY > tempPlayerBoardY && enemyBoardX == tempPlayerBoardX)
            {
                obstacleInTheWay = true;
                return true;
            }
            else
            {
                // continue iterating, don't do anything else
            }
        }

        obstacleInTheWay = false;
        return false;
    }

    bool checkObstacleInTheWayLeft()
    {
        foreach (GameObject indexEnemy in playerPiece.GetComponent<playerMovement>().enemiesOnBoard)
        {
            float indexBoardY = indexEnemy.GetComponent<rookLogic>().enemyBoardY;
            float indexBoardX = indexEnemy.GetComponent<rookLogic>().enemyBoardX;

            if (enemyBoardX > indexBoardX && enemyBoardY == indexBoardY && indexBoardX > tempPlayerBoardX && enemyBoardY == tempPlayerBoardY)
            {
                obstacleInTheWay = true;
                return true;
            }
            else
            {
                // continue iterating, don't do anything else
            }
        }

        obstacleInTheWay = false;
        return false;
    }

    bool checkObstacleInTheWayRight()
    {
        foreach (GameObject indexEnemy in playerPiece.GetComponent<playerMovement>().enemiesOnBoard)
        {
            float indexBoardY = indexEnemy.GetComponent<rookLogic>().enemyBoardY;
            float indexBoardX = indexEnemy.GetComponent<rookLogic>().enemyBoardX;

            if (enemyBoardX < indexBoardX && enemyBoardY == indexBoardY && indexBoardX < tempPlayerBoardX && enemyBoardY == tempPlayerBoardY)
            {
                obstacleInTheWay = true;
                return true;
            }
            else
            {
                // continue iterating, don't do anything else
            }
        }

        obstacleInTheWay = false;
        return false;
    }

    // for bishop and queen
    bool checkObstacleInTheWayDownRight()
    {
        foreach (GameObject indexEnemy in playerPiece.GetComponent<playerMovement>().enemiesOnBoard)
        {
            float indexBoardY = indexEnemy.GetComponent<rookLogic>().enemyBoardY;
            float indexBoardX = indexEnemy.GetComponent<rookLogic>().enemyBoardX;

            if (indexBoardX > enemyBoardX && indexBoardY > enemyBoardY && tempPlayerBoardX > indexBoardX && tempPlayerBoardY > indexBoardY && indexBoardX - enemyBoardX == indexBoardY - enemyBoardY)
            {
                obstacleInTheWay = true;
                return true;
            }
            else
            {
                // continue iterating, don't do anything else
            }
        }

        obstacleInTheWay = false;
        return false;
    }

    bool checkObstacleInTheWayDownLeft()
    {
        foreach (GameObject indexEnemy in playerPiece.GetComponent<playerMovement>().enemiesOnBoard)
        {
            float indexBoardY = indexEnemy.GetComponent<rookLogic>().enemyBoardY;
            float indexBoardX = indexEnemy.GetComponent<rookLogic>().enemyBoardX;

            if (indexBoardX < enemyBoardX && indexBoardY > enemyBoardY && tempPlayerBoardX < indexBoardX && tempPlayerBoardY > indexBoardY && indexBoardX - enemyBoardX == -(indexBoardY - enemyBoardY))
            {
                obstacleInTheWay = true;
                return true;
            }
            else
            {
                // continue iterating, don't do anything else
            }
        }

        obstacleInTheWay = false;
        return false;
    }

    bool checkObstacleInTheWayUpLeft()
    {
        foreach (GameObject indexEnemy in playerPiece.GetComponent<playerMovement>().enemiesOnBoard)
        {
            float indexBoardY = indexEnemy.GetComponent<rookLogic>().enemyBoardY;
            float indexBoardX = indexEnemy.GetComponent<rookLogic>().enemyBoardX;

            if (indexBoardX < enemyBoardX && indexBoardY < enemyBoardY && tempPlayerBoardX < indexBoardX && tempPlayerBoardY < indexBoardY && indexBoardX - enemyBoardX == indexBoardY - enemyBoardY)
            {
                obstacleInTheWay = true;
                return true;
            }
            else
            {
                // continue iterating, don't do anything else
            }
        }

        obstacleInTheWay = false;
        return false;
    }

    bool checkObstacleInTheWayUpRight()
    {
        foreach (GameObject indexEnemy in playerPiece.GetComponent<playerMovement>().enemiesOnBoard)
        {
            float indexBoardY = indexEnemy.GetComponent<rookLogic>().enemyBoardY;
            float indexBoardX = indexEnemy.GetComponent<rookLogic>().enemyBoardX;

            if (indexBoardX > enemyBoardX && indexBoardY < enemyBoardY && tempPlayerBoardX > indexBoardX && tempPlayerBoardY < indexBoardY && indexBoardX - enemyBoardX == -(indexBoardY - enemyBoardY))
            {
                obstacleInTheWay = true;
                return true;
            }
            else
            {
                // continue iterating, don't do anything else
            }
        }

        obstacleInTheWay = false;
        return false;
    }






    bool otherEnemyNotInTheWay()
    {
        otherEnemyNotInWay = true;
        // more on this later :/
        return true;
    }


    // checkers for if enemy is in range of player
    bool rookInRange()
    {
        // checking up, down, sideways
        if (tempPlayerBoardX == enemyBoardX && !isDead || tempPlayerBoardY == enemyBoardY && !isDead)
        {
            rookIsInRange = true;
            return true;
        }
        else
        {
            rookIsInRange = false;
            return false;
        }
    }

    bool bishopInRange()
    {
        // checking diagonal up left and down right
        if (enemyBoardX - tempPlayerBoardX == enemyBoardY - tempPlayerBoardY && !isDead)
        {
            bishopIsInRange = true;
            return true;
        }
        // checking diagonal up right and down left
        else if (enemyBoardX - tempPlayerBoardX == -(enemyBoardY - tempPlayerBoardY) && !isDead)
        {
            bishopIsInRange = true;
            return true;
        }
        else
        {
            bishopIsInRange = false;
            return false;
        }
    }

    bool queenInRange()
    {
        // checking up, down, sideways
        if (tempPlayerBoardX == enemyBoardX && !isDead || tempPlayerBoardY == enemyBoardY && !isDead)
        {
            queenIsInRange = true;
            return true;
        }
        // checking diagonal up left and down right
        else if (enemyBoardX - tempPlayerBoardX == enemyBoardY - tempPlayerBoardY && !isDead)
        {
            queenIsInRange = true;
            return true;
        }
        // checking diagonal up right and down left
        else if (enemyBoardX - tempPlayerBoardX == -(enemyBoardY - tempPlayerBoardY) && !isDead)
        {
            queenIsInRange = true;
            return true;
        }
        else
        {
            queenIsInRange = false;
            return false;
        }
    }




    bool playerNotMoving()
    {
        if (playerPiece.GetComponent<playerMovement>().isMoving == false)
        {
            playerIsNotMoving = true;
            return true;
        }
        else
        {
            playerIsNotMoving = false;
            return false;
        }
    }

    bool attackIdIsZero()
    {
        if (playerPiece.GetComponent<playerMovement>().attackID == 0)
        {
            attackIdZero = true;
            return true;
        }
        else
        {
            attackIdZero = false;
            return false;
        }
    }


    bool bishopCanAttack()
    {
        return true;
    }

    bool queenCanAttack()
    {
        return true;
    }


    void determineRookDirection()
    {
        //if player is to the left
        if (tempPlayerBoardX < enemyBoardX && tempPlayerBoardY == enemyBoardY)
        {
            if (!checkObstacleInTheWayLeft())
            {
                //move to front of layers
                transform.position = new Vector3(transform.position.x, transform.position.y, -4f);

                //move left
                movingLeft = true;
                movingRight = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //if player is to the right
        else if (tempPlayerBoardX > enemyBoardX && tempPlayerBoardY == enemyBoardY)
        {
            if (!checkObstacleInTheWayRight())
            {
                //move to front of layers
                transform.position = new Vector3(transform.position.x, transform.position.y, -4f);

                //move right
                movingRight = true;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //if player is to the down
        else if (tempPlayerBoardY > enemyBoardY && tempPlayerBoardX == enemyBoardX)
        {
            if (!checkObstacleInTheWayDown())
            {
                //move to front of layers
                transform.position = new Vector3(transform.position.x, transform.position.y, -4f);

                //move down
                movingDown = true;
                movingUp = false;
                movingRight = false;
                movingLeft = false;

                finalizeAttack();
            }

        }
        //if player is to the up
        else if (tempPlayerBoardY < enemyBoardY && tempPlayerBoardX == enemyBoardX)
        {
            if (!checkObstacleInTheWayUp())
            {
                //move to front of layers 
                transform.position = new Vector3(transform.position.x, transform.position.y, -4f);

                //move up
                movingUp = true;
                movingDown = false;
                movingLeft = false;
                movingRight = false;

                finalizeAttack();
            }

        }
    }

    void determineBishopDirection()
    {
        //testing up right
        if (tempPlayerBoardY < enemyBoardY && tempPlayerBoardX > enemyBoardX)
        {
            if (!checkObstacleInTheWayUpRight())
            {
                movingUpRight = true;

                movingDownLeft = false;
                movingDownRight = false;
                movingUpLeft = false;

                movingRight = false;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //testing up left
        else if (tempPlayerBoardY < enemyBoardY && tempPlayerBoardX < enemyBoardX)
        {
            if (!checkObstacleInTheWayUpLeft())
            {
                movingUpLeft = true;

                movingDownLeft = false;
                movingDownRight = false;
                movingUpRight = false;

                movingRight = false;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //testing down right
        else if (tempPlayerBoardY > enemyBoardY && tempPlayerBoardX > enemyBoardX)
        {
            if (!checkObstacleInTheWayDownRight())
            {
                movingDownRight = true;

                movingDownLeft = false;
                movingUpRight = false;
                movingUpLeft = false;

                movingRight = false;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //testing down left
        else if (tempPlayerBoardY > enemyBoardY && tempPlayerBoardX < enemyBoardX)
        {
            if (!checkObstacleInTheWayDownLeft())
            {
                movingDownLeft = true;

                movingDownRight = false;
                movingUpRight = false;
                movingUpLeft = false;

                movingRight = false;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
    }

    void determineQueenDirection()
    {
        //if player is to the left
        if (tempPlayerBoardX < enemyBoardX && tempPlayerBoardY == enemyBoardY)
        {
            if (!checkObstacleInTheWayLeft())
            {
                //move to front of layers
                transform.position = new Vector3(transform.position.x, transform.position.y, -4f);

                //move left
                movingLeft = true;
                movingRight = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //if player is to the right
        else if (tempPlayerBoardX > enemyBoardX && tempPlayerBoardY == enemyBoardY)
        {
            if (!checkObstacleInTheWayRight())
            {
                //move to front of layers
                transform.position = new Vector3(transform.position.x, transform.position.y, -4f);

                //move right
                movingRight = true;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //if player is to the down
        else if (tempPlayerBoardY > enemyBoardY && tempPlayerBoardX == enemyBoardX)
        {
            if (!checkObstacleInTheWayDown())
            {
                //move to front of layers
                transform.position = new Vector3(transform.position.x, transform.position.y, -4f);

                //move down
                movingDown = true;
                movingUp = false;
                movingRight = false;
                movingLeft = false;

                finalizeAttack();
            }

        }
        //if player is to the up
        else if (tempPlayerBoardY < enemyBoardY && tempPlayerBoardX == enemyBoardX)
        {
            if (!checkObstacleInTheWayUp())
            {
                //move to front of layers 
                transform.position = new Vector3(transform.position.x, transform.position.y, -4f);

                //move up
                movingUp = true;
                movingDown = false;
                movingLeft = false;
                movingRight = false;

                finalizeAttack();
            }

        }
        // if player is up right
        else if (tempPlayerBoardY < enemyBoardY && tempPlayerBoardX > enemyBoardX)
        {
            if (!checkObstacleInTheWayUpRight())
            {
                movingUpRight = true;

                movingDownLeft = false;
                movingDownRight = false;
                movingUpLeft = false;

                movingRight = false;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //testing up left
        else if (tempPlayerBoardY < enemyBoardY && tempPlayerBoardX < enemyBoardX)
        {
            if (!checkObstacleInTheWayUpLeft())
            {
                movingUpLeft = true;

                movingDownLeft = false;
                movingDownRight = false;
                movingUpRight = false;

                movingRight = false;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //testing down right
        else if (tempPlayerBoardY > enemyBoardY && tempPlayerBoardX > enemyBoardX)
        {
            if (!checkObstacleInTheWayDownRight())
            {
                movingDownRight = true;

                movingDownLeft = false;
                movingUpRight = false;
                movingUpLeft = false;

                movingRight = false;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
        //testing down left
        else if (tempPlayerBoardY > enemyBoardY && tempPlayerBoardX < enemyBoardX)
        {
            if (!checkObstacleInTheWayDownLeft())
            {
                movingDownLeft = true;

                movingDownRight = false;
                movingUpRight = false;
                movingUpLeft = false;

                movingRight = false;
                movingLeft = false;
                movingUp = false;
                movingDown = false;

                finalizeAttack();
            }
        }
    }

    void generateID()
    {
        randomID = new System.Random();
        myID = randomID.Next(minIdValue, maxIdValue);

        if (listID.Contains(myID))
        {
            generateID();
        }
        else
        {
            listID.Add(myID);
        }
    }

    float calculateDistance(float xOne, float yOne, float xTwo, float yTwo)
    {
        float xSquared = (xTwo - xOne) * (xTwo - xOne);
        float ySquared = (yTwo - yOne) * (yTwo - yOne);

        return (float)Math.Sqrt(xSquared + ySquared);
    }

    void calculateSpeed()
    {
        float speed = calculateDistance(tempPlayerBoardX, tempPlayerBoardY, enemyBoardX, enemyBoardY)/speedFactor;
        speedX = speed;
        speedY = speed;
    }

    public void destroyClone()
    {
        Destroy(gameObject);
    }
}
