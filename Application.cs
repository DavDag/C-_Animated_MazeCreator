namespace maze {

    using System;
    using SFML.Audio;
    using SFML.Graphics;
    using SFML.System;
    using SFML.Window;

    class Application {

        public Application() {
            window = new RenderWindow(new VideoMode(width, height), "Maze Creator");
            window.Closed += new EventHandler(OnClose);
            // CHANGE THIS TO '0' TO MAKE IT FASTER
            window.SetFramerateLimit(60U);
            grid = new Grid((int) width, (int) height, 30, 30);
        }

        public void MainLoop() {
            while (window.IsOpen) {
                window.DispatchEvents();
                grid.Update();
//                if (grid.timeTaken) {
                    window.Draw(grid);
                    window.Display();
//                }
            }
        }

        private static void OnClose(object sender, EventArgs e) {
            RenderWindow window = (RenderWindow) sender;
            window.Close();
        }

        private static uint width = 650, height = 650;
        private RenderWindow window;
        private Grid grid;
    }

}