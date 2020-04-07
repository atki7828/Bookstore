using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Camera cam;
    GameObject player;
    List<GameObject> layers;
    float speed = 10f;
    float playerPos;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log(player);
        playerPos = player.transform.position.x;
        cam = Camera.main;
        layers = new List<GameObject>();

        foreach(Transform child in transform)
        {
            layers.Add(child.gameObject);
            AddNewSprite(child.gameObject);
        }

        foreach(GameObject l in layers)
        {
            l.transform.parent = this.transform;
        }
    }

    private void AddNewSprite(GameObject go)
    {
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        float width = sr.sprite.bounds.size.x;

        GameObject newLayer = new GameObject();
        newLayer.AddComponent<SpriteRenderer>().sprite = sr.sprite;
        newLayer.GetComponent<SpriteRenderer>().sortingOrder = sr.sortingOrder;
        newLayer.transform.position += new Vector3(width, 0, 0);
        //newLayer.transform.parent = this.transform;
        layers.Add(newLayer);

        newLayer = new GameObject();
        newLayer.AddComponent<SpriteRenderer>().sprite = sr.sprite;
        newLayer.GetComponent<SpriteRenderer>().sortingOrder = sr.sortingOrder;
        newLayer.transform.position -= new Vector3(width, 0, 0);
        //newLayer.transform.parent = this.transform;
        layers.Add(newLayer);
    }

    void moveTo(float x)
    {
        foreach(GameObject l in layers)
        {
            float dist = -(layers.Count + l.GetComponent<SpriteRenderer>().sortingOrder*speed);
            l.transform.position += new Vector3(x * dist * Time.deltaTime, 0, 0);
        }
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, cam.transform.position.y, this.transform.position.z);
        moveTo(player.transform.position.x - playerPos);
        playerPos = player.transform.position.x;
    }

}

