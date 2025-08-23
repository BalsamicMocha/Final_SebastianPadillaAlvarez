using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string nombre)
    {
        foreach (Sound s in sounds)
        {
            if (s.nombre == nombre)
            {
                s.source.Play();
                return;
            }
           
        }
    }

    public void Stop(string nombre)
    {
        foreach (Sound s in sounds)
        {
            if (s.nombre == nombre)
            {
                s.source.Stop();
                return;
            }
            
        }
    }
}
