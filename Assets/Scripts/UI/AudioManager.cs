using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbientRandomAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private float minDelay = 5f;
    [SerializeField] private float maxDelay = 15f;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            if (clips.Length == 0)
                continue;

            source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
        }
    }
}