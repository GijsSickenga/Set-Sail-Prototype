using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
	public KeyCode leftButton, rightButton, upButton, downButton, confirmButton, shootButton, cannonUpButton, cannonDownButton;

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

    public float minimumCannonAngle = -5f;
    public float maximumCannonAngle = 45f;
    public float cannonAngle = 10f;
    public float cannonTiltSpeed = 45f;

    public GameObject cannonParent;

    public GameObject barrelSmokeEffect;

    [Tooltip("What percentage of the ship's velocity is transferred to the cannonballs being shot from it.")]
    public float cannonBallShipSpeedMultiplier = 80f;
    public float CannonBallShipSpeedMultiplier
    {
        get
        {
            return cannonBallShipSpeedMultiplier * 0.5f;
        }
    }

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

        if (Input.GetKey(cannonUpButton))
        {
            cannonAngle += cannonTiltSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(cannonDownButton))
        {
            cannonAngle -= cannonTiltSpeed * Time.deltaTime;
        }

        // Adjust cannon angle to new angle.
        cannonAngle = Mathf.Clamp(cannonAngle, minimumCannonAngle, maximumCannonAngle);
        cannonParent.transform.localRotation = Quaternion.Euler(0, 0, cannonAngle);

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
            Rigidbody cannonBallBody = newCannonBall.GetComponent<Rigidbody>();
            cannonBallBody.AddForce(cannon.transform.GetChild(0).up * cannonBallShootVelocity +
                                    transform.forward * sailVelocity * CannonBallShipSpeedMultiplier);

            // Give the cannon balls some rotation over time.
            cannonBallBody.AddTorque(Random.Range(-15, 16), 0f, 0f);

            // Ignore collision between the ship's components and the cannonball.
            Collider cannonBallCol = newCannonBall.GetComponent<Collider>();
            foreach(Collider col in _colliders)
            {
                Physics.IgnoreCollision(cannonBallCol, col);
            }

            // Play a random cannon shooting sound.
			int randomCannonSound = Random.Range(0, cannonSounds.Count);
			cannonSounds[randomCannonSound].Play();

            // Spawn barrel smoke at the barrel the cannon was shot out of.
            Instantiate(barrelSmokeEffect, cannon.transform.position, Quaternion.identity, cannon.transform);

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
        _myBody.velocity = transform.forward * sailVelocity;
    }

    private void IncrementRotation()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
    }
}
