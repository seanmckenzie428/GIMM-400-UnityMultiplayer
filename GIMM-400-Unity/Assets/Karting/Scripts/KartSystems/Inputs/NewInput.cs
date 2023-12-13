using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KartGame.KartSystems {

    public class NewInput: BaseInput
    {
        private bool accelerate = false;
        private bool brake = false;
        private float steer = 0;

        public void OnAccelerate(InputAction.CallbackContext context)
        {
            accelerate = context.ReadValueAsButton();
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            brake = context.ReadValueAsButton();
        }

        public void OnSteer(InputAction.CallbackContext context)
        {
            steer = context.ReadValue<float>();
        }

        public override InputData GenerateInput() {
            return new InputData
            {
                Accelerate = accelerate,
                Brake = brake,
                TurnInput = steer,
            };
        }
    }
}
