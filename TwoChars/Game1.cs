using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TwoChars
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // ----------------------------------------------------------
        // VARIABLES
        // ----------------------------------------------------------
        public Texture2D _background, _player1, _player2;
        public Vector2 _p1Pos, _p2Pos;
        public Rectangle _screenBounds = new(0, 0, 1280, 720);
        public Rectangle _playerBounds;
        public float _movementSpeed = 100f;
        public Vector2 _p1Size = new(152f, 256f), _p2Size = new(152f, 256f);
        public Vector2 _sizeMultiplier = new(1.005f, 1.005f);
        // ----------------------------------------------------------

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
            KeyboardState keysState = Keyboard.GetState();

            // Exit game on pressing Touchpad/SELECT/Left/Minus on controller or Escape on Keyboard+Mouse
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keysState.IsKeyDown(Keys.Escape)) Exit();

            // PLAYER ONE MOVEMENT
            if (keysState.IsKeyDown(Keys.W))
            {
                _p1Pos.Y -= _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_p1Pos.Y > _playerBounds.Top)
                {
                    _p1Size.X /= _sizeMultiplier.X;
                    _p1Size.Y /= _sizeMultiplier.Y;
                }
            }
            if (keysState.IsKeyDown(Keys.A)) _p1Pos.X -= _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keysState.IsKeyDown(Keys.S))
            {
                _p1Pos.Y += _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_p1Pos.Y < _playerBounds.Bottom)
                {
                    _p1Size.X *= _sizeMultiplier.X;
                    _p1Size.Y *= _sizeMultiplier.Y;
                }
            }
            if (keysState.IsKeyDown(Keys.D)) _p1Pos.X += _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // PLAYER TWO MOVEMENT
            if (keysState.IsKeyDown(Keys.Up))
            {
                _p2Pos.Y -= _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_p2Pos.Y > _playerBounds.Top)
                {
                    _p2Size.X /= _sizeMultiplier.X;
                    _p2Size.Y /= _sizeMultiplier.Y;
                }
            }
            if (keysState.IsKeyDown(Keys.Left)) _p2Pos.X -= _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keysState.IsKeyDown(Keys.Down))
            {
                _p2Pos.Y += _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_p2Pos.Y < _playerBounds.Bottom)
                {
                    _p2Size.X *= _sizeMultiplier.X;
                    _p2Size.Y *= _sizeMultiplier.Y;
                }
            }
            if (keysState.IsKeyDown(Keys.Right)) _p2Pos.X += _movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _p1Pos.X = float.Clamp(_p1Pos.X, _playerBounds.Left, _playerBounds.Right);
            _p1Pos.Y = float.Clamp(_p1Pos.Y, _playerBounds.Top, _playerBounds.Bottom);

            _p2Pos.X = float.Clamp(_p2Pos.X, _playerBounds.Left, _playerBounds.Right);
            _p2Pos.Y = float.Clamp(_p2Pos.Y, _playerBounds.Top, _playerBounds.Bottom);
          
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Size and position
            Rectangle p1Rect = new(_p1Pos.ToPoint(), _p1Size.ToPoint());
            Rectangle p2Rect = new(_p2Pos.ToPoint(), _p2Size.ToPoint());

            // Origins
            Vector2 p1Origin = new(_player1.Bounds.Width / 2f, _player1.Bounds.Height);
            Vector2 p2Origin = new(_player2.Bounds.Width / 2f, _player2.Bounds.Height);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_background, _screenBounds, Color.White); // Draw background

            // Draw characters
            if (_p1Pos.Y < _p2Pos.Y)
            {
                _spriteBatch.Draw(_player1, p1Rect, null, Color.White, 0f, p1Origin, SpriteEffects.None, 0f);
                _spriteBatch.Draw(_player2, p2Rect, null, Color.White, 0f, p2Origin, SpriteEffects.None, 0f);
            }
            else
            {
                _spriteBatch.Draw(_player2, p2Rect, null, Color.White, 0f, p2Origin, SpriteEffects.None, 0f);
                _spriteBatch.Draw(_player1, p1Rect, null, Color.White, 0f, p1Origin, SpriteEffects.None, 0f);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
