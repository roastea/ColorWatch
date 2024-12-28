using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountEnemyFromText : MonoBehaviour
{
    public TextMeshProUGUI normalText;
    public TextMeshProUGUI shyText;
    public TextMeshProUGUI boarText;
    public TextMeshProUGUI octopusText;
    public TextMeshProUGUI tutorialText;

    private string normal;
    private string shy;
    private string boar;
    private string octopus;
    private string tutorial;

    private bool normalSound = false;
    private bool shySound = false;
    private bool boarSound = false;
    private bool octopusSound = false;
    private bool tutorialSound = false;

    private AudioSource soundReturnColor;

    private void Start()
    {
        soundReturnColor = GetComponent<AudioSource>();
    }

    void Update()
    {
        normal= normalText.text;
        shy= shyText.text;
        boar= boarText.text;
        octopus= octopusText.text;
        tutorial= tutorialText.text;

        if (normal == "Å~0" && shy == "Å~0" && boar == "Å~0" && octopus == "Å~0" && tutorial == "Å~0")
        {
            SceneManager.LoadScene("ClearScene");
        }

        if(normal == "Å~0" && normalSound == false)
        {
            normalSound = true;
            soundReturnColor.PlayOneShot(soundReturnColor.clip);
        }

        if (shy == "Å~0" && shySound == false)
        {
            shySound = true;
            soundReturnColor.PlayOneShot(soundReturnColor.clip);
        }

        if (boar == "Å~0" && boarSound == false)
        {
            boarSound = true;
            soundReturnColor.PlayOneShot(soundReturnColor.clip);
        }

        if (octopus == "Å~0" && octopusSound == false)
        {
            octopusSound = true;
            soundReturnColor.PlayOneShot(soundReturnColor.clip);
        }

        if (tutorial == "Å~0" && tutorialSound == false)
        {
            tutorialSound = true;
            soundReturnColor.PlayOneShot(soundReturnColor.clip);
        }
    }
}
