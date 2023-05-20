using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LuckyRaffleWinningTicket : MonoBehaviour
{
    public Text winingNumber;

    public Text[] ticketIDs;

    public Text wininerCountry;

    public Text andXMore;

    string[] countrys = new string[20] {    "France", "United States", "China", "Spain", "Italy",
                                            "Turkey", "United Kingdom", "Germany", "Russian Federation", "Malaysia",
                                            "Mexico", "Austria", "Ukraine", "Thailand", "Saudi Arabia",
                                            "Canada", "Poland", "Netherlands", "Singapore", "Croatia",};

    public void setUpWiningTicket(DateTime ticketDate)
    {
        
        //Debug.Log(ticketDate + " - date - " + this.name);
        //Debug.Log(PlayerPrefs.GetString(ticketDate + "_TicketsOpenedCode"));

        if (!PlayerPrefs.HasKey(ticketDate + "_TicketsOpened"))
        {
            this.gameObject.SetActive(false);
            return;
        }

        int tickOpened = PlayerPrefs.GetInt(ticketDate + "_TicketsOpened");

        string[] ticketsOpenedArray = PlayerPrefs.GetString(ticketDate + "_TicketsOpenedCode").Split(' ');

        //DateTime tempDate = DateTime.Today - TimeSpan.FromDays(1);
        if (!PlayerPrefs.HasKey(ticketDate + "_WinningTicket"))
        {
            int tempWinTick = generateWinnerTicketID(ticketsOpenedArray);

            PlayerPrefs.SetString(ticketDate + "_WinnerCountry", countrys[UnityEngine.Random.Range(0, 20)]);
            PlayerPrefs.SetString(ticketDate + "_WinningTicket", tempWinTick.ToString());

        }

        winingNumber.text = PlayerPrefs.GetString(ticketDate + "_WinningTicket");
        wininerCountry.text = PlayerPrefs.GetString(ticketDate + "_WinnerCountry");

        for (int i = 0; i < ticketIDs.Length; i++)
        {
            if (ticketsOpenedArray[i] != null)
                ticketIDs[i].text = ticketsOpenedArray[i];
        }

        int tempInt = ticketIDs.Length - tickOpened;
        if (tempInt > 0)
        {
            andXMore.text = "and " + tempInt + " more...";
        }
        else
        {
            andXMore.gameObject.SetActive(false);
        }
    }


    // winner ticlet gemerator
    int generateWinnerTicketID(string[] ticketsOpenedArray)
    {
        int tempWinTick = UnityEngine.Random.Range(625356465, 865456460);
        foreach (var item in ticketsOpenedArray)
        {
            int temp = 19; int.TryParse(item, out temp);
            if (tempWinTick == temp)
            {
                return 615356460;
            }
        }
        return tempWinTick;
    }


}
