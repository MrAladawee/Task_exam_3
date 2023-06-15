using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_exam_3
{
    internal class Gauss
    {
        private double[] coef;
        private double[] coef_res;
        public Gauss(double[] coef_from, double[] coef_res_from)
        {
            this.coef = coef_from;
            this.coef_res = coef_res_from;
        }


        public static double[] Solve(double[] coefficients, double[] rightHandSide)
        {
            // Проверяем размеры массивов
            if (coefficients.Length != 9 || rightHandSide.Length != 3)
            {
                throw new ArgumentException("Неверные размеры массивов коэффициентов и/или правой части");
            }

            double[,] matrix = new double[3, 3];
            double[] vector = new double[3];

            // Заполняем матрицу и вектор из переданных массивов
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = coefficients[i * 3 + j];
                }
                vector[i] = rightHandSide[i];
            }

            return Solve(matrix, vector);
        }

        public static double[] Solve(double[,] matrix, double[] vector)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Проверяем размеры матрицы и вектора
            if (rows != cols || rows != vector.Length)
            {
                throw new ArgumentException("Неверные размеры матрицы и/или вектора");
            }

            double[] solution = new double[rows];

            // Прямой ход метода Гаусса
            for (int i = 0; i < rows - 1; i++)
            {
                // Поиск максимального элемента в текущем столбце
                int maxRowIndex = i;
                double maxElement = Math.Abs(matrix[i, i]);
                for (int j = i + 1; j < rows; j++)
                {
                    double currentElement = Math.Abs(matrix[j, i]);
                    if (currentElement > maxElement)
                    {
                        maxRowIndex = j;
                        maxElement = currentElement;
                    }
                }

                // Перестановка строк, если необходимо
                if (maxRowIndex != i)
                {
                    SwapRows(matrix, vector, i, maxRowIndex);
                }

                // Приведение текущего столбца к диагональному виду
                for (int j = i + 1; j < rows; j++)
                {
                    double factor = matrix[j, i] / matrix[i, i];
                    for (int k = i; k < cols; k++)
                    {
                        matrix[j, k] -= factor * matrix[i, k];
                    }
                    vector[j] -= factor * vector[i];
                }
            }

            // Обратный ход метода Гаусса
            for (int i = rows - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < rows; j++)
                {
                    sum += matrix[i, j] * solution[j];
                }
                solution[i] = (vector[i] - sum) / matrix[i, i];
            }

            return solution;
        }

        private static void SwapRows(double[,] matrix, double[] vector, int row1, int row2)
        {
            int cols = matrix.GetLength(1);

            for (int j = 0; j < cols; j++)
            {
                double temp = matrix[row1, j];
                matrix[row1, j] = matrix[row2, j];
                matrix[row2, j] = temp;
            }

            double tempVector = vector[row1];
            vector[row1] = vector[row2];
            vector[row2] = tempVector;
        }

    }
}