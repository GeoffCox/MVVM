namespace BellaCodeAir.Models
{
    using System;

    public class Airline
    {
        public Airline(string code, string name)
        {
            if (code == null)
            {
                throw new ArgumentNullException("code");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            this.Code = code;
            this.Name = name;
        }

        public string Code {get; private set;}

        public string Name { get; private set;}
    }
}
