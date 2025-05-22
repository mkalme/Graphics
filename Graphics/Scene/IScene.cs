using System;
using System.Collections.Generic;

namespace Graphics {
    public interface IScene {
        ICamera Camera { get; set; }
        IList<ISceneObject> Objects { get; set; }
        IList<ILight> Lights { get; set; }
    }
}
