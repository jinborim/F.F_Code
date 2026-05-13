using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Music_Changer : MonoBehaviour
{
    public AudioSource Audio_Source;

    public AudioClip BeforeBoss;
    public AudioClip Boss_Music;
    public AudioClip AfterBoss;

    public void BossMusic(string MusicName)
    {
        switch (MusicName)
        {
            case "BEFORE":
                Audio_Source.clip = BeforeBoss;
                Audio_Source.Play();
                break;
            case "BOSS":
                Audio_Source.clip = Boss_Music;
                Audio_Source.Play();
                break;
            case "AFTER":
                Audio_Source.clip = AfterBoss;
                Audio_Source.Play();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
