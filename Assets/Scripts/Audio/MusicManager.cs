using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource sourceA;
    public AudioSource sourceB;
    private AudioSource activeSource;
    private AudioSource inactiveSource;
    private AudioClip currentClip;

    void Awake()
    {
        activeSource = sourceA;
        inactiveSource = sourceB;
        Debug.Log("<color=green>Music Manager System Online.</color>");
    }

    public void RequestMusic(AudioClip clip)
    {
        if (clip == null || currentClip == clip) return;
        currentClip = clip;
        StopAllCoroutines();
        StartCoroutine(FadeTo(clip));
    }

    IEnumerator FadeTo(AudioClip newClip)
    {
        inactiveSource.clip = newClip;
        inactiveSource.Play();
        float t = 0;
        while (t < 1.5f)
        {
            t += Time.deltaTime;
            activeSource.volume = Mathf.Lerp(activeSource.volume, 0, t / 1.5f);
            inactiveSource.volume = Mathf.Lerp(inactiveSource.volume, 1, t / 1.5f);
            yield return null;
        }
        activeSource.Stop();
        AudioSource temp = activeSource;
        activeSource = inactiveSource;
        inactiveSource = temp;
    }
}