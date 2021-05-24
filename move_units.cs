using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

public class move_units: MonoBehaviour
{
    public Vector2 MousePosition;
    public float speed;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //движение героя по клику мыши
        if (Input.GetMouseButtonDown(0))
        {
            //берется позиция мыши с мировыми координатами
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //вычисляется вектор движения
            direction = (MousePosition - (Vector2)transform.position).normalized;
        }
        //постоянное вдижение
        float step = speed * Time.deltaTime;
        transform.Translate(direction * step);
    }
}