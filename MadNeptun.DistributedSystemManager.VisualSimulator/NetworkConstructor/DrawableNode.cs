using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace MadNeptun.DistributedSystemManager.VisualSimulator.NetworkConstructor
{
    internal class DrawableNode : Drawable
    {
        /// <summary>
        /// If id used as default value / 0 then id will be generated
        /// </summary>
        /// <param name="place"></param>
        /// <param name="id"></param>
        public DrawableNode(Point place, int id = 0)
            : base(new Point[] { new Point(-25, -25), new Point(-25, 25), new Point(25, 25), new Point(25, -25)})
        {
            this.CenterPoint = place;
            if(id>0)
            {
                Id = id;
            }
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            Pen pen2 = new Pen(BgColor, 2);
            g.DrawPolygon(pen, this.RecalculatePolygon().ToArray());
            g.FillPolygon(pen2.Brush, this.RecalculatePolygon().ToArray());
            g.DrawString(this.Id.ToString(), new Font("Arial", 12), pen.Brush, this.CenterPoint.X-6, this.CenterPoint.Y-10);
        }

        public override string Name()
        {
            return "Node Id: " + Id.ToString();
        }
    }
}
