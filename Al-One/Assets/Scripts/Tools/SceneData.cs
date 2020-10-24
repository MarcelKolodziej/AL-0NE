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
                return "Water Planet";
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
                return "Water Planet";
            case Scene.HomePlanet:
                return "HomeWorld";
        }

        return "HubWorld";
    }

    public static Scene GetEnumFromSceneString(string sceneName)
    {
        switch (sceneName)
        {
            case "HubWorld":
                return Scene.HubPlanet;
            case "LavaPlanet":
                return Scene.LavaPlanet;
            case "Tree Planet":
                return Scene.TreePlanet;
            case "Water Planet":
                return Scene.WaterPlanet;
            case "HomeWorld":
                return Scene.HomePlanet;
        }

        return Scene.HubPlanet;
    }
}
