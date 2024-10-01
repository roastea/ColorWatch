using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform player;
    public Transform judge;
    public float maxAngle = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (player != null)
        {
            Renderer[] renderers = FindObjectsOfType<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material material in renderer.materials)
                {
                    if (material.shader.name == "Unlit/BlackLight")
                    {
                        material.SetVector("_Position", new Vector4(player.position.x, player.position.y, player.position.z, 0));
                        material.SetVector("_Judge", new Vector4(judge.position.x, judge.position.y, judge.position.z, 0));
                        material.SetFloat("_MaxAngle", maxAngle);
                    }
                }
            }
        }
    }
}
