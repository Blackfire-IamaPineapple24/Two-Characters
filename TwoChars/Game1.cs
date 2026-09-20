using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TwoChars
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D _background, _player1, _player2;
        public Vector2 _p1Pos, _p2Pos;
        public Rectangle _screenBounds = new(0, 0, 1280, 720);
        public Rectangle _playerBounds;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = _screenBounds.Width;
            _graphics.PreferredBackBufferHeight = _screenBounds.Height;
            _graphics.ApplyChanges();

            int margin = 16;
            _playerBounds = new Rectangle
            (
                _screenBounds.X + margin,
                (int)(_screenBounds.Height / 8f * 5f),
                _screenBounds.Width - margin - margin,
                (int)(_screenBounds.Height / 8f * 3f)
            );

            _p1Pos = _playerBounds.Center.ToVector2() + new Vector2(-200, 0);
            _p2Pos = _playerBounds.Center.ToVector2() + new Vector2(200, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _background = Content.Load<Texture2D>("meadow");
            _player1 = Content.Load<Texture2D>("character1");
            _player2 = Content.Load<Texture2D>("character2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, new Rectangle())
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
