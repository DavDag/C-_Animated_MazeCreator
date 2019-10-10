namespace maze {
    
    using System;
    using SFML.System;
    using SFML.Graphics;
    using SFML.Window;
    
    class Tile: Transformable, Drawable {

        [Flags]
        public enum Direction { NORD = 1, SUD = 2, EST = 4, OVEST = 8, CENTER = 16 }

        public Tile(int width, int height) {
            rects = new RectangleShape[3, 3];
            float dw = width / 3.0f, dh = height / 3.0f;
            for(int i = 0; i < 3; ++i) 
                for(int j = 0; j < 3; ++j) {
                rects[i, j] = new RectangleShape(new Vector2f(dw, dh));
                rects[i, j].FillColor = Color.Black;
                rects[i, j].Position += new Vector2f(dw * i, dh * j);
            }
            doors = 0;
            empty = true;
            renderTexture = new RenderTexture((uint) width, (uint) height, false);
            sprite = new Sprite(renderTexture.Texture);
            Reload();
        }

        public void ToggleDoor(Direction dir) {
            empty = false;
            doors |= dir | Direction.CENTER;
            Reload();
        }

        public Boolean IsEmpty() {
            return empty;
        }

        public void Draw(RenderTarget target, RenderStates states) {
            states.Transform *= Transform;
            states.Texture = null;
            target.Draw(sprite, states);
        }

        private void Reload() {
            if ((doors & Direction.NORD) != 0)      rects[1, 0].FillColor = Color.White;
            if ((doors & Direction.SUD) != 0)       rects[1, 2].FillColor = Color.White;
            if ((doors & Direction.EST) != 0)       rects[2, 1].FillColor = Color.White;
            if ((doors & Direction.OVEST) != 0)     rects[0, 1].FillColor = Color.White;
            if ((doors & Direction.CENTER) != 0)    rects[1, 1].FillColor = Color.White;

            foreach(RectangleShape rect in rects) {
                renderTexture.Draw(rect);
            }
            renderTexture.Display();
            sprite.Texture = renderTexture.Texture;
        }

        private RectangleShape[ , ] rects;
        private Direction doors;

        private RenderTexture renderTexture;
        private Sprite sprite;

        private Boolean empty;
    }
}