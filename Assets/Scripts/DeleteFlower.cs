using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MergeFactory;
using UnityEngine;
using UnityEngine.UI;

public class DeleteFlower : MonoBehaviour
{
    private static DeleteFlower _instance;
    public static DeleteFlower Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<DeleteFlower>();
            
            return _instance;
        }
    }
    
    public GameObject flowerPriceTxt;
    public Transform shovelPos;
    private Text _flowerPriceTxt;

    private void Awake()
    {
        shovelPos = transform;
    }

    public void ShowDeletedFlowerPrice(float price)
    {
        _flowerPriceTxt = Instantiate(flowerPriceTxt, shovelPos).GetComponent<Text>();
        _flowerPriceTxt.transform.GetComponent<RectTransform>().anchoredPosition=Vector2.zero;
        _flowerPriceTxt.text = "+" + price;
        _flowerPriceTxt.GetComponent<Animator>().SetTrigger("ShowText");
        DataProvider.instance.gameData.playerCoins += price;
    }

    public void DestroyFlowerTxtClone()
    {
        Destroy(_flowerPriceTxt.gameObject);
    }

    public void IncreaseObjSize()
    {
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
    }

    public void DecreaseObjSize()
    {
        transform.localScale = Vector3.one;
    }
}
