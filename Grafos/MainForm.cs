/*
 * Created by SharpDevelop.
 * User: Gustavo
 * Date: 16/03/2021
 * Time: 03:31 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Threading;
using System.Diagnostics;

namespace Actividad04
{

	

	public partial class MainForm : Form
	{
		OpenFileDialog abrir = new OpenFileDialog();
		int sombra=0,tamano=1,clic=0;
		Circulo origenDij;
		List<Circulo> circulos = new List<Circulo>();
		Grafo grf = new Grafo();
		Bitmap buffer;
		Bitmap orig;
		Bitmap anim;
		public MainForm()
		{
			InitializeComponent();
			analisis.Hide();
			treeView_graph.Hide();
			label_grafo.Hide();
		}
		
	
		
		
		void CargarClick(object sender, EventArgs e)
		{
			if(abrir.ShowDialog() == DialogResult.OK)
			{
		
				label_NOIMG.Hide();
				imagen.Image = Image.FromFile(abrir.FileName);
				if(imagen.Image.Height > imagen.Image.Width)
					tamano = imagen.Image.Height / 475;
				else
					tamano = imagen.Image.Width / 433;
				imagen.Width = imagen.Image.Width/tamano;
				imagen.Height = imagen.Image.Height/tamano;
				restart();
			}
			
		}
		
		void restart()
		{
			analisis.Show();
			treeView_graph.Hide();
			circulos=null;
			circulos = new List<Circulo>();

			grf.eliminarTodo();
			label_grafo.Hide();
			INFO.Text="";

			
		}
		
		void AnalisisClick(object sender, EventArgs e)
		{
			if(INFO.Text == "")
			{
				grf.eliminarTodo();
				Bitmap bmp = new Bitmap(abrir.FileName);
				imagen.Image = bmp;
				encontrarCirculos(bmp);
				INFO.Text=parPuntos(bmp);
				drawLines(bmp);
				mostrarCentros(bmp);
				label_grafo.Show();
				treeView_graph.Show();
				ListaAdyacencia();
				
				buffer=(Bitmap)imagen.Image;
				orig = new Bitmap(abrir.FileName);
				foreach(Circulo c in circulos)
				{
					colorearCirc(c,orig,Color.Yellow);
				}
				mostrarCentros(orig);
				imagen.BackgroundImage = imagen.Image;
				imagen.BackgroundImageLayout = ImageLayout.Zoom;
	
			}
		}
		

		void ImagenMouseClick(object sender, MouseEventArgs e)
		{
			if(INFO.Text=="")
				return;
			int x=e.X*tamano;
			int y=e.Y*tamano;
				
			foreach(Circulo c in circulos)
			{
				if(c.esParte(x,y))
				{
					if(clic == 1)
					{
						crearParticula(c,9);
						List<VerticeD> caminos = new List<VerticeD>();
						caminos=Camino(origenDij.getID(),c.getID());
					
						if(caminos!=null)
						{
							
							foreach (var v in caminos)
				            {
								float x1=0,y1=0,x2=0,y2=0;
								int ido,idd;
								ido=v.verticeP.vertice.getID();
								idd=v.vertice.getID();
								foreach(Circulo cx in circulos)
								{
									if(cx.getID() == ido)
									{
										x1=cx.getX();
										y1=cx.getY();
									}
									if(cx.getID() == idd)
									{
										x2=cx.getX();
										y2=cx.getY();
									}
								}
				            	imagen.BackgroundImage = orig;
				            	animacion(x1,y1,x2,y2);	

				            }
							
							buffer=new Bitmap(orig);
							imagen.Image = orig;							

						}	
						else
						{
							buffer=new Bitmap(orig);
							imagen.BackgroundImage = orig;
							imagen.Image = orig;
							
						}
						clic = 0;
					}
					else
					{
						clic=1;
						origenDij=c;
						crearParticula(c,9);
						imagen.Image=buffer;
						
					}
				}
			}
			

		}
		
		void animacion(float x_0,float y_0,float x_f,float y_f)
		{
			anim = new Bitmap(orig.Width,orig.Height);
			imagen.Image = anim;
			if(x_0 == x_f)
				x_f+=1;
			float r=8;
			Graphics g = Graphics.FromImage(anim);
			Brush br = new SolidBrush(Color.Violet);
			Brush wh = new SolidBrush(Color.White);
			float x_k=x_0,y_k=y_0,m,b,x_ant=x_0,y_ant=y_0;
			int inc=1;
			
			m = (y_f - y_0) / (x_f - x_0);
			b = y_f - x_f * m;
			if(m>-1 && m<1)
			{
				if(x_f < x_0)
					inc = -1;
				for(x_k = x_0+1; x_k != x_f; x_k+= inc)
				{
					y_k = m*x_k+b;
					g.FillEllipse(wh,(int)(Math.Round(x_k-inc-r)),(int)(Math.Round(y_ant-r)),2*r,2*r);
					g.FillEllipse(br,(int)(Math.Round(x_k-r)),(int)(Math.Round(y_k-r)),2*r,2*r);
					imagen.Refresh();
					y_ant=y_k;
					g.Clear(Color.Transparent);
					
				}
			}
			else
			{
				if(y_f < y_0)
					inc = -1;
				for(y_k = y_0; y_k != y_f; y_k +=inc)
				{
					x_k = (y_k - b) / m;
					g.FillEllipse(wh,(int)(Math.Round(x_ant-r)),(int)(Math.Round(y_k-r-inc)),2*r,2*r);
					g.FillEllipse(br,(int)(Math.Round(x_k-r)),(int)(Math.Round(y_k-r)),2*r,2*r);
					imagen.Refresh();
					x_ant=x_k;
					g.Clear(Color.Transparent);
				}
			}
		}
		
		List<VerticeD> Camino(int origen,int destino)
		{
			if(origen==destino)
			{
				if (MessageBox.Show("Es el mismo vertice!", "Dijkstra", MessageBoxButtons.OK, 
				                    MessageBoxIcon.Exclamation) == DialogResult.OK)
            	{
	                return null;

            	}
				return null;
			}
			List<VerticeD> distancias= new List<VerticeD>();
			distancias=grf.Dijkstra(origen);
			
			
			Vertice cI = grf.circuloToVert(origen);
			VerticeD cF = distancias.Find(x=>x.vertice.getID() == destino);
            List<VerticeD> camino = new List<VerticeD>();
            
            try{
	            while(cF.vertice != cI)
	            {
	            	camino.Add(cF);
	            	cF = cF.verticeP;
	            }
	            camino.Reverse();
            }catch{
            	if (MessageBox.Show("No existe un camino!", "Dijkstra", MessageBoxButtons.OK, 
				                    MessageBoxIcon.Exclamation) == DialogResult.OK)
            	{
	                return null;

            	}
				return null;
            		
            }
            
            
         
			return camino;
				
		}
		
		void crearParticula(Circulo c,int tam)
		{
			int x=c.getX()-((tam-1)/2),y=c.getY()-((tam-1)/2);
			for(int i=0;i<tam;i++)
			{
				for(int j=0;j<tam;j++)
				{
					buffer.SetPixel(x+i,y+j,Color.Violet);
				}
			}
			
		}

		

		
		void drawGrafo(Grafo g,Bitmap bmp)
		{
			Vertice VerticeAux;
		    Arista AristaAux;
		    
		    VerticeAux = g.getFst();//Inicio Recorrido de Vertices
		    while(VerticeAux != null)
		    {
		    	foreach(Circulo c in circulos)
		    	{
		    		if(c.getID() == VerticeAux.getID())
		    		{
		    			colorearCirc(c,bmp,Color.Yellow);
		    			break;
		    		}
		    	}
		        VerticeAux = VerticeAux.getSig();
		    }
		    
		    Graphics gs = Graphics.FromImage(bmp);
			Pen p = new Pen(Color.Red,1);
		    VerticeAux = g.getFst();//Inicio Recorrido de Vertices
		    while(VerticeAux != null)
		    {
		    	
		    	AristaAux = VerticeAux.getAdy();//Inicio Recorrido de Arista de cada Vertice
		        while(AristaAux != null)
		        {
		        	int x1=0,y1=0,x2=0,y2=0;
		        	foreach(Circulo c in circulos)
			    	{
		        		if(c.getID() == AristaAux.getSrc().getID())
			    		{
		        			x1=c.getX();
		        			y1=c.getY();
			    			break;
			    		}
			    	}
		        	foreach(Circulo c in circulos)
			    	{
		        		if(c.getID() == AristaAux.getAdy().getID())
			    		{
		        			x2=c.getX();
		        			y2=c.getY();
			    			break;
			    		}
			    	}
		        	gs.DrawLine(p,x1,y1,x2,y2);
		        	AristaAux = AristaAux.getSig();
		        }
		        VerticeAux = VerticeAux.getSig();
		    }
		   
		}
		
		void ListaAdyacencia()
		{
			treeView_graph.BeginUpdate();
			treeView_graph.Nodes.Clear();
		    Vertice VerticeAux;
		    Arista AristaAux;
			int i=0;
		    VerticeAux = grf.getFst();//Inicio Recorrido de Vertices
		    while(VerticeAux != null)
		    {
		    	string msg;
		    	msg=" Circulo: "+ VerticeAux.getID().ToString();
		    	treeView_graph.Nodes.Add(msg);
		    	AristaAux = VerticeAux.getAdy();//Inicio Recorrido de Arista de cada Vertice
		        while(AristaAux != null)
		        {
		        	msg="  ";
		        	msg+= VerticeAux.getID().ToString() + " -> " + AristaAux.getAdy().getID().ToString() 
		        		+ ": " + AristaAux.getDe().ToString() + "px";
		        	treeView_graph.Nodes[i].Nodes.Add(msg);
		        	AristaAux = AristaAux.getSig();
		        }
		        VerticeAux = VerticeAux.getSig();

		        i++;
		    }
		    treeView_graph.EndUpdate();
		    
		}
		

		
		void encontrarCirculos(Bitmap bmp)
		{
			int x,y,cont=1;
			Color colorAct;
			//Encontrar valores
			for(y=0;y<bmp.Height;y++)
			{
				for(x=0;x<bmp.Width;x++)
				{
					colorAct = bmp.GetPixel(x,y);
					if(colorAct.R <= sombra)
					{
						if(colorAct.G <= sombra)
						{
							if(colorAct.B <= sombra)
							{
								Circulo aux = new Circulo();
								aux=buscarCentro(x,y,bmp);
								aux.setID(cont);
								circulos.Add(aux);
								cont++;
								
										

							}
						}
					}
									
				}
			}


		}
		
		
		
		Circulo buscarCentro(int x,int y,Bitmap bmp)
		{
			Circulo circActual = new Circulo();
			int i=y;
			float a;
			Color aux;
			bool flg=true;
			while(flg)
			{
				aux = bmp.GetPixel(x,i);
				if(aux.B != 0)
				{
					flg=false;
					
				}
				else
				{
					i++;
				}
				
			}
			i=i-y;
			i=i/2;
			circActual.setY(i+y);
			circActual.setRadio(i+2);
			
			a=i*i;
			a=a*3.1416f;
			circActual.setArea(a);
			
			
			i=x;
			flg=true;
			while(flg)
			{
				aux = bmp.GetPixel(i,y);
				if(aux.B != 0)
				{
					flg=false;
					
				}
				else
				{
					i++;
				}
				
			}
			i=i-x;
			i=i/2;
			circActual.setX(i+x);
			if(i > circActual.getRadio())
			{
				circActual.setRadio(i+2);
			}
			
			colorearCirc(circActual,bmp,Color.Yellow);
			return circActual;
		}
		
		
		void mostrarCentros(Bitmap bmp)
		{
			foreach(Circulo circActual in circulos)
			{
				bmp.SetPixel(circActual.getX(),circActual.getY(),Color.Blue);
				bmp.SetPixel(circActual.getX()+1,circActual.getY(),Color.Blue);
				bmp.SetPixel(circActual.getX()-1,circActual.getY(),Color.Blue);
				bmp.SetPixel(circActual.getX(),circActual.getY()+1,Color.Blue);
				bmp.SetPixel(circActual.getX(),circActual.getY()-1,Color.Blue);
				bmp.SetPixel(circActual.getX()+1,circActual.getY()+1,Color.Blue);
				bmp.SetPixel(circActual.getX()+1,circActual.getY()-1,Color.Blue);
				bmp.SetPixel(circActual.getX()-1,circActual.getY()+1,Color.Blue);
				bmp.SetPixel(circActual.getX()-1,circActual.getY()-1,Color.Blue);
			}
		}
		
		void drawID(Bitmap bmp)
		{
			foreach(Circulo i in circulos)
			{
				int x,y;
				x=i.getX()-10;
				y=i.getY()+i.getRadio()-15;
				RectangleF rectf = new RectangleF(x,y, 90, 50);
				Graphics g = Graphics.FromImage(bmp);
				g.DrawString(i.getID().ToString(), new Font("Arial",30), Brushes.Black, rectf);
				g.Flush();

			}
		}
		
		bool muyCerca(Circulo ver)
		{
			foreach(Circulo i in circulos)
			{
				int y,x;
				y = ver.getY() - i.getY();
				x = ver.getX() - i.getX();
				if(y >= -50 && y <= 50)
				{
					if(x >= -50 && x <= 50)
					{
						return false;
					}
				}
				
			}
			return true;
		}
		
		void rellenarLista(Circulo [] arr)
		{
			
			for(int i=0; i < arr.Length ; i++)
			{
				circulos.RemoveAt(0);
			}
			
			for(int i=0; i < arr.Length ; i++)
			{
				circulos.Insert(i,arr[i]);
			}
		}
		
		string mostrarInfo()
		{
			string result="";
			foreach(Circulo i in circulos)
			{
				result+=i.toString() + string.Format(Environment.NewLine);
			}
			return result;
		}
		
		void drawLines(Bitmap bmp)
		{
			Graphics g = Graphics.FromImage(bmp);
			Pen p = new Pen(Color.Red,1);
			llenarGrafoVertices();
			
			int i,j,cont=0;
			Circulo[] arr = circulos.ToArray();
			
			for(i=0;i < arr.Length ;i++)
			{
				for(j=i+1;j < arr.Length ;j++)
				{
					
					if( caminoLibre( bmp,arr[i].getX(),arr[i].getY(),arr[j].getX(),arr[j].getY(),
					                arr[i],arr[j]))
                    {
                        double de=0;
                        de=abs(arr[i].getX()-arr[j].getX()) + abs(arr[i].getY()-arr[j].getY());
                        grf.insertarArista(i+1,j+1,0,de);
						cont++;
                    }
                }
			}


		}
		
		
		void llenarGrafoVertices()
		{
			foreach (Circulo c in circulos)
			{
				grf.insertaVertice(c.getID());
			}
			
		}
		
		
		string parPuntos(Bitmap bmp)
		{
			string msg="";
			int i,j,ida=-1,idb=-1;
			double d,dmin=bmp.Width; 
			Circulo[] arr = circulos.ToArray();
			
			for(i=0;i < arr.Length ;i++)
			{
				int xi,yi;
				xi=arr[i].getX();
				yi=arr[i].getY();
				for(j=i+1;j < arr.Length ;j++)
				{
					int xj,yj;
					xj=arr[j].getX();
					yj=arr[j].getY();
					d=Math.Sqrt((abs(xi-xj))/2 + (abs(yi-yj))/2);
					if(d < dmin)
					{
						dmin = d;
						ida=i+1;
						idb=j+1;
						
					}
					
                }
		
			}
			foreach(Circulo c in circulos)
			{
				colorearCirc(c,bmp,Color.Yellow);
			}
			if(ida == -1 || idb == -1)
			{
				return "No existen par de puntos";
			}
			else
			{
				msg+="Centros Mas Cercanos: "+ida.ToString()+" y "+idb.ToString();
			}
			foreach(Circulo c in circulos)
			{
				colorearCirc(c,bmp,Color.Yellow);
			}

			return msg;
		}
		
		void colorearCirc(Circulo circActual,Bitmap bmp,Color color)
		{
			int xA,yA,A,B,C,fc;
			for(yA=0; yA < circActual.getY()+circActual.getRadio() ;yA++)
			{
				for(xA=0 ; xA < circActual.getX()+circActual.getRadio(); xA++)
				{
					try
					{
						A=(circActual.getX()-xA);
						A=A*A;
						B=(circActual.getY()-yA);	
						B=B*B;
						C=circActual.getRadio();
						C=C*C;
						C=C+10;
						fc=A+B-C;
						if(fc<=0)
						{
							bmp.SetPixel(xA,yA,color);
						}
						
					}
					catch
					{
						//Nada
					}
				}
			}
			
		}
		
		
		
		int abs(int x)
		{
			if(x<0)
				return x * -1;
			
			return x;
		}
		
		
		bool caminoLibre(Bitmap bmp,int x0,int y0,int x1,int y1,Circulo pri,Circulo seg)
		{
			bool resul=true;
			if (abs(y1 - y0) < abs(x1 - x0))
			{
				if (x0 > x1)
		            resul=plotLineLow(bmp,x1, y1, x0, y0,pri,seg);
		        else
		            resul=plotLineLow(bmp,x0, y0, x1, y1,pri,seg);
		        
			}
		        
		    else
		    {
		    	if (y0 > y1)
		            resul=plotLineHigh(bmp,x1, y1, x0, y0,pri,seg);
		        else
		            resul=plotLineHigh(bmp,x0, y0, x1, y1,pri,seg);
		    }
		    return resul;
            
		}
		
		bool plotLineHigh(Bitmap bmp,int x0,int y0,int x1,int y1,Circulo pri,Circulo seg)
		{
			int dx,dy,xi,D,x,y;
			Color col;
			
		    dx = x1 - x0;
		    dy = y1 - y0;
		    xi = 1;
		    if (dx < 0)
		    {
		    	xi = -1;
		        dx = dx * -1;
		    }
		        
		    D = (2 * dx) - dy;
		    x = x0;
		
		    for (y=y0; y <= y1 ; y++)
		    {
		    	//Verifica Colision con paredes
		    	col = bmp.GetPixel(x,y);
		    	
		    	if(pri.esParte(x,y) || seg.esParte(x,y))
		    	{
		    		//Salta
		    	}
		    	else
		    	{
		    		if(col.R  > 210) //Blanco y rojo
			    	{
			    		if(col.G > 210)
			    		{
			    			if(col.B > 210)
				    		{
			    				//Salta
				    		}
			    			else
		    				{
								return false;

			    			}
			    		}
			    		else
			    		{
			    			if(col.G > 35)
			    			{
			    				return false;
			    			}
			    			if(col.B > 35)
				    		{
				    			return false;
				    		}
			    		}
		    				
			    	}
		    		else
		    		{
		    			return false;
		    		}	
		    	
		    	}
		    	

		    	
		    	if (D > 0)
		    	{
		    		x = x + xi;
		            D = D + (2 * (dx - dy));
		    	}
		            
		        else
		        {
		        	D = D + 2*dx;
		        }
		    
		    }
		    return true;
		        
		}
		
		bool plotLineLow(Bitmap bmp,int x0,int y0,int x1,int y1,Circulo pri,Circulo seg)
		{
			int dx,dy,yi,D,x,y;
			Color col;
		    dx = x1 - x0;
		    dy = y1 - y0;
		    yi = 1;
		    if (dy < 0)
		    {
		    	yi = -1;
		        dy = dy * -1;
		    }
		        
		    D = (2 * dy) - dx;
		    y = y0;
		
		    for (x=x0; x <= x1 ; x++)
		    {
		    	//Verifica Colision con paredes
		    	col = bmp.GetPixel(x,y);
		    	if(pri.esParte(x,y) || seg.esParte(x,y))
		    	{
		    		//Salta
		    	}
		    	else
		    	{
		    		if(col.R  > 210) //Blanco y rojo
			    	{
			    		if(col.G > 210)
			    		{
			    			if(col.B > 210)
				    		{
			    				//Salta
				    		}
			    			else
		    				{
				    			return false;
			    			}
			    		}
			    		else
			    		{
			    			if(col.G > 35)
			    			{
			    				return false;
			    			}
			    			if(col.B > 35)
				    		{
				    			return false;
				    		}
			    		}
		    				
			    	}
		    		else
		    		{
		    			return false;
		    		}	
		    	
		    	}
		    	
		    	
		    	if (D > 0)
		    	{
		    		y = y + yi;
		            D = D + (2 * (dy - dx));
		    	}
		            
		        else
		        {
		        	D = D + 2*dy;
		        }
		    
		    }
		    return true;
		        
		}


		
		bool igual(Grafo a,Grafo b)
		{
			return a.getFst().getID() == b.getFst().getID();
		}
		
		
		
		
	}
}
