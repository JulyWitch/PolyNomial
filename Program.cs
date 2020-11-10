

using System;

namespace t1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Poly poly = new Poly(" x-32x^2+x^2+2x^3 -x^2");
            Poly poly2 = new Poly(" x-32x^2+x^2+2x^3 -x^2");


            Poly newn = poly - poly2;
            Console.WriteLine("MINUX : " + newn.ToString());
            System.Console.WriteLine("Poly 1 : " + poly.ToString());
            System.Console.WriteLine("Poly 2 : " + poly2.ToString());

            Poly newp = poly + poly2;
            Console.WriteLine("Sum: " + newp.ToString());
            System.Console.WriteLine("Poly 1 : " + poly.ToString());
            System.Console.WriteLine("Poly 2 : " + poly2.ToString());


            // poly._inputToMember();
        }
    }


    struct Poly
    {
        public PolyMember[] members;
        public string input;
        public Poly(string input) : this()
        {
            this.input = input;
            this._inputToMember();
        }
        public Poly(int Length) : this()
        {
            this.members = new PolyMember[Length];
        }
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < this.members.Length; i++)
            {
                if (members[i].prefix > 0) output += "+";
                output += members[i].ToString();
            }
            return output == "" ? "0" : output;
        }
        //    x+x^2+2x^3
        public static Poly operator +(Poly poly, Poly poly2)
        {

            Poly sum = new Poly(poly.members.Length);
            for (int k = 0; k < poly.members.Length; k++)
            {
                sum.members[k].power = poly.members[k].power;
                sum.members[k].prefix = poly.members[k].prefix;
                sum.members[k].x = poly.members[k].x;
            }
            Poly beSummed = poly2;
            for (int i = 0; i < poly.members.Length; i++)
                for (int j = 0; j < beSummed.members.Length; j++)
                    if (poly.members[i].power == beSummed.members[j].power) sum.members[i].prefix += beSummed.members[j].prefix;
            return sum;
        }

        public static Poly operator -(Poly poly, Poly poly2)
        {
            Poly minus = new Poly(poly.members.Length);
            for (int k = 0; k < poly.members.Length; k++)
            {
                minus.members[k].power = poly.members[k].power;
                minus.members[k].prefix = poly.members[k].prefix;
                minus.members[k].x = poly.members[k].x;
            }
            for (int i = 0; i < minus.members.Length; i++)
                for (int j = 0; j < poly2.members.Length; j++)
                    if (minus.members[i].power == poly2.members[j].power) minus.members[i].prefix -= poly2.members[j].prefix;
            return minus;

        }
        public void _inputToMember()
        {
            string input = this.input;
            input = input.Replace("+", "p+");
            input = input.Replace("-", "p-");
            input = input.Replace(" ", "");
            string[] inputSep = input.Split('p');
            this.members = new PolyMember[inputSep.Length];
            for (int i = 0; i < inputSep.Length; i++)

                this.members[i] = new PolyMember(inputSep[i]);
            //    +x -32x^2 +x^2+ 2x^3
            // PolyMember[] members2 = members;
            for (int i = 0; i < inputSep.Length; i++)
                for (int j = 0; j < inputSep.Length; j++)
                    if (members[i].power == members[j].power && i != j && members[j].prefix != 0)
                    {
                        members[i].prefix += members[j].prefix;
                        members[j].prefix = 0;
                        members[j].power = 0;
                    }



        }

    }
    struct PolyMember
    {
        // char sign;
        public bool x;
        public int power;
        public double prefix;
        public PolyMember(string item)
        {
            this.x = true;
            this.prefix = 1;
            this.power = 1;
            if (item[0] == '-')
            {
                this.prefix *= -1;
                item = item.Remove(0, 1);
            };

            if (item[0] == '+') item = item.Remove(0, 1);
            ;
            if (item.Contains("x^"))
            {
                this.prefix *= item.Substring(0, item.Length - item.Substring(item.IndexOf('x')).Length) == "" ? 1 :
                Convert.ToDouble(item.Substring(0, item.Length - item.Substring(item.IndexOf('x')).Length));
            }
            else if (!item.Contains('x'))
            {
                this.x = false;
                this.prefix *= item == "" ? 1 : Convert.ToDouble(item);
            }
            if (item.Contains('^'))
            {
                this.power = Convert.ToInt32(item.Substring(item.IndexOf('^') + 1));
            }

        }
        public override string ToString()
        {
            return this.prefix == 0 ? "" : (this.prefix != 1 ? this.prefix == -1 ? "-" : this.prefix.ToString() : "") +
             (this.x ? "x" : "") +
              (this.power > 1 ? "^" + this.power : "");
        }
    }

}