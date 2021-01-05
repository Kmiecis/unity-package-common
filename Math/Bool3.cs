﻿using System;

namespace Common.Mathematics
{
	[Serializable]
	public struct Bool3
	{
		public bool x;
		public bool y;
		public bool z;

		public Bool3(bool x, bool y, bool z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Bool3(bool b) :
			this(b, b, b)
		{
		}

		public Bool3(Bool3 b) :
			this(b.x, b.y, b.z)
		{
		}

		public bool this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return x;
					case 1: return y;
					case 2: return z;
					default:
						throw new IndexOutOfRangeException($"Invalid Bool3 index {index}");
				}
			}
			set
			{
				switch (index)
				{
					case 0: x = value; break;
					case 1: y = value; break;
					case 2: z = value; break;
					default:
						throw new IndexOutOfRangeException($"Invalid Bool3 index {index}");
				}
			}
		}

		public bool Equals(Bool3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		public override bool Equals(object obj)
		{
			return obj is Bool3 && Equals((Bool3)obj);
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() | (y.GetHashCode() << 1) | (z.GetHashCode() << 2);
		}

		public override string ToString()
		{
			return string.Format("[{0}, {1}, {2}]", x, y, z);
		}
	}
}