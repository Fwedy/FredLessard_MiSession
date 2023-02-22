using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointsLayout : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private RectTransform newPointsText;
    private int lenght = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pointsText.text.Length > lenght)
        {
            lenght = pointsText.text.Length;
            newPointsText.position = new Vector3(newPointsText.position.x - 15, newPointsText.position.y, 0);
        }
    }
}
