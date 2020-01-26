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
