using UnityEngine;

public class EnemyParticles : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component attached to the game object
        animator = GetComponent<Animator>();

        // Subscribe to the animation event
        AnimationClip animationClip = GetAnimationClip();
        if (animationClip != null)
        {
            AnimationEvent animationEvent = new AnimationEvent
            {
                time = animationClip.length,
                functionName = "DestroyGameObject"
            };
            animationClip.AddEvent(animationEvent);
        }
    }

    // Function to destroy the game object
    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    // Function to get the first animation clip from the Animator
    private AnimationClip GetAnimationClip()
    {
        if (animator == null || animator.runtimeAnimatorController == null)
        {
            Debug.LogError("Animator or Animator Controller not set.");
            return null;
        }

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        if (clips.Length > 0)
        {
            return clips[0];
        }
        else
        {
            Debug.LogError("No animation clips found in the Animator Controller.");
            return null;
        }
    }
}
