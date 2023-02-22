using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Floating : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 startingPosition;
    private Vector3 startingScale;
    private Vector3 shadowStartingPosition;

    private Transform shadow;
    public float shadowScale;
    void Start()
    {
        startingPosition = transform.position;

        shadow = gameObject.transform.GetChild(0).GetComponent<Transform>();
        shadowStartingPosition = shadow.position;
        startingScale = shadow.localScale;
    }

    void Update()
    {
        float y = amplitude * Mathf.Sin(frequency * Time.time);
        transform.position = startingPosition + new Vector3(0f, y, 0f);

        float scale = 1f -  (y * shadowScale) ;
        shadow.localScale = startingScale * scale ;
        shadow.position = shadowStartingPosition + new Vector3(0f, (-1 * y) * 0.5f, 0f);
    }
}
