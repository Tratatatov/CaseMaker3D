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
                Vector2 textureCoord = raycastHit.textureCoord;// куда ударяется луч на координатах текстуры(в формате от 0 до 1)

                int pixelX = (int)(textureCoord.x * _dirtMaskTexture.width); // конкретная позиция на текстуре
                int pixelY = (int)(textureCoord.y * _dirtMaskTexture.height);

                Vector2Int paintPixelPosition = new Vector2Int(pixelX, pixelY); //  итак позиция по x и y

                // расчитывается расстояние между последним и новым пикселем

                int paintPixelDistance = Mathf.Abs(paintPixelPosition.x - _lastPaintPixelPosition.x) + Mathf.Abs(paintPixelPosition.y - _lastPaintPixelPosition.y);
                if (paintPixelDistance < _maxPaintDistance) // если расстояние между текущим и последним пикселем больше максимального расстояния..
                {
                    return;
                }
                _lastPaintPixelPosition = paintPixelPosition; // в последнюю точку записывается текущий




                int pixelXOffset = pixelX - (_dirtBrush.width / 2); // вычисляем, где будет находиться середина кисти
                int pixelYOffset = pixelY - (_dirtBrush.height / 2); 

                for (int x = 0; x < _dirtBrush.width; x++)
                {
                    for (int y = 0; y < _dirtBrush.height; y++)
                    {
                        Color pixelDirt = _dirtBrush.GetPixel(x, y); 
                        Color pixelDirtMask = _dirtMaskTexture.GetPixel(pixelXOffset + x, pixelYOffset + y);
                        // там где green = 0 в текстуре кисти - там же будет 0 в текстуре маски
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
