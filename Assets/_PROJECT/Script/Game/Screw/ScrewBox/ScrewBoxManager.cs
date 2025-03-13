using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quocanh.pattern;
using DG.Tweening;

public class ScrewBoxManager : MonoBehaviour
{
    public List<ScrewboxBehaviour> screwBoxes = new List<ScrewboxBehaviour>();      //all the screwboxes that in level
    public int currentScrewBox = 0;            //current screwbox that need to fill
    public Transform screwBoxPos;               //position of screwbox
    public void NextScrewBox() 
    {
        //move the box to outside border
        screwBoxes[currentScrewBox].transform.DOMoveX(5, 2);
        //get next screw box id
        currentScrewBox++;
        Debug.Log($"COUNT {screwBoxes.Count}");
        //if the current screw box is less than screw box count
        if (currentScrewBox < screwBoxes.Count)
        {
            //move the screw box to the center to display next screw box
            screwBoxes[currentScrewBox].transform.DOMove(screwBoxPos.position, 2).OnComplete(() => 
            {
                //checking if there is any suitable screw on temp slot
                screwBoxes[currentScrewBox].CheckTempSlots();
            });
        }   
        else 
        { 
            //win
        }
        

    }
}
