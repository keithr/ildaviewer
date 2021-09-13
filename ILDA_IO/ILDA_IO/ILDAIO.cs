
using System.Collections;
using System.Drawing;
using System.Text;

namespace ILDAIO;

public interface IILDAFrame : IList<ILDAPoint>
{
    string Name { get; set;  }
    int Left { get; }
    int Top { get; }

    int Right { get; }

    int Bottom { get; }
}

public interface ILDAPoint
{
    public bool IsBlank { get; }
    public bool IsLast { get; }
    public Int16 X { get; set; }
    public Int16 Y { get; set; }

    public int Status { get; set; }
    public int ColorIndex { set; get; }

    public System.Drawing.Color RGBColor { get { return ColorTable.Standard[ColorIndex]; } set { }  }
}

public class ILDAPoint3D : ILDAPoint
{
    private Color _rgbcolor;
    private int _colorIndex = 0;

    public Int16 X { get; set; }
    public Int16 Y { get; set; }
    public Int16 Z { get; set; }

    public int Status { get; set; }
    public int ColorIndex { 
        set {
            if (value >= 0 && value <= 255)
            {
                _rgbcolor = ColorTable.Standard[value];
                _colorIndex = value;
            }
        } 
        get { return _colorIndex; } 
    }
    public Color RGBColor { get => _rgbcolor; set { _rgbcolor = value; } }

    public bool IsBlank => (Status & 64) != 0;

    public bool IsLast => (Status & 128) != 0;

    Color ILDAPoint.RGBColor { get { return RGBColor; } set { RGBColor = value; } }

    override public string ToString()
    {
        string status = "";
        if (IsLast)
        {
            status += ",Last Point";
        }
        if (IsBlank)
        {
            status += ",blank";
        }
        return $"[{X},{Y},{Z}]-ColorIndex:{ColorIndex}" + status;
    }
}

public class ILDAPoint2D : ILDAPoint
{
    private Color _rgbcolor;
    private int _colorIndex = 0;

    public bool IsBlank => (Status & 64) != 0;

    public bool IsLast => (Status & 128) != 0;

    public Int16 X { get; set; }
    public Int16 Y { get; set; }

    public int Status { get; set; }
    public int ColorIndex
    {
        set
        {
            if (value >= 0 && value <= 255)
            {
                _rgbcolor = ColorTable.Standard[value];
                _colorIndex = value;
            }
        }
        get { return _colorIndex; }
    }
    public Color RGBColor { get => _rgbcolor; set { _rgbcolor = value; } }

    override public string ToString()
    {
        string status = "";
        if (IsLast)
        {
            status += ",Last Point";
        }
        if (IsBlank)
        {
            status += ",blank";
        }
        return $"[{X},{Y}]-ColorIndex:{ColorIndex}" + status;
    }
}

