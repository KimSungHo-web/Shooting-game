using UnityEngine;

public enum ParitcleType
{
    None,
    Rain,
    Snow
}

public class WeatherManager : MonoBehaviour
{
    public ParticleSystem rain;
    public ParticleSystem snow;

    void Awake()
    {
        rain = GameObject.Find("Rain").GetComponent<ParticleSystem>();
        snow = GameObject.Find("Snow").GetComponent<ParticleSystem>();
    }

    void Start()
    {
        rain.Stop();
        snow.Stop();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetParticle(ParitcleType.None);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            SetParticle(ParitcleType.Rain);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SetParticle(ParitcleType.Snow);
        }
    }

    private void SetParticle(ParitcleType particle)
    {
        switch (particle)
        {
            case ParitcleType.None:
                rain.Stop();    snow.Stop();    break;
            case ParitcleType.Rain:
                snow.Stop();    rain.Play();    break;
            case ParitcleType.Snow:
                rain.Stop();    snow.Play();    break;
        }
    }
}
