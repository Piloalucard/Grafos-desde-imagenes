/*
 * Created by SharpDevelop.
 * User: Gustavo
 * Date: 15/05/2021
 * Time: 09:34 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Actividad04
{

	public class Grafo
	{
		Vertice fst;
		List<Arista> totalAristas = new List<Arista>();

		public Grafo()
		{
			fst=null;
			this.totalAristas = new List<Arista>();
		}
		
		public Vertice getFst()
		{
			return fst;
		}
		
		bool estaVacia()
		{
			return fst==null;
		}
		
		public double getPeso()
		{
			double total=0;
			Vertice v=fst;
			Arista a;
			while(v != null)
			{
				a=v.getAdy();
				while(a!=null)
				{
					total+=a.getDe();
					a=a.getSig();
				}
				v=v.getSig();
			}
			total=total/2;
			return total;
		}
		
		public Vertice circuloToVert(int id)
		{
			Vertice v=fst;
			while(v != null)
			{
				if(v.getID() == id)
					return v;
				v=v.getSig();
			}
			return null;
		}
		
		public string getStringAristas(int modo)
		{
			List <Arista> deshechable = new List<Arista>();
			string total="/";
			Vertice v=fst;
			Arista a;
			while(v != null)
			{
				a=v.getAdy();
				while(a!=null)
				{
					deshechable.Add(a);
					a=a.getSig();
				}
				v=v.getSig();
			}
			if(modo==1)
			{
				deshechable.Sort(
				delegate(Arista x, Arista y)
				{
					return x.getDe().CompareTo(y.getDe());
				}
				);
			
			}
			
			foreach(Arista x in deshechable)
			{
				total+=x.getSrc().getID()+"--"+x.getAdy().getID()+"/";
			}
			
			
			return total;
		}
		
		public string getStringVertices()
		{
			string total="/";
			Vertice v=fst;
			while(v != null)
			{
				total+=v.getID().ToString()+"/";
				v=v.getSig();
			}
			return total;
		}
		
		public int getTotalAristas()
		{
			int cont=0;
			foreach(Arista a in totalAristas)
			{
				cont=cont+1;
			}
			return cont;
		}
		
		public int getTotalVertices()
		{
			int cont = 0;
			Vertice aux=fst;
			while(aux != null)
			{
				aux = aux.getSig();
				cont+=1;
			}
			return cont;
		}
		
		bool repetido(int id)
		{
			Vertice aux=fst;
			while(aux != null)
			{
				if(aux.getID() == id)
				{
					return true;
				}
				aux = aux.getSig();
			}
			return false;
		}
		
		public Vertice GetVertice(int ident)
		{
		    Vertice aux;
		    aux=fst;
		    while(aux != null)
		    {
		    	if(aux.getID() == ident)
		        {
		            return aux;
		        }
		    	aux=aux.getSig();
		    }
		    return null;
		}
		
		
		
		public string insertaVertice(int id)
		{
		    string msg;
		    if(repetido(id))
		    {
		    	return "Vertice ya se encuentra en el grafo\n";
		    }
		    Vertice newVertice = new Vertice();
		    newVertice.setID(id);
		    
		    if(estaVacia())
		    {
		        fst = newVertice;
		    }
		    else
		    {
		        Vertice aux;
		        aux=fst;
		        while(aux.getSig() != null)
		        {
		        	aux=aux.getSig();
		        }
		        aux.setSig(newVertice);
		    }
		    msg = "Vertice insertado\n";
		    return msg;
		
		}
		
		public string insertarArista(int nOrigen,int nDestino, int modo,double de) 
		{
		    string msg="";
		    Vertice origen,destino;
		    origen=GetVertice(nOrigen);
		    destino=GetVertice(nDestino);
		    if(origen == null)
		    {
		        msg = "Vertice Origen no encontrado\n";
		        return msg;
		    }
		    if(destino == null)
		    {
		        msg = "Vertice Destino no encontrado\n";
		        return msg;
		    }
		    
		    Arista newArista = new Arista();
		    Arista aux;
		  
		    aux=origen.getAdy();
		    if(aux==null)
		    {
		        origen.setAdy(newArista);
		        newArista.setAdy(destino);
		    }
		    else
		    {
		    	while(aux.getSig() != null)
		        {
		    		aux = aux.getSig();
		        }
		    	aux.setSig(newArista);
		    	newArista.setAdy(destino);
		    }
		    newArista.setDe(de);
		    newArista.setSrc(origen);
		    totalAristas.Add(newArista);
		    
		    if(modo==0)
		    {
		    	insertarArista(nDestino,nOrigen,1,de);
		    	msg += "Las 2 ";
		    }
		    
		    msg += "Arista(s) han sido insertadas\n";
		    return msg;
		
		}
		
		
		public void eliminarTodo()
		{
		    Vertice aux;
		    while (fst != null)
		    {
		        aux=fst;
		        Arista auxa;
		        while(aux.getAdy() != null)
		        {
		        	auxa=aux.getAdy();
		        	aux.setAdy(aux.getAdy().getSig());
		        	auxa = null;
		        }
		        fst=fst.getSig();
		        aux=null;
		    }
		    totalAristas = new List<Arista>();
		}
		
		public string eliminarArista(int nOrigen,int nDestino,int modo) 
		{
		    string msg="";
		    Vertice origen,destino;
		    origen=GetVertice(nOrigen);
		    destino=GetVertice(nDestino);
		    if(origen == null || destino==null)
		    {
		        msg = "Vertice Origen y/o Destino no encontrado\n";
		        return msg;
		    }
		    Arista act,ant=null;
		    act = origen.getAdy();
		    if(act==null)
		    {
		        msg = "El Vertice origen no tiene aristas\n";
		    }
		    else if(act.getAdy() == destino)
		    {
		    	origen.setAdy(act.getSig());
		        act=null;
		        msg = "Arista eliminada!\n";
		    }
		    else
		    {
		    	while(  act != null &&  act.getAdy() != destino)
		        {
		            ant = act;
		            act = act.getSig();
		        }
		        if(act==null)
		        {
		            msg = "No se encontro ningun Vertice origen con la Arista hacia el destino\n";
		        }
		        else
		        {
		        	ant.setSig(act.getSig());
		        	totalAristas.Remove(act);
		            act=null;
		            msg = "Arista eliminada!\n";
		        }
		    }
		    
		    if(modo==0)
		    {
		    	eliminarArista(nDestino,nOrigen,1);
		    }
		    
		    return msg;
		}
		
		public string eliminarVertice(int id)
		{
		    string msg="";
		    Vertice nod;
		    nod=GetVertice(id);
		    if(nod==null)
		    {
		        msg = "No se encontro el Pais";
		    }
		
		    Vertice act,ant=null;
		    Arista aux;
		
		    act=fst;
		    while(act != null)
		    {
		    	aux=act.getAdy();
		        while(aux != null) //Borra las conexiones de las aristas al vertice a eliminar
		        {
		        	if(aux.getAdy() == nod)
		            {
		        		eliminarArista(act.getID(),aux.getAdy().getID(),0);
		                break;
		            }
		        	aux=aux.getSig();
		        }
		        act = act.getSig();
		    }
		
		    act=fst;
		    if(act == nod) //Elimina al vertice
		    {
		    	fst=fst.getSig();
		        act=null;
		        msg = "Vertice eliminado!";
		    }
		    else
		    {
		        while(act != nod)
		        {
		            ant = act;
		            act = act.getSig();
		        }
		        ant.setSig(act.getSig());
		        act=null;
		        msg = "Vertice eliminado!";
		    }
		
		    return msg;
		}
		
		private static int Find(Subset[] subsets, int i)
		{
			if (subsets[i].getParent() != i)
				subsets[i].setParent(Find(subsets, subsets[i].getParent()));
		
			return subsets[i].getParent();
		}
		
		private static void Union(Subset[] subsets, int x, int y)
		{
			int xroot = Find(subsets, x);
			int yroot = Find(subsets, y);
		
			if (subsets[xroot].getRank() < subsets[yroot].getRank())
				subsets[xroot].setParent(yroot);
			else if (subsets[xroot].getRank() > subsets[yroot].getRank())
				subsets[yroot].setParent(xroot);
			else
			{
				subsets[yroot].setParent(xroot);
				int aux = subsets[xroot].getRank() + 1;
				subsets[xroot].setRank(aux);
			}
		}
			
		public Grafo Kruskal()
		{
			int verticesCount=getTotalVertices();
			totalAristas.Sort(
				delegate(Arista a, Arista b)
				{
					return a.getDe().CompareTo(b.getDe());
				}
			);
			Arista [] candidatos = totalAristas.ToArray();
			Arista [] prometedor = new Arista[getTotalAristas()];
			Subset [] subsets = new Subset[verticesCount];
			
			for (int v = 0; v < verticesCount; ++v)
			{
				subsets[v] = new Subset();
				subsets[v].setParent(v);
				subsets[v].setRank(0);
			}
			
			int i=0,e=0;
			while (e < verticesCount - 1 && i < getTotalAristas())
			{
				Arista nextA = candidatos[i++];
				int x = Find(subsets, nextA.getSrc().getID()-1);
				int y = Find(subsets, nextA.getAdy().getID()-1);
		
				if (x != y)
				{
					prometedor[e++] = nextA;
					Union(subsets, x, y);
				}
			}
			
			Grafo ARM = new Grafo();
			Vertice aux=fst;
			while(aux != null)
			{
				ARM.insertaVertice(aux.getID());
				aux = aux.getSig();
			}
			
			for(i=0;i<e;i++)
			{
				Arista a=prometedor[i];
				ARM.insertarArista(a.getSrc().getID(),a.getAdy().getID(),0,a.getDe());
			}
				
			
			
			return ARM;


		}
		
		public Grafo Prim(int inicial)
		{
			List<Arista> candidatos = new List<Arista>();
			Vertice auxV= GetVertice(inicial);
			Arista auxA=auxV.getAdy();
			while(auxA!=null)
			{
				candidatos.Add(auxA);
				auxA=auxA.getSig();
			}
			candidatos.Sort(
				delegate(Arista a, Arista b)
				{
					return a.getDe().CompareTo(b.getDe());
				}
			);

			List <Arista> prometedor = new List<Arista>();
			List<Vertice> visitados = new List<Vertice>();
			visitados.Add(auxV);
			int j=0;
			
			try{
			    	while(candidatos.ElementAt(j) != null)
			    	{
			    		auxA=candidatos.ElementAt(j);
			    		if(!visitados.Contains(auxA.getAdy()))
			    		{
			    			prometedor.Add(auxA);
			    			visitados.Add(auxA.getAdy());
			    			auxV=auxA.getAdy();
			    			Arista auxA2=auxV.getAdy();
			    			while(auxA2 != null)
			    			{
			    				candidatos.Add(auxA2);
			    				auxA2=auxA2.getSig();
			    			}
			    			candidatos.Sort(
								delegate(Arista a, Arista b)
								{
									return a.getDe().CompareTo(b.getDe());
								}
							);
			    			j=-1;
			    		}
			    		j++;
			    		
			    	}
		    	}catch{}
			
			Grafo ARM = new Grafo();
			foreach(Vertice v in visitados)
			{
				ARM.insertaVertice(v.getID());
			}
			foreach(Arista a in prometedor)
			{
				ARM.insertarArista(a.getSrc().getID(),a.getAdy().getID(),1,a.getDe());
			}
			
			return ARM;
			
			
		}
		
		
		
		private bool solucion(List<VerticeD> can)
        {
            foreach (var c in can)
            {
                if (!c.def)
                    return false;
            }

            return true;
        }

        public List<VerticeD> Dijkstra(int id)
        {
            List<VerticeD> candidatos = new List<VerticeD>();
            Vertice vI = new Vertice();
            Vertice aux=fst;
			while(aux != null)
			{
				if(aux.getID() == id)
					vI=aux;
				VerticeD v = new VerticeD();
                v.vertice = aux;
                v.verticeP = v;
                candidatos.Add(v);
				aux=aux.getSig();
			}

            int tmpI = candidatos.FindIndex(x => x.vertice == vI);
            int dbg = tmpI;
            VerticeD tmp = candidatos[tmpI];
            tmp.peso = 0;
            tmp.def = true;
            candidatos[tmpI] = tmp;
            VerticeD actual = candidatos[tmpI];
            while (!solucion(candidatos))
            {
            	Arista auxA=actual.vertice.getAdy();
            	while(auxA != null)
                {
            		tmpI =  candidatos.FindIndex(x => x.vertice == auxA.getAdy());
                    tmp = candidatos[tmpI];
                    if (!tmp.def)
                    {
                    	float peso = actual.peso + (float)auxA.getDe();
                        if (tmp.peso > peso)
                        {
                            tmp.peso = peso;
                            tmp.verticeP = actual;
                            candidatos[tmpI] = tmp;
                        }
                    }
                    auxA=auxA.getSig();
                }

                VerticeD min = new VerticeD();
                min.peso = Single.PositiveInfinity;
                bool f = false;
                foreach (var can in candidatos)
                {
                    if (!can.def)
                    {
                        if (can.peso < min.peso)
                        {
                            min = can;
                        }
                        f = true;
                    }
                }
                try{

	                if (f)
	                {
	                    tmpI =  candidatos.FindIndex(x => x.vertice == min.vertice);
	                    tmp = candidatos[tmpI];
	                    tmp.def = true;
	                    actual = tmp;
	                }
                }catch{
                	return candidatos;
                }
            }
            return candidatos;
        }
		
		public int[,] GrafoToMatriz()
		{
			int verticesCount = getTotalVertices();
			int[,] graph = new int[verticesCount, verticesCount];
			for (int i = 0; i < verticesCount; ++i)
			{
				for (int j = 0; j < verticesCount; ++j)
				{
					graph[i,j]=0;
				}
			}
			foreach(Arista a in totalAristas)
			{
				graph[a.getSrc().getID()-1,a.getAdy().getID()-1]=Convert.ToInt32(a.getDe());
			}
				
			return graph;
		}
		
		
		public int[,] FloydWarshall()
		{
			int[,] graph = GrafoToMatriz();
			int verticesCount = getTotalVertices();
			int[,] distance = new int[verticesCount, verticesCount];
		
			for (int i = 0; i < verticesCount; ++i)
				for (int j = 0; j < verticesCount; ++j)
					distance[i, j] = graph[i, j];
		
			for (int k = 0; k < verticesCount; ++k)
			{
				for (int i = 0; i < verticesCount; ++i)
				{
					for (int j = 0; j < verticesCount; ++j)
					{
						if (distance[i, k] + distance[k, j] < distance[i, j])
							distance[i, j] = distance[i, k] + distance[k, j];
					}
				}
			}
			
			return distance;
		

		}

		
	}
	
	

}
