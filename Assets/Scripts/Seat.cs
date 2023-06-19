using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public bool Occupied;
    [SerializeField] public GameObject Customer;

    [Header("References setup")]
    [SerializeField] public Transform _sittingPoint;
}
