using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExercicioFinalLoop
{
    internal class Program
    {

        static void Main(string[] args)
        {
            char[] questionAnswers = new char[] { 'A', 'B', 'C', 'D', 'E', 'E', 'D', 'C', 'B', 'A' };
            char[] choices = new char[] { 'A', 'B', 'C', 'D', 'E' };
            List<StudentGrade> studentsList = new List<StudentGrade>();

            int op = 1;

            while (op == 1)
            {
                Console.Write("Digite o nome do aluno: ");

            ReaskName:
                string name = Console.ReadLine();
                if (name.Length == 0)
                {
                    Console.Write("Nome invalido, tente novamente: ");
                    goto ReaskName;
                }

                double grade = 0;

                for (int i=0; i<questionAnswers.Length;i++)
                {
                    Console.Clear();
                Question:
                    Console.WriteLine($"Pergunta {i + 1}/{questionAnswers.Length}:");
                    for (int j = 0; j < choices.Length; j++)
                    {
                        Console.Write($"{j + 1} - {choices[j]} \t ");
                    }
                    Console.WriteLine();
                    Console.Write("Selecione uma opção: ");
                ReaskChoice:
                    int choice;
                    try
                    {
                        choice = int.Parse(Console.ReadLine());

                    }
                    catch (Exception)
                    {
                        Console.Write("Entrada invalida, somente numeros são aceitos\nTente novamente: ");
                        goto ReaskChoice;
                    }

                    if (choice >= 1 && choice <= choices.Length)
                    {
                        if (choices[choice - 1] == questionAnswers[i])
                        {
                            Console.WriteLine("Resposta salva...");
                            grade++;
                        }
                        else Console.WriteLine("Resposta salva...");
                    }
                    else
                    {
                        Console.WriteLine("Resposta invalida, repetindo pergunta...\n");
                        goto Question;
                    }
                    System.Threading.Thread.Sleep(150);
                }

                StudentGrade studentGrade = new StudentGrade
                {
                    Name = name,
                    grade = grade * (10 / questionAnswers.Length)
                };
                studentsList.Add(studentGrade);

                Console.WriteLine("\n1 - Continuar \t 0 - Terminar");
                Console.WriteLine("Selecione uma opção: ");
                op = int.Parse(Console.ReadLine());
            }

            double average = 0;
            int highestScoreIdx = 0, lowestScoreIdx = 0;

            for(int i=0; i<studentsList.Count; i++)
            {
                if (studentsList[i].grade > studentsList[highestScoreIdx].grade) highestScoreIdx = i;
                if (studentsList[i].grade < studentsList[lowestScoreIdx].grade) lowestScoreIdx = i;
                average += studentsList[i].grade;
            }

            Console.WriteLine($"\nO aluno {studentsList[highestScoreIdx].Name} tirou a maior nota: {studentsList[highestScoreIdx].grade}");
            Console.WriteLine($"O aluno {studentsList[lowestScoreIdx].Name} tirou a menor nota: {studentsList[lowestScoreIdx].grade}");
            Console.WriteLine("Media: " + (average / studentsList.Count));
            Console.WriteLine(studentsList.Count + " provas feitas");
            Console.ReadKey();
        }
    }
}
