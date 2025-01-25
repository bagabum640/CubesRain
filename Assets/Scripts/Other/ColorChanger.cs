using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color GetColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }
}