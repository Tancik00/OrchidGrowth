using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinnerScript : MonoBehaviour
{
    private Image spinnerImage;
    private Vector3 spinnerRotation;
    private bool switched;
    // Start is called before the first frame update
    void Start()
    {
        spinnerImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        spinnerRotation.z -= Time.deltaTime * 270f;
        transform.localEulerAngles = spinnerRotation;
        spinnerImage.fillAmount = Mathf.PingPong(Time.time, 1f);
        if (spinnerImage.fillAmount <= 0.02f)
        {
            switched = true;
            
        }else if(spinnerImage.fillAmount >= 0.98f)
        {
            switched = false;
        }

        if(switched)
            spinnerImage.fillClockwise = switched;
        else
            spinnerImage.fillClockwise = switched;

    }
}
