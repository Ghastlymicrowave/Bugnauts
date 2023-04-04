using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips;
    AudioSource source;
    [SerializeField] float minPitch = 1f;
    [SerializeField] float maxPitch = 1f;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(int index)
    {
        source.pitch = Random.Range(minPitch, maxPitch);
        source.PlayOneShot(clips[index]);
    }
}
