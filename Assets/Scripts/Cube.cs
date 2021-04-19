using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public enum Status
{
    NORMAL = 0,
    PRESSED
}

public class Cube : MonoBehaviour
{
    public Status status; 

    [Header("Material")]
    [SerializeField] public Material pressedMaterial;
    [SerializeField] public Material normalMaterial;

    bool isChanged = false;

    public delegate void ClickAction();
    public static event ClickAction OnIncreased;
    public static event ClickAction OnDecreased;

    // Start is called before the first frame updat

    private void Awake()
    {
        status = Status.NORMAL;
        GetComponent<Renderer>().material = normalMaterial;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (isChanged)
        {
            status = Status.NORMAL;
            GetComponent<Renderer>().material = normalMaterial;
            isChanged = false;
            OnDown();
        }
        else
        {
            status = Status.PRESSED;
            GetComponent<Renderer>().material = pressedMaterial;
            isChanged = true;
            OnUp();
        }
    }

    public static void OnUp()
    {
        if (OnIncreased != null)
        {
            OnIncreased();
        }
    }

    public static void OnDown()
    {
        if (OnDecreased != null)
        {
            OnDecreased();
        }
    }
}