public static class ColorTable
{
    #region ILDA Standard 
    private static Color[] _standard = new Color[] {
        Color.FromArgb(255, 255, 255, 255),	    // Black/blanked (fixed)
        Color.FromArgb(255, 255, 255, 255),	    // White (fixed)
        Color.FromArgb( 255,   0,   0),         // Red (fixed)
        Color.FromArgb(255, 255, 255,   0),     // Yellow (fixed)
        Color.FromArgb(255,   0, 255,   0),     // Green (fixed)
        Color.FromArgb(255,  0, 255, 255),      // Cyan (fixed)
        Color.FromArgb(255,   0,   0, 255 ),    // Blue (fixed)
        Color.FromArgb(255, 255,   0, 255 ),    // Magenta (fixed)
        Color.FromArgb(255, 255, 128, 128 ),    // Light red
        Color.FromArgb(255, 255, 140, 128 ),
        Color.FromArgb(255, 255, 151, 128 ),
        Color.FromArgb(255, 255, 163, 128 ),
        Color.FromArgb(255, 255, 174, 128 ),
        Color.FromArgb(255, 255, 186, 128 ),
        Color.FromArgb(255, 255, 197, 128 ),
        Color.FromArgb(255, 255, 209, 128 ),
        Color.FromArgb(255, 255, 220, 128 ),
        Color.FromArgb(255, 255, 232, 128 ),
        Color.FromArgb(255, 255, 243, 128 ),
        Color.FromArgb(255, 255, 255, 128 ),	// Light yellow
        Color.FromArgb(255, 243, 255, 128 ),
        Color.FromArgb(255, 232, 255, 128 ),
        Color.FromArgb(255, 220, 255, 128 ),
        Color.FromArgb(255, 209, 255, 128 ),
        Color.FromArgb(255, 197, 255, 128 ),
        Color.FromArgb(255, 186, 255, 128 ),
        Color.FromArgb(255, 174, 255, 128 ),
        Color.FromArgb(255, 163, 255, 128 ),
        Color.FromArgb(255, 151, 255, 128 ),
        Color.FromArgb(255, 140, 255, 128 ),
        Color.FromArgb(255, 128, 255, 128 ),	// Light green
        Color.FromArgb(255, 128, 255, 140 ),
        Color.FromArgb(255, 128, 255, 151 ),
        Color.FromArgb(255, 128, 255, 163 ),
        Color.FromArgb(255, 128, 255, 174 ),
        Color.FromArgb(255, 128, 255, 186 ),
        Color.FromArgb(255, 128, 255, 197 ),
        Color.FromArgb(255, 128, 255, 209 ),
        Color.FromArgb(255, 128, 255, 220 ),
        Color.FromArgb(255, 128, 255, 232 ),
        Color.FromArgb(255, 128, 255, 243 ),
        Color.FromArgb(255, 128, 255, 255 ),	// Light cyan
        Color.FromArgb(255, 128, 243, 255 ),
        Color.FromArgb(255, 128, 232, 255 ),
        Color.FromArgb(255, 128, 220, 255 ),
        Color.FromArgb(255, 128, 209, 255 ),
        Color.FromArgb(255, 128, 197, 255 ),
        Color.FromArgb(255, 128, 186, 255 ),
        Color.FromArgb(255, 128, 174, 255 ),
        Color.FromArgb(255, 128, 163, 255 ),
        Color.FromArgb(255, 128, 151, 255 ),
        Color.FromArgb(255, 128, 140, 255 ),
        Color.FromArgb(255, 128, 128, 255 ),	// Light blue
        Color.FromArgb(255, 140, 128, 255 ),
        Color.FromArgb(255, 151, 128, 255 ),
        Color.FromArgb(255, 163, 128, 255 ),
        Color.FromArgb(255, 174, 128, 255 ),
        Color.FromArgb(255, 186, 128, 255 ),
        Color.FromArgb(255, 197, 128, 255 ),
        Color.FromArgb(255, 209, 128, 255 ),
        Color.FromArgb(255, 220, 128, 255 ),
        Color.FromArgb(255, 232, 128, 255 ),
        Color.FromArgb(255, 243, 128, 255 ),
        Color.FromArgb(255, 255, 128, 255 ), // Light magenta
        Color.FromArgb(255, 255, 128, 243 ),
        Color.FromArgb(255, 255, 128, 232 ),
        Color.FromArgb(255, 255, 128, 220 ),
        Color.FromArgb(255, 255, 128, 209 ),
        Color.FromArgb(255, 255, 128, 197 ),
        Color.FromArgb(255, 255, 128, 186 ),
        Color.FromArgb(255, 255, 128, 174 ),
        Color.FromArgb(255, 255, 128, 163 ),
        Color.FromArgb(255, 255, 128, 151 ),
        Color.FromArgb(255, 255, 128, 140 ),
        Color.FromArgb(255, 255,   0,   0 ),	// Red (cycleable)
        Color.FromArgb(255, 255,  23,   0 ),
        Color.FromArgb(255, 255,  46,   0 ),
        Color.FromArgb(255, 255,  70,   0 ),
        Color.FromArgb(255, 255,  93,   0 ),
        Color.FromArgb(255, 255, 116,   0 ),
        Color.FromArgb(255, 255, 139,   0 ),
        Color.FromArgb(255, 255, 162,   0 ),
        Color.FromArgb(255, 255, 185,   0 ),
        Color.FromArgb(255, 255, 209,   0 ),
        Color.FromArgb(255, 255, 232,   0 ),
        Color.FromArgb(255, 255, 255,   0 ),	//Yellow (cycleable)
        Color.FromArgb(255, 232, 255,   0 ),
        Color.FromArgb(255, 209, 255,   0 ),
        Color.FromArgb(255, 185, 255,   0 ),
        Color.FromArgb(255, 162, 255,   0 ),
        Color.FromArgb(255, 139, 255,   0 ),
        Color.FromArgb(255, 116, 255,   0 ),
        Color.FromArgb(255,  93, 255,   0 ),
        Color.FromArgb(255,  70, 255,   0 ),
        Color.FromArgb(255,  46, 255,   0 ),
        Color.FromArgb(255,  23, 255,   0 ),
        Color.FromArgb(255,   0, 255,   0 ),	// Green (cycleable)
        Color.FromArgb(255,   0, 255,  23 ),
        Color.FromArgb(255,   0, 255,  46 ),
        Color.FromArgb(255,   0, 255,  70 ),
        Color.FromArgb(255,   0, 255,  93 ),
        Color.FromArgb(255,   0, 255, 116 ),
        Color.FromArgb(255,   0, 255, 139 ),
        Color.FromArgb(255,   0, 255, 162 ),
        Color.FromArgb(255,   0, 255, 185 ),
        Color.FromArgb(255,   0, 255, 209 ),
        Color.FromArgb(255,   0, 255, 232 ),
        Color.FromArgb(255,   0, 255, 255 ),	// Cyan (cycleable)
        Color.FromArgb(255,   0, 232, 255 ),
        Color.FromArgb(255,   0, 209, 255 ),
        Color.FromArgb(255,   0, 185, 255 ),
        Color.FromArgb(255,   0, 162, 255 ),
        Color.FromArgb(255,   0, 139, 255 ),
        Color.FromArgb(255,   0, 116, 255 ),
        Color.FromArgb(255,   0,  93, 255 ),
        Color.FromArgb(255,   0,  70, 255 ),
        Color.FromArgb(255,   0,  46, 255 ),
        Color.FromArgb(255,   0,  23, 255 ),
        Color.FromArgb(255,   0,   0, 255 ),	// Blue (cycleable)
        Color.FromArgb(255,  23,   0, 255 ),
        Color.FromArgb(255,  46,   0, 255 ),
        Color.FromArgb(255,  70,   0, 255 ),
        Color.FromArgb(255,  93,   0, 255 ),
        Color.FromArgb(255, 116,   0, 255 ),
        Color.FromArgb(255, 139,   0, 255 ),
        Color.FromArgb(255, 162,   0, 255 ),
        Color.FromArgb(255, 185,   0, 255 ),
        Color.FromArgb(255, 209,   0, 255 ),
        Color.FromArgb(255, 232,   0, 255 ),
        Color.FromArgb(255, 255,   0, 255 ),	// Magenta (cycleable)
        Color.FromArgb(255, 255,   0, 232 ),
        Color.FromArgb(255, 255,   0, 209 ),
        Color.FromArgb(255, 255,   0, 185 ),
        Color.FromArgb(255, 255,   0, 162 ),
        Color.FromArgb(255, 255,   0, 139 ),
        Color.FromArgb(255, 255,   0, 116 ),
        Color.FromArgb(255, 255,   0,  93 ),
        Color.FromArgb(255, 255,   0,  70 ),
        Color.FromArgb(255, 255,   0,  46 ),
        Color.FromArgb(255, 255,   0,  23 ),
        Color.FromArgb(255, 128,   0,   0 ),	// Dark red
        Color.FromArgb(255, 128,  12,   0 ),
        Color.FromArgb(255, 128,  23,   0 ),
        Color.FromArgb(255, 128,  35,   0 ),
        Color.FromArgb(255, 128,  47,   0 ),
        Color.FromArgb(255, 128,  58,   0 ),
        Color.FromArgb(255, 128,  70,   0 ),
        Color.FromArgb(255, 128,  81,   0 ),
        Color.FromArgb(255, 128,  93,   0 ),
        Color.FromArgb(255, 128, 105,   0 ),
        Color.FromArgb(255, 128, 116,   0 ),
        Color.FromArgb(255, 128, 128,   0 ),	// Dark yellow
        Color.FromArgb(255, 116, 128,   0 ),
        Color.FromArgb(255, 105, 128,   0 ),
        Color.FromArgb(255,  93, 128,   0 ),
        Color.FromArgb(255,  81, 128,   0 ),
        Color.FromArgb(255,  70, 128,   0 ),
        Color.FromArgb(255,  58, 128,   0 ),
        Color.FromArgb(255,  47, 128,   0 ),
        Color.FromArgb(255,  35, 128,   0 ),
        Color.FromArgb(255,  23, 128,   0 ),
        Color.FromArgb(255,  12, 128,   0 ),
        Color.FromArgb(255,   0, 128,   0 ),	// Dark green
        Color.FromArgb(255,   0, 128,  12 ),
        Color.FromArgb(255,   0, 128,  23 ),
        Color.FromArgb(255,   0, 128,  35 ),
        Color.FromArgb(255,   0, 128,  47 ),
        Color.FromArgb(255,   0, 128,  58 ),
        Color.FromArgb(255,   0, 128,  70 ),
        Color.FromArgb(255,   0, 128,  81 ),
        Color.FromArgb(255,   0, 128,  93 ),
        Color.FromArgb(255,   0, 128, 105 ),
        Color.FromArgb(255,   0, 128, 116 ),
        Color.FromArgb(255,   0, 128, 128 ),	// Dark cyan
        Color.FromArgb(255,   0, 116, 128 ),
        Color.FromArgb(255,   0, 105, 128 ),
        Color.FromArgb(255,   0,  93, 128 ),
        Color.FromArgb(255,   0,  81, 128 ),
        Color.FromArgb(255,   0,  70, 128 ),
        Color.FromArgb(255,   0,  58, 128 ),
        Color.FromArgb(255,   0,  47, 128 ),
        Color.FromArgb(255,   0,  35, 128 ),
        Color.FromArgb(255,   0,  23, 128 ),
        Color.FromArgb(255,   0,  12, 128 ),
        Color.FromArgb(255,   0,   0, 128 ),	// Dark blue
        Color.FromArgb(255,  12,   0, 128 ),
        Color.FromArgb(255,  23,   0, 128 ),
        Color.FromArgb(255,  35,   0, 128 ),
        Color.FromArgb(255,  47,   0, 128 ),
        Color.FromArgb(255,  58,   0, 128 ),
        Color.FromArgb(255,  70,   0, 128 ),
        Color.FromArgb(255,  81,   0, 128 ),
        Color.FromArgb(255,  93,   0, 128 ),
        Color.FromArgb(255, 105,   0, 128 ),
        Color.FromArgb(255, 116,   0, 128 ),
        Color.FromArgb(255, 128,   0, 128 ),	// Dark magenta
        Color.FromArgb(255, 128,   0, 116 ),
        Color.FromArgb(255, 128,   0, 105 ),
        Color.FromArgb(255, 128,   0,  93 ),
        Color.FromArgb(255, 128,   0,  81 ),
        Color.FromArgb(255, 128,   0,  70 ),
        Color.FromArgb(255, 128,   0,  58 ),
        Color.FromArgb(255, 128,   0,  47 ),
        Color.FromArgb(255, 128,   0,  35 ),
        Color.FromArgb(255, 128,   0,  23 ),
        Color.FromArgb(255, 128,   0,  12 ),
        Color.FromArgb(255, 255, 192, 192 ),	// Very light red
        Color.FromArgb(255, 255,  64,  64 ),	// Light-medium red
        Color.FromArgb(255, 192,   0,   0 ),	// Medium-dark red
        Color.FromArgb(255,  64,   0,   0 ),	// Very dark red
        Color.FromArgb(255, 255, 255, 192 ),	// Very light yellow
        Color.FromArgb(255, 255, 255,  64 ),	// Light-medium yellow
        Color.FromArgb(255, 192, 192,   0 ),	// Medium-dark yellow
        Color.FromArgb(255,  64,  64,   0 ),	// Very dark yellow
        Color.FromArgb(255, 192, 255, 192 ),	// Very light green
        Color.FromArgb(255,  64, 255,  64 ),	// Light-medium green
        Color.FromArgb(255,   0, 192,   0 ),	// Medium-dark green
        Color.FromArgb(255,   0,  64,   0 ),	// Very dark green
        Color.FromArgb(255, 192, 255, 255 ),	// Very light cyan
        Color.FromArgb(255,  64, 255, 255 ),	// Light-medium cyan
        Color.FromArgb(255,   0, 192, 192 ),	// Medium-dark cyan
        Color.FromArgb(255,   0,  64,  64 ),	// Very dark cyan
        Color.FromArgb(255, 192, 192, 255 ),	// Very light blue
        Color.FromArgb(255,  64,  64, 255 ),	// Light-medium blue
        Color.FromArgb(255,   0,   0, 192 ),	// Medium-dark blue
        Color.FromArgb(255,   0,   0,  64 ),	// Very dark blue
        Color.FromArgb(255, 255, 192, 255 ),	// Very light magenta
        Color.FromArgb(255, 255,  64, 255 ),	// Light-medium magenta
        Color.FromArgb(255, 192,   0, 192 ),	// Medium-dark magenta
        Color.FromArgb(255,  64,   0,  64 ),	// Very dark magenta
        Color.FromArgb(255, 255,  96,  96 ),	// Medium skin tone
        Color.FromArgb(255, 255, 255, 255 ),	// White (cycleable)
        Color.FromArgb(255, 245, 245, 245 ),
        Color.FromArgb(255, 235, 235, 235 ),
        Color.FromArgb(255, 224, 224, 224 ),	// Very light gray (7/8 intensity)
        Color.FromArgb(255, 213, 213, 213 ),
        Color.FromArgb(255, 203, 203, 203 ),
        Color.FromArgb(255, 192, 192, 192 ),	// Light gray (3/4 intensity)
        Color.FromArgb(255, 181, 181, 181 ),
        Color.FromArgb(255, 171, 171, 171 ),
        Color.FromArgb(255, 160, 160, 160 ),	// Medium-light gray (5/8 int.)
        Color.FromArgb(255, 149, 149, 149 ),
        Color.FromArgb(255, 139, 139, 139 ),
        Color.FromArgb(255, 128, 128, 128 ),	// Medium gray (1/2 intensity)
        Color.FromArgb(255, 117, 117, 117 ),
        Color.FromArgb(255, 107, 107, 107 ),
        Color.FromArgb(255,  96,  96,  96 ),	// Medium-dark gray (3/8 int.)
        Color.FromArgb(255,  85,  85,  85 ),
        Color.FromArgb(255,  75,  75,  75 ),
        Color.FromArgb(255,  64,  64,  64 ),	// Dark gray (1/4 intensity)
        Color.FromArgb(255,  53,  53,  53 ),
        Color.FromArgb(255,  43,  43,  43 ),
        Color.FromArgb(255,  32,  32,  32 ),	// Very dark gray (1/8 intensity)
        Color.FromArgb(255,  21,  21,  21 ),
        Color.FromArgb(255,  11,  11,  11 ),
        Color.FromArgb(255,   0,   0,   0 )	// Black
    };
    #endregion

