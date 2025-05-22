using System;

namespace Graphics {
    public interface IRenderer {
        IScene Scene { get; set; }

        void Render(LockedBitmap output);
    }
}
