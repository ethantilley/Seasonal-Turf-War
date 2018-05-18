using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///         
/// not inuse.
/// cause FMOD
/// </summary>
public class AudioManager : MonoBehaviour
{

    #region AudioClipData
    [System.Serializable]
    public class AudioClipData
    {
        [ClipDrawer()]
        public string clipName;
        [Space(-15)]
        public AudioClip audioClip;
        //TODO: (if clip is used alot and it's annoying then click this and it will make the pitch of the sound random.)
        public bool randomisedPitch = false;
        [Header("Pitch To Randomise Between"), Tooltip("Two Values For Pitch To Randomise Between")] //Todo: Maybe make that a property drawer of two floats, so there's no x and y that will likely confuse the designers
        public Vector2 randomPitchValues;

    }
    #endregion

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
    }


    public List<AudioClipData> clips = new List<AudioClipData>();

    public AudioClip[] bgMusic;

    public AudioSource sfxSource;

    public AudioSource bg;

    public void Start()
    {
        StartCoroutine(PlayBGMusic(0));
    }

    public void PlaySound(string clipName)
    {
        foreach (AudioClipData sound in clips)
        {
            if (sound.audioClip.name == clipName)
            {

                if (sound.randomisedPitch)
                    sfxSource.pitch = Random.Range(sound.randomPitchValues.x, sound.randomPitchValues.y);
                else

                    sfxSource.pitch = 1;

                sfxSource.PlayOneShot(sound.audioClip);
            }
        }
    }

    public IEnumerator PlayBGMusic(int bgMusicNumber)
    {
        bgMusicNumber++;
        if (bgMusicNumber >= 4)
        {
            bgMusicNumber = 0;
        }
        bg.clip = bgMusic[bgMusicNumber];
        bg.Play();
        yield return new WaitForSeconds(bgMusic[bgMusicNumber].length);
        StartCoroutine(PlayBGMusic(bgMusicNumber));
    }
}
