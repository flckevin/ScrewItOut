using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public ScrewboxBehaviour[] screwBoxes;      //all of the screw boxes

    // Start is called before the first frame update
    void Start()
    {
        //loop all screw boxes
        for (int i = 0; i < screwBoxes.Length; i++) 
        {
            //add all of them to screwbox manager in order of the array
            GameManager.Instance.ScrewBoxManager.screwBoxes.Add(screwBoxes[i]);
        }    
    }

   
}
