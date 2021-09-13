using System.Drawing;
using System.Windows.Forms;

namespace ILDAViewer
{
    public partial class IDLAViewerControl : UserControl
    {
        ILDAIO.ILDA ilda = null;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private string _filename = "";

        private int _currentframe = 0;

        private bool _animate = false;

        public event Action<int> CurrentFrameChangeEvent;
        public event Action<int> TotalFrameChangeEvent;

        public IDLAViewerControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
            timer.Interval = 100;
            timer.Tick += AnimateFrame;
        }

        private void AnimateFrame(object? sender, EventArgs e)
        {
            CurrentFrame += 1;
        }

        public bool Animate { 
            get { return _animate; } 
            set { 
                _animate = value;
                if (_animate)
                    timer.Start();
                else
                    timer.Stop();
            } 
        }

        public string Filename { 
            get { return _filename; } 
            set {
                _filename = value;
                try
                {
                    ilda = ILDAIO.ILDAIO.Read(_filename);
                    UpdateFrameEvents();
                }
                catch 
                {  
                }
                
                Invalidate();
            }  
        }

        public int CurrentFrame { 
            get { return _currentframe; } 
            set
            {
                if (value < 0) return;
                _currentframe = value;
                Invalidate();
                UpdateFrameEvents();
            }
        }

        public int TotalFrames { get { return ilda != null ? ilda.Count : 0; } set { } }

        protected override void OnDragDrop(DragEventArgs e)
        {
            string[]? s = e.Data.GetData(DataFormats.FileDrop, false) as string[];
            if (s == null || s.Length == 0) return;
            Filename = s[0];
            UpdateFrameEvents();
        }

        private void UpdateFrameEvents()
        {
            if (CurrentFrameChangeEvent != null)
                CurrentFrameChangeEvent(CurrentFrame);
            if (TotalFrameChangeEvent != null)
                TotalFrameChangeEvent(TotalFrames);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var bg = new SolidBrush(BackColor);
            var offset = 6;
            e.Graphics.FillRectangle(bg, ClientRectangle);
            if (TotalFrames == 0) return;

            double max = ClientRectangle.Height - offset;

            if (ilda != null)
            {
                var frame = TotalFrames == 1 ? ilda[0] : ilda[CurrentFrame%(TotalFrames-1)];
                double left = ilda.Left;
                double top = ilda.Top;
                double bottom = ilda.Bottom;
                double right = ilda.Right;
                double width = right - left + 1;
                double height = top - bottom + 1;

                for (int i = 0; i < frame.Count-1; i++)
                {
                    if (frame[i+1].IsBlank) continue;   
                    var pen = new Pen(frame[i].RGBColor, 1f);
                    e.Graphics.DrawLine(pen, 
                        new Point { X = (int) ((((frame[i].X-left)/width)*max) + (ClientRectangle.Width/2 - max / 2) + offset), Y = (int) (((top - frame[i].Y-bottom)/height*max) - (ClientRectangle.Height/2) + offset) },
                        new Point { X = (int) (((frame[i+1].X-left)/width*max) + (ClientRectangle.Width/2 - max / 2) + offset), Y = (int)(((top - frame[i+1].Y-bottom)/height*max) - (ClientRectangle.Height / 2) + offset) }
                    );
                    pen.Dispose();
                }
            }
            base.OnPaint(e);
        }
    }
}
