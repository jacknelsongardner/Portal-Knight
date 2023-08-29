using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class tileSquare : MonoBehaviour
{
    // tile x and y
    public int boardX;
    public int boardY;

    // objects this tile can assign to clones
    public GameObject counter;
    public GameObject hintButton;

    public GameObject gameBoard;

    // objects this tile can clone
    public GameObject blackRookInstance;
    public GameObject blackBishopInstance;
    public GameObject blackQueenInstance;

    public GameObject portalInstance;
    public GameObject keyInstance;

    public GameObject playerKnightInstance;

    // object this tile gets the board map from
    public GameObject boardMapObject;
    public String[,] boardMap;

    // the board peice belonging to this tile
    public GameObject boardPiece;

    public GameObject moveCounter;

    public String assignedPiece;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = gameBoard.transform;
        // setting gameBoard to parent object
        //transform.parent = gameBoard.transform;

        // getting the assigned piece from the boardMap
        boardMap = gameBoard.GetComponent<CreateBoardSetup>().boardMap;
        assignedPiece = boardMap[boardX - 1, boardY - 1];

        // getting the chess piece and setting it to tile peice
        playerKnightInstance = counter.GetComponent<counterScript>().playerKnight;
        portalInstance = counter.GetComponent<counterScript>().portal;
        keyInstance = counter.GetComponent<counterScript>().key;

        // checking if the square is rook
        if (assignedPiece == "rook")
        {
            cloneRook();
        }
        else if (assignedPiece == "bishop")
        {
            cloneBishop();
        }
        else if (assignedPiece == "queen")
        {
            cloneQueen();
        }
        else if (assignedPiece == "start")
        {
            clonePlayer();
        }
        else if (assignedPiece == "key")
        {
            cloneKey();
        }
        else if (assignedPiece == "portal")
        {
            clonePortal();
        }
        else
        { 
            // do nothing :/
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void Awake()
    {

        transform.parent = gameBoard.transform;
        // setting gameBoard to parent object
        //transform.parent = gameBoard.transform;

        // getting the assigned piece from the boardMap
        boardMap = gameBoard.GetComponent<CreateBoardSetup>().boardMap;
        assignedPiece = boardMap[boardX - 1, boardY - 1];

        // getting the chess piece and setting it to tile peice
        playerKnightInstance = counter.GetComponent<counterScript>().playerKnight;
        portalInstance = counter.GetComponent<counterScript>().portal;
        keyInstance = counter.GetComponent<counterScript>().key;

        // checking if the square is rook
        if (assignedPiece == "rook")
        {
            cloneRook();
        }
        else if (assignedPiece == "bishop")
        {
            cloneBishop();
        }
        else if (assignedPiece == "queen")
        {
            cloneQueen();
        }
        else if (assignedPiece == "start")
        {
            clonePlayer();
        }
        else if (assignedPiece == "key")
        {
            cloneKey();
        }
        else if (assignedPiece == "portal")
        {
            clonePortal();
        }
        else
        {
            // do nothing :/
        }
    }
    */

    void cloneRook()
    {
        boardPiece = Instantiate(blackRookInstance);

        // setting the boardpeices BOARD position
        boardPiece.GetComponent<rookLogic>().enemyBoardX = boardX;
        boardPiece.GetComponent<rookLogic>().enemyBoardY = boardY;

        boardPiece.GetComponent<rookLogic>().gameBoard = this.gameBoard;

        // setting boardpeices ACTUAL position
        boardPiece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, boardPiece.transform.position.z);
        boardPiece.GetComponent<rookLogic>().playerPiece = playerKnightInstance;
    }

    void cloneBishop()
    {
        boardPiece = Instantiate(blackBishopInstance);

        // setting the boardpeices BOARD position
        boardPiece.GetComponent<rookLogic>().enemyBoardX = boardX;
        boardPiece.GetComponent<rookLogic>().enemyBoardY = boardY;

        boardPiece.GetComponent<rookLogic>().gameBoard = this.gameBoard;

        // setting boardpeices ACTUAL position
        boardPiece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, boardPiece.transform.position.z);
        boardPiece.GetComponent<rookLogic>().playerPiece = playerKnightInstance;
    }

    void cloneQueen()
    {
        boardPiece = Instantiate(blackQueenInstance);

        // setting the boardpeices BOARD position
        boardPiece.GetComponent<rookLogic>().enemyBoardX = boardX;
        boardPiece.GetComponent<rookLogic>().enemyBoardY = boardY;

        boardPiece.GetComponent<rookLogic>().gameBoard = this.gameBoard;

        // setting boardpeices ACTUAL position
        boardPiece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, boardPiece.transform.position.z);
        boardPiece.GetComponent<rookLogic>().playerPiece = playerKnightInstance;
    }

    void clonePortal()
    {
        boardPiece = portalInstance;

        // setting the boardpeices BOARD position
        boardPiece.GetComponent<portalCollision>().portalBoardX = boardX;
        boardPiece.GetComponent<portalCollision>().portalBoardY = boardY;

        boardPiece.GetComponent<portalCollision>().key = keyInstance;
        boardPiece.GetComponent<portalCollision>().counter = counter;

        //boardPiece.GetComponent<rookLogic>().gameBoard = this.gameBoard;

        // setting boardpeices ACTUAL position
        boardPiece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, boardPiece.transform.position.z);
        boardPiece.GetComponent<portalCollision>().playerPeice = playerKnightInstance;
    }

    void cloneKey()
    {
        boardPiece = keyInstance;

        // setting the boardpeices BOARD position
        boardPiece.GetComponent<keyCollision>().keyBoardX = boardX;
        boardPiece.GetComponent<keyCollision>().keyBoardY = boardY;

        //boardPiece.GetComponent<rookLogic>().gameBoard = this.gameBoard;

        // setting boardpeices ACTUAL position
        boardPiece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, boardPiece.transform.position.z);
        boardPiece.GetComponent<keyCollision>().playerPeice = playerKnightInstance;
    }

    void clonePlayer()
    {
        boardPiece = playerKnightInstance;

        // setting the boardpeices BOARD position
        boardPiece.GetComponent<playerMovement>().playerBoardX = boardX;
        boardPiece.GetComponent<playerMovement>().playerBoardY = boardY;

        boardPiece.GetComponent<playerMovement>().counter = counter;
        boardPiece.GetComponent<playerMovement>().exit = portalInstance;

        //boardPiece.GetComponent<rookLogic>().gameBoard = this.gameBoard;

        // setting boardpeices ACTUAL position
        boardPiece.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, boardPiece.transform.position.z);
    }

    void respawnPieces()
    {
        // getting the chess piece and setting it to tile peice
        playerKnightInstance = counter.GetComponent<counterScript>().playerKnight;
        portalInstance = counter.GetComponent<counterScript>().portal;
        keyInstance = counter.GetComponent<counterScript>().key;

        // checking if the square is rook
        if (assignedPiece == "rook")
        {
            cloneRook();
        }
        else if (assignedPiece == "bishop")
        {
            cloneBishop();
        }
        else if (assignedPiece == "queen")
        {
            cloneQueen();
        }
        else if (assignedPiece == "start")
        {
            clonePlayer();
        }
        else if (assignedPiece == "key")
        {
            cloneKey();
        }
        else if (assignedPiece == "portal")
        {
            clonePortal();
        }
        else
        {
            // do nothing :/
        }
    }
}
