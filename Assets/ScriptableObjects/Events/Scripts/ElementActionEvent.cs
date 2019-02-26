using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementActionEvent", menuName = "Gameplay/Events/ElementActionEvent", order = 3)]
public class ElementActionEvent : NativeEvent<SystemElementController, VirusValue> { }
