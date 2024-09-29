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
    private float startTime;
    private AudioSource[] pingSources;

    private void Start()
    {
        // Start the timed spatial audio pings coroutine
        //timedPingCoroutine = StartCoroutine(TriggerTimedSpatialAudioPings());
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
        if (Input.GetKeyDown(KeyCode.Z))
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

    // private IEnumerator TriggerTimedSpatialAudioPings()
    // {
    //     while (true)
    //     {
    //         // Trigger audio pings at the specified times
    //         yield return new WaitForSeconds(0f); // Wait for 1 minute
    //         StartPing(southeastPing); // South-East
    //         Debug.Log("ping!");

    //         yield return new WaitForSeconds(45f); // Now at 1:45 (105 seconds in total)
    //         StartPing(northwestPing); // North-West
    //         Debug.Log("ping!");

    //         yield return new WaitForSeconds(45f); // Now at 2:30 (150 seconds in total)
    //         StartPing(southwestPing); // South-West
    //         Debug.Log("ping!");

    //         yield return new WaitForSeconds(35f); // Now at 3:05 (185 seconds in total)
    //         StartPing(eastPing); // East
    //         Debug.Log("ping!");

    //         yield return new WaitForSeconds(50f); // Now at 3:55 (235 seconds in total)
    //         StartPing(westPing); // West
    //         Debug.Log("ping!");

    //         // Wait for the next cycle to start
    //         yield return new WaitForSeconds(0f); // Start immediately or modify as needed
    //     }
    // }

    private IEnumerator TriggerTimedSpatialAudioPings()
    {
        while (true)
        {
            // Pick a random interval between 30 and 50 seconds
            float randomInterval = Random.Range(30f, 50f);
            
            // Trigger a random audio ping (you can add a mechanism to randomly choose a ping if needed)
            StartPing(southeastPing); // Example: triggering Southeast ping
            Debug.Log("ping!");

            // Wait for the random interval before the next ping
            yield return new WaitForSeconds(randomInterval);
            
            // Repeat for additional pings if needed
            randomInterval = Random.Range(30f, 50f);
            StartPing(northwestPing); // Example: triggering Northwest ping
            Debug.Log("ping!");

            yield return new WaitForSeconds(randomInterval);
            
            randomInterval = Random.Range(30f, 50f);
            StartPing(southwestPing); // Example: triggering Southwest ping
            Debug.Log("ping!");

            yield return new WaitForSeconds(randomInterval);
            
            randomInterval = Random.Range(30f, 50f);
            StartPing(eastPing); // Example: triggering East ping
            Debug.Log("ping!");

            yield return new WaitForSeconds(randomInterval);
            
            randomInterval = Random.Range(30f, 50f);
            StartPing(westPing); // Example: triggering West ping
            Debug.Log("ping!");

            // Wait for the next cycle to start with a random interval
            yield return new WaitForSeconds(Random.Range(30f, 50f)); // Start next cycle
        }
    }


}