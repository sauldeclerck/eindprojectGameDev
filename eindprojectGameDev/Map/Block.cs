using eindprojectGameDev.Characters;
using eindprojectGameDev.interfaces;
using eindprojectGameDev.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Windows.Forms;

public class Block : IGameObject
{
    public Rectangle BoundingBox { get; set; }
    public bool Passable { get; set; }
    private Vector2 position;
    public Rectangle Hitbox { get; set; }
    public Texture2D Texture { get; set; }
    public Block(int x, int y, int PositionX, int PositionY, ContentManager content)
    {
        this.position = new Vector2(PositionX, PositionY);
        BoundingBox = new Rectangle(x*16, y*16, 16,16);
        Hitbox = new Rectangle(PositionX, PositionY, 16, 16);
        Passable = false;
        Texture = content.Load<Texture2D>("tileset");
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, position, BoundingBox, Color.White);
    }
    public void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }
}
