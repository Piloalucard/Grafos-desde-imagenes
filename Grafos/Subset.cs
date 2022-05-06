
using System;

namespace Actividad04
{
	/// <summary>
	/// Description of Subset.
	/// </summary>
	public class Subset
	{
		int rank=0;
		int parent=0;
		public Subset()
		{
			rank=0;
			parent=0;
		}
		
		public void setRank(int a)
		{
			this.rank=a;
		}
		
		public void setParent(int a)
		{
			this.parent=a;
		}
		
		public int getRank()
		{
			return rank;
		}
		
		public int getParent()
		{
			return parent;
		}
	}
}
