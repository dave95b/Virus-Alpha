using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SystemElementControl : ScriptableObject {

    [SerializeField]
    private Controls systemElementControl;

    public Controls Control { get { return systemElementControl; } }
    
    //Albo całą liste i potem piętro wyżej szukanie po swoim terytorium?
    //Musi wiedziec, jaka część jego terytorium ma zajęte wirus
    [SerializeField]
    private PercentageOccupation PercentageOccupation;

    //Ważność/ moc jaka dajemy danemu typowi
    [SerializeField]
    private float power;

    //Moc zalezna od okupacji czy osobna zmienna?
    public float Power { get { return power; } }
    
    //teraz trzeba to jakoś uzależnić
    public float OccupationDiscount
    {
        get { return PercentageOccupation.GetPercentageOccupation(this) / 2; } //because Milena said so
    }


    [SerializeField]
    private new string name;

    public string Name { get { return name; } }

    [SerializeField]
    private Color color;
    public Color Color { get { return color; } }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }


    public override bool Equals(object other)
    {
        if (other == null || !(other is SystemElementControl))
            return false;

        var otherControl = other as SystemElementControl;

        return systemElementControl == otherControl.systemElementControl;
    }

    public override int GetHashCode()
    {
        return (int)systemElementControl;
    }
}
