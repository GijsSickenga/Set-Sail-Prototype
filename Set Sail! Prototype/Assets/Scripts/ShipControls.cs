using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
	public KeyCode leftButton, rightButton, upButton, downButton, confirmButton, shootButton;

	public float rotationSpeed = 0f;
	public float maxRotationSpeed = 60f;

	public float sailVelocity = 0f;
	public float maxSailVelocity = 30f;
    public float sailAcceleration = 5.0f;

    public float totalShootCooldown = 2f;
	public float shootInterval = 0.3f;
	private bool _reloading = false;

    public GameObject cannonBall;
    public float cannonBallShootVelocity = 100f;

    public List<Transform> cannons = new List<Transform>();

	public List<AudioSource> cannonSounds = new List<AudioSource>();

    private List<Collider> _colliders = new List<Collider>();

    private Rigidbody _myBody;

    private void Start()
    {
        foreach(Collider col in GetComponentsInChildren(typeof(Collider), true))
        {
            _colliders.Add(col);
        }

        _myBody = GetComponent<Rigidbody>();
    }

	private void Update()
    {
        if (Input.GetKey(leftButton))
        {
            // Instantly reach max turning speed, counter-clockwise.
            rotationSpeed = -maxRotationSpeed;
        }
        else if (Input.GetKey(rightButton))
        {
			// Instantly reach max turning speed, clockwise.
            rotationSpeed = maxRotationSpeed;
        }
		else
		{
            // Turning friction.
            rotationSpeed = Mathf.Lerp(rotationSpeed, 0, maxRotationSpeed * 0.5f * Time.deltaTime);
		}

        if (Input.GetKey(upButton))
        {
			// Speed up with user-set acceleration.
			sailVelocity += sailAcceleration * Time.deltaTime;
        }
		else if (Input.GetKey(downButton))
        {
			// Slow down in 5 seconds.
            sailVelocity -= maxSailVelocity / 5f * Time.deltaTime;
        }
		else
        {
            // Sailing friction.
            sailVelocity = Mathf.Lerp(sailVelocity, 0, maxSailVelocity * 0.01f * Time.deltaTime);
		}

        if (Input.GetKeyDown(confirmButton))
        {
			// shop stuff
        }

        if (Input.GetKeyDown(shootButton))
        {
			if (!_reloading)
			{
                StartCoroutine(ShootCannons());
            }
        }

        // Clamp velocities.
        rotationSpeed = Mathf.Clamp(rotationSpeed, -maxRotationSpeed, maxRotationSpeed);
        sailVelocity = Mathf.Clamp(sailVelocity, 0, maxSailVelocity);

        IncrementPosition();
        IncrementRotation();
	}

	private IEnumerator ShootCannons()
	{
        _reloading = true;
		foreach (Transform cannon in cannons)
		{
            // Spawn cannonball and start moving it.
			GameObject newCannonBall = Instantiate(cannonBall, cannon.transform.position, Quaternion.identity);
            newCannonBall.GetComponent<Rigidbody>().AddForce(cannon.transform.GetChild(0).up * cannonBallShootVelocity);

            // Ignore collision between the ship's components and the cannonball.
            Collider cannonBallCol = newCannonBall.GetComponent<Collider>();
            foreach(Collider col in _colliders)
            {
                Physics.IgnoreCollision(cannonBallCol, col);
            }

            // Play a random cannon shooting sound.
			int randomCannonSound = Random.Range(0, cannonSounds.Count);
			cannonSounds[randomCannonSound].Play();

            // Delay between shots.
            yield return new WaitForSeconds(shootInterval);
		}

        // Weapon cooldown.
        yield return new WaitForSeconds(totalShootCooldown);
		_reloading = false;
		yield break;
    }

    private void IncrementPosition()
    {
        _myBody.velocity = transform.forward * sailVelocity * Time.deltaTime;
    }

    private void IncrementRotation()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
    }
}
