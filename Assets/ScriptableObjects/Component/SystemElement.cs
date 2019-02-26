using System;
using UnityEngine;

[Serializable]
public class SystemElement
{
    //Jaki jest typ danego elementu - odpowiada za obrazek
    public SystemElementType Type { get; private set; }

    //Do jakiego regionu przynalezy - odpowiada za kolor
    public SystemElementControl Control { get; private set; }

    //Ilość pól, które element zajmuje
    public int Size { get; private set; }

    private Modifiers modifiers;

    public int DestroyReward
    {
        get { return Mathf.CeilToInt(PowerModifier * modifiers.DestroyReward); }
    }

    // Ilość punktów akcji dodawana co turę.
    public int HackReward
    {
        get { return Mathf.CeilToInt(PowerModifier * modifiers.HackReward); }
    }

    //sparametryzowac od czegos koszty hackowania, niszczenia, zdobycia
    public int HackCost
    {
        get
        {
            float modifier = PowerModifier * (1 - Control.OccupationDiscount);
            return Mathf.RoundToInt(modifier * modifiers.HackCost);
        }
    }

    public int DestroyCost
    {
        get
        {
            float modifier = PowerModifier * (1 - Control.OccupationDiscount);
            return Mathf.RoundToInt(modifier * modifiers.DestroyCost);
        }
    }

    public int UnhackCost
    {
        get
        {
            float modifier = PowerModifier * (1 - Control.OccupationDiscount);
            return Mathf.RoundToInt(modifier * modifiers.UnhackCost);
        }
    }

    public string Name { get; private set; }

    public Player OwnerVirus;
    public bool IsDestroyed, IsHacked;

    public SystemElementController Controller;


    // Bazowy wyznacznik kosztów związany z tym elementem. Będzie także używany do wyzaczania bonusów związanych z hackowaniem i niszczeniem.
    public int PowerModifier { get; private set; }

    public SystemElement(SystemElementControl control, SystemElementType type, int size, Modifiers modifiers)
    {
        Control = control;
        Type = type;
        Size = size;
        this.modifiers = modifiers;

        Name = control.Name + " " + type.Name;

        float modifier = (Type.Power + Control.Power) * Size;
        PowerModifier = Mathf.RoundToInt(modifier);
    }


    public bool IsHackedBy(VirusValue virus)
    {
        return IsHacked && OwnerVirus == virus.Value;
    }

    public bool IsHackedBy(Player virus)
    {
        return IsHacked && OwnerVirus == virus;
    }

    public bool IsDestroyedBy(VirusValue virus)
    {
        return IsDestroyed && OwnerVirus == virus.Value;
    }
}
