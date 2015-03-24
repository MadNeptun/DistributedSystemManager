using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    internal abstract class Drawable
    {
        private static int _nextId = 1;
        private static int NextId { get { return _nextId++; } } 

        protected IEnumerable<Point> Polygon { get; private set; }

        /// <summary>
        /// Polygon representation in Cartesian (2D) coordinate system as vectors beginning at (0,0) point
        /// </summary>
        /// <param name="polygon"></param>
        protected Drawable(IEnumerable<Point> polygon)
        {
            Polygon = polygon;
            Id = NextId;
            BgColor = Color.Orange;
        }

        public Point CenterPoint { get; set; }

        public int Id { get; protected set; }

        protected IEnumerable<Point> RecalculatePolygon()
        {
            return Polygon.Select(p => new Point(CenterPoint.X + p.X, CenterPoint.Y + p.Y));
        }

        public Color BgColor { get; set; }

        public abstract string Name();

        public abstract void Draw(Graphics g);

        public virtual bool WasHit(Point p)
        {
            var maxX = Polygon.Max(g => Math.Abs(g.X));
            var maxY = Polygon.Max(g => Math.Abs(g.Y));
            var distancePtoCentral = Math.Sqrt(Math.Pow(p.X-CenterPoint.X,2)+Math.Pow(p.Y-CenterPoint.Y,2));
            var maxAllowedDistance = Math.Sqrt(Math.Pow(maxX, 2) + Math.Pow(maxY, 2));
            return maxAllowedDistance >= distancePtoCentral;
        }

        public override string ToString()
        {
            return Name();
        }
    }
}
