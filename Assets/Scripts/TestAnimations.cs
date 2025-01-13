using System.Collections;
using UnityEngine;

public class TestAnimations : AnimatorBrain
{
    private int currentIdle = 0;
    private const int UPPERBODY = 0;
    private const int LOWERBODY = 1;

    public static TestAnimations instance;

    private readonly Animations[] IdleAnimations =
    {
        Animations.IDLE1,
    };

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Initialize(GetComponent<Animator>().layerCount, Animations.IDLE1, GetComponent<Animator>(), DefaultAnimation);

        IEnumerator ChangeIdle()
        {
            while (true) {
                yield return new WaitForSeconds(2);
                ++currentIdle;
                if (currentIdle >= IdleAnimations.Length)
                    currentIdle = 0;
            }
        }
    }

    private void CheckTopAnimation()
    {
        CheckMovementAnimations(UPPERBODY);
    }

    private void CheckBottomAnimation()
    {
        CheckMovementAnimations(LOWERBODY);
    }

    private void CheckMovementAnimations(int layer)
    {
        Play(IdleAnimations[currentIdle], layer, false, false);
    }

    void DefaultAnimation(int layer)
    {
        if (layer == UPPERBODY)
            CheckTopAnimation();
        else 
            CheckBottomAnimation();
    }


    void Update()
    {
        CheckDeath();
        CheckShooting();

        CheckTopAnimation();
        CheckBottomAnimation();

        void CheckDeath()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Play(Animations.DEATH, UPPERBODY, true, true);
                Play(Animations.DEATH, LOWERBODY, true, true);
            }
        }

        void CheckShooting()
        {
            if(Input.GetKeyDown(KeyCode.Q)) Play(Animations.SHOOT, UPPERBODY, false, false);
        }
    }
}
