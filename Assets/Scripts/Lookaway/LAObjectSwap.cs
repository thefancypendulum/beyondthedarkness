using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LAObjectSwap : MonoBehaviour, ILookawayObject
{
    [Header("Game Objects")]
    [SerializeField]
    protected GameObject SecondaryObject;
    [SerializeField]
    protected Transform SpawnAnchor;

    [Header("Settings")]
    [SerializeField]
    protected bool HasAudioCue = false;
    [SerializeField, HideField("HasAudioCue")]
    protected AudioClip AudioCue;
    [SerializeField]
    protected bool HasAnimationCue = false;
    [SerializeField, HideField("HasAnimationCue")]
    protected Animator AnimationCueController;
    [SerializeField]
    protected bool UseDistanceParameter = false;
    [SerializeField, HideField("UseDistanceParameter")]
    protected float DistanceFromObject;

    //=============[PRIVATE FIELDS]===============
    protected bool HasBeenSeen;
    
    private void OnBecameVisible()
    {
        if (!HasBeenSeen)
            HasBeenSeen = true;
    }

    private void OnBecameInvisible()
    {
        if (HasBeenSeen)
        {
            if (UseDistanceParameter)
            {
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                float dist = -1;
                if (playerObject != null)
                {
                    dist = Vector3.Distance(transform.position, playerObject.transform.position);
                }

                if (dist != -1 && dist <= DistanceFromObject)
                {
                    TriggerLookawayEffect();
                }
            }
            else
            {
                TriggerLookawayEffect();
            }
        }
    }

    public void TriggerLookawayEffect()
    {
        if (SecondaryObject != null)
        {
            GetComponent<MeshRenderer>().enabled = false;
            Instantiate(SecondaryObject, SpawnAnchor);
        }

        if (HasAnimationCue && AnimationCueController != null)
        {
            AnimationCueController.SetBool("lookaway", true);
        }

        if (HasAudioCue && AudioCue != null)
        {
            AudioSource.PlayClipAtPoint(AudioCue, SpawnAnchor.position);
        }
    }

    private void OnDrawGizmos()
    {
        if (UseDistanceParameter)
        {
            Gizmos.DrawWireSphere(SpawnAnchor.position, DistanceFromObject);
        }
    }
}
