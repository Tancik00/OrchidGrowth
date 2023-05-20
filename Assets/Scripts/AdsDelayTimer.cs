using System;
using System.Collections;
using System.Collections.Generic;
using MergeFactory;
using UnityEngine;
using UnityEngine.UI;

public class AdsDelayTimer : MonoBehaviour
{
    public GameObject priceText;
    private Button _buttonParent;
    private Text _timerText;
    private float _time;
    private bool _isTimerStarting;

    private void Start()
    {
        _buttonParent = GetComponentInParent<Button>();
        _timerText = GetComponent<Text>();
        _time = 30f;
    }

    private void Update()
    {
        if (_isTimerStarting)
        {
            _time -= Time.deltaTime;
            _timerText.text = "0:" + Mathf.RoundToInt(_time);
            _buttonParent.interactable = false;
            priceText.SetActive(false);
            if (_time <= 0)
            {
                _buttonParent.interactable = true;
                _timerText.text = "";
                priceText.SetActive(true);
                _time = 30f;
                _isTimerStarting = false;
            }
        }
    }

    public void StartAdsDelayTimer()
    {
        if (ads.instance.rewardedAvailable)
        {
            _isTimerStarting = true;
        }
    }
}
