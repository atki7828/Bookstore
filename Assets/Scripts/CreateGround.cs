using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(TilemapCollider2D))]
public class CreateGround : MonoBehaviour
{
    Tilemap map;
    [SerializeField]
    Sprite blockSprite;
    Tile block;
    int length = 200;

    void Start()
    {
        map = this.GetComponent<Tilemap>();
        block = new Tile();
        block.sprite = blockSprite;
        for(int i = -length; i < length; i++)
        {
            map.SetTile(new Vector3Int(i, -3, 0), block);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
