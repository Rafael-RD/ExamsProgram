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
            Console.WriteLine("Gostaria de criar um gabarito personalizado?");
            Console.WriteLine("1 - Personalizado \t 2 - Padrão");
            Console.Write("Escolha uma opção: ");

            int op;
            ReAskCustomSheet:
            while (!int.TryParse(Console.ReadLine(), out op))
            {
                Console.Write("Entrada invalida, somente numeros são aceitos\nTente novamente: ");
            }
            if (op < 1 || op > 2)
            {
                Console.Write("Escolha uma opção valida: ");
                goto ReAskCustomSheet;
            }

            char[] questionAnswers, choices;
            int maxChoices=1;
            if(op == 1)
            {
                Console.Write("Quantas questões? ");

                int nQuestions; 
                ReAskNQuestions:
                
                while(!int.TryParse(Console.ReadLine(), out nQuestions)){
                    Console.Write("Entrada invalida, somente numeros são aceitos\nTente novamente: ");
                }
                if (nQuestions < 1)
                {
                    Console.Write("Escolha uma opção valida: ");
                    goto ReAskNQuestions;
                }


                questionAnswers = new char[nQuestions];
                for(int i=0; i < questionAnswers.Length; i++)
                {
                    Console.Write($"Qual a Resposta correta para a questão {i+1}? ");
                    char sheetAnswer;
                    ReAskSheetAnswer:
                    while (!char.TryParse(Console.ReadLine().ToUpper(), out sheetAnswer)){
                        Console.Write("Entrada invalida, somente uma letra é aceita\nTente novamente: ");
                    }
                    if(!(sheetAnswer >= 'A' && sheetAnswer <= 'Z'))
                    {
                        Console.Write("Entrada deve ser uma letra de A a Z\nTente novamente: ");
                        goto ReAskSheetAnswer;
                    }
                    questionAnswers[i] = sheetAnswer;
                    if(maxChoices < ((int)sheetAnswer) -64) maxChoices = (int)sheetAnswer -64;
                }

                Console.WriteLine("\nQuantas escolhas por questão?");
                Console.WriteLine($"Com base no gabarito criado as perguntas devem ter no minimo {maxChoices} escolhas!");

                int nChoicesQuestions;
                ReAskNChoicesQuestions:
                while(!int.TryParse(Console.ReadLine(), out nChoicesQuestions))
                {
                    Console.Write("Entrada invalida, somente numeros são aceitos\nTente novamente: ");
                }
                if(!(nChoicesQuestions >= maxChoices && nChoicesQuestions <= (int) 'Z'))
                {
                    Console.Write($"Entrada invalida, deve ser entre {maxChoices} e {((int)'Z' - (int)'A') + 1}\nTente novamente: ");
                    goto ReAskNChoicesQuestions;
                }
                choices= new char[nChoicesQuestions];
                for (int i = 0; i < choices.Length; i++)
                {
                    choices[i] = (char)('A' + i);
                }
            }
            else
            {
                questionAnswers = new char[] { 'A', 'B', 'C', 'D', 'E', 'E', 'D', 'C', 'B', 'A' };
                choices = new char[] { 'A', 'B', 'C', 'D', 'E' };
            }

            List<StudentGrade> studentsList = new List<StudentGrade>();


            op = 1;
            while (op == 1)
            {
                Console.WriteLine("Começando Prova!");
                Console.Write("\nDigite o nome do aluno: ");

            ReaskName:
                string name = Console.ReadLine();
                if (name.Length == 0 || name[0] == ' ')
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

                Console.Clear();
                Console.WriteLine("1 - Continuar \t 0 - Terminar");
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

            Console.Clear();
            Console.WriteLine($"O aluno {studentsList[highestScoreIdx].Name} tirou a maior nota: {studentsList[highestScoreIdx].grade}");
            Console.WriteLine($"O aluno {studentsList[lowestScoreIdx].Name} tirou a menor nota: {studentsList[lowestScoreIdx].grade}");
            Console.WriteLine("Media: " + (average / studentsList.Count));
            Console.WriteLine(studentsList.Count + " provas feitas");
            Console.ReadKey();
        }
    }
}
