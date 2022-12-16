using eindprojectGameDev;
using eindprojectGameDev.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

class Block : IGameObject
{
    public Rectangle BoundingBox { get; set; }
    public bool Passable { get; set; }
    private int x,y;
    //public Vector2 Position { get => position; set => position = value; }
    public Texture2D Texture { get; set; }
    public Block(int x, int y, int PositionX, int PositionY, Texture2D texture)
    {
        this.x = PositionX;
        this.y=PositionY;
        BoundingBox = new Rectangle(x*16, y*16, 16,16);
        Passable = false;
        Texture = texture;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, new Vector2(x,y), BoundingBox, Color.White);
    }
    /*public virtual void IsCollidedWithEvent
    (Character collider)
    {
        CollideWithEvent.Execute();
    }
    */
    public void Update(GameTime gameTime)
    {
        throw new System.NotImplementedException();
    }
}
