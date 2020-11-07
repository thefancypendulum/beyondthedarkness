using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldSpaceButton : UICore
{
    //sprite swapping for button visuals
    public Sprite IdleSprite;
    public Sprite HoveredSprite;
    public Sprite ClickedSprite;

    SpriteRenderer spriteRenderer;
    
    //  Button events for hover enter/exit and on click. 
    //  Trigger any public function you want with these bad boys!
    public UnityEvent HoverEnterEvent;
    public UnityEvent HoverExitEvent;
    public UnityEvent OnClickEvent;
    public UnityEvent OnClickStopEvent;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void OnHoverEnter()
    {
        spriteRenderer.sprite = HoveredSprite;
        HoverEnterEvent.Invoke();
    }
    public override void OnHoverExit()
    {
        spriteRenderer.sprite = IdleSprite;
        HoverExitEvent.Invoke();
    }
    public override void OnClickObject()
    {
        spriteRenderer.sprite = ClickedSprite;
        OnClickEvent.Invoke();
    }

    public override void OnClickStop()
    {
        spriteRenderer.sprite = HoveredSprite;
        OnClickStopEvent.Invoke();
    }



    public void DebugText(string debugText)
    {
        print(debugText);
    }
}
