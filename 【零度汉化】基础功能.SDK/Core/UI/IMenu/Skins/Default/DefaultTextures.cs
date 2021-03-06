﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultTextures.cs" company="LeagueSharp">
//   Copyright (C) 2015 LeagueSharp
//   
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
// <summary>
//   A default implementation of <see cref="ADrawable{MenuButton}" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueSharp.SDK.Core.UI.IMenu.Skins.Default
{
    using System.Drawing;

    using LeagueSharp.SDK.Properties;

    using SharpDX;
    using SharpDX.Direct3D9;

    internal enum DefaultTexture
    {
        Dragging
    }

    internal class DefaultTextures
    {

        private readonly Dictionary<DefaultTexture, TextureWrapper> textures = new Dictionary<DefaultTexture, TextureWrapper>();

        public static readonly DefaultTextures Instance = new DefaultTextures();

        private DefaultTextures()
        {
            textures[DefaultTexture.Dragging] = BuildTexture(Resources.cursor_drag, 16, 16);
        }

        ~DefaultTextures()
        {
            foreach (var entry in textures.Where(entry => !entry.Value.Texture.IsDisposed)) {
                entry.Value.Texture.Dispose();
            }
        }

        public TextureWrapper this[DefaultTexture textureType]
        {
            get
            {
                return textures[textureType];
            }
        }

        private static TextureWrapper BuildTexture(Image bmp, int height, int width)
        {
            var resized = new Bitmap(bmp, width, height);
            var texture =  Texture.FromMemory(
                Drawing.Direct3DDevice,
                (byte[])new ImageConverter().ConvertTo(resized, typeof(byte[])),
                resized.Width,
                resized.Height,
                0,
                Usage.None,
                Format.A1,
                Pool.Managed,
                Filter.Default,
                Filter.Default,
                0);
            resized.Dispose();
            bmp.Dispose();
            return new TextureWrapper(texture, width, height);
        }
        
    }

    internal class TextureWrapper
    {
        public Texture Texture { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public TextureWrapper(Texture texture, int width, int height)
        {
            Texture = texture;
            Width = width;
            Height = height;
        }
        
    }
}
