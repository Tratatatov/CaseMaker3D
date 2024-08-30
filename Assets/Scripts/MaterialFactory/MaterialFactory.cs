using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MaterialFactory :ScriptableObject
{
    public abstract Material GetMaterial(MaterialType type);
}
public enum MaterialType
{
    Acrylic,
    Gradient,
    Glitter
}



