using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public PlayerInput inputActions;    
    public Rigidbody2D rb;
    [Range(0f, 10f)]
    public float movementSpeed;

    public Vector2 direction { get; private set; }

    void Start()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }


    public void OnDestroy()
    {
        inputActions.Player.Disable();
    }

    void FixedUpdate()
    {
        if(ShopManager.Instance.shopActive == true || GameManager.Instance.deathScreen.activeSelf == true)
        {
            return;
        }
        direction = inputActions.Player.Move.ReadValue<Vector2>();
        rb.MovePosition(rb.position + direction * movementSpeed * Time.deltaTime);
        
    }
}
