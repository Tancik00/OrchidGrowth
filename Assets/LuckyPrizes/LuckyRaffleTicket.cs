using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckyRaffleTicket : MonoBehaviour
{
    public bool ticketOpen = false;
    public string ID;

    public GameObject addOneSlot;
    public GameObject activateObject;
    public GameObject infoObject;

    public Text ticketNumber;

    Color needToBuyColor = new Color(0, 0, 0, 0.3f);
    Color activeColor = new Color(1, 0.645f, 0);
    Color inactiveColor = new Color(0, 0, 0, 0.3f);

    public void activateActivationTicket()
    {
        this.GetComponent<Image>().color = inactiveColor;
        addOneSlot.SetActive(false);
        activateObject.SetActive(true);
    }


    public void activateTicket(string theID)
    {
        ticketOpen = true;
        this.GetComponent<Image>().color = activeColor;
        activateObject.SetActive(false);
        infoObject.SetActive(true);
        ID = theID;
        ticketNumber.text = ID;
    }

    public void ticketIsActivated(string theID)
    {
        addOneSlot.SetActive(false);
        activateTicket(theID);
    }

    public void resetTicket()
    {
        this.GetComponent<Image>().color = needToBuyColor;
        addOneSlot.SetActive(true);
        activateObject.SetActive(false);
        infoObject.SetActive(false);
    }
}
