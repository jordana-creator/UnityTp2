using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Weather Preset", menuName = "Scriptables/Weather Preset", order = 1)]
public class WeatherPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}