/*
 * Created by SharpDevelop.
 * User: Gustavo
 * Date: 18/03/2021
 * Time: 10:58 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Actividad04
{	
	public class Circulo
	{
		int x=0,y=0,radio;
		float area=0;
		int id;
		public Circulo()
		{
		}
		
		public void setX(int a)
		{
			x=a;
		}
		
		public void setY(int a)
		{
			y=a;
		}
		
		public void setArea(float a)
		{
			area=a;
		}
		
		public void setRadio(int a)
		{
			radio=a;
		}
		
		public void setID(int a)
		{
			id=a;
		}
		
		public bool esParte(int xA,int yA)
		{
			int A,B,C,fc;
			A=(x-xA);
			A=A*A;
			B=(y-yA);	
			B=B*B;
			C=radio;
			C=C*C;
			C=C+10;
			fc=A+B-C;
			if(fc<=0)
			{
				return true;
			}
			return false;
		}
		
		public int getID()
		{
			return id;
		}
		
		public int getX()
		{
			return x;
		}
		
		public int getY()
		{
			return y;
		}
		
		public float getArea()
		{
			return area;
		}
		
		public int getRadio()
		{
			return radio;
		}
		
		public string toString()
		{
			string result;
			result=id.ToString()+": ";
			result+="Centro = (" + x.ToString() + ","+ y.ToString() +")    ";
			result+="Radio = " + radio.ToString() + "    ";
			result+="Area = " + area.ToString() + "    ";
			return result;
		}
	}
	

}



 