using System;
using System.Collections;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class HideField : PropertyAttribute
{
    // Name of the bool field that controls the property
    public string ConditionalSourceField = "";

    // True = Hidden False = disabled
    public bool HideInInspector = false;

    public HideField(string conditionalSourceField, bool hidden = true)
    {
        this.ConditionalSourceField = conditionalSourceField;
        HideInInspector = hidden;
    }
}
