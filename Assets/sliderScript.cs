using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{
    public Text sliderText;

    public int currentSliderVal;
    public int lastSliderVal;

    public string textMsg;

    public string playerPrefVal;

    // Start is called before the first frame update
    void Start()
    {
        this.UpdateValue();
    }

    // Update is called once per frame
    void Update()
    {
        lastSliderVal = currentSliderVal;
        currentSliderVal = (int)this.GetComponent<Slider>().value;



        PlayerPrefs.SetInt(playerPrefVal, (int)this.GetComponent<Slider>().value);

        this.UpdateValue();
    }

    void UpdateValue()
    {
        sliderText.text = textMsg + this.GetComponent<Slider>().value.ToString();
        this.GetComponent<Slider>().value = (float)PlayerPrefs.GetInt(playerPrefVal);
    }
}
