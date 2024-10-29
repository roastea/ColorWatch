using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_Body : MonoBehaviour
{
    [SerializeField] private float playerspeed = 3;
    [SerializeField] private float dashspeed = 1.3f;
    [SerializeField] private float maxStamina = 100f; //スタミナの最大値
    [SerializeField] private float limitMove = 10; //歩く速さの上限
    [SerializeField] private float limitDash = 15; //走る速さの上限

    private Rigidbody rb;

    private float nowStamina;

    private bool moving;
    private bool running;

    private Vector2 moveVector;

    private Vector3 playermove;

    public GameObject staminaGauge;
    private Slider staminaSlider;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();

        if (context.started)
        {
            moving = true;
        }
        else if (context.canceled)
        {
            moving = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            running = true;
        }
        else if (context.canceled)
        {
            running = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        staminaSlider = staminaGauge.GetComponent<Slider>();
        staminaSlider.maxValue = maxStamina;
        nowStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの移動
        if (moving)
        {
            if (running) //ダッシュ時
            {
                playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed * dashspeed;
                rb.AddRelativeForce(playermove);

                nowStamina -= 0.1f;
                staminaSlider.value = nowStamina;

                if (rb.velocity.magnitude > limitDash)
                {
                    rb.velocity = rb.velocity.normalized * limitDash;
                }

                //スタミナが切れたら
                if (nowStamina <= 0)
                {
                    running = false;
                }
            }
            else //通常時
            {
                playermove = new Vector3(moveVector.x, 0, moveVector.y) * playerspeed;
                rb.AddRelativeForce(playermove);

                //加速しすぎないように制限
                if (rb.velocity.magnitude > limitMove)
                {
                    rb.velocity = rb.velocity.normalized * limitMove;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //スタミナの回復
        if (nowStamina < staminaSlider.maxValue && !running)
        {
            nowStamina += 0.15f;
            staminaSlider.value = nowStamina;
        }
    }
}
