using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioSource source; // 발소리를 재생할 AudioSource
    [SerializeField] private AudioClip[] DefaultSounds; // 기본 발소리
    [SerializeField] private AudioClip[] RunSounds; // 돌 발소리1
    [SerializeField] private AudioClip[] DirtSounds; // 흙 발소리
    [SerializeField] private float pitchMin = 0.9f; // 피치 최소값
    [SerializeField] private float pitchMax = 1.1f; // 피치 최대값
    [SerializeField] private Movement movement; // Movement 스크립트 참조
    [SerializeField] private float walkStepInterval = 0.5f; // 걷기 간격
    [SerializeField] private float runStepInterval = 0.3f; // 뛰기 간격

    private float actualInterval; // 현재 간격
    private int defaultSoundIndex; // 기본 발소리 배열 인덱스
    private int stoneSoundIndex; // 돌 발소리 배열 인덱스
    private int dirtSoundIndex; // 흙 발소리 배열 인덱스

    private void Update()
    {
        // 캐릭터가 지면에 있고 이동 중일 때만 실행
        if (movement.CharacterController.isGrounded && movement.CharacterController.velocity.magnitude > 0)
        {
            actualInterval += Time.deltaTime;

            // 이동 모드에 따라 발소리 재생 간격 조정
            if (movement.moveMode == Movement.MoveMode.Run && actualInterval >= runStepInterval)
            {
                Debug.Log("뛰는 중");
                CheckGround();
            }
            else if (movement.moveMode == Movement.MoveMode.Walk && actualInterval >= walkStepInterval)
            {
                Debug.Log("걷는 중");
                CheckGround();
            }
        }
    }

    private void CheckGround()
    {
        // 바닥 감지
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1.5f))
        {
            actualInterval = 0; // 간격 초기화
            source.pitch = Random.Range(pitchMin, pitchMax); // 피치 랜덤 설정

            if (hitInfo.collider.CompareTag("Room"))
            {
                Debug.Log("Room위를 걷고 있음");
                PlaySound(DefaultSounds, ref defaultSoundIndex);
            }
            //else if (hitInfo.collider.CompareTag("Dirt"))
            //{
            //    PlaySound(DirtSounds, ref dirtSoundIndex); // 흙 발소리 재생
            //}
            //else
            //{
            //    PlaySound(DefaultSounds, ref defaultSoundIndex); // 기본 발소리 재생
            //}
        }
    }

    private void PlaySound(AudioClip[] clips, ref int index)
    {
        Debug.Log($"재생 전 Index 값: {index}"); // 변경 전의 index 값 로그 출력
        source.PlayOneShot(clips[index]); // 현재 index에 해당하는 AudioClip 재생
        index++; // index 값을 증가

        if (index >= clips.Length) // 배열 길이를 초과하면 0으로 초기화
        {
            index = 0;
            Debug.Log("Index가 배열 길이를 초과하여 0으로 초기화됨"); // 초기화 로그 출력
        }

        Debug.Log($"재생 후 Index 값: {index}"); // 변경 후의 index 값 로그 출력
    }

}
