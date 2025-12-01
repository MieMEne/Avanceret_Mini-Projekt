using UnityEngine;

public class BridgeMover : MonoBehaviour
{
    public Vector3 targetOffset = new Vector3(0, 0, 5);
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool move = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + targetOffset;
    }

    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );
        }
    }

    public void ActivateBridge()
    {
        move = true;
    }
}
