
using EasyTrader.Core.Common;
using System.ComponentModel;

namespace EasyTrader.Tests.Models.Helpers
{
    public class MockObject
    {
        [DisplayName("Dog EntityId")]
        public string DogName { get; set; }

        public long Age { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public object DogTagObj { get; set; }

        public MyEnum_Test MyEnumTest { get; set; }


        public Animal DogType { get; set; }

        //public CatOfAnimal CatOfAnimal_Property { get; set; }

        //public Cat Cat_Property { get; set; }


        //public string[] StringArray_Test { get; set; }

        //public object[] ObjectArray_Test { get; set; }

        //public Animal[] ClassArray_Test { get; set; }


        //public IList<Animal> ClassList_Test { get; set; }
        
        //public ICollection<Animal> ClassCollection_Test { get; set; }

        public MockObject()
        {
            DogName = "Trevor";
            Age = 8;
            DateOfBirth = DateTime.Now;
            DogTagObj = new Animal(8, "Spider");
            MyEnumTest = MyEnum_Test.Enum1;

            DogType = new Animal(4, "White");

            //StringArray_Test = ["Dog1 Array", "Dog Array2 test"];
            //ObjectArray_Test = [new Animal(2, "Brown"), new Animal(3, "Green")];
            //ClassArray_Test = [new Animal(10, "Black"), new Animal(9, "Yellow")];
            //ClassList_Test = new List<Animal>([new Animal(13, "aaa"), new Animal(14, "bbb")]);
            //ClassCollection_Test =
            //    new HashSet<Animal>([new Animal(20, "Magenta"), new Animal(21, "Cyan")]);

            //CatOfAnimal_Property = new CatOfAnimal(4, "Grey") { CatName = "Sushi", };
            //Cat_Property = new Cat() { CatOfAnimal_Property = new CatOfAnimal(44, "Grey44") { CatName = "Mizti", }, };
        }
    }

    public class CatOfAnimal : Animal
    {
        public CatOfAnimal(int legs, string color) : base(legs, color)
        {
        }
        public string CatName { get; set; }
    }

    public class Cat
    {
        public Cat()
        {
        }
        public CatOfAnimal CatOfAnimal_Property { get; set; }
    }

    public class Animal
    {
        public Animal(int legs, string color)
        {
            Legs = legs;
            Color = color;
        }
        public int Legs { get; set; }
        public string Color { get; set; }
    }

    public enum MyEnum_Test
    {
        Enum1, Enum2

    }

}
