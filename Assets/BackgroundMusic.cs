using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    public static BackgroundMusic instance;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance.priority)
            {
                DestroyImmediate(this);
            }
            else
            {
                DestroyImmediate(instance);
                instance = this;
            }
        }
        else
        {
            instance = this;
        }
    }

    public bool priority;

    public AudioClip[] bgMusic;

    public AudioSource bg;

    public void Start()
    {
        StartCoroutine(PlayBGMusic(0));
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator PlayBGMusic(int bgMusicNumber)
    {
        // lol ben, you're adorable
        if (bgMusicNumber >= bgMusic.Length)
        {
            bgMusicNumber = 0;
        }
        bg.clip = bgMusic[bgMusicNumber];
        bg.Play();
        yield return new WaitForSeconds(bg.clip.length);
        bgMusicNumber++;
        StartCoroutine(PlayBGMusic(bgMusicNumber));
    }
}
