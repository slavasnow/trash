using UnityEngine;
using UnityEngine.Tilemaps;
/// <summary>
/// Генерация шума и последующее преобразование в карту
/// </summary>
public class MapGenerate : MonoBehaviour
{
    //Простое поле для тестированимя на тайлах
    [Header("Simple Generate")]
    public Tilemap map; 
    public Tile tile;
    
    public float catPlane;//хз для чего
    public float intesiviti;//хз для чего
    
    [Header("Noise")]
    //Генерация текстуры 
    public Vector2Int size; //Размер
    public float zoom; //Зум
    public Vector2 offset; //смещение

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DifficultGenerate();
    }

    // Сложная генерация мира с использованием текстуры и шума перлина
    void NoiseGenerate()
    {
        Texture2D texture = new Texture2D(size.x, size.y); //задаем текстуру необходимых рамеров
        
        //двумерный массив который создает сам объект 
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                float gradient = Mathf.PerlinNoise((x+offset.x) / zoom, (y+offset.y) / zoom); //Создаем градиент с помощью шума перлина
                //texture.SetPixel(x, y, new UnityEngine.Color(gradient, gradient, gradient, 1)); //создаем текстуру простую
                texture.SetPixel(x,y, GetColorByIntensive(gradient)); //Создаем цветную основу
            }
        }
        texture.Apply(); // проецируем кестуру (посмотреть в документации более подробюно)
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, size.x, size.y), new Vector2(0, 0)); //Создаем спрайт
        GetComponent<SpriteRenderer>().sprite = sprite; //вызываем компонент спрайтиа(предварительно добавив его на объекс)
    }

    /// <summary>
    /// Аналогично, предыдущему методу, только используется тайлы
    /// </summary>
    void DifficultGenerate()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                float gradient = Mathf.PerlinNoise((x+offset.x) / zoom, (y+offset.y) / zoom) * intesiviti; //Создаем градиент с помощью шума перлина
                Tile tile2 = tile;
                tile2.color = GetColorByIntensive(gradient);
                
                if (gradient > catPlane)
                {
                    map.SetTile(new Vector3Int(x,y,0),tile2);
                }
            }
        }
    }
    
    /// <summary>
    /// Публичная типа функция котороая отвечает, каким цветом или тайлом, будет заполнена область.
    /// </summary>
    /// <param name="input">Используем переменную "gradient" на вход для проверки</param>
    /// <returns>возвращаем output, если соответвует определенному условию</returns>
    public Color GetColorByIntensive(float input)
    {
        Color output = new Color(); // переменная для вывода
        if (input < 0.5f) // проверка на соответвие и вывод цвета спрайта
        {
            output = Color.cyan;
        }
        if (input < 0.2f)
        {
            output = Color.blue;
        }
        if (input < 0.7f)
        {
            output = Color.yellow;
        }
        if (input < 0.8f)
        {
            output = Color.green;
        }
        return output;
    }
    
    /// <summary>
    /// Функция простого распределения тайлов на карте
    /// </summary>
    void SimpleGenerateMap()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                map.SetTile(new Vector3Int(size.x, size.y, 0), tile);
            }
        }
    }
    /// <summary>
    /// Функция границы краев
    /// </summary>
    void GenerateMapBorders() // генерация границ карты
    {
        for (int x = -size.x - 1; x < size.x + 1; x++)
        {
            for (int y = -size.y - 1; y < size.y + 1; y++)
            {
                baseMapBorders.SetTile(new Vector3Int(x,size.y,0), objectTile);
                baseMapBorders.SetTile(new Vector3Int(x,-size.y - 1,0), objectTile);
                baseMapBorders.SetTile(new Vector3Int(size.x,y,0), objectTile);
                baseMapBorders.SetTile(new Vector3Int(-size.x - 1,y,0), objectTile);
            }
        }
    }
}
