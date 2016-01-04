using System.Drawing;
using System.Linq;

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
            : base(new[] { new Point(-25, -25), new Point(-25, 25), new Point(25, 25), new Point(25, -25)})
        {
            CenterPoint = place;
            if(id>0)
            {
                Id = id;
            }
        }

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            Pen pen2 = new Pen(BgColor, 2);
            g.DrawPolygon(pen, RecalculatePolygon().ToArray());
            g.FillPolygon(pen2.Brush, RecalculatePolygon().ToArray());
            g.DrawString(Id.ToString(), new Font("Arial", 12), pen.Brush, CenterPoint.X-6, CenterPoint.Y-10);
        }

        public override string Name()
        {
            return "Node Id: " + Id;
        }
    }
}
