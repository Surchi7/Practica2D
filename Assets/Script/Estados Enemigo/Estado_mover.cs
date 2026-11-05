using UnityEngine;

public class Estado_mover : State_Base_Enemigo
{
    [SerializeField] private float velocidad = 3f;

    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();

        rb = controlledObject.GetComponent<Rigidbody2D>();
    }

    public override void UpdateState()
    {
        Transform player = state_machine.Jugador;

        if (player == null || rb == null)
            return;

        float direccion = Mathf.Sign(
            player.position.x - controlledObject.transform.position.x
        );

        rb.linearVelocity = new Vector2(
            direccion * velocidad,
            rb.linearVelocity.y
        );
    }
}