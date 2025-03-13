using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask touchMask;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0) 
        { 
            Touch _touch = Input.GetTouch(0);

            if(_touch.phase == TouchPhase.Began) 
            {
                //We transform the touch position into word space from screen space and store it.
                Vector2 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);

                //We now raycast with this information. If we have hit something we can process it.
                RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward,touchMask);

                if (hitInformation.collider != null) 
                {
                    if (hitInformation.collider.transform.CompareTag("Screw")) 
                    {
                        Debug.Log("SCREW");
                        hitInformation.collider.GetComponent<ScrewboltBehaviour>().UnscrewCall();
                    }
                }
            }
        }
    }
}
