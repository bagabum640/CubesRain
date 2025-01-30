using UnityEngine;

public class TransparencyChanger
{ 
    private readonly Renderer _renderer;

    public TransparencyChanger(Renderer renderer)
    {
        _renderer = renderer;
    }

    public void SetAlpha(float alpha)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }
}
