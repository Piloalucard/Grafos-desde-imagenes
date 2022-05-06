
using System;

namespace Actividad04
{
	public class Arista
	{
		Arista sig;
		Vertice ady;
		Vertice src_aux;
		
		double de;
		public Arista()
		{	
			sig = null;
	    	ady = null;
		}

		
		public void setSig(Arista a)
		{
			sig = a;
		}
		
		public void setAdy(Vertice a)
		{
			ady = a;
		}
		
		public void setDe(double d)
		{
			de=d;
		}
		
		public void setSrc(Vertice a)
		{
			src_aux=a;
		}
		
		public Arista getSig()
		{
			return sig;
		}
		
		public Vertice getAdy()
		{
			return ady;
		}
		
		public Vertice getSrc()
		{
			return src_aux;
		}
	   	
		public double getDe()
		{
			return de;
		}
		

	
	}
}
