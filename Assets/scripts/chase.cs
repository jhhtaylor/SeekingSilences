using UnityEngine;
using System.Collections;

public class chase : MonoBehaviour {

    public float chaseSpeed = 0.05f;
	public Transform player;
	static Animator anim;

    public AudioClip[] screamSounds;
    private AudioSource audio;

    // Use this for initialization
    void Start () 
	{
		anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle(direction,this.transform.forward);
		if(Vector3.Distance(player.position, this.transform.position) < 10 && angle < 90)
		{
			
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
										Quaternion.LookRotation(direction), 0.1f);

			anim.SetBool("isIdle",false);
			if(direction.magnitude > 5)
			{
				this.transform.Translate(0,0,chaseSpeed);
				anim.SetBool("isWalking",true);
				anim.SetBool("isAttacking",false);
			}
			else
			{
				anim.SetBool("isAttacking",true);
				anim.SetBool("isWalking",false);
                Scream();
			}

		}
		else 
		{
			anim.SetBool("isIdle", true);
			anim.SetBool("isWalking", false);
			anim.SetBool("isAttacking", false);
		}

	}
    void Scream()
    {
        if (audio.isPlaying)
        {
            return;
        }
        audio.clip = screamSounds[Random.Range(0, screamSounds.Length)];
        audio.Play();

    }
}
