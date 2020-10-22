using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    public enum Scene : int
    {
        HubPlanet,
        LavaPlanet,
        TreePlanet,
        WaterPlanet,
        HomePlanet,
    }

    public static string GetSceneStringFromEnum(Scene scene)
    {
        switch (scene)
        {
            case Scene.HubPlanet:
                return "HubWorld";
            case Scene.LavaPlanet:
                return "LavaPlanet";
            case Scene.TreePlanet:
                return "Tree Planet";
            case Scene.WaterPlanet:
                Debug.LogError("Please Assign Level");
                return "HubWorld";
            case Scene.HomePlanet:
                return "HomeWorld";
        }

        return "HubWorld";
    }

    public static string GetSceneStringFromInt(int sceneIndex)
    {
        switch ((Scene)sceneIndex)
        {
            case Scene.HubPlanet:
                return "HubWorld";
            case Scene.LavaPlanet:
                return "LavaPlanet";
            case Scene.TreePlanet:
                return "Tree Planet";
            case Scene.WaterPlanet:
                Debug.LogError("Please Assign Level");
                return string.Empty;
        }

        return "HubWorld";
    }
}
