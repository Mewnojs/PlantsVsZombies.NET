using System;
using System.Collections.Generic;

namespace Sexy.Misc
{
	internal class EquationSystem
	{
		public EquationSystem(int theNumVariables)
		{
			this.mRowSize = theNumVariables + 1;
			this.mCurRow = 0;
			this.eqs.Resize(this.mRowSize * theNumVariables);
			this.sol.Resize(theNumVariables);
		}

		public void SetCoefficient(int theRow, int theCol, float theValue)
		{
			int num = this.mRowSize * theRow + theCol;
			this.eqs[num] = theValue;
		}

		public void SetConstantTerm(int theRow, float theValue)
		{
			int num = this.mRowSize * theRow + this.mRowSize - 1;
			this.eqs[num] = theValue;
		}

		public void SetCoefficient(int theCol, float theValue)
		{
			this.SetCoefficient(this.mCurRow, theCol, theValue);
		}

		public void SetConstantTerm(float theValue)
		{
			this.SetConstantTerm(this.mCurRow, theValue);
		}

		public void NextEquation()
		{
			this.mCurRow++;
		}

		public void Solve()
		{
			int num = this.mRowSize;
			int num2 = this.mRowSize - 1;
			for (int i = 0; i < num2; i++)
			{
				int num3 = i;
				for (int j = i + 1; j < num2; j++)
				{
					if (Math.Abs(this.eqs[j * num + i]) > Math.Abs(this.eqs[num3 * num + i]))
					{
						num3 = j;
					}
				}
				for (int k = 0; k < num2 + 1; k++)
				{
					float num4 = this.eqs[i * num + k];
					this.eqs[i * num + k] = this.eqs[num3 * num + k];
					this.eqs[num3 * num + k] = num4;
				}
				for (int j = i + 1; j < num2; j++)
				{
					float num5 = this.eqs[j * num + i] / this.eqs[i * num + i];
					if (num5 != 0f)
					{
						for (int k = num2; k >= i; k--)
						{
							List<float> list;
							int num6;
							(list = this.eqs)[num6 = j * num + k] = list[num6] - this.eqs[i * num + k] * num5;
						}
					}
				}
			}
			for (int j = num2 - 1; j >= 0; j--)
			{
				float num7 = 0f;
				for (int k = j + 1; k < num2; k++)
				{
					num7 += this.eqs[j * num + k] * this.sol[k];
				}
				this.sol[j] = (this.eqs[j * num + num2] - num7) / this.eqs[j * num + j];
			}
		}

		public List<float> eqs = new List<float>();

		public List<float> sol = new List<float>();

		public int mRowSize;

		public int mCurRow;
	}
}
