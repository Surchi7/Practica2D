using UnityEngine;

public class Estado_mover : State_Base_Enemigo
{
    [SerializeField] private float velocidad = 3f;

    private Transform player;
    private Rigidbody2D rb;

    protected override void Awake()
    {
        base.Awake();

        rb = controlledObject.GetComponent<Rigidbody2D>();
    }

    public override void EnterState()
    {
        GameObject objetoPlayer = GameObject.FindGameObjectWithTag("Player");

        if (objetoPlayer != null)
        {
            player = objetoPlayer.transform;
        }
    }

    public override void UpdateState()
    {
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