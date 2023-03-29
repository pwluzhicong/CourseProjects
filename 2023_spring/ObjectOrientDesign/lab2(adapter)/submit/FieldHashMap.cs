using System;
namespace lab2
{
	public static class FieldHashMap
	{
		static Dictionary<long, string> map_ = new Dictionary<long, string>();

		public static long getKey(string s)
		{
			long res = s.GetHashCode();
            map_[res] = s;
			return res;
		}

		public static string getValue(long key)
		{
			return map_[key];
		}
	}
}

