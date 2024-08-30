using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PaintConfigData")]
public class PaintConfig : ScriptableObject
{
    public Texture2D DirtMaskTextureBase;
    public Texture2D DirtBrush;
    public int MaxPaintDistance = 7;
}
