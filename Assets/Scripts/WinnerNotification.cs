using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WinnerNotification : MonoBehaviour
{
    public Text notificationText;
    public Text notificationText1;
    public Text notificationText2;
    public GameObject[] iPhonePrizeIcons;
    public GameObject[] appleWatchPrizeIcons;
    public GameObject[] samsungPrizeIcons;
    public GameObject[] goProPrizeIcons;
    public GameObject[] JBLPrizeIcons;
    public GameObject[] payPalImages;

    private int _randomStartNumber;
    private int _randomEndNumber;
    private RectTransform _rectTransform;
    private void Start()
    {
        _randomStartNumber = Random.Range(100, 1000);
        _randomEndNumber = Random.Range(100, 1000);
        string[] notificationsTexts = new string[6];
        notificationsTexts[0] = "User ID " + _randomStartNumber+ "****" + _randomEndNumber + " redeemed $150";
        notificationsTexts[1] = "User ID " + _randomStartNumber+ "****" + _randomEndNumber + " won iPhone 12 Pro Max";
        notificationsTexts[2] = "User ID " + _randomStartNumber+ "****" + _randomEndNumber + " won Galaxy S21 Ultra";
        notificationsTexts[3] = "User ID " + _randomStartNumber+ "****" + _randomEndNumber + " won AirPods Pro";
        notificationsTexts[4] = "User ID " + _randomStartNumber+ "****" + _randomEndNumber + " claim 0.05 Bitcoin";
        notificationsTexts[5] = "User ID " + _randomStartNumber+ "****" + _randomEndNumber + " claim 1.0 Ethereum";
        var randomIndex = Random.Range(0, 6);
        ShowOrHidePayPalImage(randomIndex);
        notificationText.text = notificationsTexts[randomIndex];
        notificationText1.text = notificationText.text;
        notificationText2.text = notificationText.text;
        
        _rectTransform = GetComponent<RectTransform>();
        var pos = _rectTransform.localPosition;
        pos.x = 900f;
        _rectTransform.localPosition = pos;
    }

    private void Update()
    {
        var pos = _rectTransform.localPosition;
        pos.x = pos.x - 100f * Time.deltaTime;
        _rectTransform.localPosition = pos;

        if (Vector2.Distance(_rectTransform.localPosition, new Vector2(-2150f, _rectTransform.localPosition.y)) <= 50f)
        {
            WithdrawController.Instance.GetRandomTime();
            Destroy(gameObject);
        }
    }

    private void ShowOrHidePayPalImage(int index)
    {
        switch (index)
        {
            case 0:
                ShowOrHideIcons(payPalImages, iPhonePrizeIcons, appleWatchPrizeIcons, samsungPrizeIcons, goProPrizeIcons, JBLPrizeIcons);
                break;
            case 1:
                ShowOrHideIcons(iPhonePrizeIcons, payPalImages, appleWatchPrizeIcons, samsungPrizeIcons, goProPrizeIcons, JBLPrizeIcons);
                break;
            case 2:
                ShowOrHideIcons(appleWatchPrizeIcons, payPalImages, iPhonePrizeIcons, samsungPrizeIcons, goProPrizeIcons, JBLPrizeIcons);
                break;
            case 3:
                ShowOrHideIcons(samsungPrizeIcons, payPalImages, appleWatchPrizeIcons, iPhonePrizeIcons, goProPrizeIcons, JBLPrizeIcons);
                break;
            case 4:
                ShowOrHideIcons(goProPrizeIcons, payPalImages, appleWatchPrizeIcons, samsungPrizeIcons, iPhonePrizeIcons, JBLPrizeIcons);
                break;
            case 5:
                ShowOrHideIcons(JBLPrizeIcons, payPalImages, appleWatchPrizeIcons, samsungPrizeIcons, goProPrizeIcons, iPhonePrizeIcons);
                break;
        }
    }

    private void ShowOrHideIcons(GameObject[] active, GameObject[] nonActive1, GameObject[] nonActive2, GameObject[] nonActive3, GameObject[] nonActive4, GameObject[] nonActive5)
    {
        for (int i = 0; i < payPalImages.Length; i++)
        {
            active[i].SetActive(true);
            nonActive1[i].SetActive(false);
            nonActive2[i].SetActive(false);
            nonActive3[i].SetActive(false);
            nonActive4[i].SetActive(false);
            nonActive5[i].SetActive(false);
        }
    }
}
