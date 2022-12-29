using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace eindprojectGameDev.Map
{
    public class BlockFactory
    {
        enum blockOrder { first, middle, last };
        public static Block[,] CreateBlocks(char[,] blockArray, Texture2D texture, ContentManager content)
        {
            char previousChar = '#', currentChar = '#', nextChar = '#', upperChar = '#', underChar = '#';
            Block[,] Tileset = new Block[blockArray.GetLength(0), blockArray.GetLength(1)];

            for (int k = 0; k < blockArray.GetLength(0); k++)
                for (int l = 0; l < blockArray.GetLength(1); l++)
                {
                    previousChar = '#';
                    nextChar = '#';
                    upperChar = '#';
                    underChar = '#';
                    currentChar = '#';
                    //horizontaal
                    if (k == 0)
                    {
                        previousChar = '#';
                        nextChar = blockArray[k + 1, l];
                    }
                    else if (k == blockArray.GetLength(0) - 1)
                    {
                        previousChar = blockArray[k - 1, l];
                        nextChar = '#';
                    }
                    else
                    {
                        previousChar = blockArray[k - 1, l];
                        nextChar = blockArray[k + 1, l];
                    }

                    if (l == 0)
                    {
                        upperChar = '#';
                        underChar = blockArray[k, l + 1];
                    }
                    else if (l == blockArray.GetLength(1) - 1)
                    {
                        underChar = '#';
                        upperChar = blockArray[k, l - 1];
                    }
                    else
                    {
                        underChar = blockArray[k, l + 1];
                        upperChar = blockArray[k, l - 1];
                    }
                    currentChar = blockArray[k, l];
                    switch (currentChar)
                    {
                        case '.':
                            //first row
                            if (upperChar == '#')
                            {
                                if (previousChar == '#')
                                {
                                    Tileset[k, l] = new Block(0, 0, l * 16, (16 * k), content);
                                }
                                else if (nextChar == '#')
                                {
                                    Tileset[k, l] = new Block(0, 2, l * 16, (16 * k), content);
                                }
                                else
                                {
                                    Tileset[k, l] = new Block(0, 1, l * 16, (16 * k), content);
                                }
                            }

                            //second row
                            else if (upperChar == '.' && underChar == '.')
                            {
                                if (previousChar == '#' && nextChar == '.')
                                {
                                    Tileset[k, l] = new Block(1, 0, l * 16, (16 * k), content);
                                }
                                else if (previousChar == '.' && nextChar == '.')
                                {
                                    Tileset[k, l] = new Block(1, 1, l * 16, (16 * k), content);
                                }
                                else if (previousChar == '.' && nextChar == '#')
                                {
                                    Tileset[k, l] = new Block(1, 2, l * 16, (16 * k), content);
                                }
                            }

                            //third row
                            else if (underChar == '#')
                            {
                                if (previousChar == '#')
                                {
                                    Tileset[k, l] = new Block(2, 0, l * 16, (16 * k), content);
                                }
                                else if (previousChar == '.' && nextChar == '.')
                                {
                                    Tileset[k, l] = new Block(2, 1, l * 16, (16 * k), content);
                                }
                                else if (previousChar == '.' && nextChar == '#')
                                {
                                    Tileset[k, l] = new Block(2, 2, l * 16, (16 * k), content);
                                }
                            }
                            break;
                        default:
                            Tileset[k, l] = null;
                            break;
                    }
                }
            return Tileset;
        }
    }
}
