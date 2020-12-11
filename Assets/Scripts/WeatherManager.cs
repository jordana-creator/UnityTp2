using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class WeatherManager : MonoBehaviour
{

    public ParticleSystem Rain;
    public ParticleSystem Snow;
    public Button Btn_day;
    public Button Btn_night;
    public Button Btn_rain;
    public Button Btn_snow;

    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private WeatherPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    public int state;


    public void Start()
    {
        state = 0;
        Btn_day.onClick.AddListener(SetDayLight);
        Btn_night.onClick.AddListener(SetNightLight);
        Btn_rain.onClick.AddListener(LetItRain);
        Btn_snow.onClick.AddListener(LetItSnow);

        Rain.Stop();
        Snow.Stop();
    }
    public void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            WeatherSetup();
        }
        else
        {
            UpdateWeather(TimeOfDay / 24f);
        }
    }
    public void SetDayLight()
    {
        state = 1;
        
    }
    public void SetNightLight()
    {
        state = 2;
    }
    public void LetItRain()
    {
        state = 3;
    }
    public void LetItSnow()
    {
        state = 4;
    }
    private void UpdateWeather(float timePercent)
    {
       
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }
    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for Weather tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
    void WeatherSetup()
    {
        //print(TimeOfDay);
        if (state == 1 || state == 2)
        {
            // jour
            if (state == 1)
            {
                //print("<jour>");
                if (TimeOfDay > 3 && TimeOfDay < 15)
                {
                    TimeOfDay += 0.1F;
                }
                else
                {
                    state = 0;
                }
                Rain.Stop();
                Snow.Stop();
            }
            // nuit
            if (state == 2)
            {
                //print("<nuit>");
                if (TimeOfDay > 4 && TimeOfDay < 16)
                {
                    print(TimeOfDay);
                    TimeOfDay -= 0.1F;
                }
                else
                {
                    state = 0;
                }
            }
            UpdateWeather(TimeOfDay / 24f);  
        }
        else
        {
            // pluie
            if (state == 3)
            {
                Rain.Play();
                Snow.Stop();
            }
            // neige
            if (state == 4)
            {
                Snow.Play();
                Rain.Stop();
            }
        }  
    }
}