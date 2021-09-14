using System.Drawing;
using System.Collections;

namespace ILDAIO
{
    /// <summary>
    /// public static class ILDA_IO
    /// </summary>
    public class NormalizedPoint2d
    {
        /// <summary>
        /// 
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Color RGBColor { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NormalizedFrame : IList<NormalizedPoint2d>
    {
        #region Fields
        private List<NormalizedPoint2d> _contents = new List<NormalizedPoint2d>();
        #endregion

        #region List Impl
        public NormalizedPoint2d this[int index] { get => _contents[index]; set { _contents[index] = value; } }

        public int Count => _contents.Count;

        public bool IsReadOnly => false;

        public void Add(NormalizedPoint2d item)
        {
            _contents.Add(item);
        }

        public void Clear()
        {
            _contents.Clear();
        }

        public bool Contains(NormalizedPoint2d item)
        {
            return _contents.Contains(item);
        }

        public void CopyTo(NormalizedPoint2d[] array, int arrayIndex)
        {
            _contents.CopyTo(array, arrayIndex);
        }

        public IEnumerator<NormalizedPoint2d> GetEnumerator()
        {
            return _contents.GetEnumerator();
        }

        public int IndexOf(NormalizedPoint2d item)
        {
            return _contents.IndexOf(item);
        }

        public void Insert(int index, NormalizedPoint2d item)
        {
            _contents.Insert(index, item);
        }

        public bool Remove(NormalizedPoint2d item)
        {
            return _contents.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _contents.RemoveAt(index);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _contents.GetEnumerator();
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class NormalizedILDA : IList<NormalizedFrame>
    {
        #region Fields
        private List<NormalizedFrame> _frames = new List<NormalizedFrame>();
        #endregion

        #region Fields
        public NormalizedFrame this[int index] { get => _frames[index]; set { _frames[index] = value; } }

        public int Count => _frames.Count;

        public bool IsReadOnly => false;

        public void Add(NormalizedFrame item)
        {
            _frames.Add(item);
        }

        public void Clear()
        {
            _frames.Count();
        }

        public bool Contains(NormalizedFrame item)
        {
            return _frames.Contains(item);
        }

        public void CopyTo(NormalizedFrame[] array, int arrayIndex)
        {
            _frames.CopyTo(array, arrayIndex);
        }

        public IEnumerator<NormalizedFrame> GetEnumerator()
        {
            return _frames.GetEnumerator();
        }

        public int IndexOf(NormalizedFrame item)
        {
            return _frames.IndexOf(item);
        }

        public void Insert(int index, NormalizedFrame item)
        {
            _frames.Insert(index, item);
        }

        public bool Remove(NormalizedFrame item)
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

        #region Create
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ilda"></param>
        /// <returns></returns>
        public static NormalizedILDA Create(ILDA ilda)
        {
            int left = short.MinValue;
            int right = short.MaxValue;
            int top = short.MaxValue;
            int bottom = short.MaxValue;
            double width = left - right + 1;
            double height = top - bottom + 1;
            double center = width / 2;

            var n = new NormalizedILDA();

            foreach (var frame in ilda)
            {
                var f = new NormalizedFrame();
                for (int i = 0; i < frame.Count - 1; i++)
                {
                    if (frame[i + 1].IsBlank) continue;
                    f.Add(new NormalizedPoint2d {
                                RGBColor= frame[i].RGBColor, 
                                X = (int)((frame[i].X - left + center) / width), 
                                Y = (int)((top - frame[i].Y - bottom + center) / height)
                    });
                    f.Add(new NormalizedPoint2d
                    {
                        RGBColor = frame[i].RGBColor,
                        X = (int)((frame[i+1].X - left + center) / width),
                        Y = (int)((top - frame[i+1].Y - bottom + center) / height)
                    });
                }
                n.Add(f);
            }

            return n;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void Write(string filename)
        {

        }
    }
}
