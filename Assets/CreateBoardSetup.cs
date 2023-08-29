using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreateBoardSetup : MonoBehaviour
{
    public GameObject counter;

    public int boardWidth;
    public int boardHeight;

    public int howManyKeys;

    public int playerStartPosX;
    public int playerStartPosY;

    public int portalPosX;
    public int portalPosY;

    public int ammountQueensAllowed;
    public int ammountKnightsAllowed;
    public int ammountRooksAllowed;
    public int ammountBishopsAllowed;

    public List<int> solutionX = new List<int>();
    public List<int> solutionY = new List<int>();

    public List<int> keysX = new List<int>();
    public List<int> keysY = new List<int>();

    public List<int> queensX = new List<int>();
    public List<int> queensY = new List<int>();

    public List<int> rooksX = new List<int>();
    public List<int> rooksY = new List<int>();

    public List<int> bishopsX = new List<int>();
    public List<int> bishopsY = new List<int>();

    public List<int> knightsX = new List<int>();
    public List<int> knightsY = new List<int>();

    public List<String> tempList1 = new List<String>();
    public List<String> tempList2 = new List<String>();
    public List<String> tempList3 = new List<String>();
    public List<String> tempList4 = new List<String>();
    public List<String> tempList5 = new List<String>();
    public List<String> tempList6 = new List<String>();
    public List<String> tempList7 = new List<String>();
    public List<String> tempList8 = new List<String>();

    public int solutionSizeLowerBound;
    public int solutionSizeHigherBound;
    public int solutionSize;
    public int keyLocation;

    public int rookMax;
    public int bishopMax;
    public int queenMax;
    public int knightMax;

    public int rookCount;
    public int bishopCount;
    public int queenCount;
    public int knightCount;

    public int rookChance;
    public int bishopChance;
    public int queenChance;
    public int knightChance;

    private System.Random random = new System.Random();

    public String[,] boardMap;

    // Start is called before the first frame update
    void Start()
    {
        boardWidth = PlayerPrefs.GetInt("bw");
        boardHeight = PlayerPrefs.GetInt("bh");

        if (boardWidth == 0 || boardHeight == 0)
        {
            PlayerPrefs.SetInt("bw", 4);
            PlayerPrefs.SetInt("bh", 4);

            boardWidth = PlayerPrefs.GetInt("bw");
            boardHeight = PlayerPrefs.GetInt("bh");
        }

        solutionSizeHigherBound = boardHeight + boardWidth / 2;
        solutionSizeLowerBound = solutionSizeHigherBound / 4;
        
        solutionSize = random.Next(solutionSizeLowerBound, solutionSizeHigherBound);

        // setting the players default position
        playerStartPosX = random.Next(0, boardWidth-1);
        playerStartPosY = random.Next(0, boardHeight-1);

        // try to make the board map. Afterwords, check to make sure it iterated correctly
        do
        {
            // setting the board map to blank
            boardMap = new String[boardWidth, boardHeight];
            MakeAllMapItemsBlank();

            // create the solution path 
            BeginIteratingPath(playerStartPosX, playerStartPosY);
            Debug.Log("finished iterating path");

        } while (!CheckIteratedPathCorrectly());

        AddEnemiesToSolutionPath();
        Debug.Log("finished adding enemies to solution path");
        
        AddEnemiesToBoardSquares();
        Debug.Log("finished adding enemies to board squares");

        PrintBoardToConsole();
        Debug.Log("finished printing board to console");
    }

    // Update is called once per frame
    void Update()
    {
    }

    // STEP 1 : Iterating through the board creating solution. See notes for more details.

    public bool CheckIteratedPathCorrectly()
    {
        bool hasKey = false;
        bool hasPortal = false;

        for( int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                if (boardMap[x,y] == "key")
                {
                    hasKey = true;
                }

                if (boardMap[x,y] == "portal")
                {
                    hasPortal = true;
                }
            }
        }

        if (hasKey == true && hasPortal == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // here we will begin iterating through the solution path which we create out of nothing
    void BeginIteratingPath(int startX, int startY)
    {
        // how many moves there will be in the solution
        int movesLeft = solutionSize; //random.Next(boardHeight/2, boardHeight) + random.Next(boardWidth/2, boardWidth);
        keyLocation = solutionSize/2;

        solutionX.Add(startX);
        solutionY.Add(startY);

        PlaceStart(startX,startY);
        Debug.Log("placed start");

        // start iterating!
        ContinueIteratingPath(startX, startY, 0, movesLeft, keyLocation);
    }

    void ContinueIteratingPath(int lastX, int lastY, int lastMove, int movesLeft, int keyLocation)
    {
        bool needToTryAgain = true;
        int timesLooping = 0;

        //Debug.Log(lastX + ", " + lastY + ", " + movesLeft);

        while (needToTryAgain == true && timesLooping <= 1000)
        {
            timesLooping++;

            if (movesLeft >= 0)
            {
                // making next move on the board, one of eight movements
                int nextMove = random.Next(1, 8);

                int nextX = lastX;
                int nextY = lastY;

                // up 2 and left 1
                if (nextMove == 1 && lastMove != 5)
                {
                    nextX -= 1;
                    nextY += 2;
                }
                // up 2 and right 1
                else if (nextMove == 2 && lastMove != 6)
                {
                    nextX += 1;
                    nextY += 2;
                }
                // up 1 and right 2
                else if (nextMove == 3 && lastMove != 7)
                {
                    nextX += 2;
                    nextY += 1;
                }
                // down 1 and right 2
                else if (nextMove == 4 && lastMove != 8)
                {
                    nextX += 2;
                    nextY -= 1;
                }
                // down 2 and right 1
                else if (nextMove == 5 && lastMove != 1)
                {
                    nextX += 1;
                    nextY -= 2;
                }
                // down 2 and left 1
                else if (nextMove == 6 && lastMove != 2)
                {
                    nextX -= 1;
                    nextY -= 2;
                }
                // down 1 and left 2
                else if (nextMove == 7 && lastMove != 3)
                {
                    nextX -= 2;
                    nextY -= 1;
                }
                // up 1 and left 2
                else if (nextMove == 8 && lastMove != 4)
                {
                    nextX -= 2;
                    nextY += 1;
                }

                // testing to see if movement made was off the board
                if (nextY > boardHeight - 1 || nextY < 0 || nextX > boardWidth - 1 || nextX < 0 || nextX == lastX)
                {
                    // try again with last iterations stats
                    //ContinueIteratingPath(lastX, lastY, lastMove, movesLeft, keyLocation);
                    needToTryAgain = true;
                }
                else
                {
                    // testing to see if we should drop the key now
                    if (movesLeft == keyLocation)
                    {
                        // testing to see if the key is on player start position
                        if (boardMap[nextX, nextY] != "start")
                        {
                            // place the key
                            PlaceKey(nextX, nextY);

                            // record to the solution list
                            solutionX.Add(nextX);
                            solutionY.Add(nextY);

                            // continue iterating
                            ContinueIteratingPath(nextX, nextY, nextMove, movesLeft - 1, keyLocation);
                            needToTryAgain = false;
                        }
                        else
                        {
                            // try again with last iteration stats
                            //ContinueIteratingPath(lastX, lastY, lastMove, movesLeft, keyLocation);
                            Debug.Log(lastX + ", " + lastY + " | " + nextX + ", " + nextY + " | " + movesLeft);
                            needToTryAgain = true;
                        }
                    }
                    // testing to see if we should drop the portal now
                    else if (movesLeft == 0)
                    {
                        if (boardMap[nextX, nextY] != "start" && boardMap[nextX, nextY] != "key")
                        {
                            // place the portal
                            PlacePortal(nextX, nextY);

                            // record to the solution list
                            solutionX.Add(nextX);
                            solutionY.Add(nextY);
                            needToTryAgain = false;
                        }
                        /*
                        else if (boardMap[nextX,nextY] == "start" || boardMap[nextX, nextY] == "key")
                        {
                            solutionX.Add(nextX);
                            solutionY.Add(nextY);
                            ContinueIteratingPath(nextX, nextY, nextMove, movesLeft + 1, keyLocation);
                            needToTryAgain = false;
                        }
                        */
                        else
                        {
                            // try again with last iteration stats
                            //ContinueIteratingPath(lastX, lastY, lastMove, movesLeft, keyLocation);
                            needToTryAgain = true;
                        }
                    }
                    else
                    {
                        // record to the solution list
                        solutionX.Add(nextX);
                        solutionY.Add(nextY);

                        // continue iterating
                        ContinueIteratingPath(nextX, nextY, nextMove, movesLeft - 1, keyLocation);
                        needToTryAgain = false;
                    }
                }
            }
            else if (movesLeft <= 0)
            {
                PlacePortal(lastX, lastY);
                needToTryAgain = false;
            }
        }
    }

    // STEP 2 : Placing enemies randomly on the solution squares 

    void AddEnemiesToSolutionPath()
    {
        // iterating through each solution path element
        for (int index = solutionX.Count-2; index >= 1; index--)
        {
            
            // 50/50 chance place rook
            if (random.Next(0, rookChance) == 0)
            {
                bool canPlace = true;

                // iterating backwards through the solution to check and make sure it's not interfering with previous 
                for (int i = index-1; i >= 0; i--)
                {
                    // in case it puts a previous solution square in danger
                    if (CheckRookAttack(solutionX[index], solutionY[index], solutionX[i], solutionY[i]) == false)
                    {
                        canPlace = false;
                        break;
                    }
                }

                if (canPlace == true)
                {
                    PlaceRook(solutionX[index], solutionY[index]);
                }
            }

            // 50/50 chance to place bishop
            if (random.Next(0, bishopChance) == 0)
            {
                bool canPlace = true;

                // iterating through the solution to check and make sure it's not interfering with previous 
                for (int i = index-1; i >= 0; i--)
                {
                    if (CheckBishopAttack(solutionX[index], solutionY[index], solutionX[i], solutionY[i]) == true)
                    {
                        canPlace = false;
                        break;
                    }
                }

                if (canPlace == true)
                {
                    PlaceBishop(solutionX[index], solutionY[index]);
                }
            }

            // 50/50 chance to place queen
            if (random.Next(0, queenChance) == 0)
            {
                bool canPlace = true;

                // iterating through the solution to check and make sure it's not interfering with previous 
                for (int i = index-1; i >= 0; i--)
                {
                    if (CheckQueenAttack(solutionX[index], solutionY[index], solutionX[i], solutionY[i]) == true)
                    {
                        canPlace = false;
                        break;
                    }
                }

                if (canPlace == true)
                {
                    PlaceQueen(solutionX[index], solutionY[index]);
                }
            }
        }
    }

    // STEP 3 : Placing enemies randomly on the board squares

    void AddEnemiesToBoardSquares()
    {
        // iterating through each board column
        for (int x = 0; x < boardWidth; x++)
        {
            // iterating through each board row
            for (int y = 0; y < boardHeight; y++)
            {
                // 50/50 chance place rook
                if (random.Next(0, rookChance) == 0)
                {
                    bool canPlace = true;

                    // iterating through the solution to check and make sure it's not interfering with previous 
                    for (int i = 0; i < solutionX.Count; i++)
                    {
                        if (CheckRookAttack(x, y, solutionX[i], solutionY[i]) == true)
                        {
                            canPlace = false;
                        }
                    }

                    if (canPlace == true)
                    {
                        PlaceRook(x, y);
                    }
                }

                // 50/50 chance to place bishop
                if (random.Next(0, bishopChance) == 0)
                {
                    bool canPlace = true;

                    // iterating through the solution to check and make sure it's not interfering with previous 
                    for (int i = 0; i < solutionX.Count; i++)
                    {
                        if (CheckBishopAttack(x, y, solutionX[i], solutionY[i]) == true)
                        {
                            canPlace = false;
                        }
                    }

                    if (canPlace == true)
                    {
                        PlaceBishop(x, y);
                    }
                }

                // 50/50 chance to place queen
                if (random.Next(0, queenChance) == 0)
                {
                    bool canPlace = true;

                    // iterating through the solution to check and make sure it's not interfering with previous 
                    for (int i = 0; i < solutionX.Count; i++)
                    {
                        if (CheckQueenAttack(x, y, solutionX[i], solutionY[i]) == true)
                        {
                            canPlace = false;
                        }
                    }

                    if (canPlace == true)
                    {
                        PlaceQueen(x, y);
                    }
                }
            }
        }
    }

    void PlaceRook(int x, int y)
    {
        if (rookMax > rookCount)
        {
            if (boardMap[x, y] != "key" && boardMap[x, y] != "portal" && boardMap[x, y] != "start")
            {
                boardMap[x, y] = "rook";

                rooksX.Add(x);
                rooksY.Add(y);

                rookCount++;
            }
        }
    }

    void PlaceBishop(int x, int y)
    {
        if (bishopMax > bishopCount)
        {
            if (boardMap[x, y] != "key" && boardMap[x, y] != "portal" && boardMap[x, y] != "start")
            {
                boardMap[x, y] = "bishop";

                bishopsX.Add(x);
                bishopsY.Add(y);

                bishopCount++;
            }
        }
    }

    void PlaceQueen(int x, int y)
    {
        if (queenMax > queenCount)
        {
            if (boardMap[x, y] != "key" && boardMap[x, y] != "portal" && boardMap[x, y] != "start")
            {
                boardMap[x, y] = "queen";

                queensX.Add(x);
                queensY.Add(y);

                queenCount++;
            }
        }
    }

    void PlaceKnight(int x, int y)
    {
        if (knightMax > knightCount)
        {
            if (boardMap[x, y] != "key" && boardMap[x, y] != "portal" && boardMap[x, y] != "start")
            {
                boardMap[x, y] = "knight";

                knightsX.Add(x);
                knightsY.Add(y);

                knightCount++; 
            }
        }
    }

    void PlaceKey(int x, int y)
    {
        keysX.Add(x);
        keysY.Add(y);

        boardMap[x, y] = "key";
    }

    void PlaceStart(int x, int y)
    {
        playerStartPosX = x;
        playerStartPosY = y;

        boardMap[x, y] = "start";
        
    }

    void PlacePortal(int x, int y)
    {
        boardMap[x, y] = "portal";
    }

    bool CheckRookAttack(int rookX, int rookY, int posX, int posY)
    {
        if (rookX == posX)
        {
            return true;
        }
        else if (rookY == posY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckBishopAttack(int bishopX, int bishopY, int posX, int posY)
    {
        if (bishopX - posX == bishopY - posY)
        {
            return true;
        }
        else if (bishopX - posX == -(bishopY - posY))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CheckQueenAttack(int queenX, int queenY, int posX, int posY)
    {
        if (queenX == posX)
        {
            return true;
        }
        else if (queenY == posY)
        {
            return true;
        }
        else if (queenX - posX == queenY - posY)
        {
            return true;
        }
        else if (queenX - posX == -(queenY - posY))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MakeAllMapItemsBlank()
    {
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                boardMap[x, y] = "blank";
            }
        }
    }

    void PrintBoardToConsole()
    {
        for (int i = 0; i < boardHeight; i++)
        {
            tempList1.Add(boardMap[0, i]);           
        }

        for (int i = 0; i < boardHeight; i++)
        {
            tempList2.Add(boardMap[1, i]);
        }

        for (int i = 0; i < boardHeight; i++)
        {
            tempList3.Add(boardMap[2, i]);
        }

        for (int i = 0; i < boardHeight; i++)
        {
            tempList4.Add(boardMap[3, i]);
        }

        for (int i = 0; i < boardHeight; i++)
        {
            tempList5.Add(boardMap[4, i]);
        }

        for (int i = 0; i < boardHeight; i++)
        {
            tempList6.Add(boardMap[5, i]);
        }

        for (int i = 0; i < boardHeight; i++)
        {
            tempList7.Add(boardMap[6, i]);
        }

        for (int i = 0; i < boardHeight; i++)
        {
            tempList8.Add(boardMap[7, i]);
        }

    }

    public void restartBoard()
    {
        // delete all the enemies on the board
        BroadcastMessage("destroyClone", 5.0);

        // tell all the tiles that they need to respawn their peices
        BroadcastMessage("respawnPieces", 5.0);

        // tell player to reset to normal
        BroadcastMessage("playerReset", 5.0);

        // tell the counter to reset
        counter.GetComponent<counterScript>().counterReset();

        // tell key to reset to normal
        BroadcastMessage("keyReset", 5.0);

        // tell portal to reset to normal
        BroadcastMessage("portalReset", 5.0);
    }

}
