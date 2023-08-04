using Assets.PixelHeroes.Scripts.CharacterScrips;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

namespace Assets.PixelHeroes.Scripts.ExampleScripts
{
    public class CharacterControls : MonoBehaviour
    {
        public Character Character;
        public CharacterController Controller; // https://docs.unity3d.com/ScriptReference/CharacterController.html
        public float RunSpeed = 1f;
        public float JumpSpeed = 3f;
        public float CrawlSpeed = 0.25f;
        public ParticleSystem MoveDust;
        public ParticleSystem JumpDust;

        private Vector3 _motion = Vector3.zero;
        private int _inputX, _inputY;
        private float _activityTime;
        
        public void Start()
        {
            Character.SetState(AnimationState.Idle);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) Character.Animator.SetTrigger("Attack");
            else if (Input.GetKeyDown(KeyCode.J)) Character.Animator.SetTrigger("Jab");
            else if (Input.GetKeyDown(KeyCode.P)) Character.Animator.SetTrigger("Push");
            else if (Input.GetKeyDown(KeyCode.H)) Character.Animator.SetTrigger("Hit");
            else if (Input.GetKeyDown(KeyCode.I)) { Character.SetState(AnimationState.Idle); _activityTime = 0; }
            else if (Input.GetKeyDown(KeyCode.R)) { Character.SetState(AnimationState.Ready); _activityTime = Time.time; }
            else if (Input.GetKeyDown(KeyCode.B)) Character.SetState(AnimationState.Blocking);
            else if (Input.GetKeyUp(KeyCode.B)) Character.SetState(AnimationState.Ready);
            else if (Input.GetKeyDown(KeyCode.D)) Character.SetState(AnimationState.Dead);

            // Builder characters only.
            else if(Input.GetKeyDown(KeyCode.S)) Character.Animator.SetTrigger("Slash");
            else if (Input.GetKeyDown(KeyCode.O)) Character.Animator.SetTrigger("Shot");
            else if(Input.GetKeyDown(KeyCode.F)) Character.Animator.SetTrigger("Fire1H");
            else if (Input.GetKeyDown(KeyCode.E)) Character.Animator.SetTrigger("Fire2H");
            else if (Input.GetKeyDown(KeyCode.C)) Character.SetState(AnimationState.Climbing);
            else if (Input.GetKeyUp(KeyCode.C)) Character.SetState(AnimationState.Ready);
            else if (Input.GetKeyUp(KeyCode.L)) Character.Blink();

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _inputX = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _inputX = 1;
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _inputY = 1;
                
                if (Controller.isGrounded)
                {
                    JumpDust.Play(true);
                }
            }
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            
        }

        private void Turn(int direction)
        {
            Character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
        }
    }
}