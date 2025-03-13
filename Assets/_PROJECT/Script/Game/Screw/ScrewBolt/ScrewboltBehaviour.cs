using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewboltBehaviour : MonoBehaviour
{
    [Header("SCREW INFO"),Space(10)]
    public int colorID;                 //color id

    private bool _unscrewed;            //identity whether screw been unscrewed
    private Collider2D _collider;       //collider of the screw
    private ScrewSlots _screwSlotIn;    //store screw slot that screw in if it happens

    // Start is called before the first frame update
    void Start()
    {
        //==================== GET ====================
        //get collider 
        _collider = GetComponent<Collider2D>();
        //=============================================
    }

    /// <summary>
    /// function to start unscrew
    /// </summary>
    public void UnscrewCall() 
    {
        //rotate around screw
        //move the screw up
        this.transform.DOLocalMoveY(this.transform.localPosition.y + 0.5f, 0.5f).OnComplete(() => 
        { 
            StoreScrew();
        });
    }

    /// <summary>
    /// function to execute storage of the screw
    /// </summary>
    private void StoreScrew() 
    {
        //get the screw box manager
        ScrewBoxManager _screwBoxMana = GameManager.Instance.ScrewBoxManager;
        //get current screw box id
        int _currentScrewBoxID = _screwBoxMana.currentScrewBox;

        //if the current screwbox have the same color as the screw and 
        //there is an empty slot on the box
        if (_screwBoxMana.screwBoxes[_currentScrewBoxID].colorID == colorID &&
            _screwBoxMana.screwBoxes[_currentScrewBoxID].screwPositions.Count > 0)
        {
            //if the screw is from temp screw slot then reset screw slot
            if (_screwSlotIn != null) _screwSlotIn.ResetSlot();

            //get slot for new screw
            Transform _newPos = _screwBoxMana.screwBoxes[_currentScrewBoxID].screwPositions[_screwBoxMana.screwBoxes[_currentScrewBoxID].avalibleSpot];

            //remove that slot so other screws won't overlap it
            _screwBoxMana.screwBoxes[_currentScrewBoxID].avalibleSpot++;

            //move screws to box
            MoveScrew(_newPos, true, () =>
            {
                //set new screw parent so it can move along with the box
                this.transform.parent = _screwBoxMana.screwBoxes[_currentScrewBoxID].transform;

                //add screw to screwbox list
                //_screwBoxMana.screwBoxes[_currentScrewBoxID].screws.Add(this);

                //increase amount of screws
                _screwBoxMana.screwBoxes[_currentScrewBoxID].amountCurrentScrew++;

                //check if the screwbox have enough of screws
                _screwBoxMana.screwBoxes[_currentScrewBoxID].CheckScrew();
            });

            

        }
        else 
        {
            //if screw is unscrewed then stop execute
            //we need this since when the new screwbox come
            //it gonna call this function and condition is to prevent
            //doing those action below twice
            if (_unscrewed == true) return;

            //loop to find temporary slots for the bolt
            for (int i = 0; i < LevelManager.Instance.screwTempSlots.Count; i++) 
            {
                //we there is no slot taken
                if (LevelManager.Instance.screwTempSlots[i].taken == false) 
                {
                    //get that temp slot
                    _screwSlotIn = LevelManager.Instance.screwTempSlots[i];
                    //assign screw behaviour to temp screw slots
                    LevelManager.Instance.screwTempSlots[i].screw = this;
                    //move scew to that slot
                    MoveScrew(_screwSlotIn.position);
                    //set screw taken to be true
                    _screwSlotIn.taken = true;
                    //break out of loop
                    break;
                }
            }

            //loose game since there aren't enough temp screw slots
        }
        
        //set unscrewd to true as the screw been remove it from origin pos
        _unscrewed = true;

    }

    /// <summary>
    /// function to move screw to desired position
    /// </summary>
    /// <param name="_pos"> desiered postion </param>
    /// <param name="_enabler"> bool to check if disable or still enable class after move </param>
    /// <param name="_OnComplete"> on complete moving function </param>
    private void MoveScrew(Transform _pos,bool _enabler = true,Action _OnComplete = null) 
    {
        //unparent screw from root
        this.transform.parent = null;
        //move the screw to the screw box
        this.transform.DOMove(_pos.position, 0.5f).OnComplete(() =>
        {
            if (_OnComplete != null) _OnComplete();
            //parent the screw to the screw box
            this.transform.parent = _pos;
            //disable collider so the player won't be able to interact with
            _collider.enabled = _enabler;
            //disable screw behaviour
            this.enabled = _enabler;
        });

    }
}
