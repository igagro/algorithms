using System;

namespace GaussianElimination
{
    class Gaussian
    {
        static void Main(string[] args)
        {
            Equation equation = ReadEquation();
            double[] solution = SolveEquation(equation);
            PrintColumn(solution);
            Console.ReadLine();
        }

        static Equation ReadEquation()
        {
            // Example of input
            //4
            //1 0 0 0 1
            //0 1 0 0 5
            //0 0 1 0 4
            //0 0 0 1 3
            int size = Convert.ToInt32(Console.ReadLine());

            double[][] a = new double[size][];
            for (int i = 0; i < size; i++)
            {
                a[i] = new double[size];
            }
            double[] b = new double[size];
            for (int row = 0; row < size; row++)
            {
                var temp = Console.ReadLine().Split(' ');
                var ingredient = Array.ConvertAll(temp, int.Parse);
                for (int column = 0; column < size; ++column)
                {
                    a[row][column] = ingredient[column];
                }
                b[row] = ingredient[ingredient.Length - 1];
            }
            return new Equation(a, b);
        }

        static Position SelectPivotElement(double[][] a, int step)
        {
            // step defines which row we're currently examing, max defines column where is the pivot element
            int max = step;
            for (int i = step + 1; i < a.Length; i++)
            {
                if (Math.Abs(a[i][step]) > Math.Abs(a[max][step]))
                {
                    max = i;
                }
            }
            return new Position(max, step);
        }

        // 1st type of row operations (Swap the positions of two rows)
        static void SwapLines(double[][] a, double[] b, bool[] used_rows, Position pivot_element)
        {
            int size = a.Length;

            for (int column = 0; column < size; column++)
            {
                double tmpa = a[pivot_element.column][column];
                a[pivot_element.column][column] = a[pivot_element.row][column];
                a[pivot_element.row][column] = tmpa;
            }

            double tmpb = b[pivot_element.column];
            b[pivot_element.column] = b[pivot_element.row];
            b[pivot_element.row] = tmpb;

            bool tmpu = used_rows[pivot_element.column];
            used_rows[pivot_element.column] = used_rows[pivot_element.row];
            used_rows[pivot_element.row] = tmpu;

            pivot_element.row = pivot_element.column;
        }

        // 2nd type of row operations (Multiply a row by a nonzero scalar)
        static void ProcessPivotElement(double[][] a, double[] b, Position pivot_element)
        {
            for (int i = pivot_element.column + 1; i < b.Length; i++)
            {
                double alpha = a[i][pivot_element.column] / a[pivot_element.row][pivot_element.column];
                b[i] -= b[pivot_element.row] * alpha;
                for (int j = pivot_element.column; j < a[0].Length; j++)
                {
                    a[i][j] -= (a[pivot_element.row][j] * alpha);
                }
            }
        }

        static void MarkPivotElementUsed(Position pivot_element, bool[] used_rows, bool[] used_columns)
        {
            used_rows[pivot_element.row] = true;
            used_columns[pivot_element.column] = true;
        }

        static double[] SolveEquation(Equation equation)
        {
            double[][] a = equation.a;
            double[] b = equation.b;
            int size = a.Length;

            bool[] used_columns = new bool[size];
            bool[] used_rows = new bool[size];
            for (int step = 0; step < size; step++)
            {
                Position pivot_element = SelectPivotElement(a, step);
                SwapLines(a, b, used_rows, pivot_element);
                ProcessPivotElement(a, b, pivot_element);
                MarkPivotElementUsed(pivot_element, used_rows, used_columns);
            }
            for (int i = a.Length - 1; i >= 0; i--)
            {
                double firstCoef = a[i][i];
                for (int j = i + 1; j < a[0].Length; j++)
                {
                    b[i] -= (b[j] * a[i][j]);
                }
                b[i] /= firstCoef;
            }
            return b;
        }

        static void PrintColumn(double[] column)
        {
            int size = column.Length;
            foreach (var col in column)
            {
                Console.Write(col + " ");
            }
        }
    }

    class Equation
    {
        public Equation(double[][] a, double[] b)
        {
            this.a = a;
            this.b = b;
        }

        public double[][] a;
        public double[] b;
    }

    class Position
    {
        public Position(int row, int column)
        {
            this.column = column;
            this.row = row;
        }

        public int column;
        public int row;
    }
}
