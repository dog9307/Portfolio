using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Calcatz.ArrivalGUI;

public class PlayerInteractiveIcon : FadingMenuBase
{
    private void Awake()
    {
        //Add all fade-able graphics
        List<Graphic> graphics = new List<Graphic>();
        graphics.AddRange(GetComponentsInChildren<Graphic>());
        InitializeGraphicAlphas(graphics);

        Close();
    }
}
