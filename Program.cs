/* assignment 04
   Lee Burke
   20393941
   Using Command Design Pattern */
namespace App
{
    public class Program {
        public static void Main(String[] args) {
            Console.Clear();
            List<string> canvas = new List<string>();
            List<string> redoString = new List<string>();

            addCircle newCirc = new addCircle();
            addRectangle newRect = new addRectangle();
            printCanvas print = new printCanvas();
            toFile outputFile = new toFile();
            clearCanvas clear = new clearCanvas();
            undo newUndo = new undo();
            redo newredo = new redo();
            
            Boolean exit = false;

            Console.WriteLine("-h           Display this message");
            Console.WriteLine("-a <shape>   Add <circle> | <rectangle>");
            Console.WriteLine("-u           Undo last operation");
            Console.WriteLine("-r           Redo last operation");
            Console.WriteLine("-c           Clear canvas");
            Console.WriteLine("-p           Print current canvas");
            Console.WriteLine("-o           Output canvas as svg file");
            Console.WriteLine("-q           Quit application\n");

            while(exit == false) {
                
                string? input = Console.ReadLine();
                switch(input) {
                    case "-h":
                        Console.WriteLine("\n-h           Display this message");
                        Console.WriteLine("-a <shape>   Add <circle> | <rectangle>");
                        Console.WriteLine("-u           Undo last operation");
                        Console.WriteLine("-r           Redo last operation");
                        Console.WriteLine("-c           Clear canvas");
                        Console.WriteLine("-p           Print current canvas");
                        Console.WriteLine("-o           Output canvas as svg file");
                        Console.WriteLine("-q           Quit application\n");
                    break;
                    case "-a circle":
                        newCirc.execute(canvas);
                    break;
                    case "-a rectangle":
                        newRect.execute(canvas);
                    break;
                    case "-u":
                        newUndo.execute(canvas, redoString);
                    break;
                    case "-r":
                        newredo.execute(canvas, redoString);
                    break;
                    case "-c":
                        clear.execute(canvas);
                    break;
                    case "-p":
                        print.execute(canvas);
                    break;
                    case "-o":
                        outputFile.execute(canvas);
                    break;
                    case "-q":
                        exit = true;
                    break;
                    default:
                        Console.WriteLine(input + " not recognised. Type -h for help");
                    break;
                }
            }
        }
    }
    public class Shape {
        public int x, y;
        public string? printShape(){
            return null;
        }
    }
    public class Circle : Shape{
        private new int x, y;
        private int radius;
        Random r = new Random();
        
        public Circle() {
            this.x = r.Next(0, 500);
            this.y = r.Next(0, 500);
            
            //So that the border of the circle is always contained within the canvas parameters
            
            //if both x && y are >= mid-point
            if(this.x >= 250 && this.y >= 250)
                this.radius = r.Next(1, (this.x > this.y) ? 500 - this.x : 500 - this.y);

            //if both x && y are < mid-point
            else if(this.x < 250 && this.y < 250)
                this.radius = r.Next(1, (this.x < this.y) ? 0 + this.x : 0 + this.y);
                
            //if one value is more and one value is less than mid-point
            else if(this.x < this.y && 0 + this.x <= 500 - this.y)
                this.radius = r.Next(1, 0 + this.x);
            else if(this.x < this.y && 0 + this.x > 500 - this.y)
                this.radius = r.Next(1, 500 - this.y);
            else this.radius = r.Next(1, (500 - this.x < 0 + this.y) ? 500 - this.x : 0 + this.y);
        }
        public new string printShape() {
            string data = "<circle cx= \"" + this.x + "\" cy= \"" + this.y +  "\" r= \"" + this.radius + "\"/>";
            return data;
        }
    }

    public class Rectangle : Shape {
        private new int x, y;
        private int h, w;
        Random r = new Random();
        
        public Rectangle() {
            this.x = r.Next(0, 500);
            this.y = r.Next(0, 500);
            this.h = r.Next(0, 500 - this.y);
            this.w = r.Next(0, 500 - this.x);
        }
        public new string printShape() {
            string data = "<rect x= \"" + this.x + "\" y= \"" + this.y +  "\" h= \"" + this.h +  "\" w= \"" + this.w +  "\"/>";
            return data;
        }
    }

    public class Command {
        public void execute(List<string> canvas) {

        }
    }

    public class addCircle : Command {
        public new void execute(List<string> canvas) {
            Circle circle = new Circle();
            canvas.Add(circle.printShape());
            Console.WriteLine("Circle added!");
        }
    }

    public class addRectangle : Command {
        public new void execute(List<string> canvas) {
            Rectangle rectangle = new Rectangle();
            canvas.Add(rectangle.printShape());
            Console.WriteLine("Rectangle added!");
        }
    }

    public class printCanvas : Command {
        public new void execute(List<string> canvas) {
            foreach(string shape in canvas) 
                Console.WriteLine(shape);
        }
    }

    public class toFile : Command {
        public new void execute(List<string> canvas) {
            File.WriteAllLines("canvas.svg", canvas);
        }
    }

    public class clearCanvas : Command {
        public void execute(List<string> canvas, List<string> redoString) {
            canvas.Clear();
            redoString.Clear();
            Console.WriteLine("Canvas cleared!");
        }
    }

    public class undo : Command {
        public void execute(List<string> canvas, List<string> redoString) {
            if(canvas.Count >= 1) {
                redoString.Add(canvas[canvas.Count() - 1]);
                canvas.RemoveAt(canvas.Count() - 1);
            } else { Console.WriteLine("There is nothing to Undo!"); }
        }
    }

    public class redo : Command {
        public void execute(List<string> canvas, List<string> redoString) {
            if(redoString.Count >= 1) {
                canvas.Add(redoString[redoString.Count() - 1]);
                redoString.RemoveAt(redoString.Count() - 1);
            } else { Console.WriteLine("There is nothing to Redo!"); }
        }
    }
}


/* Command design pattern encapsulates all the information an object needs to perform an action. The Memento pattern provides the ability to restore an object to its previous state. Personally I think that the Command pattern is a better fit for my assignment as the user is given a list of commands that can be performed on the canvas so the Command design pattern makes the code easier to understand and fix. The Command pattern would scale well with my program as adding new commands is simple and the implementation of a new command won't cause problems for the other commands.
*/
