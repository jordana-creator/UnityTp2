using UnityEngine;

[ExecuteAlways]
public class WeatherManager : MonoBehaviour
{

    public ParticleSystem rain;
    public ParticleSystem snow;

    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private WeatherPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private bool stateKeyZ;
    private bool stateKeyX;
    private bool stateKeyC;
    private bool stateKeyV;
    private bool stateButton1;
    private bool stateButton2;
    private bool stateButton3;
    private bool stateButton4;

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            UpdateInteractivity();
            // modifier l'état du bouton en fonction de l'état des touches du clavier
            UpdateButtonStates();

            // configurer l'animation de la température
            WeatherSetup();
        }
        else
        {
            UpdateWeather(TimeOfDay / 24f,1);
        }
    }

    // fonction de mise à jour de l'interactivité
    void UpdateInteractivity()
    {
        stateKeyZ = Input.GetKey(KeyCode.Z);
        stateKeyX = Input.GetKey(KeyCode.X);
        stateKeyC = Input.GetKey(KeyCode.C);
        stateKeyV = Input.GetKey(KeyCode.V);
    }

    private void UpdateWeather(float timePercent, int type)
    {
        if (type == 1)
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
    // fonction qui modifie l'état des boutons en fonction de l'état de touches du clavier
    void UpdateButtonStates()
    {
        stateButton1 = stateButton1 || stateKeyZ ? true : false;
        stateButton2 = stateButton2 || stateKeyX ? true : false;
        stateButton3 = stateButton3 || stateKeyC ? true : false;
        stateButton4 = stateButton4 || stateKeyV ? true : false;
    }
    void WeatherSetup()
    {
        print(TimeOfDay);
        // jour
        if (stateButton1)
        { 
            print("<jour>");
            if (TimeOfDay > 3 && TimeOfDay < 15)
            {
                TimeOfDay += 0.1F;
            }
            else{
                stateButton1 = false;
            }
        }
        // nuit
        if (stateButton2)
        {
            print("<nuit>");
            if (TimeOfDay > 4 && TimeOfDay < 16)
            {
                print(TimeOfDay);
                TimeOfDay -= 0.1F;
            }
            else
            {
                stateButton2 = false;
            }
        }
        // pluie
        if (stateButton3)
        {
            //rain.Emit(10000);
        }
        // soleil
        if (stateButton4)
        {
            //snow.Emit(10000);
        }
        UpdateWeather(TimeOfDay / 24f,1);
    }
}