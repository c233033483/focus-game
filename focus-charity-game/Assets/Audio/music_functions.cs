using UnityEngine;

public class music_functions : MonoBehaviour

{

    public float musicLevel = 0;
    private float musicVolume = 0.5f;
    public AudioSource chords;
    public AudioSource bass;
    public AudioSource drums;
    public AudioSource guitar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chords.volume = musicVolume;
        bass.volume = 0;
        drums.volume = 0;
        guitar.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MusicUp()
    {
        if (musicLevel < 4)
        {
            musicLevel += 1;
        }
        switch (musicLevel)
        {
            case 1:
                bass.volume = musicVolume;
                break;
            case 2:
                drums.volume = musicVolume;
                break;
            case 3:
                guitar.volume = musicVolume;
                break;

        }
        
    }

    public void MusicDown()
    {
        if (musicLevel > 0)
        {
            musicLevel -= 1;
        }
        switch (musicLevel)
        {
            case 2:
                guitar.volume = 0;
                break;
            case 1:
                drums.volume = 0;
                break;
            case 0:
                bass.volume = 0;
                break;

        }
    }
}
