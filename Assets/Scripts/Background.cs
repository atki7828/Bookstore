<<<<<<< HEAD
﻿using System;
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

=======
using UnityEngine;
using UnityEngine.UI;

// runs automatically,
// we have to make it respond to player movement, and make sure it always completely fills the background....
public class Background : MonoBehaviour {

    [SerializeField]
    bool autorun = false;
    Camera cam;
    float camLeft, camRight;
    GameObject Player;
    Vector3 playerPos; 
    void Start() {
        cam = Camera.main;
        // orthographicSize is half the vertical height of cam.
        // aspect is aspect ratio
        camLeft = -cam.orthographicSize*cam.aspect; 
        camRight = -camLeft;
        Player = GameObject.FindWithTag("Player");
        playerPos = Player.transform.position;
    }

    // iterates through all children in order,
    // so the layers have to be in the right order in the editor .
    // each iteration, dx is incremented by deltaTime, so each one steps a little further.
    void Update() {
        float dx = Time.deltaTime;
        foreach(Transform child in this.transform) {
            if(autorun) {
                child.position -= new Vector3(dx,0,0);
                if(!InCam(child.gameObject)) {
                    float dist = child.gameObject.GetComponent<SpriteRenderer>().bounds.size.x*3;//+cam.orthographicSize*cam.aspect*2;
                    child.position += new Vector3(dist,0,0);
                }
                dx += Time.deltaTime; 
            }
            else {
                child.position += new Vector3(dx*(playerPos.x-Player.transform.position.x),0,0);
            dx += Time.deltaTime*7; 
            }
            //dx *= 1.5f;
        child.position = new Vector3(child.position.x,cam.transform.position.y,child.position.z);
        }

        playerPos = Player.transform.position;
    }

    // returns whether the background image is completely out of camera view.
    // if so, it jumps back to the other side of the camera
    bool InCam(GameObject o) {
        float spriteWidth = o.GetComponent<SpriteRenderer>().bounds.size.x;
        float rightEdge = o.transform.position.x + spriteWidth/2;
        Vector3 vp = cam.WorldToViewportPoint(new Vector3(rightEdge,cam.transform.position.y,0));
        return vp.x >= 0.0f; 
    }
}
>>>>>>> f98885ee5d9f953ac142933a21dfe0869a72e229
