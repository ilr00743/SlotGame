using System;

namespace Services
{
    public interface IMachineButtonsService
    {
        void SetInteractable(bool isInteractable);
        void AddListenerOnSpin(Action listener);
    }
}