using UnityEngine;
[CreateAssetMenu(fileName = "SimpleMaterialFactoryConfig", menuName = "Simple Factory")]

public class SimpleMaterialFactory : MaterialFactory
{
    [SerializeField] private Texture2D _glitterTexture;
    [SerializeField] private Texture2D _acrylicTexture;
    [SerializeField] private Texture2D _gradientTexture;
    [SerializeField] private Shader _shader;
    private void Awake()
    {
    }
    public override Material GetMaterial(MaterialType type)
    {
        _shader = Shader.Find("Universal Render Pipeline/Lit");
        Material material = new Material(_shader);
        switch (type)
        {
            case (MaterialType.Glitter):
                material.mainTexture = _glitterTexture;
                break;

            case (MaterialType.Acrylic):
                material.mainTexture = _acrylicTexture;
                break;

            case (MaterialType.Gradient):
                material.mainTexture = _gradientTexture;
                break;
        }
        return material;
    }
}


