using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;

public class DoozyUITesting : MonoBehaviour
{
    public UIView[] Views;
    [SerializeField] List<UIView> ViewHistory;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            {
            GoBack();
            }
    }

    public void SwitchViews(UIView targetView) //public facing version of ChangeToView() so it will work for buttons
    {
        ChangeToView(targetView);
    }

    private void ChangeToView(UIView targetView, bool SaveToHistory = true)
    {
        //go through every View
        foreach (UIView view in Views)
        {
            if (view != targetView)     //if it's not the view we want to switch to, hide it
            {
                if (view.IsActive() && SaveToHistory)    // BUT!!! If this is the current view, add it to the history
                {
                    ViewHistory.Add(view);
                    print($"{view.name} was added to the view history.");
                }
                view.Hide();
            }
            else                        //if its the target view, show it so we can switch to it
            {
                view.Show();
            }
        }
    }

    public void GoBack()
    {
        //if theres a history to undo
        if (ViewHistory.Count != 0) 
        { 
            ChangeToView(ViewHistory[ViewHistory.Count - 1], false);    //go back
            ViewHistory.RemoveAt(ViewHistory.Count-1);                //take this out of the history
            
        }

    }
}
