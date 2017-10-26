namespace OOPEncapsValid
{
    using System;
    using System.Runtime.Remoting.Messaging;
    using System.Text;

    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        {
            get { return this.length; }
           private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Length cannot be zero or negative.");
                }
                this.length = value;
            }
        }

        public double Width
        {
            get { return this.width; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width cannot be zero or negative.");
                }
                this.width = value;
            }
        }

        public double Height
        {
            get { return this.height; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height cannot be zero or negative.");
                }
                this.height = value;
            }
        }

        private double SurfaceAreaP()
        {
            return 2 * (this.Length * this.Width + this.Height * this.Length + this.Width * this.Height);
        }

       private double LateralSurfaceAreaP()
        {
            return 2 * this.Height * (this.Length + this.Width);
        }

        private double VolumeP()
        {
            return this.Length * this.Height * this.Width;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Surface Area – {this.SurfaceAreaP():f2}");
            sb.AppendLine($"Lateral Surface Area – {this.LateralSurfaceAreaP():f2}");
            sb.Append($"Volume – {this.VolumeP():f2}");

            return sb.ToString();
        }

        public void SurfaceArea()
        {
            var area = 2 * (this.Length * this.Width + this.Width * this.Height + this.Height * this.Length);
            Console.WriteLine($"Surface Area - {area:f2}");
        }

        public void LateralSurfaceArea()
        {
            var area = 2 * this.Height * (this.Length + this.Width);
            Console.WriteLine($"Lateral Surface Area - {area:f2}");
        }

        public void Volume()
        {
            var area = this.Height * this.Length * this.Width;
            Console.WriteLine($"Volume - {area:f2}");
        }
    }
}
