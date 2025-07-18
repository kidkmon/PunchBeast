using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    [SerializeField] AudioClip _punchSound;
    [SerializeField] AudioClip _upradeSound;

    public void PlayPunchSound() => AudioSource.PlayClipAtPoint(_punchSound, Camera.main.transform.position, 0.5f);
    public void PlayUpgradeSound() => AudioSource.PlayClipAtPoint(_upradeSound, Camera.main.transform.position, 0.5f);
} 