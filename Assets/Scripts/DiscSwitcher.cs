using UnityEngine;

public class DiscSwitcher : MonoBehaviour
{
    public Transform discA;
    public Transform discB;

    public Renderer discARenderer;
    public Renderer discBRenderer;

    private bool isADiscFront = true;
    private bool isSwitching = false;

    public Vector3 frontPos = new Vector3(10.7f, 12.09f, 1.35f);
    public Vector3 backPos = new Vector3(12.42f, 11.37f, 1.27f);

    public float moveSpeed = 5f;
    public float switchThreshold = 0.01f;

    private Material frontMaterial;
    private Material backMaterial;

    void Start()
    {
        discA.localPosition = frontPos;
        discB.localPosition = backPos;
    }

    private void Update()
    {
        if (isSwitching)
        {
            Transform targetFront = isADiscFront ? discA : discB;
            Transform targetBack = isADiscFront ? discB : discA;

            targetFront.localPosition = Vector3.Lerp(targetFront.localPosition, frontPos, Time.deltaTime * 8f);
            targetBack.localPosition = Vector3.Lerp(targetBack.localPosition, backPos, Time.deltaTime * 8f);

            // 閾値以下になったら完了とみなす
            if (Vector3.Distance(targetFront.localPosition, frontPos) < 0.01f &&
                Vector3.Distance(targetBack.localPosition, backPos) < 0.01f)
            {
                targetFront.localPosition = frontPos;
                targetBack.localPosition = backPos;
                isSwitching = false;
            }
        }
    }


    public void SetMaterials(Material front, Material back)
    {
        frontMaterial = front;
        backMaterial = back;

        isADiscFront = true;
        discA.localPosition = frontPos;
        discB.localPosition = backPos;
        //ApplyMaterials();
    }

public void SwitchToFront(bool isADiscFrontNow, bool force = false)
{
    if (isADiscFront != isADiscFrontNow || force)
    {
        isADiscFront = isADiscFrontNow;
        ApplyMaterials();
        isSwitching = true; // ✅ ここを force でも必ず有効にするのが目的！
    }
    else
    {
        ApplyMaterials();
        if (force) isSwitching = true; // ← ✅ 追加でアニメーションフラグON
    }
}


    private void ApplyMaterials()
    {
        if (discARenderer == null || discBRenderer == null) return;

        if (isADiscFront)
        {
            discARenderer.material = frontMaterial;
            discBRenderer.material = backMaterial;
        }
        else
        {
            discARenderer.material = backMaterial;
            discBRenderer.material = frontMaterial;
        }

    }
}
