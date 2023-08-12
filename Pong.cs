using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    private Paddle _leftPaddle;
    private Paddle _rightPaddle;
    private Ball _ball;



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


        _ball.DetectPosition(_rightPaddle, _leftPaddle);
        InputUpdate();
        _ball.Move();


        base.Update(gameTime);
    }

    private void InputUpdate()
    {
        // Left paddle
        Direction leftDirection = _inputHandler.GetLeftPaddleDirection();
        _leftPaddle.Move(leftDirection);
        if (leftDirection != Direction.None && _ball.Locked) // Only send the follow command if the paddle is moving
            _ball.FollowPaddle(leftDirection, Player.Left);

        if (_inputHandler.IsLeftLaunchPressed())
        {
            _ball.Launch(_leftPaddle.PaddleDirection);
        }

        // Right paddle
        Direction rightDirection = _inputHandler.GetRightPaddleDirection();
        _rightPaddle.Move(rightDirection);
        if (rightDirection != Direction.None && _ball.Locked) // Only send the follow command if the paddle is moving
            _ball.FollowPaddle(rightDirection, Player.Right);

        if (_inputHandler.IsRightLaunchPressed())
        {
            _ball.Launch(_rightPaddle.PaddleDirection);
        }
    }


    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _leftPaddle.Draw(_spriteBatch, _texture2D);
        _rightPaddle.Draw(_spriteBatch, _texture2D);
        _ball.Draw(_spriteBatch, _texture2D);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    public void CreatePaddles()
    {
        _leftPaddle = new Paddle(Paddle.Player.Left, Color.White, GraphicsDevice.Viewport);
        _rightPaddle = new Paddle(Paddle.Player.Right, Color.White, GraphicsDevice.Viewport);
        _ball = new Ball(GraphicsDevice.Viewport, selectRandomPlayer());
    }

    public Player selectRandomPlayer()
    {
        var randomPlayer = new Random().Next(0, 2);
        //return randomPlayer == 0 ? Player.Left : Player.Right;
        return Player.Right;
    }
}
