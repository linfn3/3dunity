using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyNamespace
{
    public interface IUserAction
    {
        void CharacterClicked(CharacterController controller);
        void Restart();
        void Boatmoved();

    }
    public interface ISceneController
    {
        void LoadResources();
    }

}
