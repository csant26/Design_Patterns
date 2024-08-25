using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns
{

    //Single Responsibility Principle

    //public class Journal
    //{
    //    private readonly List<String> entries = new List<String>();
    //    private static int count;

    //    public int AddEntry(String entry)
    //    {
    //        entries.Add($"{++count}:{entry}");
    //        return count; //memento
    //    }

    //    public void RemoveEntry(int index)
    //    {
    //        entries.RemoveAt( index );
    //    }
    //    public override string ToString()
    //    {
    //        return String.Join(Environment.NewLine, entries);
    //    }
    //}
    //public class Persistece
    //{
    //    public void Save(Journal j, String fileName, bool overwrite = false)
    //    {
    //        if (overwrite || !File.Exists(fileName))
    //        {
    //            File.WriteAllText(fileName, j.ToString());
    //        }
    //    }
    //}

    //

    //Open/Closed Principle

    public enum Color
    {
        Red,Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large
    }
    public class Product
    {
        public string Name;
        public Color color;
        public Size size;
        public Product(string Name, Color color, Size size)
        {
            if (Name == null)
            {
                throw new ArgumentNullException("name");
            }
            this.Name = Name;
            this.color = color;
            this.size = size;
        }
    }
    public interface ICondition<T>
    {
        bool isMatched(T t);
    }
    public interface IFilter<T>
    {
        IEnumerable<T> FilterItem(IEnumerable<T> items, ICondition<T> condition);
    }
    public class ColorCondition : ICondition<Product>
    {
        private Color _color;
        public ColorCondition(Color color)
        {
            _color = color;
        }
        public bool isMatched(Product product)
        {
            return product.color == _color;
        }
    }
    public class SizeCondition : ICondition<Product>
    {
        private Size _size;
        public SizeCondition(Size size)
        {
            _size = size;
        }
        public bool isMatched(Product product)
        {
            return product.size == _size;
        }
    }
    public class AndCondition<T> : ICondition<T>
    {
        private ICondition<T> _first,_second;
        public AndCondition(ICondition<T> first, ICondition<T> second)
        {
            _first = first;
            _second = second;
        }
        public bool isMatched(T t)
        {
            return _first.isMatched(t)&&_second.isMatched(t);
        }

    }
    public class Filter : IFilter<Product>
    {
        public IEnumerable<Product> FilterItem(IEnumerable<Product> items, ICondition<Product> condition)
        {
            foreach (var item in items)
                if (condition.isMatched(item))
                    yield return item;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Single Responsibility Principle
            //Journal j = new Journal();
            //j.AddEntry("Dear Diary, I was sad today.");
            //j.AddEntry("Dear Diary, I went out today.");
            //Console.WriteLine(j);
            //Persistece p = new Persistece();
            //var fileName = @"E:\CODING\C# Projects\DesignPatterns\journal.txt";
            //p.Save(j, fileName, true);
            //Process.Start(fileName);

            //Open/Closed Principle
            Product p1 = new Product("Bottle",Color.Blue,Size.Small);
            Product p2 = new Product("Pen",Color.Red,Size.Small);
            Product p3 = new Product("Grass",Color.Green,Size.Large);
            Product p4 = new Product("Mobile", Color.Green, Size.Medium);
            Product p5 = new Product("Jacket", Color.Blue, Size.Small);
            Product p6 = new Product("Purse", Color.Red, Size.Large);
            Product[] products = { p1, p2, p3, p4, p5, p6};
            Filter filter = new Filter();
            foreach(var item in filter.FilterItem(products, new AndCondition<Product>
                (new ColorCondition(Color.Red),
                new SizeCondition(Size.Large))))
            {
                 Console.WriteLine($"~{item.Name}");

            }

            

            Console.ReadLine();
        }
    }
}
