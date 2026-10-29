using System.Collections;
using UnityEngine;

public class State_Machine : MonoBehaviour
{
    [SerializeField] private State_Base currentState;

    private State_Base nextState;
    private State_Base estadoAnterior;

    private Coroutine stateMachineCoroutine;

    // Cantidad de colisiones que tenemos con el suelo
    private int contactosSuelo = 0;


    void Start()
    {
        if (currentState != null)
        {
            stateMachineCoroutine = StartCoroutine(StateMachineLoop());
        }
    }

    private IEnumerator StateMachineLoop()
    {
        while (true)
        {
            if (currentState == null)
            {
                yield return null;
                continue;
            }

            currentState.EnterState();

            while (nextState == null)
            {
                currentState.UpdateState();

                yield return null;
            }

            currentState = nextState;
            nextState = null;
        }
    }

    public void SetState(State_Base newState)
    {
        if (newState != null && newState != currentState)
        {
            estadoAnterior = currentState;
            nextState = newState;
        }
    }

    public void SetState<T>() where T : State_Base
    {
        var newState = GetComponentInChildren<T>();

        if (newState != null && newState != currentState)
        {
            estadoAnterior = currentState;
            nextState = newState;
        }
    }

    public State_Base GetCurrentState()
    {
        return currentState;
    }

    public State_Base GetPreviousState()
    {
        return estadoAnterior;
    }

    public bool EstaEnElSuelo
    {
        get
        {
            return contactosSuelo > 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            SetState<Estado_Danio>();
        }

        // Suelo
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            contactosSuelo++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Suelo
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            contactosSuelo--;

            if (contactosSuelo < 0)
            {
                contactosSuelo = 0;
            }
        }
    }

    private void OnDisable()
    {
        if (stateMachineCoroutine != null)
        {
            StopCoroutine(stateMachineCoroutine);
        }
    }
}
