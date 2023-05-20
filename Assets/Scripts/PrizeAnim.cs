using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeAnim : MonoBehaviour
{
    [Header("Lucky Prizes")]
    public Sprite[] luckyPrizesToShow;
    public Image luckyPrizesButtonImage;
    private int prizesToShowActualIndex = 0;
    public GameObject LuckyPrizesManager;
    public GameObject LuckyPrizesYouWonPanel;
    public GameObject LuckyPrizesYouWonPrizeName;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeLuckyBuutonIcon", 3, 20);
    }



    void ChangeLuckyBuutonIcon()
    {
        var prizesdb = LuckyPrizesManager.GetComponent<LuckyPrizesManager>().prizeDatabase;
        prizesToShowActualIndex++;

        // reseting 
        if (prizesToShowActualIndex == prizesdb.Length)
        {
            prizesToShowActualIndex = 0;
        }




        StartCoroutine(changeSprite());
    }

    IEnumerator changeSprite()
    {
        LuckyPrizesYouWonPanel.SetActive(false);
        var prizesdb = LuckyPrizesManager.GetComponent<LuckyPrizesManager>().prizeDatabase;
        luckyPrizesButtonImage.GetComponent<Animation>().Play();
        
        yield return new WaitForSeconds(0.25f);
        luckyPrizesButtonImage.sprite = prizesdb[prizesToShowActualIndex].sprite;
        LuckyPrizesYouWonPrizeName.GetComponent<Text>().text = prizesdb[prizesToShowActualIndex].name;
        yield return new WaitForSeconds(0.40f);
        //LuckyPrizesYouWonPanel.SetActive(true);
    }

}
