using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPlayer : MonoBehaviour
{
    [TextArea(1, 100), Tooltip("Doesn't do anything. Just comments shown in inspector")]
    public string Notes = "[HOW DOES IT WORKS]\n" +
                  "In this script there are two main arrays, audioClips & timeSplits, DON'T FILL BOTH THE ARRAYS, USE ONLY ONE OF THEM.\n" +
                  "The first array, audioClips, constains a list of audio that will be played for the same object, for example: for a guy, you want multiple sounds, like: ciao,come stai?,al� com'e?. Instead of just: ciao,ciao,ciao.\n" +
                  "The content of this array will be picked at random, just to spice up an object and not make it too much static.\n" +
                  "The second array, timeSplits, you need to set a particular audio in the <AudioSource> Component located in the same GameObject of <audioPlayer> Component.\n" +
                  "This array will constain all the time locations inside of the audio in the <AudioSource> Component. For example: we have a very long audio like 3 min long and we are lazy as fuck and don't want to cut it properly inside of an actualy audio editor program " +
                  "so we set the start locations of every single piece of sound constained inside the 3 min audio.\n" +
                  "If you need to play just one audio, fill the timeSplits array and set timeSplits to 0 i guess haven't tried it but it should works\n" +
                  "please i swear to my dog, DON'T USE BOTH OF THE ARRAYS AT ONCE.";

    [Space,Space,Header("USE ONLY ONE, NOT BOTH")]
    [Tooltip("Contains the audio clips that will be randomly played inside the <AudioSource> Component.")]
    public List<AudioClip> audioClips;
    [Space, Tooltip("Constains all the random time locations where to start the <AudioSource> Component.")]
    public List<float> timeSplits;
    [Space,Tooltip("If the audio can be stopped and playerd again even if it's not finished playing")]
    public bool overrideEnd = false;

    bool isTime = false;
    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        if (source == null) Debug.LogError("No AudioSource found. Please add <AudioSource> Component inside the same GameObject with <AudioSource> Component.");
        if (audioClips.Count==0) isTime = true;

        if(audioClips.Count != 0 && timeSplits.Count != 0) Debug.LogError("I said, DON'T USE BOTH OF THE ARRAYS AT ONCE for <AudioSource> Component. That's the result, have fun.");
    }

    //function that need to be called by other functionsa that need to play an audio
    public void playAudio()
    {
        if(overrideEnd) {
            source.Stop();
        }

        if (!source.isPlaying)
        { 
            int randomAudio;

            if (!isTime)
            {
                randomAudio = Random.RandomRange(0, audioClips.Count - 1);
                source.clip = audioClips[randomAudio];
            }
            else { randomAudio = (int)Random.RandomRange(0f, timeSplits.Count - 1f); }

            if (isTime)
                source.time=timeSplits[randomAudio];

            source.Play();
        }
    }

    public void stopAudio() { if (source.isPlaying) source.Stop(); }
}
