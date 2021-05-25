using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Level specified values are stored and can be altered here.
/// </summary>

[Serializable]
public class LevelSettings
{
    public int MovementSpeed;    // Road movement speed
    public Color PlayerColor;    // Player Color
    public Color CornColor;    // Corn Color
    public Color ObstacleColor;    // Obstacle Color
    // public Color RoadColor;
}
