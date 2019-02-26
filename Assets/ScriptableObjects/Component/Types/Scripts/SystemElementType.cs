using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SystemElementType : ScriptableObject {

    [SerializeField]
    private Types systemElementType;

    public Types Type { get { return systemElementType; } }

    //Ważność/ moc jaka dajemy danemu typowi
    [SerializeField]
    private float power;

    public float Power { get { return power; } }

    [SerializeField]
    private new string name;

    public string Name { get { return name; } }

    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    [SerializeField]
    private Sprite destroyedSprite;
    public Sprite DestroyedSprite { get { return destroyedSprite; } }
}