    public static Color[] Standard => _standard;
}

public interface IILDA : IList<IILDAFrame>
{
    Rectangle Bounds { get; }
}

public class ILDA : IILDA
{
    public Rectangle Bounds => throw new NotImplementedException();

    private List<IILDAFrame> _contents = new List<IILDAFrame>();

    #region List
    public IILDAFrame this[int index] { get => _contents[index]; set => _contents[index] = value; }

    public int Count => _contents.Count;

    public bool IsReadOnly => false;

    public void Add(IILDAFrame item)
    {
        _contents.Add(item);
    }

    public void Clear()
    {
        _contents.Clear();
    }

    public bool Contains(IILDAFrame item)
    {
        return _contents.Contains(item);
    }

    public void CopyTo(IILDAFrame[] array, int arrayIndex)
    {
        _contents.CopyTo(array, arrayIndex);
    }

    public IEnumerator<IILDAFrame> GetEnumerator()
    {
        return _contents.GetEnumerator();   
    }

    public int IndexOf(IILDAFrame item)
    {
        return _contents.IndexOf(item);
    }

    public void Insert(int index, IILDAFrame item)
    {
        _contents.Insert(index, item);
    }

    public bool Remove(IILDAFrame item)
    {
        return _contents.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _contents.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _contents.GetEnumerator();
    }
    #endregion

