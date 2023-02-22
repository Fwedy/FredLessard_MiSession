using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPtsMove : MonoBehaviour
{
    public float speed = 100f;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(-1f, Random.Range(-1f, 1f)).normalized;
        Destroy(gameObject, 1f);


    }

    // Update is called once per frame
    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += direction * speed * Time.deltaTime;


    }

   
}
