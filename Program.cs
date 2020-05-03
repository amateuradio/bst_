using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;



namespace bst
{

    public enum Side
    {
        Left,
        Right,
        None
    }

    // Узел бинарного дерева
    public class BTNode<T> where T : IComparable
    {

        // Конструктор класса
        public BTNode(T data)
        {
            Data = data;
            LeftNode = null;
            RightNode = null;
        }

        // Данные которые хранятся в узле
        public T Data { get; set; }

        // Левая ветка
        public BTNode<T> LeftNode { get; set; }

        // Правая ветка
        public BTNode<T> RightNode { get; set; }

        // Родитель
        public BTNode<T> ParentNode { get; set; }

        // Расположение узла относительно его родителя
        public Side? NodeSide =>
            ParentNode == null
            ? (Side?)null
            : ParentNode.LeftNode == this
                ? Side.Left
                : Side.Right;

        /// Преобразование экземпляра класса в строку
        public override string ToString() => Data.ToString();
    }


    // Бинарное дерево
    public class BT<T> where T : IComparable
    {
        // Корень бинарного дерева
        public BTNode<T> RootNode { get; set; }

        // Добавление нового узла в бинарное дерево
        //Нерекурсивно

        public void Add(BTNode<T> node, BTNode<T> RN = null)
        {
            BTNode<T> cur = null;

            if (RootNode == null)
            {
                node.ParentNode = null;
                RootNode = node;
                return;
            }

            cur = RN;
            //поиск место вставки
            while (true)
            {
                node.ParentNode = cur;
                if (node.Data.CompareTo(cur.Data) == 0) return;//случай если узел уже есть
                if (node.Data.CompareTo(cur.Data) < 0) //переход в левое подерево
                {
                    if (cur.LeftNode == null) { cur.LeftNode = node; return; } //вставка
                    cur = cur.LeftNode;
                }
                else
                {
                    if (cur.RightNode == null) { cur.RightNode = node; return; } //вставка в правое поддерево
                    cur = cur.RightNode;
                }
            }

        }

        public void Add(T data)
        {
            Add(new BTNode<T>(data), RootNode);
        }

        // Поиск узла по значению
        // Нерекурсивно
        public BTNode<T> FindNode(T data)
        {
            BTNode<T> cur = RootNode;
            BTNode<T> last = null;

            //поиск пока остались не просмотренные узлы
            //нерекурсивный обход дерева
            while (cur != null)
            {
                if (cur.Data.CompareTo(data) == 0) return (cur); //возврат найденного значения
                if (last == cur.ParentNode)
                {
                    if (cur.LeftNode != null) //перемещение влево
                    {
                        last = cur;
                        cur = cur.LeftNode;
                        continue;
                    }
                    else
                    {
                        last = null;
                    }
                }
                if (last == cur.LeftNode)
                {
                    if (cur.RightNode != null) //перемещение вправо
                    {
                        last = cur;
                        cur = cur.RightNode;
                        continue;
                    }
                    else
                    {
                        last = null;

                    }
                }
                if (last == cur.RightNode)
                {
                    last = cur;
                    cur = cur.ParentNode;


                }
            }

            return (null);      //врзвращается null если узел не найден    
        }


        // размер дерева
        public int Size()
        {
            BTNode<T> cur = RootNode;
            BTNode<T> last = null;
            int res = 0;

            //поиск пока остались не просмотренные узлы
            //нерекурсивный обход дерева
            while (cur != null)
            {

                if (last == cur.ParentNode)
                {

                    res++;
                    if (cur.LeftNode != null) //перемещение влево
                    {
                        last = cur;
                        cur = cur.LeftNode;
                        continue;
                    }
                    else
                    {

                        last = null;
                    }
                }
                if (last == cur.LeftNode)
                {
                    if (cur.RightNode != null) //перемещение вправо
                    {
                        last = cur;
                        cur = cur.RightNode;
                        //res++;
                        continue;
                    }
                    else
                    {

                        last = null;

                    }
                }
                if (last == cur.RightNode)
                {
                    last = cur;
                    cur = cur.ParentNode;


                }
            }

            return (res);
        }



