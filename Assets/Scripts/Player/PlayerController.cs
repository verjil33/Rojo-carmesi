using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    private Animator animator;
    public LayerMask solidObjectLayer;
    public LayerMask longGrassLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isMoving) // lo hace en el primer video
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //quitar el movimiento diagonal
            if (input.x != 0) input.y = 0;

            if(input != Vector2.zero)
            {
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if(isWalkable(targetPos)) StartCoroutine(Move(targetPos));
            }
        }
        animator.SetBool("isMoving", isMoving);

    }
    IEnumerator Move(Vector3 targetPos) //lo hace en el primer video
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();

    }
    private bool isWalkable(Vector3 targetPos)
    { 
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectLayer) != null) return false;
        return true;

    }

    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, longGrassLayer) != null)
        {
            if (Random.Range(1, 101) <= 10)
            {
                Debug.Log("Random Encounter");
            }
        }
            
    }
}
