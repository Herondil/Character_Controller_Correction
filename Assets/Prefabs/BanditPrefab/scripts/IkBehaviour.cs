using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine;

public class IkBehaviour : MonoBehaviour
{
    public Animator _anim;

    //possible d'avoir un V3 aussi
    public Transform HeadDirection;
    public Transform RightHandPosition;

    public Cinemachine.CinemachineFreeLook fl;

    private void Awake()
    {
        TryGetComponent<Animator>(out _anim);
    }

    private void OnAnimatorIK(int layerIndex)
    {

        //si le personnage est immobile (en state idle) 
        // on envoie des raycast pour la position des pieds


        _anim.SetLookAtWeight(0.5f); //diminuer le weight si la target est trop derrière
        _anim.SetLookAtPosition(HeadDirection.position);



        _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        _anim.SetIKPosition(AvatarIKGoal.RightHand, RightHandPosition.position);
        //raycast depuis la position de la main
        //normale du point de collision

        Quaternion handRotation = Quaternion.LookRotation(RightHandPosition.position - transform.position);
        _anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        _anim.SetIKRotation(AvatarIKGoal.RightHand, handRotation);

        //fl.m_YAxisRecentering = new AxisState.Recentering(false, 1, 2); ;

        Transform leftFoot = _anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        RaycastHit hit;
        Vector3     targetPosition = leftFoot.position;
        Quaternion  targetRotation = leftFoot.rotation;

        if (Physics.Raycast(leftFoot.position, -Vector3.up, out hit, 3))
        {
            targetPosition = hit.point;

            Quaternion rot = Quaternion.LookRotation(transform.forward);

            targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * rot;
        }

        _anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
        _anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
        _anim.SetIKPosition(AvatarIKGoal.LeftFoot, targetPosition);
        _anim.SetIKRotation(AvatarIKGoal.LeftFoot, targetRotation);

        Transform RightFoot = _anim.GetBoneTransform(HumanBodyBones.RightFoot);
        //RaycastHit hit;
        targetPosition = RightFoot.position;
        targetRotation = RightFoot.rotation;

        if (Physics.Raycast(RightFoot.position, -Vector3.up, out hit, 3))
        {
            targetPosition = hit.point;

            Quaternion rot = Quaternion.LookRotation(transform.forward);

            targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * rot;
        }

        _anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
        _anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
        _anim.SetIKPosition(AvatarIKGoal.RightFoot, targetPosition);
        _anim.SetIKRotation(AvatarIKGoal.RightFoot, targetRotation);
    }
}
