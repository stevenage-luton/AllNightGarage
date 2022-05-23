using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Collider playerCollider;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private AudioClip sfxDoorOpen;

    [SerializeField]
    private AudioClip sfxDoorClose;

    private void OnTriggerEnter(Collider playerCollider)
    {
        _animator.SetBool("Opening", true);
        _source.clip = sfxDoorOpen;
        _source.Play();
    }

    private void OnTriggerExit(Collider playerCollider)
    {
        _animator.SetBool("Opening", false);
        _source.clip = sfxDoorClose;
        _source.Play();
    }

}
