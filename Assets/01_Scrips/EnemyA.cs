using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Transform punto_shooter;
    public GameObject bala;
    public Animator anim;
    private float tiempo;
    [field: SerializeField]
    public bool PlayerDetected { get; private set; }
    public Vector2 DirectionTarget => target.transform.position - detectorOrigin.position;
    [Header("OverlapBox parameters")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffSet = Vector2.zero;
    public float detectionDelay = 0.3f;
    public LayerMask detectorLayerMask;
    [Header("Gizmo parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;
    private GameObject target;
    public float hp = 5;
    public float dropChance = 30;
    public AudioClip deadEnemy;
    public List<PowerUp> powerUpList = new List<PowerUp>();


    public GameObject Target
    {
        get => target;
        private set 
        {
            target = value;
            PlayerDetected = target != null;
        }
    }
    void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }
    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }
    public void TakeDamageB()
    {
        hp--;
        if (hp <= 0)
        {
            
            Destroyer();
        }
    }
    void Destroyer()
    {
        GameManager.instance.PlaySFX(deadEnemy);
        anim.SetBool("Attack", true);
        Destroy(gameObject);
        int chance = Random.Range(0, 101); // 0 -> 100
        if (chance >= dropChance)
        {
            Instantiate(powerUpList[Random.Range(0, powerUpList.Count)],transform.position, transform.rotation);
        }
        
    }
    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffSet, detectorSize, 0, detectorLayerMask);
        if (collider != null)
        {
            Target = collider.gameObject;
        }
        else
        {
            Target = null;
        }
    }
    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            Gizmos.color = gizmoIdleColor;
            anim.SetBool("Attack", false);
            if (PlayerDetected)
            {
                Gizmos.color = gizmoDetectedColor;
                Gizmos.DrawCube((Vector2)detectorOrigin.position+detectorOriginOffSet,detectorSize);
                tiempo += Time.deltaTime; 
                Cheek();
                anim.SetBool("Attack", true);
            }
            
        }
    }
    void Cheek() 
    {
        tiempo += Time.deltaTime;
        if (tiempo >= 2)
        {
            Instantiate(bala, punto_shooter.position, transform.rotation);
            tiempo = 0;
        }
    }
}
