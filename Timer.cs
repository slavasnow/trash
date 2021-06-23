using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] enemys;
    
    public Text hours;
    public Text minuts;
    public Text days;
    
    public float countMinut;
    public int countHours;
    public int countDays;

    public Image bar;
    public float fill;

    public Slider slider;

    public float maxValue;
    // Start is called before the first frame update
    void Start()
    {
        ArrayReload(enemy);

        fill = 1f;
        maxValue = 100f;
        slider.value = maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        ArrayReload(enemy);
        
        countMinut += Time.deltaTime;
        minuts.text = countMinut.ToString();
        
        if (countMinut > 5)
        {
            countMinut = 0;
            countHours += 1;
            hours.text = countHours.ToString();
            
            foreach (GameObject enemy in enemys)
            {
                Spawn(enemy);
            }
            Array.Resize(ref enemys, enemys.Length + 1);
        }
        if (countHours > 3)
        {
            countHours = 0;
            countDays += 1;
            days.text = countDays.ToString();
        }
        slider.value -= Time.deltaTime;
        
        if (slider.value == 0) slider.value = maxValue;
    }

    void Spawn(GameObject enemy)
    {
        Instantiate(enemy, new Vector2(Random.Range(0,3),Random.Range(0,3)), Quaternion.identity);
    }

    void ArrayReload(GameObject enemy)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i] = enemy;
        }
    }

}
