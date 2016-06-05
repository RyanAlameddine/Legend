using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Legend
{
    public class Camera
    {
        private static Camera main;
        public static Camera Main
        {
            get
            {
                return main;
            }
            set
            {
                if (main == null)
                {
                    main = value;
                }
            }
        }

        private Matrix view;
        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Matrix projection;
        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateView();
            }
        }

        private Vector2 offset;
        public Vector2 Offset
        {
            get
            {
                return offset;
            }
            set
            {
                offset = value;
                UpdateView();
            }
        }

        private float rotation;
        public float Rotation
        {
            get 
            { 
                return rotation; 
            }
            set 
            { 
                rotation = value;
                UpdateView();
            }
        }

        private Vector2 scale;

        public Vector2 Scale
        {
            get { return scale; }
            set 
            {
                scale = value;
                UpdateProjection();
            }
        }
        

        private GraphicsDevice graphicsDevice;
        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
            set { graphicsDevice = value; }
        }


        public Camera(GraphicsDevice graphicsDevice, Vector2 position = default(Vector2), float rotation = (float)Math.PI)
            : this(graphicsDevice, position, rotation, Vector2.One)
        {}

        public Camera(GraphicsDevice graphicsDevice, Vector2 position, float rotation, Vector2 scale)
        {
            this.graphicsDevice = graphicsDevice;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            UpdateView();
            UpdateProjection();
            Main = this;
        }

        public void UpdateView()
        {
            Vector2 pos = position + offset;
            view = Matrix.CreateLookAt(pos.ToVector3(-1), pos.ToVector3(0), new Vector3((float)Math.Sin(rotation), (float)Math.Cos(rotation), 0));
        }

        public void UpdateProjection()
        {
            projection = Matrix.CreateOrthographic(graphicsDevice.Viewport.Width * scale.X, graphicsDevice.Viewport.Height * scale.Y, 0, 10);
        }
    }
}
