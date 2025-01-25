using System;

public interface IObject<T> where T : IObject<T>
{
    public event Action<T> Destroyed;

    public void ResetToDefault();
}