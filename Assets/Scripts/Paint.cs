using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Paint : MonoBehaviour
{

    [SerializeField] private Texture2D _dirtMaskTextureBase; 
    [SerializeField] private Texture2D _dirtBrush;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _maxPaintDistance =7;

    private Texture2D _dirtMaskTexture;
    private Vector2Int _lastPaintPixelPosition;

    private void Awake()
    {
        _dirtMaskTexture = new Texture2D(_dirtMaskTextureBase.width, _dirtMaskTextureBase.height); 
        _dirtMaskTexture.SetPixels(_dirtMaskTextureBase.GetPixels()); 
        _dirtMaskTexture.Apply(); 
        _renderer.material.SetTexture("_DirtMask", _dirtMaskTexture); 
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit))
            {
                Vector2 textureCoord = raycastHit.textureCoord;// ���� ��������� ��� �� ����������� ��������(� ������� �� 0 �� 1)

                int pixelX = (int)(textureCoord.x * _dirtMaskTexture.width); // ���������� ������� �� ��������
                int pixelY = (int)(textureCoord.y * _dirtMaskTexture.height);

                Vector2Int paintPixelPosition = new Vector2Int(pixelX, pixelY); //  ���� ������� �� x � y

                // ������������� ���������� ����� ��������� � ����� ��������

                int paintPixelDistance = Mathf.Abs(paintPixelPosition.x - _lastPaintPixelPosition.x) + Mathf.Abs(paintPixelPosition.y - _lastPaintPixelPosition.y);
                if (paintPixelDistance < _maxPaintDistance) // ���� ���������� ����� ������� � ��������� �������� ������ ������������� ����������..
                {
                    return;
                }
                _lastPaintPixelPosition = paintPixelPosition; // � ��������� ����� ������������ �������




                int pixelXOffset = pixelX - (_dirtBrush.width / 2); // ���������, ��� ����� ���������� �������� �����
                int pixelYOffset = pixelY - (_dirtBrush.height / 2); 

                for (int x = 0; x < _dirtBrush.width; x++)
                {
                    for (int y = 0; y < _dirtBrush.height; y++)
                    {
                        Color pixelDirt = _dirtBrush.GetPixel(x, y); 
                        Color pixelDirtMask = _dirtMaskTexture.GetPixel(pixelXOffset + x, pixelYOffset + y);
                        // ��� ��� green = 0 � �������� ����� - ��� �� ����� 0 � �������� �����
                        _dirtMaskTexture.SetPixel(
                            pixelXOffset + x,
                            pixelYOffset + y,
                            new Color(0, pixelDirtMask.g * pixelDirt.g, 0)
                        );
                    }
                }


                _dirtMaskTexture.Apply();
            }
        }

    }
}
