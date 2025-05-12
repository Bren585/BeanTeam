using UnityEngine;

public class DiscSwitcher : MonoBehaviour
{
    public Transform discA;
    public Transform discB;

    private bool isADiscActive = true;

    public Vector3 activePos = new Vector3(0, 0, -0.1f);
    public Vector3 inactivePos = new Vector3(0, 0, 0.1f);
    public float moveSpeed = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isADiscActive = !isADiscActive;
        }

        // アクティブと非アクティブのディスク位置を滑らかに補間
        discA.localPosition = Vector3.Lerp(discA.localPosition, isADiscActive ? activePos : inactivePos, Time.deltaTime * moveSpeed);
        discB.localPosition = Vector3.Lerp(discB.localPosition, isADiscActive ? inactivePos : activePos, Time.deltaTime * moveSpeed);
    }
}
