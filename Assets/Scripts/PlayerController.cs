using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI liveText;
    public GameObject winTextObject;
    public GameObject deathTextObject;

    private Rigidbody rb;
    private int money;
    private int live;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        money = 0;
        live = 4;

        SetLiveText();
        SetMoneyText();
        winTextObject.SetActive(false);
        deathTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetLiveText()
    {
        liveText.text = "Live: " + live.ToString();
        if (live == 0)
        {
            deathTextObject.SetActive(true);
        }
    }

    void SetMoneyText()
    {
        moneyText.text = "Money: $" + money.ToString();
        if (money >= 24 && live > 0)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            if (live >= 1)
            {
                other.gameObject.SetActive(false);
                money = money + 1;
                SetMoneyText();
            }
        }

        if (other.gameObject.CompareTag("DeathCude"))
        {
            other.gameObject.SetActive(false);
            live = live - 1;
            SetLiveText();
        }
    }
}
