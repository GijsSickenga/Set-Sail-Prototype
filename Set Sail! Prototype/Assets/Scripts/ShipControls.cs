using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
	public KeyCode leftButton, rightButton, upButton, downButton, confirmButton, shootButton;

	public float _rotationSpeed = 0f;
	public float _maxRotationSpeed = 60f;

	public float _sailVelocity = 0f;
	public float _maxSailVelocity = 30f;
    public float _sailAcceleration = 5.0f;

    public float _totalShootCooldown = 2f;
	public float _shootInterval = 0.3f;
	private bool _reloading = false;

    public GameObject cannonBall;
    public float cannonBallShootVelocity = 100f;

    public int health = 9;
    public int maxHealth = 9;

    public List<Transform> cannons = new List<Transform>();

	public List<AudioSource> cannonSounds = new List<AudioSource>();

    public List<Collider> colliders = new List<Collider>();

	private void Update()
    {
        if (Input.GetKey(leftButton))
        {
            // Instantly reach max turning speed, counter-clockwise.
            _rotationSpeed = -_maxRotationSpeed;
        }
        else if (Input.GetKey(rightButton))
        {
			// Instantly reach max turning speed, clockwise.
            _rotationSpeed = _maxRotationSpeed;
        }
		else
		{
            // Turning friction.
            _rotationSpeed = Mathf.Lerp(_rotationSpeed, 0, _maxRotationSpeed * 0.5f * Time.deltaTime);
		}

        if (Input.GetKey(upButton))
        {
			// Speed up in 3 seconds.
			_sailVelocity += _sailAcceleration * Time.deltaTime;
        }
		else if (Input.GetKey(downButton))
        {
			// Slow down in 2 seconds.
            _sailVelocity -= _maxSailVelocity / 5f * Time.deltaTime;
        }
		else
        {
            // Sailing friction.
            _sailVelocity = Mathf.Lerp(_sailVelocity, 0, _maxSailVelocity * 0.1f * Time.deltaTime);
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
        _rotationSpeed = Mathf.Clamp(_rotationSpeed, -_maxRotationSpeed, _maxRotationSpeed);
        _sailVelocity = Mathf.Clamp(_sailVelocity, 0, _maxSailVelocity);

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
            newCannonBall.GetComponent<Rigidbody>().AddForce(cannon.transform.right * cannonBallShootVelocity);

            // Ignore collision between the ship's components and the cannonball.
            Collider cannonBallCol = newCannonBall.GetComponent<Collider>();
            foreach(Collider col in colliders)
            {
                Physics.IgnoreCollision(cannonBallCol, col);
            }

            // Play a random cannon shooting sound.
			int randomCannonSound = Random.Range(0, cannonSounds.Count);
			cannonSounds[randomCannonSound].Play();

            // Delay between shots.
            yield return new WaitForSeconds(_shootInterval);
		}

        // Weapon cooldown.
        yield return new WaitForSeconds(_totalShootCooldown);
		_reloading = false;
		yield break;
    }

    private void IncrementPosition()
    {
        transform.position += transform.forward * _sailVelocity * Time.deltaTime;
    }

    private void IncrementRotation()
    {
        transform.Rotate(transform.up, _rotationSpeed * Time.deltaTime);
    }
}
