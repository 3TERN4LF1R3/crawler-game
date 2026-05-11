using UnityEngine;
using System.Collections.Generic;

public class MusicShuffle : MonoBehaviour
{
    // These are the public variables that MUST show up in the inspector
    [Header("Music Settings")]
    public AudioClip[] playlist; 
    
    private List<int> _playOrder = new List<int>();
    private AudioSource _audioSource;
    private int _currentIndex = -1;

    void Awake()
    {
        // This is the part that keeps music playing between scenes
        DontDestroyOnLoad(this.gameObject);
        
        // Ensure we have an AudioSource
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        if (playlist != null && playlist.Length > 0)
        {
            CreateShuffledOrder();
            PlayNextSong();
        }
    }

    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void CreateShuffledOrder()
    {
        _playOrder.Clear();
        for (int i = 0; i < playlist.Length; i++)
        {
            _playOrder.Add(i);
        }

        for (int i = 0; i < _playOrder.Count; i++)
        {
            int temp = _playOrder[i];
            int randomIndex = Random.Range(i, _playOrder.Count);
            _playOrder[i] = _playOrder[randomIndex];
            _playOrder[randomIndex] = temp;
        }
        
        _currentIndex = 0;
    }

    void PlayNextSong()
    {
        if (_playOrder.Count == 0) return;

        if (_currentIndex >= _playOrder.Count)
        {
            CreateShuffledOrder();
        }

        int trackIndex = _playOrder[_currentIndex];
        _audioSource.clip = playlist[trackIndex];
        _audioSource.Play();
        
        _currentIndex++;
    }
}
