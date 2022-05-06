
using System;

namespace Actividad04
{
	
	
	
	
	public class Vertice
	{
		
		
		
		
		Consol
		
		Vertice sig;
		Arista ady;
	    int id;
	    public Vertice()
	    {
	    	id=-1;
	    	sig = null;
	    	ady = null;
	    }
	    
	    public void setSig(Vertice a)
		{
			sig = a;
		}
	    
	    public void setAdy(Arista a)
	    {
	    	ady = a;
	    }
	    
	    public void setID(int i)
	    {
	    	id=i;
	    }
		
		public Vertice getSig()
		{
			return sig;
		}
		
		public Arista getAdy()
		{
			return ady;
		}
		
		public int getID()
		{
			return id;
		}
		

	
	}
	
	public class VerticeD
    {
        public Vertice vertice = null;
        public VerticeD verticeP = null;
        public float peso = Single.PositiveInfinity;
        public bool def = false;
    }
}
