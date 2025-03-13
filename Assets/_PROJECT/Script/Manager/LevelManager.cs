using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quocanh.pattern;
using System;
public class LevelManager : QuocAnhSingleton<LevelManager>
{
    public List<ScrewSlots> screwTempSlots = new List<ScrewSlots>();    //slots of temporary screw in levels

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}

[Serializable]
public class ScrewSlots 
{
    public Transform position;          //position of that temp slot
    public ScrewboltBehaviour screw;    //storage of screw
    public bool taken;                  //identifier whether slot is taken

    public void ResetSlot() 
    {
        screw = null;
        taken = false;
    }
}
