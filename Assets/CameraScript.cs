using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float zoomAmount;
    public float ratio;

    public float screenWidth;
    public float screenHeight;

    public float defaultZoom;

    public float zoomRate;

    public GameObject gameBoard;

    public int boardWidth;
    public int boardHeight;
    public float boardRatio;

    // Start is called before the first frame update
    void Start()
    {
        // setting the default Zoom based on board width and height
        boardWidth = PlayerPrefs.GetInt("bw");
        boardHeight = PlayerPrefs.GetInt("bh");

        boardRatio = boardWidth / boardHeight;
        
        if (boardRatio > 1)
        {
            defaultZoom = 4.0f + (0.6f * (boardWidth-4.0f));
        }
        else if (boardRatio <= 1)
        {
            defaultZoom = 4.0f + (0.7f * (boardHeight-4.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        calculateScreenRatio();
        zoom();

        // checking if board ratio is above or below minimum/max
        /*
        if (ratio < .5f)
        {
            Screen.SetResolution(screenWidth - 1.0f, screenHeight, false);
        }
        else if (ratio > 2.0f)
        {
            Screen.SetResolution(screenWidth, screenHeight - 1.0f, false);
        }
        */
    }

    void calculateScreenRatio()
    {
       ratio = screenHeight / screenWidth;
    }

    void zoom()
    {
        if (ratio <= 1)
        {
            // set it to a default number
            Camera.main.orthographicSize = defaultZoom;
        }
        else if (ratio > 1)
        {
            // set the zoom out relative to the ammount
            Camera.main.orthographicSize = defaultZoom + (ratio-1)*(defaultZoom/zoomRate);
        }
        else
        {
            // do nothing :(
        }
    }
}
