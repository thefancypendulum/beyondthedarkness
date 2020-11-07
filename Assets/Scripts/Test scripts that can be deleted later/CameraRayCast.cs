using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    public bool MenuRaycast = true;

    //stored infomation for the raycast hit
    GameObject HitObject;

    GameObject PreviousHitObject;

    

    enum Layers
    {
        //just to make the if statements more readable and idiot proof
        UI = 5
    }

    void Update()
    {
        
        if (MenuRaycast) //only raycast if the bool is true
        {
            //raycast
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 30))
            {
                //store some information about the raycast hit
                HitObject = hit.transform.gameObject;
            }
        }

        


        if (HitObject != null) //put all hit obj logic inside here
        {
            if (PreviousHitObject != HitObject) //on new object hovered
            {
                if (PreviousHitObject != null && PreviousHitObject.GetComponent<UICore>() != null)
                {
                    PreviousHitObject.GetComponent<UICore>().OnHoverExit();
                }

                if (HitObject != null && HitObject.GetComponent<UICore>() != null)
                {
                    HitObject.GetComponent<UICore>().OnHoverEnter();
                }
                
                PreviousHitObject = HitObject;
            }

            if (Input.GetMouseButtonDown(0)) //when you click
            {
                if (HitObject.GetComponent<UICore>() == null) return;
                HitObject.GetComponent<UICore>().OnClickObject();
            }
            if (Input.GetMouseButtonUp(0)) //when you lift the mouse back up
            {
                if (HitObject.GetComponent<UICore>() == null) return;
                HitObject.GetComponent<UICore>().OnClickStop();
            }
        }
    }
}
