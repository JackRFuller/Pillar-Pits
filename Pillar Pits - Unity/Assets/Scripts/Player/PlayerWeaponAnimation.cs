using UnityEngine;
using System.Collections;

public class PlayerWeaponAnimation : MonoBehaviour {

    private Animation gunAnim;

    [Header("Animation Clips")]
    public AnimationClip reload;
    public AnimationClip shoot;
    public AnimationClip idle;    

	// Use this for initialization
	void Start () {

        Init();
	
	}

    void Init()
    {
        gunAnim = GetComponent<Animation>();

        gunAnim.clip = idle;
        gunAnim.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Shoot()
    {
        gunAnim.Stop();
        gunAnim.clip = shoot;
        gunAnim.Play();
    }

    public void Reload()
    {
        gunAnim.Stop();
        gunAnim.clip = reload;
        gunAnim.Play();
    }

    public void Idle()
    {
        gunAnim.Stop();
        gunAnim.clip = idle;
        gunAnim.Play();
    }
}
