using System.Collections;
using UnityEngine;

public class BGScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;
        float height = sr.bounds.size.y;
        float width = sr.bounds.size.x;

        float worldHeight = Camera.main.orthographicSize * 2f;

        // float worldHeight = 800;
        float worldWidth = worldHeight * Screen.width / Screen.height;

        // float worldWidth = 480;
        tempScale.y = worldHeight / height;
        tempScale.x = worldWidth / width;
        transform.localScale = tempScale;
        // transform.localScale = new Vector3(worldWidth, worldHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
