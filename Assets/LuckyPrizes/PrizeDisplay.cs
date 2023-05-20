using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeDisplay : MonoBehaviour
{
    public SO_Prize prizeData;

    public Text prizeName;
    public Image prizeImage;
    public Slider prizeSlider;
    public Text prizeProgressText;

    float actualProgress;
    float maxProgress;

    private void Start()
    {
        prizeName.text = prizeData.titleOfPrize;
        prizeImage.sprite = prizeData.imageOfPrize;
        maxProgress = prizeData.picesToCollect;
        prizeSlider.maxValue = maxProgress;

        sliderUpdate();
    }

    void sliderUpdate()
    {
        if (PlayerPrefs.HasKey("Prize_" + prizeName))
            actualProgress = PlayerPrefs.GetFloat("Prize_" + prizeName);
        else
            actualProgress = 0;

        prizeSlider.value = actualProgress / maxProgress;
        if (prizeSlider.value < 0.2f)
        {
            prizeSlider.value = 0.2f;
        }

        prizeProgressText.text = actualProgress + "/" + maxProgress;
    }

    public void addPrizeProgress(float amount)
    {
        actualProgress += amount;
        sliderUpdate();
    }


}
