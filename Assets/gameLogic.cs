using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLogic : MonoBehaviour
{

    //FOR IMPORTING OBJECT INFORMATION THROUGHOUT THE GAME (ENEMIES, PLAYER, ETC)

    //for player
    public GameObject player;
    public float playerX;
    public float playerY;
    public float playerBoardX;
    public float playerBoardY;

    //for enemies
    //public LinkedList<GameObject>;

    //for key
    public GameObject key;

    //for portal
    public GameObject portal;

    //these may not be needed...will decide later on
    /*
    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject enemyThree;
    */
    
    //stats for winning the game (pretty important)

    public bool hasKey;
    public bool hasEnteredPortal;
    public bool hasWon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update player stats

        //update paths for all enemies enemy path
        //if no path, then:

        //check for key grabbed

        //if true, check for portal contact

        

    }
}
