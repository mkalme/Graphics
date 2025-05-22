using System;
using System.Collections.Generic;
using Graphics;

namespace PathTracingGraphics {
    public class Scene : IScene {
        public ICamera Camera { get; set; }
        public IList<ISceneObject> Objects { get; set; }
        public IList<ILight> Lights { get; set; }

        public Scene() {
            Camera = new Camera();
            Objects = new List<ISceneObject>();
            Lights = new List<ILight>();
        }
    }
}
