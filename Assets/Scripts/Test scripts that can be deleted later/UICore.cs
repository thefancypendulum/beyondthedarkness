using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UICore : MonoBehaviour
{
    public abstract void OnHoverEnter();
    public abstract void OnHoverExit();
    public abstract void OnClickObject();
    public abstract void OnClickStop();
}
