using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotData", menuName = "ShotDate")]
public class ShotData : ScriptableObject
{
    public List<Vector2> mousePositions = new List<Vector2>();
    public List<Vector2> enemiesPosition = new List<Vector2>();


}
