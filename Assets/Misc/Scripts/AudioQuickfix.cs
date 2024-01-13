using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioQuickfix : MonoBehaviour
{
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void DestroyAudioManager (int trackNumber)
    {
        audioManager = FindObjectOfType<AudioManager>();
        Destroy(audioManager.gameObject);
    }
}
