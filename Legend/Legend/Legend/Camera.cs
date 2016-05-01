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

        private GraphicsDevice graphicsDevice;
        public GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
            set { graphicsDevice = value; }
        }


        public Camera(GraphicsDevice graphicsDevice, Vector2 position = default(Vector2), float rotation = (float)Math.PI)
        {
            this.graphicsDevice = graphicsDevice;
            this.position = position;
            this.rotation = rotation;
            UpdateView();
            UpdateProjection();
        }

        public void UpdateView()
        {
            view = Matrix.CreateLookAt(position.ToVector3(-1), position.ToVector3(0), new Vector3((float)Math.Sin(rotation), (float)Math.Cos(rotation), 0));
        }

        public void UpdateProjection()
        {
            projection = Matrix.CreateOrthographic(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 10);
        }
    }
}