    public int Left
    {
        get
        {
            if (_contents.Count == 0) return 0;
            int left = _contents[0].Left;
            foreach (var frame in _contents)
            {
                if (frame.Left < left)
                    left = frame.Left;
            }
            return left;
        }
    }

    public int Top
    {
        get
        {
            if (_contents.Count == 0) return 0; 
            int top = _contents[0].Top;
            foreach (var frame in _contents)
            {
                if (frame.Top > top)
                    top = frame.Top;
            }
            return top;
        }
    }

    public int Right
    {
        get
        {
            if (_contents.Count == 0) return 0;
            int right = _contents[0].Right;
            foreach (var frame in _contents)
            {
                if (frame.Right > right)
                    right = frame.Right;
            }
            return right;
        }
    }

    public int Bottom
    {
        get
        {
            if (_contents.Count == 0) return 0;
            int bottom = _contents[0].Bottom;
            foreach (var frame in _contents)
            {
                if (frame.Bottom < bottom)
                    bottom = frame.Bottom;
            }
            return bottom;
        }
    }
}

public class Frame : IILDAFrame
{
    private List<ILDAPoint> _frames = new();

    public string Name { get; set; } = "";

    #region List
    public ILDAPoint this[int index] { get => _frames[index]; set => _frames[index] = value; }

