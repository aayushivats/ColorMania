using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void OnBottleTouch(GameObject bottle);
    public static event OnBottleTouch OnBottleTouchDelegate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check for mouse click 
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 currentposition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D raycastHit = Physics2D.GetRayIntersection(ray);

            if (raycastHit.transform != null)
            {
                if (raycastHit.transform.GetComponent<Bottle>() != null)
                {
                    OnBottleTouchDelegate(raycastHit.transform.gameObject);                    
                }
            }
        }
    }

}
