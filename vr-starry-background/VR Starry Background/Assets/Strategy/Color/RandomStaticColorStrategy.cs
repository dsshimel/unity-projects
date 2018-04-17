using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RandomStaticColorStrategy : AbstractStrategy, IColorStrategy
{
    private IDictionary<int, Color> colorMap;
    private bool didApply;

    public RandomStaticColorStrategy(IManipulator manipulator) : base(manipulator)
    {
        colorMap = new Dictionary<int, Color>();
        foreach (int gameObjectId in gameObjectIds)
        {
            Color color = new Color(
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f),
                Random.Range(0, 1.0f));
            colorMap.Add(gameObjectId, color);
        }

        didApply = false;
    }

    public override void ApplyStrategy()
    {
        if (didApply)
        {
            return;
        }

        foreach (int gameObjectId in gameObjectIds)
        {
            this.manipulator.SetMaterialColor(gameObjectId, colorMap[gameObjectId]);
        }
        didApply = true;
    }
}