using UnityEngine;

public class PlayerAnimationAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Global Volume")]
    [Range(0f, 2f)] 
    public float masterVolume = 1.0f; 

    [Header("Individual Clip Settings")]
    public AudioClip jumpClip;
    [Range(0f, 1f)] public float jumpVolume = 1.0f;

    [Space]
    public AudioClip landClip;
    [Range(0f, 1f)] public float landVolume = 1.0f;

    [Space]
    public AudioClip damageClip;
    [Range(0f, 1f)] public float damageVolume = 1.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Logic: (Clip Volume) * (Master Volume Setting)
    // This allows the master slider to scale everything while keeping individual balance
    public void PlayJumpSound() => PlaySFX(jumpClip, jumpVolume);
    public void PlayLandSound() => PlaySFX(landClip, landVolume);
    public void PlayDamageSound() => PlaySFX(damageClip, damageVolume);

    private void PlaySFX(AudioClip clip, float clipVolume)
    {
        if (clip != null && audioSource != null)
        {
            float finalVolume = clipVolume * masterVolume;
            audioSource.PlayOneShot(clip, finalVolume);
        }
    }

    // Call this from your UI Slider to control the overall volume (0 to 2.0)
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
    }
}