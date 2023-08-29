using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class counterScript : MonoBehaviour
{
    // gameboard creator (in effect, this creates the solution)
    public GameObject boardCreator;

    // player, portal, and key
    public GameObject playerKnight;
    public GameObject portal;
    public GameObject key;

    //level information
    public string level;
    public string section;
    public string collection;
    public string levelPrefix;

    //what level we are on
    public string levelName;

    //colors for the stars
    public Color visibleColorGrey;
    public Color visibleColorYellow;

    //variables for keeping the history of player and the solution map
    public List<float> solutionXY;
    public List<float> solutionX;
    public List<float> solutionY;

    public List<float> playerHistoryX;
    public List<float> playerHistoryY;

    public int solutionIndex;

    //whether player is on the hint path
    public bool playerIsOnPath;

    //represents the star particle generator, which goes off for a short (but rewarding) burst
    public ParticleSystem oneStarParticles;
    public ParticleSystem twoStarParticles;
    public ParticleSystem threeStarParticles;

    //whether particle effects have played yet
    public bool particlesHavePlayed;

    //represents the particle generator for the portal, which goes off when you win (by entering the portal)
    public ParticleSystem portalParticles;

    //represents the text above the counter
    public GameObject prettyText;

    //how many movements the player has made
    public int movementsMade;
    //how many movements the level can be completed in
    public int movementsNeeded;

    //how many moves for each star
    public int starOneMoves;
    public int starTwoMoves;
    public int starThreeMoves;

    //stores what the 
    public string displayString;

    //whether the player has more moves than necesary to beat the level
    public bool madeTooManyMoves;

    //whether the player has lost
    public bool hasLost;
    public bool hasWon;

    //win and lose messages
    public string winMessage;
    public string loseMessage;

    //star messages
    public string noStarMessage;
    public string oneStarMessage;
    public string twoStarMessage;
    public string threeStarMessage;

    //how many stars the player has earned (before and after the level is completed)
    public int howManyStarsBefore;
    public int howManyStarsAfter;

    //starObjects
    public GameObject starOne;
    public GameObject starTwo;
    public GameObject starThree;

    //starObjectsText
    public GameObject starOneText;
    public GameObject starTwoText;
    public GameObject starThreeText;

    public float playerBoardX;
    public float playerBoardY;

    public bool hasNotWonYet;

    public float nextX;
    public float nextY;

    // Start is called before the first frame update
    void Start()
    {
        // setting movements made to 0 by default
        movementsMade = 0;

        // setting the solution map
        solutionX = boardCreator.GetComponent<CreateBoardSetup>().solutionX.ConvertAll(x => (float)x);
        solutionY = boardCreator.GetComponent<CreateBoardSetup>().solutionY.ConvertAll(x => (float)x);

        this.movementsNeeded = solutionX.Count - 1;

        for (int i = 0; i < solutionX.Count; i++)
        {
            solutionX[i] = solutionX[i] + 1;
        }

        for (int i = 0; i < solutionY.Count; i++)
        {
            solutionY[i] = solutionY[i] + 1;
        }

        //solutionX.RemoveAt(0);
        //solutionY.RemoveAt(0);

        //obstantiating the playerhistory lists and setting solution index to zero
        solutionIndex = 0;


        playerIsOnPath = true;
        playerHistoryX = new List<float>();
        playerHistoryY = new List<float>();

        hasNotWonYet = true;
        loadGame();
        
        //setting knight history to empty

        //pausing the particles immediatly
        oneStarParticles.Pause();
        twoStarParticles.Pause();
        threeStarParticles.Pause();
        portalParticles.Pause();
        particlesHavePlayed = false;

        //setting lose/win states
        hasLost = false;
        hasWon = false;

        //setting the win messages (can be changed easily from frontend)
        winMessage = "Nice";
        loseMessage = "Oops";

        //sets movements made to zero
        movementsMade = 0;
        madeTooManyMoves = false;

        //setting the display string to the currentMovementsMade
        displayString = movementsMade.ToString() + "/" + movementsNeeded.ToString();
        GetComponent<UnityEngine.UI.Text>().text = displayString;
        GetComponent<UnityEngine.UI.Text>().color = Color.black;

        //set each of the stars to white color
        starOne.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        starTwo.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        starThree.GetComponent<UnityEngine.UI.Image>().color = Color.white;

        //star text will be set by the level designer
        starOneText.GetComponent<UnityEngine.UI.Text>().text = "";
        starTwoText.GetComponent<UnityEngine.UI.Text>().text = "";
        starThreeText.GetComponent<UnityEngine.UI.Text>().text = "";

        //how many moves for each star

        //setting the "pretty text" to the level name
        prettyText.GetComponent<UnityEngine.UI.Text>().text = levelName;

        playerKnight.GetComponent<playerMovement>().playerBoardX = solutionX[0];
        playerKnight.GetComponent<playerMovement>().playerBoardY = solutionY[0];

        //levelPrefix = level + section + collection;
        playerHistoryX.Add(solutionX[0]);
        playerHistoryY.Add(solutionY[0]);
    }

    // Update is called once per frame
    void Update()
    {
        playerBoardX = playerKnight.GetComponent<playerMovement>().playerBoardX;
        playerBoardY = playerKnight.GetComponent<playerMovement>().playerBoardY;

        /*
        // testing to see if we have to delete the last two elements of playerHistory
        if (playerHistoryX.Count >= 3 && !playerIsOnPath)
        {
            if (playerHistoryX[playerHistoryX.Count - 1] == playerHistoryX[playerHistoryX.Count - 3] && playerHistoryY[playerHistoryY.Count - 1] == playerHistoryY[playerHistoryY.Count - 3] && !playerIsOnPath)
            {
                playerHistoryX.RemoveAt(playerHistoryX.Count - 1);
                playerHistoryX.RemoveAt(playerHistoryX.Count - 1);

                playerHistoryY.RemoveAt(playerHistoryY.Count - 1);
                playerHistoryY.RemoveAt(playerHistoryY.Count - 1);
            }
        }
        */
        if (playerIsOnPath && playerHistoryX.Count >= 2)
        {
            playerHistoryX.RemoveAt(0);
            playerHistoryY.RemoveAt(0);
        }

        if (!playerIsOnPath && playerHistoryX.Count >= 3)
        {
            if (playerHistoryX[playerHistoryX.Count - 1] == playerHistoryX[playerHistoryX.Count - 3] && playerHistoryY[playerHistoryY.Count - 1] == playerHistoryY[playerHistoryY.Count - 3])
            {
                playerHistoryX.RemoveAt(playerHistoryX.Count - 2);
                playerHistoryX.RemoveAt(playerHistoryX.Count - 2);

                playerHistoryY.RemoveAt(playerHistoryY.Count - 2);
                playerHistoryY.RemoveAt(playerHistoryY.Count - 2);
            }
        }

        // testing if we are on the path
        if (playerBoardX == solutionX[solutionIndex] && playerBoardY == solutionY[solutionIndex])
        {
            // making sure nextX and nextY reflect the next step we should take
            if (solutionIndex != solutionX.Count - 1)
            {
                nextX = solutionX[solutionIndex + 1];
                nextY = solutionY[solutionIndex + 1];
            }
            playerIsOnPath = true;
        }

        
        if (playerBoardX != solutionX[solutionIndex] || playerBoardY != solutionY[solutionIndex])
        {
            if (playerHistoryX.Count >= 2)
            {
                nextX = playerHistoryX[playerHistoryX.Count - 2];
                nextY = playerHistoryY[playerHistoryY.Count - 2];
            }

            playerIsOnPath = false;
        }

        //setting the display string to the currentMovementsMade
        if (!hasLost && !hasWon)
        {
            displayString = movementsMade.ToString() + "/" + movementsNeeded.ToString();
            GetComponent<UnityEngine.UI.Text>().text = displayString;

            //in case you take too many moves
            if (movementsMade > movementsNeeded)
            {
                GetComponent<UnityEngine.UI.Text>().color = Color.red;
                madeTooManyMoves = true;
            }



        }
        else if (hasLost)
        {
            displayString = loseMessage;
            //GetComponent<UnityEngine.UI.Text>().text = displayString;
            GetComponent<UnityEngine.UI.Text>().color = Color.black;

            //telling the "pretty text" to display a message
            prettyText.GetComponent<UnityEngine.UI.Text>().text = displayString;

            //make text red, for a negative effect
            GetComponent<UnityEngine.UI.Text>().color = Color.red;

            //display the numbers on the stars
            starOneText.GetComponent<UnityEngine.UI.Text>().text = starOneMoves.ToString();
            starTwoText.GetComponent<UnityEngine.UI.Text>().text = starTwoMoves.ToString();
            starThreeText.GetComponent<UnityEngine.UI.Text>().text = starThreeMoves.ToString();

            //display the stars as gray
            starOne.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
            starTwo.GetComponent<UnityEngine.UI.Image>().color = Color.gray;
            starThree.GetComponent<UnityEngine.UI.Image>().color = Color.gray;

            //if you lose, don't display any stars :(

        }
        else if (hasWon && hasNotWonYet && !hasLost)
        {
            hasNotWonYet = false;
            displayString = winMessage;
            //GetComponent<UnityEngine.UI.Text>().text = displayString;
            GetComponent<UnityEngine.UI.Text>().color = Color.black;

            if (particlesHavePlayed == false)
            {
                portalParticles.Play();
                particlesHavePlayed = true;
            }

            //display the numbers on the stars
            starOneText.GetComponent<UnityEngine.UI.Text>().text = starOneMoves.ToString();
            starTwoText.GetComponent<UnityEngine.UI.Text>().text = starTwoMoves.ToString();
            starThreeText.GetComponent<UnityEngine.UI.Text>().text = starThreeMoves.ToString();

            //display the right colors on the stars

            if (movementsMade <= starOneMoves)
            {
                //setting one star to yellow
                starOne.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;
                displayString = oneStarMessage;
                howManyStarsAfter = 1;

                //start the star particles
                oneStarParticles.Play();

            }
            else
            {
                starOne.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;
            }

            if (movementsMade <= starTwoMoves)
            {
                //setting two star to yellow
                starTwo.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;
                displayString = twoStarMessage;
                howManyStarsAfter = 2;

                //start the star particles
                twoStarParticles.Play();
            }
            else 
            {
                starTwo.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;
            }

            if (movementsMade <= starThreeMoves)
            {
                //setting three star to yellow
                starThree.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;
                displayString = threeStarMessage;
                howManyStarsAfter = 3;

                //start the star particles
                threeStarParticles.Play();
            }
            else
            {
                starThree.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;
               
            }

            //telling the "pretty text" to display a message
            prettyText.GetComponent<UnityEngine.UI.Text>().text = displayString;

            //save the game
            saveGame();

            //so that it only does all this stuff ONCE
            hasWon = false;

        }









        //
    }

    public void reset()
    {
        movementsMade = 0;
        madeTooManyMoves = false;
        
        //setting the display string to the currentMovementsMade
        displayString = movementsMade.ToString() + "/" + movementsNeeded.ToString();
        GetComponent<UnityEngine.UI.Text>().text = displayString;
        GetComponent<UnityEngine.UI.Text>().color = Color.black;

    }

    public void addMove()
    {
        movementsMade++;
    }

    public void loadGame()
    {
        //checking to see howmany stars the player earned before playing this level (if any at all)
        //if (PlayerPrefs.HasKey("stars" + levelPrefix))
        //{
            howManyStarsBefore = PlayerPrefs.GetInt("stars" + levelPrefix);
        //}
        /*
        else
        {
            howManyStarsBefore = 0;
            PlayerPrefs.SetInt("stars" + levelPrefix, howManyStarsBefore);
        }
        */
    }

    public void saveGame()
    {

        if (PlayerPrefs.HasKey("stars" + levelPrefix) && howManyStarsAfter > howManyStarsBefore)
        {
            PlayerPrefs.SetInt("stars" + levelPrefix, howManyStarsAfter);
            PlayerPrefs.SetInt("howManyStarsCollected", PlayerPrefs.GetInt("howManyStarsCollected") - howManyStarsBefore + howManyStarsAfter);
        }
    }

    public void counterReset()
    {
        // setting movements made to 0 by default
        movementsMade = 0;

        // setting the solution map
        solutionX = boardCreator.GetComponent<CreateBoardSetup>().solutionX.ConvertAll(x => (float)x);
        solutionY = boardCreator.GetComponent<CreateBoardSetup>().solutionY.ConvertAll(x => (float)x);

        this.movementsNeeded = solutionX.Count - 1;

        for (int i = 0; i < solutionX.Count; i++)
        {
            solutionX[i] = solutionX[i] + 1;
        }

        for (int i = 0; i < solutionY.Count; i++)
        {
            solutionY[i] = solutionY[i] + 1;
        }

        //solutionX.RemoveAt(0);
        //solutionY.RemoveAt(0);

        //obstantiating the playerhistory lists and setting solution index to zero
        solutionIndex = 0;


        playerIsOnPath = true;
        playerHistoryX = new List<float>();
        playerHistoryY = new List<float>();

        hasNotWonYet = true;
        loadGame();

        //setting knight history to empty

        //pausing the particles immediatly
        //oneStarParticles.Pause();
        //twoStarParticles.Pause();
        //threeStarParticles.Pause();
        //portalParticles.Pause();
        particlesHavePlayed = false;

        //setting lose/win states
        hasLost = false;
        hasWon = false;

        //setting the win messages (can be changed easily from frontend)
        winMessage = "Nice";
        loseMessage = "Oops";

        //sets movements made to zero
        movementsMade = 0;
        madeTooManyMoves = false;

        //setting the display string to the currentMovementsMade
        displayString = movementsMade.ToString() + "/" + movementsNeeded.ToString();
        GetComponent<UnityEngine.UI.Text>().text = displayString;
        GetComponent<UnityEngine.UI.Text>().color = Color.black;

        //set each of the stars to white color
        starOne.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        starTwo.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        starThree.GetComponent<UnityEngine.UI.Image>().color = Color.white;

        //star text will be set by the level designer
        starOneText.GetComponent<UnityEngine.UI.Text>().text = "";
        starTwoText.GetComponent<UnityEngine.UI.Text>().text = "";
        starThreeText.GetComponent<UnityEngine.UI.Text>().text = "";

        //how many moves for each star

        //setting the "pretty text" to the level name
        prettyText.GetComponent<UnityEngine.UI.Text>().text = levelName;

        playerKnight.GetComponent<playerMovement>().playerBoardX = solutionX[0];
        playerKnight.GetComponent<playerMovement>().playerBoardY = solutionY[0];

        //levelPrefix = level + section + collection;
        playerHistoryX.Add(solutionX[0]);
        playerHistoryY.Add(solutionY[0]);
    }

}
