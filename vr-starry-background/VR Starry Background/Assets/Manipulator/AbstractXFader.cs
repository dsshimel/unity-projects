using UnityEngine;
using UnityEditor;

public abstract class AbstractXFader<T> : IXFader<T>
{
    protected T valueOne;
    protected T valueTwo;
    protected float percentOne;

    public AbstractXFader(T valueOne, T valueTwo, float percentOne)
    {
        if (percentOne < 0 || percentOne > 1)
        {
            throw new System.ArgumentException("percent must be between 0 and 1");
        }
        this.valueOne = valueOne;
        this.valueTwo = valueTwo;
        this.percentOne = percentOne;
    }

    protected float PercentTwo()
    {
        return 1 - percentOne;
    }

    public abstract T GetXFadeValue();
}