using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckyBubble : MonoBehaviour
{
    public int ID;
    public Image bubleImage;
    public Sprite puzzleSprite;


    public void setUpBubble(Prizes prizes)
    {
        ID = prizes.ID;
        bubleImage.sprite = prizes.sprite;
    }

    
    public void setUpPuzzleBubble(Prizes prizes)
    {
        ID = prizes.ID;
        bubleImage.sprite = puzzleSprite;
    }


}
