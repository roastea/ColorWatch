using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLightScript : MonoBehaviour
{
    public Transform judge;
    public Transform player;
    public float maxAngle = 25f;

    // Update is called once per frame
    void Update()
    {
        if(judge!=null)
        {
            Renderer[] renderers = FindObjectsOfType<Renderer>();
            foreach(Renderer renderer in renderers)
            {
                foreach(Material material in renderer.materials)
                {
                    if(material.shader.name=="Unlit/BlackLight")
                    {
                        material.SetVector("_Judge", new Vector4(judge.position.x, judge.position.y, judge.position.z, 0));

                        material.SetVector("_Position", new Vector4(player.position.x, player.position.y, player.position.z, 0));

                        material.SetFloat("_MaxAngle", maxAngle);
                    }
                }
            }
        }
    }
}
