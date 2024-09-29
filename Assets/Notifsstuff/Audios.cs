using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audios : MonoBehaviour
{
    public AudioSource northPing;       // Assign these in the inspector
    public AudioSource southPing;
    public AudioSource eastPing;
    public AudioSource westPing;
    public AudioSource northeastPing;
    public AudioSource northwestPing;
    public AudioSource southeastPing;
    public AudioSource southwestPing;

    public float pingDuration = 1.0f; // Duration the ping sound will play for
    public float repeatInterval = 0.5f; // Interval between pings
    public float pingRepeatDuration = 5.0f; // Total duration for repeated pings

    private Coroutine currentPingCoroutine;
    private AudioSource[] pingSources; // Array to hold all audio sources
    private float startTime; // To record the start time

    private void Start()
    {
        // Initialize the array with the audio sources
        pingSources = new AudioSource[]
        {
            northPing, southPing, eastPing, westPing,
            northeastPing, northwestPing, southeastPing, southwestPing
        };

        // Record the start time
        startTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(TriggerTimedSpatialAudioPings());
        }

        // Manual pings can still be triggered if needed
        if (Input.GetKeyDown(KeyCode.R)) StartPing(northPing);
        if (Input.GetKeyDown(KeyCode.T)) StartPing(northeastPing);
        if (Input.GetKeyDown(KeyCode.Y)) StartPing(eastPing);
        if (Input.GetKeyDown(KeyCode.Z)) StartPing(southeastPing);
        if (Input.GetKeyDown(KeyCode.Y)) StartPing(southPing);
        if (Input.GetKeyDown(KeyCode.C)) StartPing(southwestPing);
        if (Input.GetKeyDown(KeyCode.F)) StartPing(westPing);
        if (Input.GetKeyDown(KeyCode.G)) StartPing(northwestPing);
    }

    private void StartPing(AudioSource audioSource)
    {
        // Stop any ongoing ping coroutine
        if (currentPingCoroutine != null)
        {
            StopCoroutine(currentPingCoroutine);
        }
        // Start a new ping coroutine
        currentPingCoroutine = StartCoroutine(RepeatPing(audioSource));
    }

    private IEnumerator RepeatPing(AudioSource audioSource)
    {
        float elapsedTime = 0f;

        while (elapsedTime < pingRepeatDuration)
        {
            audioSource.Play();
            yield return new WaitForSeconds(pingDuration); // Play for the duration
            audioSource.Stop();
            yield return new WaitForSeconds(repeatInterval); // Wait before repeating
            elapsedTime += pingDuration + repeatInterval;
        }
    }

    private IEnumerator TriggerTimedSpatialAudioPings()
    {
        for (int pingCount = 0; pingCount < 5; pingCount++) // Limit to 5 pings
        {
            // Randomly select a ping source
            AudioSource selectedPing = pingSources[Random.Range(0, pingSources.Length)];

            // Trigger the selected ping
            StartPing(selectedPing);
            float elapsedTime = Time.time - startTime; // Calculate elapsed time from start
            Debug.Log($"Ping {pingCount + 1} called at: {elapsedTime} seconds");

            // Wait for a random interval between 30 and 50 seconds before the next ping
            yield return new WaitForSeconds(Random.Range(30f, 50f));
        }
    }
}
