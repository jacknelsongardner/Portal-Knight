using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createBoardBySize : MonoBehaviour
{
    public int boardWidth;
    public int boardHeight;

    public GameObject lightTileToClone;
    public GameObject darkTileToClone;

    float boardPositionX;
    float boardPositionY;

    // Start is called before the first frame update
    void Start()
    {
        // setting board width and height to value stored in playerPrefs
        boardWidth = PlayerPrefs.GetInt("bw");
        boardHeight = PlayerPrefs.GetInt("bh");

        // TODO: setting board width and height to the values stored in playerPrefs

        boardPositionX = calculateBoardPosition(boardWidth);
        boardPositionY = (calculateBoardPosition(boardHeight) - .3f);

        // setting the xy origin of the canvas
        this.transform.position = new Vector3(boardPositionX, boardPositionY, this.transform.position.z);

        createBoardTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float calculateBoardPosition(float sizeValue)
    {
        float sizeToReturn = (sizeValue / 2.0f) - .5f;
        return sizeToReturn;
    }

    void createBoardTiles()
    {
        for (int boardYToPlace = 1; boardYToPlace <= boardHeight; boardYToPlace++)
        {

            for (int boardXToPlace = 1; boardXToPlace <= boardWidth; boardXToPlace++)
            {
                cloneTile(boardXToPlace, boardYToPlace, (boardXToPlace-1), -1*(boardYToPlace-1));
            }
        }
    }
    
    void cloneTile(int boardX, int boardY, float actualX, float actualY)
    {
        GameObject clonedTile;

        // if x is even and y is even
        if (boardX % 2 == 0 && boardY % 2 == 0)
        {
            clonedTile = Instantiate(lightTileToClone);
           
        }
        // if x is even and y is odd
        else if (boardX % 2 == 0 && boardY % 2 != 0)
        {
            clonedTile = Instantiate(darkTileToClone);
            
        }
        // if x is odd and y is even
        else if (boardX % 2 != 0 && boardY % 2 == 0)
        {
            clonedTile = Instantiate(darkTileToClone);
            
        }
        // if x is odd and y is odd
        else if (boardX % 2 != 0 && boardY % 2 != 0)
        {
            clonedTile = Instantiate(lightTileToClone);
        }
        else
        {
            clonedTile = Instantiate(lightTileToClone);
        }

        // making the cloned tile become a child of this object ( the parent.transform)
        clonedTile.transform.parent = this.transform;

        // setting board x and y 
        clonedTile.GetComponent<tileSquare>().boardX = boardX;
        clonedTile.GetComponent<tileSquare>().boardY = boardY;

        // setting canvas x and y
        clonedTile.transform.position = new Vector3(actualX - boardPositionX, actualY + boardPositionY, 50.0f);
        
    }
}
