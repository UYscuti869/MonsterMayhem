using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOptionEnabled : MonoBehaviour
{
    private void OnEnable() 
    {
        BGMController.instance.FindSliderAndUpdateReference();
    }
}
