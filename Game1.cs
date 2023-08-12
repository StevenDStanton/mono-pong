using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong;

public class Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _texture2D;


    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
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
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        
        var screenHeight = GraphicsDevice.Viewport.Height;
        var screenWidth = GraphicsDevice.Viewport.Width;
        var screenCenter = screenHeight / 2;

        drawPaddle(screenCenter, 10);
        drawPaddle(screenCenter, screenWidth - 20);
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    protected void drawPaddle(int screenCenter, int paddleOffset)
    {
        Color playerColor = Color.White;
        var paddleSize = 161;
        var paddleCenter = paddleSize / 2;
        var center = screenCenter - paddleCenter;
        var paddle = new Rectangle(paddleOffset , center, 10, 161);
        _spriteBatch.Draw(_texture2D, paddle, playerColor);
    }

    

}
