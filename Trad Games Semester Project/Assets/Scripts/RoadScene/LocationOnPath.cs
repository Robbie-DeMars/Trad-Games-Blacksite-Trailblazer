using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationOnPath", menuName = "Scriptable Objects/LocationOnPath")]
public class LocationOnPath : ScriptableObject
{
    public List<Tuple<string, float>> _event_and_when = null;
}
