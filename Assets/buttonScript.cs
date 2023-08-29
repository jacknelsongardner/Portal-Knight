using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    public GameObject gameBoard;

    public GameObject pauseCanvas;

    public GameObject gameBrain;

    public string buttonType;
    public string sceneArgument;
    public bool hintsEnabled;

    public List<int> xList;
    public List<int> yList;

    public float screenRatio;
    public string buttonPosition;

    //for if it needs to reference gameLogic...
    public GameObject director;
    public string levelSelect;
    
    // Start is called before the first frame update
    void Start()
    {
        hintsEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveToCorner();
    }

    public void WhenClicked()
    {
        if (buttonType.Equals("play"))
        {
            //take to level select
            SceneManager.LoadScene("LevelSelect");
        }
        else if (buttonType.Equals("home"))
        {
            //take to home screen
            SceneManager.LoadScene("LevelSelect");
        }
        else if (buttonType.Equals("back"))
        {
            //take back to specified place (probably the level select)
            SceneManager.LoadScene(sceneArgument);
            //TODO : find a way to get the last scene loaded, possibly pull from a xml or json file...

        }
        else if (buttonType.Equals("removeAdds"))
        {
            //take to in app purchases
            SceneManager.LoadScene("Store");

        }
        else if (buttonType.Equals("reset"))
        {
            // resets the level to what it was before
            gameBoard.GetComponent<CreateBoardSetup>().restartBoard();
        }
        else if (buttonType.Equals("tutorial"))
        {
            //take to the first tutorial level
            SceneManager.LoadScene("tutorialLevel1");
        }
        else if (buttonType.Equals("hint"))
        {

            if (hintsEnabled == true)
            {
                hintsEnabled = false;

                // restarting xList and yList
                xList = new List<int>();
                yList = new List<int>();
            }
            else if (hintsEnabled == false)
            {
                hintsEnabled = true;

                // restarting xList and yList
                xList = new List<int>();
                yList = new List<int>();

                // setting them equal to the gameBrains board solution that it created
                xList = gameBrain.GetComponent<CreateBoardSetup>().solutionX;
                yList = gameBrain.GetComponent<CreateBoardSetup>().solutionY;



                // inserting the player's first move in the beginning
                //xList.Insert(0,gameBrain.GetComponent<CreateBoardSetup>().playerStartPosX);
                //yList.Insert(0, gameBrain.GetComponent<CreateBoardSetup>().playerStartPosY);

            }
            else
            {
                hintsEnabled = false;
            }
        }
        else if (buttonType.Equals("preferences"))
        {
            if (!pauseCanvas.activeInHierarchy)
            {
                Camera.main.transform.position = new Vector3(400, 400, -14);
                pauseCanvas.SetActive(true);
            }
            else if (pauseCanvas.activeInHierarchy)
            {
                Camera.main.transform.position = new Vector3(0, 0, -14);
                pauseCanvas.SetActive(false);
            }
        }
        else  if (buttonType.Equals("newBoard"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void calculateScreenRatio()
    {
        screenRatio = Screen.width / Screen.height;
    }

    public void moveToCorner()
    {
        
    }
}