    public int Count => _frames.Count;

    public bool IsReadOnly => false;

    public int Left
    {
        get
        {
            if (_frames.Count == 0) return 0;
            int left = this[0].X;
            foreach (var pt in _frames)
            {
                if (pt.X < left)
                    left = pt.X;
            }
            return left;
        }
    }

    public int Top
    {
        get
        {
            if (_frames.Count == 0) return 0;
            int top = this[0].Y;
            foreach (var pt in _frames)
            {
                if (pt.Y > top)
                    top = pt.Y;
            }
            return top;
        }
    }

    public int Right
    {
        get
        {
            if (_frames.Count == 0) return 0;
            int right = this[0].X;
            foreach (var pt in _frames)
            {
                if (pt.X > right)
                    right = pt.X;
            }
            return right;
        }
    }

    public int Bottom
    {
        get
        {
            if (_frames.Count == 0) return 0;
            int bottom = this[0].Y;
            foreach (var pt in _frames)
            {
                if (pt.Y < bottom)
                    bottom = pt.Y;
            }
            return bottom;
        }
    }

    public void Add(ILDAPoint item)
    {
        _frames.Add(item);
    }

    public void Clear()
    {
        _frames.Clear();
    }

    public bool Contains(ILDAPoint item)
    {
        return _frames.Contains(item);
    }

