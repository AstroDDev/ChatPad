using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using ChatPad.Configuration;
using ChatPad.Configuration.JSONObjects;

namespace ChatPad.Graphics
{
    public enum PaintButtonType
    {
        None, Button, Stick, Motion
    }
    public enum PaintButtonControls
    {
        None, A, B, X, Y, Up, Down, Left, Right, LeftBumper, RightBumper, LeftTrigger, RightTrigger, LeftStick, RightStick, Plus, Minus, Motion
    }

    internal class PaintButton
    {
        public PaintButtonControls Control;
        public RectangleF DrawRect { get; private set; }
        public RectangleF InputRect { get; private set; }
        public PointF DrawCenter { get; private set; }
        public PointF InputCenter { get; private set; }
        public bool CircularInput;
        private static readonly string[] inputNames = { "null", "A Button", "B Button", "X Button", "Y Button", "D-Pad Up", "D-Pad Down", "D-Pad Left", "D-Pad Right", "Left Bumper", "Right Bumper", "Left Trigger", "Right Trigger", "Left Stick", "Right Stick", "Plus", "Minus" };
        public string Name { get 
            {
                return inputNames[(int)Control];
            } 
        }
        public PaintButtonType Type;
        public CommandButton Button { get { if (Control != PaintButtonControls.None && Control != PaintButtonControls.Motion) { return Config.Commands.ButtonMap[(int)Control - 1]; } else return null; } }
        public CommandAxis AxisX {
            get
            {
                switch (Control) 
                {
                    case PaintButtonControls.LeftStick:
                        return Config.Commands.LeftStickXAxis;
                    case PaintButtonControls.RightStick:
                        return Config.Commands.RightStickXAxis;
                    case PaintButtonControls.Motion:
                        return Config.Commands.MotionXAxis;
                    default: return null;
                }
            }
        }
        public CommandAxis AxisY
        {
            get
            {
                switch (Control)
                {
                    case PaintButtonControls.LeftStick:
                        return Config.Commands.LeftStickYAxis;
                    case PaintButtonControls.RightStick:
                        return Config.Commands.RightStickYAxis;
                    case PaintButtonControls.Motion:
                        return Config.Commands.MotionYAxis;
                    default: return null;
                }
            }
        }
        public CommandAxis AxisZ { get { if (Control == PaintButtonControls.Motion) { return Config.Commands.MotionZAxis; } else return null; } }

        public PaintButton(PaintButtonControls i, RectangleF drawRect, RectangleF inputRect, bool circularInput, PaintButtonType type)
        {
            Control = i;
            DrawRect = drawRect;
            InputRect = inputRect;
            CircularInput = circularInput;
            DrawCenter = new PointF(DrawRect.X + (DrawRect.Width / 2), DrawRect.Y + (DrawRect.Height / 2));
            InputCenter = new PointF(InputRect.X + (InputRect.Width / 2), InputRect.Y + (InputRect.Height / 2));
            Type = type;
        }
        public PaintButton(PaintButtonControls i, float x, float y, float width, float height, bool circularInput, PaintButtonType type)
        {
            Control = i;
            DrawRect = new RectangleF(x - (width / 2), y - (height / 2), width, height);
            InputRect = DrawRect;
            CircularInput = circularInput;
            DrawCenter = new PointF(x, y);
            InputCenter = new PointF(x, y);
            Type = type;
        }
        public PaintButton(PaintButtonControls i, float x, float y, float width, float height, RectangleF inputRect, bool circularInput, PaintButtonType type)
        {
            Control = i;
            DrawRect = new RectangleF(x - (width / 2), y - (height / 2), width, height);
            InputRect = inputRect;
            CircularInput = circularInput;
            DrawCenter = new PointF(x, y);
            InputCenter = new PointF(x, y);
            Type = type;
        }

        public bool InHovered(PointF point)
        {
            if (CircularInput)
            {
                float normalizedXDistance = (point.X - InputCenter.X) / (InputRect.Width / 2);
                float normalizedYDistance = (point.Y - InputCenter.Y) / (InputRect.Height / 2);

                return Math.Sqrt(Math.Pow(normalizedXDistance, 2) + Math.Pow(normalizedYDistance, 2)) < 1;
            }
            else
            {
                return InputRect.Contains(point);
            }
        }

        public void SetDrawCenter(float x, float y)
        {
            DrawRect = new RectangleF(x - (DrawRect.Width / 2), y - (DrawRect.Height / 2), DrawRect.Width, DrawRect.Height);
            DrawCenter = new PointF(x, y);
        }

        public void SetDrawCenter(PointF point)
        {
            SetDrawCenter(point.X, point.Y);
        }

        public static PointF TransformPoint(PointF point, SizeF panelSize, SizeF drawingSize)
        {
            float scaleX = panelSize.Width / drawingSize.Width;
            float scaleY = panelSize.Height / drawingSize.Height;

            PointF transformedPoint = new PointF(
                (point.X - (panelSize.Width / 2)) / Math.Min(scaleX, scaleY), 
                (point.Y - (panelSize.Height / 2)) / Math.Min(scaleX, scaleY)
            );

            return transformedPoint;
        }
    }
}
