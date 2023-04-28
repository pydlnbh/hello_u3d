using UnityEngine;

public sealed class BoosBehav : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision == null) 
        {
            return;
        }

        if (!gameObject.TryGetComponent<Spine.Unity.SkeletonAnimation>(out var spSkeAnim)
            || null == spSkeAnim)
        {
            return;
        }

        spSkeAnim.AnimationState.ClearTrack(1);
        spSkeAnim.AnimationState.SetAnimation(1, "BeHited", false);
    }
}