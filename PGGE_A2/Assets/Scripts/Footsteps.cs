using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip dirtFootstep;
    [SerializeField]
    private AudioClip concreteFootstep;
    [SerializeField]
    private AudioClip woodFootstep;
    [SerializeField]
    private AudioClip dirtFootstepRun;
    [SerializeField]
    private AudioClip concreteFootstepRun;
    [SerializeField]
    private AudioClip woodFootstepRun;

    private AudioSource audioSource;
    public LayerMask groundLayer;

    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void Step()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
        {
            string groundTag = hit.collider.gameObject.tag;

            Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);

            if (groundTag == "Dirt")
            {
                PlayFootstepSound(dirtFootstep);
            }
            if (groundTag == "Concrete")
            {
                PlayFootstepSound(concreteFootstep);
            }
            if (groundTag == "Wood")
            {
                PlayFootstepSound(woodFootstep);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    private void StepRun()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, groundLayer))
        {
            string groundTag = hit.collider.gameObject.tag;

            Debug.Log("Raycast Hit: " + hit.collider.gameObject.name);

            if (groundTag == "Dirt")
            {
                PlayFootstepSound(dirtFootstepRun);
            }
            if (groundTag == "Concrete")
            {
                PlayFootstepSound(concreteFootstepRun);
            }
            if (groundTag == "Wood")
            {
                PlayFootstepSound(woodFootstepRun);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    private void PlayFootstepSound(AudioClip clip)
    {
        audioSource.volume = Random.Range(1f, 2.5f);
        audioSource.pitch = Random.Range(0.8f, 1.8f);
        audioSource.PlayOneShot(clip);
    }
    
}
