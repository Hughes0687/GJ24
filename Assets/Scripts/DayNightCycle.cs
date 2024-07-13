using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // Reference to the directional light
    public float dayDuration = 60f; // Duration of a full day in seconds
    public float nightIntensity = 0.2f; // Intensity of the light at night
    public float dayIntensity = 1.0f; // Intensity of the light during the day

    private float time;

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>();
        }

        time = 0f;
    }

    void Update()
    {
        // Update the time
        time += Time.deltaTime;
        if (time > dayDuration)
        {
            time = 0f;
        }

        // Calculate the intensity
        float intensity = Mathf.Lerp(nightIntensity, dayIntensity, Mathf.PingPong(time / dayDuration * 2, 1));
        directionalLight.intensity = intensity;
    }
}
