using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Time : MonoBehaviour
{
    public enum Time
    {
        Day,
        Night
    }

    [SerializeField] private Time time;
    [SerializeField] private float world_time;
}
