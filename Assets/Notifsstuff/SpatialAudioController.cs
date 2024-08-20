using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAudioController : MonoBehaviour
{
    public AudioSource northPing;   // Assign these in the inspector
    public AudioSource southPing;
    public AudioSource eastPing;
    public AudioSource westPing;
    public AudioSource northeastPing;
    public AudioSource northwestPing;
    public AudioSource southeastPing;
    public AudioSource southwestPing;

    public float pingDuration = 1.0f; //duration the ping sound will play for
    public float repeatInterval = 0.5f; //Interval between pings
    public float pingRepeatDuration = 5.0f; //total duration for repeated pings

    private Coroutine currentPingCoroutine;
    private Coroutine timedPingCoroutine;

    private void Start()
    {
        // Start the timed spatial audio pings coroutine
        timedPingCoroutine = StartCoroutine(TriggerTimedSpatialAudioPings());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartPing(northPing);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartPing(northeastPing);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartPing(eastPing);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartPing(southeastPing);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartPing(southPing);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartPing(southwestPing);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartPing(westPing);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartPing(northwestPing);
        }
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
        while (true)
        {
            // Trigger audio pings at the specified times
            yield return new WaitForSeconds(285f);
            StartPing(southeastPing); // South-East
            yield return new WaitForSeconds(35f);
            StartPing(northwestPing); // North-West
            yield return new WaitForSeconds(40f);
            StartPing(southwestPing); // South-West
            yield return new WaitForSeconds(40f);
            StartPing(eastPing); // East
            yield return new WaitForSeconds(50f);
            StartPing(westPing); // West

            // Wait for the next cycle to start
            yield return new WaitForSeconds(300f); // Adjust the wait time as needed
        }
    }
}