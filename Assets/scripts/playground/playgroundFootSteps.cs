using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class playgroundFootSteps : MonoBehaviour
{
    [SerializeField] private AudioSource source; // �߼Ҹ��� ����� AudioSource
    [SerializeField] private AudioClip[] DefaultSounds; // �⺻ �߼Ҹ�
    [SerializeField] private AudioClip[] RunSounds; // �� �߼Ҹ�
    [SerializeField] private AudioClip[] DirtSounds; // �� �߼Ҹ�
    [SerializeField] private float pitchMin = 0.9f; // ��ġ �ּҰ�
    [SerializeField] private float pitchMax = 1.1f; // ��ġ �ִ밪
    [SerializeField] private playgroundMovement movement; // Movement ��ũ��Ʈ ����
    [SerializeField] private float walkStepInterval = 0.5f; // �ȱ� ����
    [SerializeField] private float runStepInterval = 0.3f; // �ٱ� ����

    private float actualInterval; // ���� ����
    private int defaultSoundIndex; // �⺻ �߼Ҹ� �迭 �ε���
    private int stoneSoundIndex; // �� �߼Ҹ� �迭 �ε���
    private int dirtSoundIndex; // �� �߼Ҹ� �迭 �ε���


    private void Start()
    {
        movement = GetComponent<playgroundMovement>();
    }
    private void Update()
    {
        // ĳ���Ͱ� ���鿡 �ְ� �̵� ���� ���� ����
        if (movement.CharacterController.isGrounded && movement.CharacterController.velocity.magnitude > 0)
        {
            actualInterval += Time.deltaTime;

            // �̵� ��忡 ���� �߼Ҹ� ��� ���� ����
            if (movement.moveMode == playgroundMovement.MoveMode.Run && actualInterval >= runStepInterval)
            {
                CheckGround();
            }
            else if (movement.moveMode == playgroundMovement.MoveMode.Walk && actualInterval >= walkStepInterval)
            {
                CheckGround();
            }
        }
    }

    private void CheckGround()
    {
        // �ٴ� ����
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1.5f))
        {
            actualInterval = 0; // ���� �ʱ�ȭ
            source.pitch = Random.Range(pitchMin, pitchMax); // ��ġ ���� ����

            if (hitInfo.collider.CompareTag("Floor"))
            {
                PlaySound(DefaultSounds, ref defaultSoundIndex);
            }
            //else if (hitInfo.collider.CompareTag("Dirt"))
            //{
            //    PlaySound(DirtSounds, ref dirtSoundIndex); // �� �߼Ҹ� ���
            //}
            //else
            //{
            //    PlaySound(DefaultSounds, ref defaultSoundIndex); // �⺻ �߼Ҹ� ���
            //}
        }
    }

    private void PlaySound(AudioClip[] clips, ref int index)
    {
        source.PlayOneShot(clips[index]); // ���� index�� �ش��ϴ� AudioClip ���
        index++; // index ���� ����
        if (index >= clips.Length) // �迭 ���̸� �ʰ��ϸ� 0���� �ʱ�ȭ
        {
            index = 0;
        }
    }

}