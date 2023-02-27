using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private Sprite ActiveSprite;

    private List<GameObject> boxList = new List<GameObject>();

    private GameObject activeBox;

    private static BoxManager instance;

    public static BoxManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<BoxManager>();

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        boxList.AddRange(GameObject.FindGameObjectsWithTag("GunBox"));

        foreach (GameObject box in boxList)
        {
            box.GetComponent<BoxScript>().boxManager = BoxManager.Instance;
            box.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
        }

        MoveBox();
    }

    public void MoveBox()
    {
        GameObject lastBox = null;
        if (activeBox != null)
        {
            lastBox = activeBox;
            activeBox.GetComponent<BoxScript>().enabled = false;
            lastBox.GetComponent<SpriteRenderer>().sprite = inactiveSprite;
        }

        while (lastBox == activeBox)
        {
            activeBox = boxList[Random.Range(0, boxList.Count)];
            
        }

        activeBox.GetComponent<SpriteRenderer>().sprite = ActiveSprite;

        
    }
}
