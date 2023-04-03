using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips;
    AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(int index)
    {
        source.PlayOneShot(clips[index]);
    }
}
