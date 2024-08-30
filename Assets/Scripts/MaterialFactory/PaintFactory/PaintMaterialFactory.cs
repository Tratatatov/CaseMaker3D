using UnityEngine;
[CreateAssetMenu(fileName = "PaintMaterialFactoryConfig", menuName = "Paint Factory")]

public class PaintMaterialFactory : MaterialFactory
{

    [SerializeField] private Texture2D _dirtTexture;
    [SerializeField] private Texture2D _glitterTexture;
    [SerializeField] private Texture2D _acrylicTexture;
    [SerializeField] private Texture2D _gradientTexture;
    [SerializeField] private Texture2D _maskTexture;
    [SerializeField] private Shader _paintShader;

    public override Material GetMaterial(MaterialType type)
    {
        Material material = new Material(_paintShader);
        material.SetTexture(TextureNames.DirtTex, _dirtTexture);
        material.SetTexture(TextureNames.DirtMask, _maskTexture);
        switch (type)
        {
            case (MaterialType.Glitter):
                material.SetTexture(TextureNames.MainTex, _glitterTexture);
                break;

            case (MaterialType.Acrylic):
                material.SetTexture(TextureNames.MainTex, _acrylicTexture);
                break;

            case (MaterialType.Gradient):
                material.SetTexture(TextureNames.MainTex, _gradientTexture);

                break;
        }
        material.SetTexture(TextureNames.DirtMask, GetNewMask());
        return material;
    }
    private Texture2D GetNewMask()
    {
        Texture2D maskTexture = new Texture2D(_maskTexture.width, _maskTexture.height); ;
        maskTexture.SetPixels(_maskTexture.GetPixels());
        maskTexture.Apply();

        return maskTexture;
    }

}

