using EasyTrader.Core.Views.PropertyView;
using System.Diagnostics;

namespace EasyTrader.Forms
{
    public partial class InformationForm : Form
    {
        public InformationForm()
        {
            InitializeComponent();

            InitializeCommonComponent();
        }

        [PropertyData("My Mock Object")]
        public MockObject MyMockObject { get; set; }

        private void InformationForm_Load(object sender, EventArgs e)
        {
            //MyMockObject = new MockObject();
            ////propertyDataUserControl1.LoadPropertyData("textObj");

            //propertyDataUserControl1.LoadPropertyData(MyMockObject, "Mock Object Name");

            //this.Text = propertyDataUserControl1.LabelTitle.Text;

            //propertyDataUserControl1.Toggle();

            //PrintControl2(this);


        }

        public void LoadData(dynamic data, string name)
        {
            propertyDataUserControl1.LoadPropertyData(data, name);
            this.Text = propertyDataUserControl1.LabelTitle.Text;
            propertyDataUserControl1.Toggle();

        }




        public static void PrintControl2(Control control, string clientId = "  ")
        {
            Debug.WriteLine($"{clientId} >{control.Name}: Size: {control.Size} Dock: {control.Dock} ");// ClientSize: {control.ClientSize} PreferredSize:{control.PreferredSize}");
            foreach (Control c in control.Controls)
            {
                PrintControl2(c, clientId + "  ");
            }
        }
        private void InitializeCommonComponent()
        {

        }

    }

    #region Mock Object

    [PropertyData("My Mock Class")]
    public class MockObject
    {
        //[PropertyData("Dog EntityId")]
        //public string DogName { get; set; }

        //public long Age { get; set; }

        //[PropertyData("Date of Birth")]
        //public DateTime DateOfBirth { get; set; }

        //[PropertyData("My Enum Test")]
        //public MyEnum_Test MyEnumTest { get; set; }


        //[PropertyData("Dog")]
        //public Animal DogType { get; set; }

        //[PropertyData("Cat Of Animal")]
        //public CatOfAnimal CatOfAnimal_Property { get; set; }

        //public Cat Cat_Property { get; set; }

        //[PropertyData("Dog Tag")]
        //public object DogTagObj { get; set; }

        //[PropertyData("First Layer Object")]
        //public FirstLayerObject FirstLayerObject { get; set; }


        //[PropertyData("String Array Test")]
        //public string[] StringArray_Test { get; set; }

        //[PropertyData("Object Array Test")]
        //public object[] ObjectArray_Test { get; set; }

        //[PropertyData("Animal (Class) Array Test")]
        //public Animal[] ClassArray_Test { get; set; }


        //[PropertyData("Animal (Class) List Test")]
        //public IList<Animal> ClassList_Test { get; set; }

        //[PropertyData("Animal (Class) Collection Test")]
        //public ICollection<Animal> ClassCollection_Test { get; set; }

        [PropertyData("Class List 2 Object")]
        public IList<string> ClassList_Test2 { get; set; }



        public MockObject()
        {
            //DogName = "Trevor";
            //Age = 8;
            //DateOfBirth = DateTime.Now;
            //MyEnumTest = MyEnum_Test.Enum1;

            //DogType = new Animal(4, "White");


            //CatOfAnimal_Property = new CatOfAnimal(4, "Grey") { CatName = "Sushi", };
            //Cat_Property = new Cat() { CatOfAnimal_Property = new CatOfAnimal(44, "Grey44") { CatName = "Mizti", }, };

            //DogTagObj = new Animal(8, "Spider");

            //StringArray_Test = ["Dog1 Array", "Dog Array2 test"];


            //ObjectArray_Test = [new Animal(2, "Brown"), new Animal(3, "Green")];
            //ClassArray_Test = [new Animal(10, "Black"), new Animal(9, "Yellow")];
            //ClassList_Test = new List<Animal>([new Animal(13, "aaa"), new Animal(14, "bbb")]);

            //ClassCollection_Test =
            //    new HashSet<Animal>([new Animal(20, "Magenta"), new Animal(21, "Cyan")]);

            //FirstLayerObject = new FirstLayerObject();


            ClassList_Test2 = new List<string>();
            //var x = 3;
            for (int i = 0; i < 20; i++)
            {
                ClassList_Test2.Add("Dana" + i);
            }

        }
    }

    public class FirstLayerObject
    {
        public FirstLayerObject()
        {
            SecondLayerObject = new SecondLayerObject();
            rd1 = "rd1";
        }

        [PropertyData("Second Layer Object")]
        public SecondLayerObject SecondLayerObject { get; set; }

        [PropertyData("rd one")]
        public string rd1 { get; set; }
    }

    public class SecondLayerObject
    {
        public SecondLayerObject()
        {
            ThirdLayerObject = new ThirdLayerObject();
            rd2 = "rd2";
        }

        [PropertyData("Third Layer Object")]
        public ThirdLayerObject ThirdLayerObject { get; set; }

        [PropertyData("rd two")]
        public string rd2 { get; set; }
    }

    public class ThirdLayerObject
    {
        public ThirdLayerObject()
        {
            rd3 = "rd3";
            CatsList = new List<CatOfAnimal>();
            for (int i = 0; i < 5; i++)
            {
                var catOfAnimal_Property = new CatOfAnimal(i, "Grey") { CatName = "Sushi", };
                CatsList.Add(catOfAnimal_Property);
            }

        }

        [PropertyData("rd three")]
        public string rd3 { get; set; }

        [PropertyData("Cats list of Animal")]
        public List<CatOfAnimal> CatsList { get; set; }
    }

    public class CatOfAnimal : Animal
    {
        public CatOfAnimal(int legs, string color) : base(legs, color)
        {
        }

        [PropertyData("Cat name")]
        public string CatName { get; set; }
    }

    public class Cat
    {
        public Cat()
        {
        }

        [PropertyData("Cat Of Animal - Property")]
        public CatOfAnimal CatOfAnimal_Property { get; set; }
    }

    public class Animal
    {
        public Animal(int legs, string color)
        {
            Legs = legs;
            Color = color;
        }

        [PropertyData("Number of legs")]
        public int Legs { get; set; }

        [PropertyData("")]
        public string Color { get; set; }
    }

    public enum MyEnum_Test
    {
        Enum1, Enum2

    }

    #endregion
}
