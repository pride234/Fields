using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fields 
{
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

	public class Polynomial
	{
	
		private int[] number = null;
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public Polynomial()
		{

			number = new int[1];	// После вызова такого конструктора будет нулевой полином
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public Polynomial(string bit)
		{
		
			var dig = bit.Length;
			number = new int[dig];

			for(var i = 0; i < dig; i++)
			{
				number[i] = Convert.ToByte(bit.Substring(bit.Length - (i + 1), 1), 2);		
			}

		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public string ToBit()
		{ // Выводит на экран полином в виде битовой строки
			
			var result = new StringBuilder();

			for (var i = number.Length-1; i >= 0; i--)
			{ 
				result.Append(Convert.ToString(number[i], 2));
			}

			//var result = "";

			//result += Convert.ToString((long)number[number.Length-1], 2);
				
			//for (int i = number.Length-2; i >= 0; i--) result += Convert.ToString((long)number[i], 2);
				
			return result.ToString();
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private static int[] Padding (int[] mas, int len) 
		{ // Расширяет массив
		
			var padded = new int[len];

			for (var i = 0; i < mas.Length; i++)
			{ 
				padded[i] = mas[i];	
			}

			return padded;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private static Polynomial Humanresult (Polynomial num, int maxlen)
		{ // Делает массив читаемым (убирает старшие разряды нулей)
			
			var result = new Polynomial();
			var i = maxlen-1;
			
			while (num.number[i] == 0)
			{
				i--;

				if (i == -1) 
				{
					return result;
				}
			}

			var humanresult = new int[i+1];
			var j = 0;

			for (; j < humanresult.Length; j++)
			{ 
				humanresult[j] = num.number[j];
			}
			
			result.number = humanresult;
			return result;			 
		}	
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public Polynomial Add (Polynomial num) 
		{

			var maxlen = this.number.Length >= num.number.Length ? this.number.Length : num.number.Length; // Длина сложения будет равна длине самого длинного полинома

			int[] a = Padding(this.number, maxlen);	
			int[] b = Padding(num.number, maxlen);	
			var result = new int[maxlen];	
			
			for (var i = 0; i < maxlen; i++)
			{
				result[i] = a[i] ^ b[i];
			}
			
			var Result = new Polynomial 
			{
				number = result,
			};

			return Humanresult(Result, maxlen);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private Polynomial ShiftDigitsToRight (int[] array, int i)
		{
			
			var a = new Polynomial();	

			a.number = new int[array.Length + i];

			for (var j = 0; j < array.Length; j++)
			{
			
				a.number[j+i] = array[j];
			}
									
			return a;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		private Polynomial mod() 
		{

			var generator = new Polynomial("100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000010000000111");  

			if (this.number.Length < generator.number.Length)
			{
				return this;
			}

			var R = new Polynomial
			{
				number = Padding(this.number, this.number.Length)
			};

			var Temp = new Polynomial();

			while (R.number.Length >= generator.number.Length ) 
			{			
				Temp = ShiftDigitsToRight(generator.number, R.number.Length - generator.number.Length);
				R = R.Add(Temp);
			}

			return R;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public Polynomial MultiplyBy (Polynomial num_par) 
		{
			
			var temp = new Polynomial();
			var result  = new Polynomial();			

			var num = new Polynomial 
			{
				number = Padding(num_par.number, num_par.number.Length)
			};

			result.number = Padding(result.number, this.number.Length + num.number.Length);

			for (var i = 0; i < num.number.Length; i++) 
			{				
				if ((num.number[i] & 1) == 0)
				{ 
					continue;
				}
				temp = ShiftDigitsToRight(this.number, i);
				result = result.Add(temp);
			}

			result = result.mod();

			return Humanresult(result, result.number.Length);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public Polynomial Square() 
		{
			
			var array = new int[2 * this.number.Length - 1];
			
			for (var i = 0; i < this.number.Length; i++)
			{
				array[2*i] = this.number[i];
			}
		
			var result = new Polynomial
			{
				number = array
			};

			return result.mod();
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|

		public Polynomial GornerPower(Polynomial num) 
		{
		
			var C = new Polynomial("1");
			var A = new Polynomial
			{
				number = Padding(this.number, this.number.Length)
			};

			for (var i = 0; i<num.number.Length; i++) 
			{
				if (num.number[i] == 1)
				{ 
					C = C.MultiplyBy(A).mod();
				}

				A = A.MultiplyBy(A).mod();
			}
			
			return Humanresult(C, C.number.Length);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	
		public Polynomial Reverse()
		{
			
			var ferma = new Polynomial
			{
				number = new int[173]
			};

			ferma.number[0] = 0;

			for (var i = 1; i < 173; i++)
			{
				ferma.number[i] = 1;
			}
		
			return this.GornerPower(ferma);
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
		
		public Polynomial Trace()
		{

			var result = new Polynomial
			{
				number = Padding(this.number, this.number.Length)
			};

			var Temp   = new Polynomial
			{
				number = Padding(this.number, this.number.Length)
			};
			
			for (var i = 1; i < 173; i++) 
			{
				Temp = Temp.Square();
				result = result.Add(Temp);
			}

			result = result.mod();

			return result;
		}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
	}}
