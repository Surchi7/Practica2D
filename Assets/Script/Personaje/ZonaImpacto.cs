
using UnityEngine;
using System.Collections.Generic;

public class ZonaImpacto : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Arma arma;

    private HashSet<GameObject> enemigosGolpeados =
        new HashSet<GameObject>();

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        arma = GetComponentInParent<Arma>();

        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
    }

    public void ActivarZona()
    {
        enemigosGolpeados.Clear();

        if (boxCollider != null)
        {
            boxCollider.enabled = true;
        }
    }

    public void DesactivarZona()
    {
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("enemy"))
        {
            return;
        }

        if (enemigosGolpeados.Contains(other.gameObject))
        {
            return;
        }

        enemigosGolpeados.Add(other.gameObject);

        Enemigo enemigo = other.GetComponent<Enemigo>();

        if (enemigo == null)
        {
            Debug.LogWarning(
                "El enemigo no tiene el componente Enemigo."
            );
            return;
        }

        if (arma == null)
        {
            Debug.LogWarning(
                "No se encontró el componente Arma."
            );
            return;
        }

        int dañoAplicado = arma.ObtenerDaño();
        enemigo.RecibirDaño(dañoAplicado);
    }
}