        // поиск n-го по значению ключа (нерекурсивный вариант)
        public T NKeyValue(int N)
        {
            BTNode<T> cur = RootNode;
            BTNode<T> last = null;
            T[] a = new T[Size()];
            int res = 0;

            //поиск пока остались не просмотренные узлы
            //нерекурсивный обход дерева
            while (cur != null)
            {

                if (last == cur.ParentNode)
                {
                    a[res] = cur.Data;
                    res++;

                    if (cur.LeftNode != null) //перемещение влево
                    {
                        last = cur;
                        cur = cur.LeftNode;
                        continue;
                    }
                    else
                    {
                        last = null;
                    }
                }
                if (last == cur.LeftNode)
                {
                    if (cur.RightNode != null) //перемещение вправо
                    {
                        last = cur;
                        cur = cur.RightNode;
                        continue;
                    }
                    else
                    {
                        last = null;

                    }
                }
                if (last == cur.RightNode)
                {
                    last = cur;
                    cur = cur.ParentNode;


                }

            }
            Array.Sort(a);
            return (a[N - 1]);
        }



        // поиск n-го по значению ключа (рекурсивный вариант)
        private T[] b;
        private int k;
        public T NKeyValueR(int N)
        {
            k = 0;
            b = new T[Size()]; //массив узлов
            NKeyValueR(RootNode); //вызов обхода
            Array.Sort(b); //сортировка
            return (b[N - 1]);

        }
        public void NKeyValueR(BTNode<T> startNode)
        {
            if (startNode != null)
            {
                //  Console.WriteLine(startNode.Data);
                b[k] = startNode.Data; //вносим значение в массив значений
                k++;
                //вызов для поддеревьев
                NKeyValueR(startNode.LeftNode);
                NKeyValueR(startNode.RightNode);
            }


        }


        //проверка на пустоту
        public bool Empty()
        {
            return (RootNode == null);
        }

        // Удаление узла бинарного дерева

        public void Remove(BTNode<T> node)
        {
            if (node == null)
            {
                return;
            }

            var curNodeSide = node.NodeSide;
            //если у узла нет подузлов, можно его удалить
            if (node.LeftNode == null && node.RightNode == null)
            {
                if (curNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = null;
                }
                else
                {
                    node.ParentNode.RightNode = null;
                }
            }
            //если нет левого, то правый ставим на место удаляемого 
            else if (node.LeftNode == null)
            {
                if (curNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = node.RightNode;
                }
                else
                {
                    node.ParentNode.RightNode = node.RightNode;
                }

                node.RightNode.ParentNode = node.ParentNode;
            }
            //если нет правого, то левый ставим на место удаляемого 
            else if (node.RightNode == null)
            {
                if (curNodeSide == Side.Left)
                {
                    node.ParentNode.LeftNode = node.LeftNode;
                }
                else
                {
                    node.ParentNode.RightNode = node.LeftNode;
                }

                node.LeftNode.ParentNode = node.ParentNode;
            }
            //если оба дочерних присутствуют, 
            //то правый становится на место удаляемого,
            //а левый вставляется в правый
            else
            {
                switch (curNodeSide)
                {
                    case Side.Left:
                        node.ParentNode.LeftNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        break;
                    case Side.Right:
                        node.ParentNode.RightNode = node.RightNode;
                        node.RightNode.ParentNode = node.ParentNode;
                        Add(node.LeftNode, node.RightNode);
                        break;
                    default:
                        var bufLeft = node.LeftNode;
                        var bufRightLeft = node.RightNode.LeftNode;
                        var bufRightRight = node.RightNode.RightNode;
                        node.Data = node.RightNode.Data;
                        node.RightNode = bufRightRight;
                        node.LeftNode = bufRightLeft;
                        Add(bufLeft, node);
                        break;
                }
            }
        }

