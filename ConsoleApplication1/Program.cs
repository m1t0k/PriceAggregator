using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    /*
    public interface ICreator<T>
    {
        T Create(Dictionary<string, object> props);
    }
    
    public class ExpressionCreator<T> : ICreator<T>
    {
        private readonly Func<Dictionary<string, object>, T> _creator;

        public ExpressionCreator()
        {
            var type = typeof(T);
            var newExpression = Expression.New(type);
            var dictParam = Expression.Parameter(typeof(Dictionary<string, object>), "d");
            var list = new List<MemberBinding>();
            var propertyInfos = type.GetProperties(BindingFlags.Instance |
                                BindingFlags.Public |
                                BindingFlags.SetProperty);

            foreach (var propertyInfo in propertyInfos)
            {
                Expression call = Expression.Call(
                                   typeof(DictionaryExtension),
                                   "GetValue", new[] { propertyInfo.PropertyType }, dictParam, 
                                   Expression.Constant(propertyInfo.Name));

                MemberBinding mb = Expression.Bind(propertyInfo.GetSetMethod(), call);
                list.Add(mb);
            }

            var ex = Expression.Lambda<Func<Dictionary<string, object>, T>>(
                                              Expression.MemberInit(newExpression, list),
                                              new[] { dictParam });
            _creator = ex.Compile();
        }
        public T Create(Dictionary<string, object> props)
        {
            return _creator(props);
        }
    }
    */
    /*
    var list=new List<U>();

    list.Select(u=>new V()).ToList();
        
    
    public class CustomTypeConverter<U, V> where U : new() where V : new()
    {
        static CustomTypeConverter()
        {
            var uPropertyInfos = typeof(U).GetProperties(BindingFlags.Instance |
                           BindingFlags.Public |
                           BindingFlags.GetProperty);

            var vPropertyInfos = typeof(V).GetProperties(BindingFlags.Instance |
                           BindingFlags.Public |
                           BindingFlags.SetProperty);


            var uValueParam = Expression.Parameter(typeof(U), "uValue");
            var bindings = new List<MemberBinding>();

            foreach (var propertyInfo in uPropertyInfos)
            {
                // Create expression representing Map(a,v)

                MethodCallExpression propertyValue = Expression.Call(uValueParam);

                    
            }
        }
    }

    static class DictionaryExtension
    {
        public static TType GetValue<TType>(this Dictionary<string, object> d, string name)
        {
            object value;
            return d.TryGetValue(name, out value) ? (TType)value : default(TType);
        }

        public static void SetValue<U,V>(U u,V v)
        {
            u.
        }
    }

    public static class MyDynamicLinq
    {
        public static IQueryable<T> CustomWhere<T>(this IQueryable<T> query) where T : new()
        {
            return query;
        }
    }

    public class Entity : IEquatable<Entity>, IComparable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            Name = Description = Id.ToString();
            TimeCreated = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeCreated { get; set; }

        public int CompareTo(Entity other)
        {
            if (other == null)
                return 1;

            return Id.CompareTo(other.Id);
        }

        public bool Equals(Entity other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }

        public void Print()
        {
            Console.WriteLine($"({Id} {Name} {Description} {TimeCreated});");
        }
    }


    public class Entity2 : IEquatable<Entity>, IComparable<Entity>
    {
        public Entity2()
        {
            Id2 = Guid.NewGuid();
            Name2 = Description2 = Id2.ToString();
            TimeCreated2 = DateTime.Now;
        }

        public Guid Id2 { get; set; }
        public string Name2 { get; set; }
        public string Description2 { get; set; }
        public DateTime TimeCreated2 { get; set; }

        public int CompareTo(Entity other)
        {
            if (other == null)
                return 1;

            return Id2.CompareTo(other.Id);
        }

        public bool Equals(Entity other)
        {
            if (other == null)
                return false;

            return Id2 == other.Id;
        }

        public void Print()
        {
            Console.WriteLine($"({Id2} {Name2} {Description2} {TimeCreated2});");
        }
    }
    */

    internal class Program
    {
        private static object LockObject = new object();
        private static int Sum = 0;

        private static async Task<int> TestAsync(int i)
        {
            Console.WriteLine($"TestAsync start {i}");
            var tcs = new TaskCompletionSource<int>();
            //
            await Task.Delay(100);
            Console.WriteLine($"TestAsync end {i}");
            tcs.SetResult(i*10);
            return await tcs.Task;
        }

        public static void Main(string[] args)
        {
            //var numbers = new List<int>() { 1, 2, 3, 4 };
            //Console.WriteLine(numbers.Where(x => x > 4).FirstOrDefault());
            SumMethod().ContinueWith(sum => { Console.WriteLine($"Sum={sum.Result}"); });
            Console.ReadLine();
        }

        private static async Task<int> SumMethod()
        {
            var sum = 0;
            var tasks = Enumerable.Range(0, 50).Select(TestAsync).ToArray();
            var result = Task.WhenAll(tasks);

            try
            {
                await result;

                sum = tasks.Sum(task => task.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return sum;
        }
    }
}