using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
	public KeyCode leftButton, rightButton, upButton, downButton, confirmButton, shootButton;

    public List<Transform> cannons = new List<Transform>();

	public float _rotationSpeed = 0f;
	private float _maxRotationSpeed = 10f;
	private float _rotationSlowdownRate = 0.1f;

	private float _sailVelocity = 0f;
	private float _maxSailVelocity = 3f;

    public float _totalShootCooldown = 2f;
	public float _shootInterval = 0.3f;
	private bool _reloading = false;

	public List<AudioSource> cannonSounds = new List<AudioSource>();

	public int health = 9;

	public GameObject cannonBall;

	public float cannonBallShootVelocity = 10f;

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
            _rotationSpeed = Mathf.Lerp(_rotationSpeed, 0, _maxRotationSpeed * 2 * Time.deltaTime);
		}

        if (Input.GetKey(upButton))
        {
			// Speed up in 3 seconds.
			_sailVelocity += _maxSailVelocity / 3f * Time.deltaTime;
        }
		else if (Input.GetKey(downButton))
        {
			// Slow down in 2 seconds.
            _sailVelocity -= _maxSailVelocity / 2f * Time.deltaTime;
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
			GameObject ball = Instantiate(cannonBall, cannon.transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().AddForce(transform.right * cannonBallShootVelocity);

			int randomCannonSound = Random.Range(0, cannonSounds.Count - 1);
			cannonSounds[randomCannonSound].Play();

            yield return new WaitForSeconds(_shootInterval);
		}

        yield return new WaitForSeconds(_totalShootCooldown);
		_reloading = false;
		yield break;
    }

    private void IncrementPosition()
    {
        transform.position += transform.forward * _sailVelocity;
    }

    private void IncrementRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y + _rotationSpeed, transform.rotation.z);
    }
}