    public void CopyTo(ILDAPoint[] array, int arrayIndex)
    {
        _frames.CopyTo(array, arrayIndex);
    }

    public IEnumerator<ILDAPoint> GetEnumerator()
    {
        return _frames.GetEnumerator();
    }

    public int IndexOf(ILDAPoint item)
    {
        return _frames.IndexOf(item);
    }

    public void Insert(int index, ILDAPoint item)
    {
        _frames.Insert(index, item);
    }

    public bool Remove(ILDAPoint item)
    {
        return _frames.Remove(item);
    }

    public void RemoveAt(int index)
    {
        _frames.RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _frames.GetEnumerator();
    }
    #endregion

    public override string ToString()
    {
        return Name;
    }
}

public class ILDAIO
{
    public static ILDA? Read(string filename)
    {
        try
        {
            if (string.IsNullOrEmpty(filename)) return null;
            if (!File.Exists(filename)) return null;
            using (var stream = File.Open(filename, FileMode.Open))
                return Read(stream);
        }
        catch
        {
            return null;
        }
    }

    public static byte[] Reverse(byte[] byteArray, int startIndex, int count)
    {
        byte[] ret = new byte[count];
        for (int i = startIndex + (count - 1); i >= startIndex; --i)
        {
            byte b = byteArray[i];
            ret[(startIndex + (count - 1)) - i] = b;
        }
        return ret;
    }

    public static byte[] SubArray(byte[] byteArray, int startIndex, int count)
    {
        byte[] ret = new byte[count];
        for (int i = 0; i < count; ++i)
            ret[0] = byteArray[i + startIndex];
        return ret;
    }

