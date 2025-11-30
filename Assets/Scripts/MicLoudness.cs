using UnityEngine;

public class MicLoudness : MonoBehaviour
{
    public static float loudness;  
    private AudioClip micClip;

    public int sampleWindow = 128; 
    public string micName;

    void Start()
    {
        // Use default microphone
        if (Microphone.devices.Length > 0)
        {
            micName = Microphone.devices[0];
            micClip = Microphone.Start(micName, true, 1, 44100);
        }
        else
        {
            Debug.LogError("No microphone found!");
        }
    }

    void Update()
    {
        loudness = GetLoudnessFromMic();
    }

    float GetLoudnessFromMic()
    {
        if (micClip == null) return 0f;

        float[] data = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(micName) - sampleWindow;
        if (micPosition < 0) return 0;

        micClip.GetData(data, micPosition);

        float total = 0;
        foreach (float s in data)
            total += Mathf.Abs(s);

        return total / sampleWindow;
    }
}
