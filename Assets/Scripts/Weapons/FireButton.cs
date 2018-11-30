using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour {

    public GunController gunController;

    // Use this for initialization
    void OnPointerUp()
    {
  
            gunController.isFiring = true;
    }
    void OnPointerDown()
    {
            gunController.isFiring = false;
    }
    
}
