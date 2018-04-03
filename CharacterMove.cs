using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
	
	const float GravityPower = 9.8f; // 중력
	const float StoppingDistance = 0.6f; // 캐릭터 정지거리
	
	Vector3 velocity = Vector3.zero; // 현재 이동속도 
    CharacterController characterController; // 캐릭터 컨트롤러의 캐시
    public bool arrived = false; // 도착했는가

    bool forceRotate = false; // 방향을 강제로 지시하는가

    Vector3 forceRotateDirection; // 강제로 향하게 하고 싶은 방향.

    public Vector3 destination; // 목적지

    public float walkSpeed = 6.0f; // 이동 속도

    public float rotationSpeed = 360.0f; // 회전 속도

	void Start () {
		characterController = GetComponent<CharacterController>();
		destination = transform.position;
	}
	
	void Update () {
		
		if (characterController.isGrounded) {

            Vector3 destinationXZ = destination; // 수평면에서 이동을 고려하므로 XZ만 다룬다.
            destinationXZ.y = transform.position.y;// 목적지와 현재 위치 높이를 똑같이 한다.

            Vector3 direction = (destinationXZ - transform.position).normalized; // 목적지까지 거리와 방향을 구한다.
			float distance = Vector3.Distance(transform.position,destinationXZ);

            Vector3 currentVelocity = velocity; // 현재 속도

            if (arrived || distance < StoppingDistance) //　목적지에 가까이 왔으면 도착.
				arrived = true;
			
			if (arrived)
				velocity = Vector3.zero;
			else
                velocity = direction * walkSpeed; // 이동 속도를 구한다.
			
			
			velocity = Vector3.Lerp(currentVelocity, velocity,Mathf.Min (Time.deltaTime * 5.0f ,1.0f));
            velocity.y = 0; // 보간 처리
			
			
			if (!forceRotate) {
				// 바꾸고 싶은 방향으로 변경한다. 
				if (velocity.magnitude > 0.1f && !arrived) { 
					// 이동하지 않았다면 방향은 변경하지 않는다.
					Quaternion characterTargetRotation = Quaternion.LookRotation(direction);
					transform.rotation = Quaternion.RotateTowards(transform.rotation,characterTargetRotation,rotationSpeed * Time.deltaTime);
				}
			} else {
				// 강제로 방향을 지정한다.
				Quaternion characterTargetRotation = Quaternion.LookRotation(forceRotateDirection);
				transform.rotation = Quaternion.RotateTowards(transform.rotation,characterTargetRotation,rotationSpeed * Time.deltaTime);
			}
			
		}

        velocity += Vector3.down * GravityPower * Time.deltaTime; // 중력.
		
		Vector3 snapGround = Vector3.zero;
		if (characterController.isGrounded)
            snapGround = Vector3.down; // 땅에 닿아 있다면 지면을 꽉 누른다.

        characterController.Move(velocity * Time.deltaTime + snapGround); // CharacterController를 사용해서 움직인다.
		
		if (characterController.velocity.magnitude < 0.1f)
			arrived = true;
		
		if (forceRotate && Vector3.Dot(transform.forward,forceRotateDirection) > 0.99f)
            forceRotate = false; // 강제로 방향 변경을 해제한다.	
		
	}
	
	public void SetDestination(Vector3 destination)
	{
		arrived = false;
        this.destination = destination; // 목적지를 설정한다. 인수 destination은 목적지.
	}
	
	public void SetDirection(Vector3 direction)
	{
		forceRotateDirection = direction;
		forceRotateDirection.y = 0;
		forceRotateDirection.Normalize();
        forceRotate = true; // 지정한 방향으로 향한다.
	}

    public void StopMove() // 이동을 그만둔다.
	{
		// 현재 지점을 목적지로 한다.
		destination = transform.position; 
	}
	
	public bool Arrived()
	{
        return arrived; // 목적지에 도착했는지 조사한다
	}


	
	
}
