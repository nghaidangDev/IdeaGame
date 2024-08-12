using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask solidObjectsLayer;
    [SerializeField] private LayerMask longGrassLayer;

    private bool isMoving;
    private Vector2 input;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Không cho di chuyển chéo
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                anim.SetFloat("MoveX", input.x);
                anim.SetFloat("MoveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        anim.SetBool("isMoving", isMoving);
    }

    //Xử lý tuần tự
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        //Kiểm tra khoảng cách so với targetPos
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //Di chuyển điểm hiện tại đến đích
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEnCounters();
    }

    //Kiểm tra xem có va chạm với solidObjects k
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

    private void CheckForEnCounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, longGrassLayer) != null)
        {
            if (Random.Range(1, 101) <= 10)
            {
                Debug.Log("Encounters a wild pokemon");
            }
        }
    }
}