        // Удаление узла дерева

        public void Remove(T data)
        {
            var foundNode = FindNode(data);
            Remove(foundNode);
        }

        // Вывод бинарного дерева
        public void PrintBT()
        {
            PrintBT(RootNode);
        }


        // Вывод бинарного дерева начиная с указанного узла

        private void PrintBT(BTNode<T> startNode, string indent = "", Side? side = null)
        {
            if (startNode != null)
            {
                var nodeSide = side == null ? "+" : side == Side.Left ? "L" : "R";
                Console.WriteLine($"{indent} [{nodeSide}]- {startNode.Data}");
                indent += new string(' ', 3);
                //рекурсивный вызов для левой и правой веток
                PrintBT(startNode.LeftNode, indent, Side.Left);
                PrintBT(startNode.RightNode, indent, Side.Right);
            }
        }

        //очистка дерева
        public void Clear()
        {
            while (RootNode != null)
                Remove(RootNode);
        }



    }




    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            test.OTest_rand();
            //test.OTest_bad();
            /*
            var BT = new BT<int>();
            
              BT.Add(8);
              BT.Add(3);
              BT.Add(10);
              BT.Add(1);
              BT.Add(6);
              BT.Add(4);
              BT.Add(7);
              BT.Add(14);
              BT.Add(16);
              BT.PrintBT();

                  Console.WriteLine(BT.NKeyValueR(3));

                  //Console.WriteLine(new string('-', 40));
                  BT.PrintBT();
                  Console.WriteLine(new string('-', 40));
                  var t = BT.FindNode(14);

                  if (t!=null)
                  Console.WriteLine(t.Data.ToString());
                  Console.WriteLine(new string('-', 40));
                  Console.WriteLine(BT.Size());
                  Console.WriteLine(new string('-', 40));
                  BT.PrintBT();

                  Console.WriteLine(new string('-', 40));
                  BT.Remove(3);
                  BT.PrintBT();
                  Console.WriteLine(new string('-', 40));
                  BT.Remove(8);
                  BT.PrintBT();
                  Console.WriteLine(new string('-', 40));
                  Console.ReadLine();
                  */
        }
    }
        /*
       int c,r;
       while (true)
       {
           //вывод меню
           Console.WriteLine("1. Вывод дерева");
           Console.WriteLine("2. Добавить элемент");
           Console.WriteLine("3. Удалить элемент");
           Console.WriteLine("4. Поиск элемента");
           Console.WriteLine("5. Опрос размера дерева");
           Console.WriteLine("6. Поиск n-го по значению ключа элемента ");
           Console.WriteLine("7. Проверка на пустоту");
           Console.WriteLine("0. Выход");
           c = Convert.ToInt32(Console.ReadLine());
           if (c == 1) BT.PrintBT();
           if (c == 2)
           {
               Console.WriteLine("Введите элемент");
               r = Convert.ToInt32(Console.ReadLine());
               BT.Add(r);
           }
           if (c == 3)
           {
               Console.WriteLine("Введите элемент");
               r = Convert.ToInt32(Console.ReadLine());
               BT.Remove(r);
           }
           if (c == 4)
           {
               Console.WriteLine("Введите элемент");
               r = Convert.ToInt32(Console.ReadLine());
               if(BT.FindNode(r)==null)
                   Console.WriteLine("Элемент не найден");
               else
                   Console.WriteLine("Элемент найден");
           }

           if (c == 5) Console.WriteLine(BT.Size());
           if (c == 6)
           {
               Console.WriteLine("Введите элемент");
               r = Convert.ToInt32(Console.ReadLine());
               Console.WriteLine(BT.NKeyValueR(r));

           }

           if (c == 7)
           {

               if (BT.Empty())
                   Console.WriteLine("Дерево пусто");
               else
                   Console.WriteLine("Дерево не пусто");
           }
           if (c == 0) break;
       }



    }
    }*/
    }