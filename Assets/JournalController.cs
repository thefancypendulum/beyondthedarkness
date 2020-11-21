using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    //set it to follow the movement of the hand once the game starts
    //just so i dont have to dig through the bone hiearchy every time i want to edit the book
    public GameObject Parent;

    Animator anim;

    public List<GameObject> Pages;
    int[] CurrentPages = new int[] { 0, 1 };

    public GameObject[] PageAnchors;

    void Start()
    {
        transform.SetPositionAndRotation(Parent.transform.position, Parent.transform.rotation);
        transform.parent = Parent.transform;
        anim = GetComponent<Animator>();
    }


    public void TurnPageRight(int stepsForward = 1)
    {
        anim.SetTrigger("flipright");
        TurnPage(true, new int[] { CurrentPages[0], CurrentPages[1], CurrentPages[0]+2*stepsForward, CurrentPages[1]+2*stepsForward });
    }
    public void TurnPageLeft(int stepsBackward = 1)
    {
        anim.SetTrigger("flipleft");
        TurnPage(false, new int[] { CurrentPages[0]-2*stepsBackward, CurrentPages[1]-2*stepsBackward, CurrentPages[0], CurrentPages[1] });
    }

    private void TurnPage(bool Isright, int[] PNum)
    {
        foreach(GameObject page in Pages)
        {
            page.SetActive(false);
        }

        for (int i = 0; i <= 3; i++)
        {
            Transform anchor = PageAnchors[i].transform;
            Pages[PNum[i]].SetActive(true);
            Pages[PNum[i]].transform.SetPositionAndRotation(anchor.position, anchor.rotation);
            Pages[PNum[i]].transform.parent = anchor;
        }

        if (Isright)
        {
            CurrentPages[0] = PNum[2];
            CurrentPages[1] = PNum[3];
        }
        else
        {
            CurrentPages[0] = PNum[0];
            CurrentPages[1] = PNum[1];
        }

    }
}
