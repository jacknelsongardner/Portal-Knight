using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelect : MonoBehaviour
{

    //SET BY SAVE/LOADED DATA

    //whether this level is unlocked
    public bool isUnlocked;

    //star objects
    public GameObject oneStarImage;
    public GameObject twoStarImage;
    public GameObject threeStarImage;

    public GameObject lockImage;
    public GameObject levelUnlockedText;

    //how many stars does this level have
    public int stars;


    //the translucent color 
    public Color invisibleColor = new Color(0, 0, 0, 0);
    public Color visibleColorGrey = new Color(115, 115, 115, 255);
    public Color visibleColorYellow = new Color(255, 246, 96, 255);

    //SET BY BUILDER

    //what level is this?
    public int level;

    //what section? (3x3, 4x4, etc)
    public string section;

    //what collection? (regular levels, mystery, special levels, etc) this will not be utilized unless the app grows immensely popular, and expansions are necesary to create player interest
    public string collection;

    //how many levels completed to unlock
    public int levelsCompletedToUnlock;

    //how many stars the player has collected over life time of game (in this section)
    public int howManyStarsCollected;
    public int howManyStarsToCollect;

    //whether this level was completed or not
    public string isCompleted;

    //how many levels the player has completed
    public int howManyLevelsCompleted;

    //this is what the levels prefix will be called
    public string levelPrefix;

    // Start is called before the first frame update
    void Start()
    {

        levelPrefix = level.ToString() + section + collection;

        loadGame();

        //checking for how many stars the player has earned on this level
        if (levelsCompletedToUnlock <= howManyLevelsCompleted)
        {
            isUnlocked = true;

        }

        //here we update the button image to look right
        //level is completed and unlocked
        if (isUnlocked)
        {

            levelUnlockedText.GetComponent<UnityEngine.UI.Text>().color = Color.white;

            lockImage.GetComponent<UnityEngine.UI.Image>().color = invisibleColor;

            if (stars == 1)
            {
                //set one star to be yellow, and other grey
                oneStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;

                twoStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;

                threeStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;


            }
            else if (stars == 2)
            {
                //set two stars to be yellow and other gray
                oneStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;

                twoStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;

                threeStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;

            }
            else if (stars == 3)
            {
                //set all the stars to be yellow
                oneStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;

                twoStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;

                threeStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorYellow;
            }
            else if (stars == 0)
            {
                //set the stars to be grey
                oneStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;

                twoStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;

                threeStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;
            }
            else
            {
                //same as if they are zero, set them all to be grey. This is in case stars was not initialized
                oneStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;

                twoStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;

                threeStarImage.GetComponent<UnityEngine.UI.Image>().color = visibleColorGrey;
            }
        }
        //uncompleted and locked
        else if (!isUnlocked)
        {
            //making sure all the star images are invisible
            oneStarImage.GetComponent<UnityEngine.UI.Image>().color = invisibleColor;

            twoStarImage.GetComponent<UnityEngine.UI.Image>().color = invisibleColor;

            threeStarImage.GetComponent<UnityEngine.UI.Image>().color = invisibleColor;

            
            levelUnlockedText.GetComponent<UnityEngine.UI.Text>().color = invisibleColor;

            //making lock image visible
            lockImage.GetComponent<UnityEngine.UI.Image>().color = Color.white;

        }



    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadGame()
    {
        //loading how many levels completed (if it exists)
        if (PlayerPrefs.HasKey("howManyLevelsCompleted"))
        {
            howManyLevelsCompleted = PlayerPrefs.GetInt("howManyLevelsCompleted");
        }
        else
        {
            howManyLevelsCompleted = 0;
            PlayerPrefs.SetInt("howManyLevelsCompleted", howManyLevelsCompleted);
        }

        //updating how many stars the player has collected
        if (PlayerPrefs.HasKey("howManyStarsCollected"))
        {
            //calculating how many stars the player has collected
            howManyStarsCollected = PlayerPrefs.GetInt("howmanyStarsCollected");
            howManyStarsCollected += stars;

            PlayerPrefs.SetInt("howManyStarsCollected", howManyStarsCollected);
        }
        else
        {
            howManyStarsCollected = 0;
            PlayerPrefs.SetInt("howManyStarsCollected", 0);
        }

        //loading how many stars the player has earned on this level
        if (PlayerPrefs.HasKey("stars" + levelPrefix))
        {
            stars = PlayerPrefs.GetInt("stars" + levelPrefix);
            
        } 
        else
        {
            stars = 0;
            PlayerPrefs.SetInt("stars" + levelPrefix,stars);
        }

        //POSSIBLY DELETE THIS CODE:
        //getting whether the level is completed
        if (PlayerPrefs.HasKey("level" + levelPrefix + "completed"))
        {

            isCompleted = PlayerPrefs.GetString("level" + levelPrefix + "completed");

        }
        else
        {
            isCompleted = "false";
            PlayerPrefs.SetString("level" + levelPrefix + "completed", isCompleted);
        }
    }

    public void loadLevel()
    {
        if (isUnlocked)
        {
            //loading desired level
            SceneManager.LoadScene(levelPrefix);
        }
    }

}
