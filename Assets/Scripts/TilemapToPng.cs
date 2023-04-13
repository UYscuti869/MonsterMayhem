using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(TilemapToPng))]
public class TilemapToPngEditor : Editor
{
    string nombre = "";

    public override void OnInspectorGUI ()
    {
        TilemapToPng GTM = (TilemapToPng)target;


        //DrawDefaultInspector();

        if (GTM.ImagenLista == null)
        {
            if (GUILayout.Button("Empacar como PNG"))
            {
                GTM.Empacar();
            }
        }
        else
        {
            GUILayout.Label("Nombre del archivo");
            nombre = GUILayout.TextField(nombre);
            if(nombre.Length > 0)
            {
                if (GUILayout.Button("Exportar archivo"))
                {
                    GTM.ExportarPng(nombre);
                }
            }
            
        }
            
        
    }

}
#endif



    public class TilemapToPng : MonoBehaviour
{

    Tilemap tm;

    int minX = 0;
    int maxX = 0;
    int minY = 0;
    int maxY = 0;
    
    public Texture2D ImagenLista;

    public void Empacar()
    {
        tm = GetComponent<Tilemap>();
        Sprite SpriteCualquiera = null;


        for (int x = 0; x < tm.size.x; x++) //Hallamos el punto menor y mayor
        {
            for (int y = 0; y < tm.size.y; y++)
            {
                Vector3Int pos = new Vector3Int(-x, -y, 0);
                if (tm.GetSprite(pos) == null)
                {
                    print("no hay sprite en esta pos");
                }
                else
                {
                    SpriteCualquiera = tm.GetSprite(pos); //seleccionamos un sprite cualquiera para más tarde saber las dimensiones de los sprites
                    if (minX > pos.x)
                    {
                        minX = pos.x;
                    }
                    if (minY > pos.y)
                    {
                        minY = pos.y;
                    }
                }

                pos = new Vector3Int(x, y, 0);
                if (tm.GetSprite(pos) == null)
                {
                    //print("no hay sprite en esta pos");
                }
                else
                {
                    if (maxX < pos.x)
                    {
                        maxX = pos.x;
                    }
                    if (maxY < pos.y)
                    {
                        maxY = pos.y;
                    }
                }
            }
        }
        // 육각형 타일의 크기 계산
        float tileWidth = SpriteCualquiera.rect.width / SpriteCualquiera.pixelsPerUnit;
        float tileHeight = SpriteCualquiera.rect.height / SpriteCualquiera.pixelsPerUnit;

        // 육각형 타일 간격 계산
        float halfTileWidth = tileWidth / 2f;
        float halfTileHeight = tileHeight / 2f;
        float spacingX = halfTileWidth;
        float spacingY = (tileHeight * 3f) / 4f;

        float hexWidth = SpriteCualquiera.bounds.size.x;
        float hexHeight = SpriteCualquiera.bounds.size.y;
        float offsetX = hexWidth * 0.75f; // 육각형 타일의 x 오프셋 계산
        float offsetY = hexHeight * 0.5f; // 육각형 타일의 y 오프셋 계산
        // offsetX와 offsetY 계산
        //float offsetX = tileWidth + spacingX;
        //float offsetY = halfTileHeight + spacingY;
        // 크기가 조정된 텍스처를 생성합니다.
        Texture2D Imagen = new Texture2D(Mathf.Abs(maxX - minX) + 1, Mathf.Abs(maxY - minY) + 1);
        int totalWidth = Mathf.RoundToInt(((maxX - minX + 1) * offsetX) + (offsetX * 0.5f));
        int totalHeight = Mathf.RoundToInt((maxY - minY + 1) * offsetY);

       
        //Asignamos toda la imagen invisible
        Color[] invisible = new Color[Imagen.width * Imagen.height];
        for (int i = 0; i < invisible.Length; i++)
        {
            invisible[i] = new Color(0f, 0f, 0f, 0f);
        }
        Imagen.SetPixels(0,0,Imagen.width, Imagen.height, invisible);
        
        
        

        //Hallamos el tamaño del sprite en pixeles
        float width = SpriteCualquiera.rect.width;
        float height = SpriteCualquiera.rect.height;


        //creamos una textura con el tamaño multiplicado por el numero de celdas
        //Texture2D Imagen = new Texture2D((int)width * tm.size.x, (int)height * tm.size.y);
        // 육각형 타일의 크기와 간격을 고려하여 전체 픽셀 크기를 계산합니다.
        
        
        

        //Ahora asignamos a cada bloque sus respectivos pixeles
        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                Sprite currentSprite = tm.GetSprite(pos);
                if (currentSprite != null)
                {
                    Texture2D spriteTexture = currentSprite.texture;
                    int spriteWidth = spriteTexture.width;
                    int spriteHeight = spriteTexture.height;

                    // 스프라이트 크기가 기대한 크기와 다른 경우 텍스처 크기를 조절합니다.
                    if (spriteWidth != (int)width || spriteHeight != (int)height)
                    {
                        Texture2D resizedTexture = new Texture2D(spriteWidth, spriteHeight);
                        resizedTexture.SetPixels(spriteTexture.GetPixels());
                        resizedTexture.Apply();
                        spriteTexture = resizedTexture;
                    }

                    // 육각형 타일의 위치 계산
                    float worldX = (x * offsetX) + ((y % 2 == 0) ? 0 : offsetX * 0.5f);
                    float worldY = y * offsetY;

                    int pixelX = Mathf.RoundToInt(worldX * currentSprite.pixelsPerUnit);
                    int pixelY = Mathf.RoundToInt(worldY * currentSprite.pixelsPerUnit);

                    // 텍스처의 miplevel을 0으로 설정하여 레벨이 일치하도록 합니다.
                    Imagen.SetPixels(pixelX, pixelY, spriteWidth, spriteHeight, spriteTexture.GetPixels(), 0);
                }
            }
        }
        Imagen.Apply();

        ImagenLista = Imagen; //Almacenamos la textura de la imagen lista
    }

    Texture2D GetCurrentSprite(Sprite sprite)
    {
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                            (int)sprite.textureRect.y,
                                            (int)sprite.textureRect.width,
                                            (int)sprite.textureRect.height);

        Texture2D textura = new Texture2D((int)sprite.textureRect.width,
                                            (int)sprite.textureRect.height);

        textura.SetPixels(pixels);
        textura.Apply();
        return textura;
    }

     public void ExportarPng (string nombre) //metodo que exporta como png
     {
         byte[] bytes = ImagenLista.EncodeToPNG();
         var dirPath = Application.dataPath + "/Exported Tilemaps/";
         if (!Directory.Exists(dirPath))
         {
             Directory.CreateDirectory(dirPath);
         }
         File.WriteAllBytes(dirPath + nombre + ".png", bytes);
        ImagenLista = null;
     }

}
