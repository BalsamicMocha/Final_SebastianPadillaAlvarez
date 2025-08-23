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
    public LineRenderer lineRenderer;

    [Header("Debug")]
    public bool showRayDebug = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AmmoManager ammoManager = GetComponent<AmmoManager>();
            if (ammoManager != null && ammoManager.HasAmmo())
            {
                ammoManager.UseAmmo();
                Shoot();
                AudioManager.Instance.Play("Gun");
            }
            else
            {
                AudioManager.Instance.Play("Empty");
                Debug.Log("Sin balas");
            }
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        Vector3 hitPoint = firePoint.position + firePoint.forward * range;

        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            hitPoint = hit.point;

            // Aplicar daño
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
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

        yield return new WaitForSeconds(0.05f);

        lineRenderer.enabled = false;
    }

    //ver raycast en Scene view
    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && showRayDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(firePoint.position, firePoint.position + firePoint.forward * range);
        }
    }
}
