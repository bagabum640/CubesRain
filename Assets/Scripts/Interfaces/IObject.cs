using System;

public interface IObject<T> where T : IObject<T>
{
    public event Action<T> IsDestroyed;

    public void Reset();
}