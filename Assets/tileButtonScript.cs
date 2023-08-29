using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileButtonScript : MonoBehaviour
{

    //parent tile for this tilebutton
    public GameObject tileParent;

    //moves made counter object
    public GameObject counter;

    //the color set by the level designer for when hints are 
    public Color highlightColor;
    public Color normalColor;

    public GameObject resetButton;

    //list of the solutions x and y
    public List<float> solutionX;
    public List<float> solutionY;

    public List<float> playerHistoryX;
    public List<float> playerHistoryY;


    public int solutionIndex;


    //the x and y of this tile
    public float tileBoardX;
    public float tileBoardY;

    //player objects X and Y variables
    public float playerBoardXcopy;
    public float playerBoardYcopy;

    //for the playerObject
    public GameObject Go;


    //for the hint button
    public GameObject HintButton;


    //for determining whether or not it will be highlighted
    public bool isHintedMove;
    public bool isBackMove;

    //in case later we have multiple peices
    

    // Start is called before the first frame update
    void Start()
    {
        // setting this tiles board x and y to the parent tile
        this.tileParent = this.transform.parent.gameObject;
        this.tileBoardX = tileParent.GetComponent<tileSquare>().boardX;
        this.tileBoardY = tileParent.GetComponent<tileSquare>().boardY;

        this.Go = counter.GetComponent<counterScript>().playerKnight;

        playerBoardXcopy = Go.GetComponent<playerMovement>().playerBoardX;
        playerBoardYcopy = Go.GetComponent<playerMovement>().playerBoardY;

        highlightColor = new Color(1f, 1f, .5f, .15f);
        normalColor = new Color(1f, 1f, 1f, .15f);

        //making sure the tile is not highlighted in the beginning
        isBackMove = false;
        isHintedMove = false;

        //making sure that the playerhistory is obstantiated with the start position of the player
        if (counter.GetComponent<counterScript>().playerHistoryX.Count == 0 && counter.GetComponent<counterScript>().playerHistoryY.Count == 0)
        {
            //counter.GetComponent<counterScript>().playerHistoryX.Add(playerBoardXcopy);
            //counter.GetComponent<counterScript>().playerHistoryY.Add(playerBoardYcopy);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //syncing player history and the solution with the counter so all tiles show same solution, playerhistory, etc...
        //playerHistoryX = counter.GetComponent<counterScript>().playerHistoryX;
        //playerHistoryY = counter.GetComponent<counterScript>().playerHistoryY;

        //solutionIndex = counter.GetComponent<counterScript>().solutionIndex;
        highlightColor = new Color(1f, .77f, .5f, .25f);
        normalColor = new Color(1f, 1f, 1f, .15f);

        playerBoardXcopy = Go.GetComponent<playerMovement>().playerBoardX;
        playerBoardYcopy = Go.GetComponent<playerMovement>().playerBoardY;

        calculateHintStatus();
        

    }

    public void calculateHintStatus()
    {
        //if this is the next move the player should be making AND the player is on the right track
        if (!Go.GetComponent<playerMovement>().isMoving && counter.GetComponent<counterScript>().nextX == this.tileBoardX && counter.GetComponent<counterScript>().nextY == this.tileBoardY && !counter.GetComponent<counterScript>().hasLost && !counter.GetComponent<counterScript>().hasWon/*&& tileBoardX == counter.GetComponent<counterScript>().solutionX[counter.GetComponent<counterScript>().solutionIndex + 1] && tileBoardY == counter.GetComponent<counterScript>().solutionY[counter.GetComponent<counterScript>().solutionIndex + 1] && playerBoardXcopy == counter.GetComponent<counterScript>().solutionX[counter.GetComponent<counterScript>().solutionIndex] && playerBoardYcopy == counter.GetComponent<counterScript>().solutionY[counter.GetComponent<counterScript>().solutionIndex]*/)
        {
            isHintedMove = true;
            isBackMove = false;

            //if hints are enabled by user
            if (HintButton.GetComponent<buttonScript>().hintsEnabled == true)
            {
                this.GetComponent<UnityEngine.UI.Image>().color = highlightColor;
            }
            //if hints are disabled
            else
            {
                this.GetComponent<UnityEngine.UI.Image>().color = normalColor;
            }
        }

        //checking to make sure the player history is bigger than 1 and the player is on the WRONG PATH and the player hasn't won the level yet...
        /*
        else if (!Go.GetComponent<playerMovement>().isMoving && counter.GetComponent<counterScript>().playerHistoryX.Count > 1 && playerBoardXcopy != counter.GetComponent<counterScript>().solutionX[counter.GetComponent<counterScript>().solutionIndex] &&
        counter.GetComponent<counterScript>().hasWon == false)
        {
            //checking if the 
            if (tileBoardX == counter.GetComponent<counterScript>().playerHistoryX[counter.GetComponent<counterScript>().playerHistoryX.Count - 2] && tileBoardY == counter.GetComponent<counterScript>().playerHistoryY[counter.GetComponent<counterScript>().playerHistoryY.Count - 2])
            {
                isHintedMove = false;
                isBackMove = true;

                //if hints are enabled by user
                if (HintButton.GetComponent<buttonScript>().hintsEnabled == true)
                {
                    this.GetComponent<UnityEngine.UI.Image>().color = highlightColor;
                }
                //if hints are disabled
                else
                {
                    this.GetComponent<UnityEngine.UI.Image>().color = normalColor;
                }
            }
            else
            {
                this.GetComponent<UnityEngine.UI.Image>().color = normalColor;
            }
            
        }
        */
        /*
        //if it's not the next move the player should be making its way backward (X NOT MATCHING UP)
        else if (tileBoardX == counter.GetComponent<counterScript>().playerHistoryX[counter.GetComponent<counterScript>().playerHistoryX.Count-2] && tileBoardY == counter.GetComponent<counterScript>().playerHistoryY[counter.GetComponent<counterScript>().playerHistoryY.Count - 2] && counter.GetComponent<counterScript>().playerHistoryX[counter.GetComponent<counterScript>().playerHistoryX.Count - 1] != counter.GetComponent<counterScript>().solutionX[counter.GetComponent<counterScript>().solutionIndex])
        {
            isHintedMove = true;
            //if hints are enabled by user
            if (HintButton.GetComponent<buttonScript>().hintsEnabled == true)
            {
                this.GetComponent<UnityEngine.UI.Image>().color = highlightColor;
            }
            //if hints are disabled
            else
            {
                this.GetComponent<UnityEngine.UI.Image>().color = normalColor;
            }
        }
        //if it's not the next move the player should be making its way backward (Y NOT MATCHING UP)
        else if (tileBoardX == counter.GetComponent<counterScript>().playerHistoryX[counter.GetComponent<counterScript>().playerHistoryX.Count - 2] && tileBoardY == counter.GetComponent<counterScript>().playerHistoryY[counter.GetComponent<counterScript>().playerHistoryY.Count - 2] && counter.GetComponent<counterScript>().playerHistoryY[counter.GetComponent<counterScript>().playerHistoryY.Count - 1] != counter.GetComponent<counterScript>().solutionY[counter.GetComponent<counterScript>().solutionIndex])
        {
            isHintedMove = true;
            //if hints are enabled by user
            if (HintButton.GetComponent<buttonScript>().hintsEnabled == true)
            {
                this.GetComponent<UnityEngine.UI.Image>().color = highlightColor;
            }
            //if hints are disabled
            else
            {
                this.GetComponent<UnityEngine.UI.Image>().color = normalColor;
            }
        */
        else
        {
            isHintedMove = false;
            isBackMove = false;
            this.GetComponent<UnityEngine.UI.Image>().color = normalColor;
        }
        //
    }


    //
    public void tellPlayerMove()
    {

        //if this is the move the player should be making
        if (isHintedMove)
        {
            //if the player is on the right track...
            if (counter.GetComponent<counterScript>().playerHistoryX[counter.GetComponent<counterScript>().playerHistoryX.Count - 1] == counter.GetComponent<counterScript>().solutionX[counter.GetComponent<counterScript>().solutionIndex] && counter.GetComponent<counterScript>().playerHistoryY[counter.GetComponent<counterScript>().playerHistoryY.Count - 1] == counter.GetComponent<counterScript>().solutionY[counter.GetComponent<counterScript>().solutionIndex])
            {
                counter.GetComponent<counterScript>().solutionIndex++;
            }
            //if player is NOT on the right track and we must lead him back to the pointer position
            /*
            if (counter.GetComponent<counterScript>().playerHistoryX[counter.GetComponent<counterScript>().playerHistoryX.Count - 1] != counter.GetComponent<counterScript>().solutionX[counter.GetComponent<counterScript>().solutionIndex] || counter.GetComponent<counterScript>().playerHistoryY[counter.GetComponent<counterScript>().playerHistoryY.Count - 1] != counter.GetComponent<counterScript>().solutionY[counter.GetComponent<counterScript>().solutionIndex])
            {
                //DELETE TOP TWO ITEMS OF THE PLAYER HISTORY X AND Y
                counter.GetComponent<counterScript>().playerHistoryX.Remove(counter.GetComponent<counterScript>().playerHistoryX[counter.GetComponent<counterScript>().playerHistoryX.Count - 1]);
                counter.GetComponent<counterScript>().playerHistoryX.Remove(counter.GetComponent<counterScript>().playerHistoryX[counter.GetComponent<counterScript>().playerHistoryX.Count - 2]);

                counter.GetComponent<counterScript>().playerHistoryY.Remove(counter.GetComponent<counterScript>().playerHistoryY[counter.GetComponent<counterScript>().playerHistoryY.Count - 1]);
                counter.GetComponent<counterScript>().playerHistoryY.Remove(counter.GetComponent<counterScript>().playerHistoryY[counter.GetComponent<counterScript>().playerHistoryY.Count - 2]);

            }
            */
        }
        

        //adding playerX to the playerHistory
        //counter.GetComponent<counterScript>().playerHistoryX.Add(playerBoardXcopy);
        //counter.GetComponent<counterScript>().playerHistoryY.Add(playerBoardYcopy);


        //testing various moves (see diagram A)

        //movement A...up,up,right
        if (tileBoardX == playerBoardXcopy + 1f && tileBoardY == playerBoardYcopy - 2f)
        {
            Go.GetComponent<playerMovement>().movementA();

            //add a move to counter
            counter.GetComponent<counterScript>().addMove();


        }
        //movement B...
        if (tileBoardX == playerBoardXcopy + 2f && tileBoardY == playerBoardYcopy - 1f)
        {
            Go.GetComponent<playerMovement>().movementB();

            //add a move to counter
            counter.GetComponent<counterScript>().addMove();
        }
        //movement C
        if (tileBoardX == playerBoardXcopy + 2f && tileBoardY == playerBoardYcopy + 1f)
        {
            Go.GetComponent<playerMovement>().movementC();

            //add a move to counter
            counter.GetComponent<counterScript>().addMove();
        }
        //movement D
        if (tileBoardX == playerBoardXcopy + 1f && tileBoardY == playerBoardYcopy + 2f)
        {
            Go.GetComponent<playerMovement>().movementD();

            //add a move to counter
            counter.GetComponent<counterScript>().addMove();
        }
        //movement E
        if (tileBoardX == playerBoardXcopy - 1f && tileBoardY == playerBoardYcopy + 2f)
        {
            Go.GetComponent<playerMovement>().movementE();

            //add a move to counter
            counter.GetComponent<counterScript>().addMove();
        }
        //movement F
        if (tileBoardX == playerBoardXcopy - 2f && tileBoardY == playerBoardYcopy + 1f)
        {
            Go.GetComponent<playerMovement>().movementF();

            //add a move to counter
            counter.GetComponent<counterScript>().addMove();
        }
        //movement G
        if (tileBoardX == playerBoardXcopy - 2f && tileBoardY == playerBoardYcopy - 1f)
        {
            Go.GetComponent<playerMovement>().movementG();

            //add a move to counter
            counter.GetComponent<counterScript>().addMove();
        }
        //movement H
        if (tileBoardX == playerBoardXcopy - 1f && tileBoardY == playerBoardYcopy  - 2f)
        {
            Go.GetComponent<playerMovement>().movementH();

            //add a move to counter
            counter.GetComponent<counterScript>().addMove();
        }
        
    }

}
