using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesBillBoard : MonoBehaviour
{
    [SerializeField] bool freezeXAxis = true;
    [SerializeField] bool freezeYAxis = true;

    private Camera camera;

    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (freezeXAxis)
        {
            transform.rotation = Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f);
        }
        else if (freezeYAxis)
        {
            transform.rotation = Quaternion.Euler(camera.transform.rotation.eulerAngles.y, 0f, 0f);
        }
        else
        {
            transform.rotation = camera.transform.rotation;
        }
    }
}
