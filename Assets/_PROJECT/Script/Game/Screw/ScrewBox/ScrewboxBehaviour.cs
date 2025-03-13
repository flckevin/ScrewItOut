using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewboxBehaviour : MonoBehaviour
{
    public List<Transform> screwPositions = new List<Transform>();              //all of the position of screwbox
    //public List<ScrewboltBehaviour> screws = new List<ScrewboltBehaviour>();    //all of the screw that screwbox contains
    public int colorID;                                                         //color id of screwbox
    public int amountCurrentScrew;                                              //track current amount of existing screw in slots
    public int avalibleSpot;

    /// <summary>
    /// function to check temporary slot
    /// </summary>
    public void CheckTempSlots()
    {
        //loop all slot
        for (int i = 0; i < LevelManager.Instance.screwTempSlots.Count; i++) 
        {
            //get the current slot being loop
            ScrewSlots _screwTempSlots = LevelManager.Instance.screwTempSlots[i];
            //if it has screw that match color of the box
            if (_screwTempSlots.screw.colorID == colorID) 
            {
                //unscrew it
                _screwTempSlots.screw.UnscrewCall();
            }
        }
    }


    /// <summary>
    /// function check if there is enough screw
    /// </summary>
    public void CheckScrew() 
    {
        //if there is enough amount
        if (amountCurrentScrew >= screwPositions.Count) 
        {
            //move to next screw
            GameManager.Instance.ScrewBoxManager.NextScrewBox();
        }
    }
}
