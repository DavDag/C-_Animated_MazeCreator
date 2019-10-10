namespace maze {

    using System;
    using System.Collections.Generic;
    using SFML.System;
    using SFML.Graphics;

    class Grid: Transformable, Drawable {

        public Grid(int width, int height, int columns, int rows) {
            border = new RectangleShape(new Vector2f(width - 20, height - 20));
            border.OutlineColor = Color.Blue;
            border.OutlineThickness = 10;

            Position += new Vector2f(10, 10);

            width -= 20;
            height -= 20;

            col = columns;
            row = rows;

            tiles = new Tile[columns, rows];
            int tw = width / col, th = height / row;
            for(int i = 0; i < col; ++i) 
                for (int j = 0; j < row; ++j) {
                tiles[i, j] = new Tile(tw, th);
                tiles[i, j].Position += new Vector2f(tw * i, th * j);
            }

            random = new Random();
            stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(random.Next(col), random.Next(row)));

            timer = new Clock();
            timeTaken = false;

            renderTexture = new RenderTexture((uint) width, (uint) height, false);
            foreach(Tile tile in tiles) {
                renderTexture.Draw(tile);
            }
            sprite = new Sprite(renderTexture.Texture);
        }

        public void Update() {
            if (stack.Count > 0) {
                Tuple<int, int> pos = stack.Peek();
                List<Tuple<int, int>> possibilities = new List<Tuple<int, int>>();
                if (pos.Item1 + 1 < col  && tiles[pos.Item1 + 1, pos.Item2].IsEmpty()) possibilities.Add(new Tuple<int, int>(pos.Item1 + 1, pos.Item2));
                if (pos.Item1 - 1 >= 0   && tiles[pos.Item1 - 1, pos.Item2].IsEmpty()) possibilities.Add(new Tuple<int, int>(pos.Item1 - 1, pos.Item2));
                if (pos.Item2 + 1 < row  && tiles[pos.Item1, pos.Item2 + 1].IsEmpty()) possibilities.Add(new Tuple<int, int>(pos.Item1, pos.Item2 + 1));
                if (pos.Item2 - 1 >= 0   && tiles[pos.Item1, pos.Item2 - 1].IsEmpty()) possibilities.Add(new Tuple<int, int>(pos.Item1, pos.Item2 - 1));
                
                if (possibilities.Count > 0) {
                    int ind = 0;
                    if (possibilities.Count == 1) {
                        stack.Pop();
                    } else {
                        ind = random.Next(possibilities.Count);
                    }
                    stack.Push(possibilities[ind]);

                    Tile.Direction dir = 0, ndir = 0;
                    if (possibilities[ind].Item1 < pos.Item1) {
                        dir |= Tile.Direction.OVEST;
                        ndir |= Tile.Direction.EST;
                    } else if (possibilities[ind].Item1 > pos.Item1) {
                        dir |= Tile.Direction.EST;
                        ndir |= Tile.Direction.OVEST;
                    }
                    if (possibilities[ind].Item2 < pos.Item2) {
                        dir |= Tile.Direction.NORD;
                        ndir |= Tile.Direction.SUD;
                    } else if (possibilities[ind].Item2 > pos.Item2) {
                        dir |= Tile.Direction.SUD;
                        ndir |= Tile.Direction.NORD;
                    }

                    tiles[pos.Item1, pos.Item2].ToggleDoor(dir);
                    tiles[possibilities[ind].Item1, possibilities[ind].Item2].ToggleDoor(ndir);

                    renderTexture.Draw(tiles[possibilities[ind].Item1, possibilities[ind].Item2]);
                    renderTexture.Draw(tiles[pos.Item1, pos.Item2]);

                    sprite.Texture = renderTexture.Texture;
                } else {
                    stack.Pop();
                }
            } else if (!timeTaken) {
                Console.WriteLine(timer.Restart().AsSeconds());
                timeTaken = true;
            }
        }

        public void Draw(RenderTarget target, RenderStates states) {
            states.Transform *= Transform;
            states.Texture = null;
            target.Draw(border, states);
            target.Draw(sprite, states);
        }

        private int col, row;
        private Tile[, ] tiles;
        private RectangleShape border;
        private Random random;
        private Stack<Tuple<int, int>> stack;

        private RenderTexture renderTexture;
        private Sprite sprite;

        private Clock timer;
        public Boolean timeTaken;
    }

}