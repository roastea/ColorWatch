using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BlackLightScript : MonoBehaviour
{
    public Transform judge;
    public Transform player;
    public float maxAngle = 25f;
    [SerializeField] float powerDown = 0.00005f;
    //public bool ShyStop = false;

    private bool powerOn = false;

    [SerializeField] private float maxBattery = 100f; //バッテリーの最大値
    private float nowbattery;

    public GameObject batteryGauge;
    private Slider batterySlider;

    //Sound
    AudioSource soundLight;

    //UI
    public GameObject lightOnIcon;
    public GameObject lightOffIcon;

    void Start()
    {
        soundLight = GetComponent<AudioSource>();
        batterySlider = batteryGauge.GetComponent<Slider>();
        batterySlider.maxValue = maxBattery;
        nowbattery = maxBattery;

        lightOnIcon.SetActive(false);
    }

    public void OnLight(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            if (!powerOn)
            {
                soundLight.PlayOneShot(soundLight.clip);
                lightOnIcon.SetActive(true);
                lightOffIcon.SetActive(false);
                powerOn = true;
            }
            else
            {
                soundLight.PlayOneShot(soundLight.clip);
                lightOnIcon.SetActive(false);
                lightOffIcon.SetActive(true);
                powerOn = false;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("EnemyShy"))
    //    {
    //        ShyStop = true;
    //        //Debug.Log("入った");
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("EnemyShy"))
    //    {
    //        ShyStop = false;
    //        //Debug.Log("出た");
    //    }
    //}

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
                        if (powerOn && nowbattery > 0) //ライトON
                        {
                            material.SetVector("_Judge", new Vector4(judge.position.x, judge.position.y, judge.position.z, 0));

                            material.SetVector("_Position", new Vector4(player.position.x, player.position.y, player.position.z, 0));

                            material.SetFloat("_MaxAngle", maxAngle);

                            nowbattery -= powerDown;
                            batterySlider.value = nowbattery;
                        }
                        else //ライトOFF
                        {
                            material.SetVector("_Judge", new Vector4(0, 0, 0, 0));

                            material.SetVector("_Position", new Vector4(0, 0, 0, 0));

                            material.SetFloat("_MaxAngle", 0);
                        }
                    }
                }
            }
        }
    }
}
