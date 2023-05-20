using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinosoidMoving : MonoBehaviour
{
    float floaingPower = 0.6f;
    float timer = 1;
    private Vector3 _startPosition;
    private RectTransform _rectTransf;

    private void Start()
    {
        _rectTransf = GetComponent<RectTransform>();
        timer = Random.Range(0, 10);
        _startPosition = transform.position;
    }

#if UNITY_EDITOR ||  UNITY_ANDROID
    void Update()
    {
        //transform.position = _startPosition + new Vector3(0.0f, Mathf.Sin(Time.time + timer) * floaingPower, 0.0f);
        _rectTransf.position = _rectTransf.position + new Vector3(0.0f, Mathf.Sin(Time.time + timer) * floaingPower, 0.0f);
    }
#endif
}
