using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public Transform firePoint;
    public float range = 50f;
    public int damage = 25;
    public LayerMask hitMask;

    [Header("Effects")]
    public GameObject impactEffect;
    public LineRenderer lineRenderer; // Asignar en inspector

    [Header("Debug")]
    public bool showRayDebug = true; // Habilita/deshabilita el rayo visible en juego

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        Vector3 hitPoint = firePoint.position + firePoint.forward * range; // Punto por defecto

        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            hitPoint = hit.point;

            // Aplicar daño
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Efecto de impacto
            if (impactEffect != null)
            {
                Instantiate(impactEffect, hitPoint, Quaternion.LookRotation(hit.normal));
            }
        }

        // Mostrar línea solo si está activado
        if (showRayDebug && lineRenderer != null)
        {
            StartCoroutine(ShowLine(firePoint.position, hitPoint));
        }
    }

    private IEnumerator ShowLine(Vector3 start, Vector3 end)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        yield return new WaitForSeconds(0.05f); // duración breve del rayo

        lineRenderer.enabled = false;
    }

    // Opcional: para ver raycast en Scene view
    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && showRayDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(firePoint.position, firePoint.position + firePoint.forward * range);
        }
    }
}
