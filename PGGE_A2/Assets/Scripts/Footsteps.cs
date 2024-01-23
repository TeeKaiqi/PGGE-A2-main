using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    public AudioClip concreteFootstep;
    [SerializeField]
    public AudioClip concreteFootstepRun;
    //[SerializeField]
    //private AudioClip woodFootstepRun;
    //[SerializeField]
    //private AudioClip woodFootstep;
    //[SerializeField]
    //private AudioClip dirtFootstepRun;
    //[SerializeField]
    //private AudioClip dirtFootstep;

    public AudioSource audioSource;

    public LayerMask groundLayer;

    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    public void Step()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
        {
            //string groundTag = hit.collider.gameObject.tag;
            PlayFootstepSound(concreteFootstep);

            Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    public void StepRun()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
        {
            //string groundTag = hit.collider.gameObject.tag;

            Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);

            PlayFootstepSound(concreteFootstepRun); 
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    public void PlayFootstepSound(AudioClip clip)
    {
        audioSource.volume = Random.Range(1f, 2.5f);
        audioSource.pitch = Random.Range(0.8f, 1.8f);
        audioSource.PlayOneShot(clip);
    }
    
}
