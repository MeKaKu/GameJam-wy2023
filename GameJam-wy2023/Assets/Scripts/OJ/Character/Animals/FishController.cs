using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class FishController : AnimalController
    {
        protected override void EndJump()
        {
        }

        protected override void StartJump()
        {
        }

        protected override void ExitGround()
        {
        }
        protected override void StayGround()
        {
        }
        protected override void EnterGround()
        {
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            onGround = true;
        }
    }
}
