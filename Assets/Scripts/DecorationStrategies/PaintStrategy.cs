using System;
using UnityEngine;
[Serializable]
public class PaintStrategy : DecorationStrategy
{
    private PaintConfig _paintConfigData;

    [SerializeField] private Texture2D _dirtMaskTexture;
    [SerializeField] private Texture2D _dirtBrush;

    private Renderer _renderer;

    [SerializeField] private Texture2D _tempDirtMaskTexture;
    private Vector2Int _lastPaintPixelPosition;
    private int _maxPaintDistance;
    private Camera _camera;
    private Material _material;
    public PaintStrategy(PaintConfig paintConfigData)
    {
        _paintConfigData = paintConfigData;
        _dirtMaskTexture = paintConfigData.DirtMaskTextureBase;
        _dirtBrush = paintConfigData.DirtBrush;
    }
    public void Init(Renderer renderer)
    {
        _renderer = renderer;
        _camera = Camera.main;
    }

    public override void Enter()
    {
        _tempDirtMaskTexture = (Texture2D)_renderer.material.GetTexture(TextureNames.DirtMask);
        Debug.Log("Paint mode Entered");
    }

    public override void Exit()
    {

    }

    public override void Update()
    {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Vector2 textureCoord = hit.textureCoord;// ���� ��������� ��� �� ����������� ��������(� ������� �� 0 �� 1)

                int pixelX = (int)(textureCoord.x * _dirtMaskTexture.width); // ���������� ������� �� ��������
                int pixelY = (int)(textureCoord.y * _dirtMaskTexture.height);

                Vector2Int paintPixelPosition = new Vector2Int(pixelX, pixelY); //  ���� ������� �� x � y

                int paintPixelDistance = Mathf.Abs(paintPixelPosition.x - _lastPaintPixelPosition.x) + Mathf.Abs(paintPixelPosition.y - _lastPaintPixelPosition.y);
                if (paintPixelDistance < _maxPaintDistance) // ���� ���������� ����� ������� � ��������� �������� ������ ������������� ����������..
                {
                    return;
                }
                _lastPaintPixelPosition = paintPixelPosition; // � ��������� ����� ������������ �������

                int pixelXOffset = pixelX - (_dirtBrush.width / 2);
                int pixelYOffset = pixelY - (_dirtBrush.height / 2);

                for (int x = 0; x < _dirtBrush.width; x++)
                {
                    for (int y = 0; y < _dirtBrush.height; y++)
                    {
                        Color pixelDirt = _dirtBrush.GetPixel(x, y);
                        Color pixelDirtMask = _tempDirtMaskTexture.GetPixel(pixelXOffset + x, pixelYOffset + y);
                        _tempDirtMaskTexture.SetPixel(
                            pixelXOffset + x,
                            pixelYOffset + y,
                            new Color(0, pixelDirtMask.g * pixelDirt.g, 0)
                        );
                    }
                }

                _tempDirtMaskTexture.Apply();
            }
        }
    }
}
