using System;
using UnityEngine;

public class ResourceID : MonoBehaviour
{
    public enum ResourceType
    {
        rock,
        wood,
        food = 3
    }

    public ResourceID.ResourceType resource_type;

    private void Start()
    {
    }
}
