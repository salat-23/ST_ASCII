using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace ST_ASCII.Utility;

    //This is from Unity idk
    /// <summary>
    /// Used for coordinates and size.
    /// Ported from Unity.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
    {
        public int x
        {
            
            get { return m_X; }

            
            set { m_X = value; }
        }


        public int y
        {
            
            get { return m_Y; }

            
            set { m_Y = value; } }

        private int m_X;
        private int m_Y;

        
        public Vector2Int(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }

        // Set x and y components of an existing Vector.
        
        public void Set(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }

        // Access the /x/ or /y/ component using [0] or [1] respectively.
        public int this[int index]
        {
            
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default:
                        throw new IndexOutOfRangeException(String.Format("Invalid Vector2Int index addressed: {0}!", index));
                }
            }

            
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default:
                        throw new IndexOutOfRangeException(String.Format("Invalid Vector2Int index addressed: {0}!", index));
                }
            }
        }

        // Returns the length of this vector (RO).
        public float magnitude {  get { return MathF.Sqrt((x * x + y * y)); } }

        // Returns the squared length of this vector (RO).
        public int sqrMagnitude {  get { return x * x + y * y; } }

        // Returns the distance between /a/ and /b/.
        
        public static float Distance(Vector2Int a, Vector2Int b)
        {
            float diff_x = a.x - b.x;
            float diff_y = a.y - b.y;

            return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y);
        }

        // Returns a vector that is made from the smallest components of two vectors.
        
        public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs) { return new Vector2Int(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y)); }

        // Returns a vector that is made from the largest components of two vectors.
        
        public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs) { return new Vector2Int(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y)); }

        // Multiplies two vectors component-wise.
        
        public static Vector2Int Scale(Vector2Int a, Vector2Int b) { return new Vector2Int(a.x * b.x, a.y * b.y); }

        // Multiplies every component of this vector by the same component of /scale/.
        
        public void Scale(Vector2Int scale) { x *= scale.x; y *= scale.y; }

        
        public void Clamp(Vector2Int min, Vector2Int max)
        {
            x = Math.Max(min.x, x);
            x = Math.Min(max.x, x);
            y = Math.Max(min.y, y);
            y = Math.Min(max.y, y);
        }

        // Converts a Vector2Int to a [[Vector2]].
        
        public static implicit operator Vector2(Vector2Int v)
        {
            return new Vector2(v.x, v.y);
        }

        public static Vector2Int operator-(Vector2Int v)
        {
            return new Vector2Int(-v.x, -v.y);
        }

        
        public static Vector2Int operator+(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x + b.x, a.y + b.y);
        }

        
        public static Vector2Int operator-(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x - b.x, a.y - b.y);
        }

        
        public static Vector2Int operator*(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x * b.x, a.y * b.y);
        }

        
        public static Vector2Int operator*(int a, Vector2Int b)
        {
            return new Vector2Int(a * b.x, a * b.y);
        }

        
        public static Vector2Int operator*(Vector2Int a, int b)
        {
            return new Vector2Int(a.x * b, a.y * b);
        }

        
        public static Vector2Int operator/(Vector2Int a, int b)
        {
            return new Vector2Int(a.x / b, a.y / b);
        }

        
        public static bool operator==(Vector2Int lhs, Vector2Int rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        
        public static bool operator!=(Vector2Int lhs, Vector2Int rhs)
        {
            return !(lhs == rhs);
        }

        
        public override bool Equals(object other)
        {
            if (!(other is Vector2Int)) return false;

            return Equals((Vector2Int)other);
        }

        
        public bool Equals(Vector2Int other)
        {
            return x == other.x && y == other.y;
        }

        
        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2);
        }

        /// *listonly*
        
        public override string ToString()
        {
            return ToString(null, null);
        }

        
        public string ToString(string format)
        {
            return ToString(format, null);
        }

        
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (formatProvider == null)
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            return string.Format("({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
        }

        public static Vector2Int zero {  get { return s_Zero; } }
        public static Vector2Int one {  get { return s_One; } }
        public static Vector2Int up {  get { return s_Up; } }
        public static Vector2Int down {  get { return s_Down; } }
        public static Vector2Int left {  get { return s_Left; } }
        public static Vector2Int right {  get { return s_Right; } }

        private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);
        private static readonly Vector2Int s_One = new Vector2Int(1, 1);
        private static readonly Vector2Int s_Up = new Vector2Int(0, 1);
        private static readonly Vector2Int s_Down = new Vector2Int(0, -1);
        private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);
        private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
    }
    