    public static ILDA Read(Stream stream)
    {
        var retval = new ILDA();
        var reader = new BinaryReader(stream);
        reader.BaseStream.Seek(0, SeekOrigin.Begin);
        long size = stream.Length;
        List<long> offsets = new();

        // Scan for All Headers
        for (long i = 0; i < size; i++)
        {
            reader.BaseStream.Seek(i, SeekOrigin.Begin);
            var buffer = reader.ReadBytes(32);
            if (buffer.Length < 4) break;
            var str = Encoding.UTF8.GetString(buffer, 0, 4);
            if (string.Compare(str, "ILDA", true) == 0)
            {
                offsets.Add(i);
            }
        }

        for (int i = 0; i < offsets.Count; i++) 
        {
            var offset = offsets[i];
            //var next = i < offsets.Count - 1 ? offsets[i + 1] : reader.BaseStream.Length;
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            var buffer = reader.ReadBytes(32);
            var idla = Encoding.UTF8.GetString(buffer, 0, 4);
            if (string.Compare(idla, "ILDA", true) != 0) continue;
            var name = Encoding.UTF8.GetString(buffer, 8, 8).Trim();
            var info = Encoding.UTF8.GetString(buffer, 16, 8).Trim();
            short totalRecords = BitConverter.ToInt16(Reverse(buffer, 24, 2));
            short frames = BitConverter.ToInt16(Reverse(buffer, 28, 2));
            short frameNumber = BitConverter.ToInt16(Reverse(buffer, 26, 2));
            int format = buffer[7];

            switch (format)
            {
                case 0:
                    {
                        var frame = new Frame { Name = name };
                        for (int fnum = 0; fnum < totalRecords; fnum++)
                        {
                            var buf = reader.ReadBytes(8);
                            var p3d = new ILDAPoint3D
                            {
                                X = BitConverter.ToInt16(Reverse(buf, 0, 2)),
                                Y = BitConverter.ToInt16(Reverse(buf, 2, 2)),
                                Z = BitConverter.ToInt16(Reverse(buf, 4, 2)),
                                Status = (int)buf[6],
                                ColorIndex = (int)buf[7]
                            };
                            frame.Add(p3d);
                        }
                        retval.Add(frame);
                    }
                    break;
                case 1:
                    {
                        var frame = new Frame { Name = name };
                        for (int fnum = 0; fnum < totalRecords; fnum++)
                        {
                            var buf = reader.ReadBytes(6);
                            var p2d = new ILDAPoint2D
                            {
                                X = BitConverter.ToInt16(Reverse(buf, 0, 2)),
                                Y = BitConverter.ToInt16(Reverse(buf, 2, 2)),
                                Status = (int)buf[4],
                                ColorIndex = (int)buf[5]
                            };
                            frame.Add(p2d);
                        }
                        retval.Add(frame);
                    }
                    break;
                case 2:
                    System.Diagnostics.Debug.WriteLine($"Unimplimented Color Palette");
                    break;
                case 4:
                    {
                        var frame = new Frame { Name = name };
                        for (int fnum = 0; fnum < totalRecords; fnum++)
                        {
                            var buf = reader.ReadBytes(10);
                            var p3d = new ILDAPoint3D
                            {
                                X = BitConverter.ToInt16(Reverse(buf, 0, 2)),
                                Y = BitConverter.ToInt16(Reverse(buf, 2, 2)),
                                Z = BitConverter.ToInt16(Reverse(buf, 4, 2)),
                                Status = (int)buf[6],
                                RGBColor = Color.FromArgb(255, buf[7], buf[8], buf[9])
                            };
                            frame.Add(p3d);
                        }
                        retval.Add(frame);
                    }
                    break;
                case 5:
                    {
                        var frame = new Frame { Name = name };
                        for (int fnum = 0; fnum < totalRecords; fnum++)
                        {
                            var buf = reader.ReadBytes(8);
                            var p2d = new ILDAPoint2D
                            {
                                X = BitConverter.ToInt16(Reverse(buf, 0, 2)),
                                Y = BitConverter.ToInt16(Reverse(buf, 2, 2)),
                                Status = (int)buf[4],
                                RGBColor = Color.FromArgb(255, buf[5], buf[6], buf[7])
                            };
                            frame.Add(p2d);
                        }
                        retval.Add(frame);
                    }
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine($"Unknown Format:{format}");
                    break;
            }
        }

        return retval;
    }

    public static void Write(IILDA ilda, string filename)
    {

    }
}
