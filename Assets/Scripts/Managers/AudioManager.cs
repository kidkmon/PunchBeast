using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    [SerializeField] AudioClip _punchSound;
    [SerializeField] AudioClip _winSound;

    public void PlayPunchSound() => AudioSource.PlayClipAtPoint(_punchSound, Camera.main.transform.position, 0.5f);
    public void PlayWinSound() => AudioSource.PlayClipAtPoint(_winSound, Camera.main.transform.position, 0.5f);
} 