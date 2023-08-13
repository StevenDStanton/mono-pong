using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using pong.objects;
using static pong.objects.InputHandler;
using static pong.objects.Paddle;



namespace pong;

public class Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private InputHandler _inputHandler;
    private Texture2D _texture2D;

    private Paddle LeftPaddle;
    private Paddle RightPaddle;
    private GameBall Ball;
    private GameCounter Counter;
    private Song newDestinationsSong;




    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _inputHandler = new InputHandler();
    }

    protected override void Initialize()
    {
        base.Initialize();
        CreatePaddles();
        Counter = new GameCounter(Content.Load<SpriteFont>("Arial"));
        newDestinationsSong = Content.Load<Song>("songs/LOOP_New Destinations");
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.5f; // 50% volume
        MediaPlayer.Play(newDestinationsSong);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _texture2D = new Texture2D(GraphicsDevice, 1, 1);
        _texture2D.SetData(new[] { Color.White });


    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        InputUpdate(deltaTime);
        Ball.Move(RightPaddle, LeftPaddle, Counter, deltaTime);


        base.Update(gameTime);
    }

    private void InputUpdate(float deltaTime)
    {
        // Left paddle
        Direction leftDirection = _inputHandler.GetLeftPaddleDirection();
        LeftPaddle.Move(leftDirection, deltaTime);

        if (_inputHandler.IsLeftLaunchPressed())
        {
            Ball.Launch(LeftPaddle.PaddleDirection);
        }

        // Right paddle
        Direction rightDirection = _inputHandler.GetRightPaddleDirection();
        RightPaddle.Move(rightDirection, deltaTime);

        if (_inputHandler.IsRightLaunchPressed())
        {
            Ball.Launch(RightPaddle.PaddleDirection);
        }
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        LeftPaddle.Draw(_spriteBatch, _texture2D);
        RightPaddle.Draw(_spriteBatch, _texture2D);
        Ball.Draw(_spriteBatch, _texture2D);
        Counter.Draw(_spriteBatch, GraphicsDevice.Viewport);





        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public void CreatePaddles()
    {
        LeftPaddle = new Paddle(Paddle.Player.Left, Color.Green, GraphicsDevice.Viewport);
        RightPaddle = new Paddle(Paddle.Player.Right, Color.Green, GraphicsDevice.Viewport);
        Ball = new GameBall(GraphicsDevice.Viewport, SelectRandomPlayer(), LeftPaddle, RightPaddle, Content);
    }

    public Player SelectRandomPlayer()
    {
        var randomPlayer = new Random().Next(0, 2);
        return randomPlayer == 1 ? Player.Left : Player.Right;
    }


}
