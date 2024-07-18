using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private bool _audioStatus;

    public void AudioClicked()
    {
        _audioStatus = !_audioStatus;
        
        if (_audioStatus)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
