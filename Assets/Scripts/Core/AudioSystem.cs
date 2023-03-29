using UnityEngine;

/// <summary>
/// Insanely basic audio system which supports 3D sound.
/// Ensure you change the 'Sounds' audio source to use 3D spatial blend if you intend to use 3D sounds.
/// </summary>
public class AudioSystem : StaticInstance<AudioSystem>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;

    public void play_music(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void pause_music()
    {
        _musicSource.Pause();
    }

    public void unpause_music()
    {
        _musicSource.UnPause();
    }

    public void stop_music()
    {
        _musicSource.Stop();
    }

    public void play_sound(AudioClip clip, Vector3 pos, float vol = 1)
    {
        _soundsSource.transform.position = pos;
        play_sound(clip, vol);
    }

    public void play_sound(AudioClip clip, float vol = 1)
    {
        _soundsSource.PlayOneShot(clip, vol);
    }
}